using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using TemplateMod2.Projectiles;
using TemplateMod2.Utils;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace TemplateMod2.Projectiles {
    public class AvoidProj : ModProjectile {
        private static int[,] dist = new int[102, 102];
        private static int[,] prev = new int[102, 102];
        private static int[] dx = { 1, -1, 0, 0, 1, -1, 1, -1 };
        private static int[] dy = { 0, 0, 1, -1, 1, 1, -1, -1 };
        private static List<Vector2> list = new List<Vector2>();
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("超级追踪弹幕");
        }
        public override void SetDefaults() {
            projectile.width = 1;
            projectile.height = 1;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.light = 0.1f;
            projectile.timeLeft = 60;
            projectile.ignoreWater = true;
            projectile.tileCollide = true;
            projectile.scale = 1f;
            projectile.alpha = 255;
        }

        bool valid(int x, int y) {
            return x >= 0 && x <= 100 && y >= 0 && y <= 100;
        }
        bool CanHitLine(Vector2 start, Vector2 end) {
            Vector2 unit = Vector2.Normalize(end - start);
            float r = unit.ToRotation();
            float dis = (end - start).Length();
            for (int i = 0; i < dis; i += 16) {
                Point pTile = (start + unit * i).ToTileCoordinates();
                for (int j = -1; j <= 1; j++) {
                    for (int k = -1; k <= 1; k++) {
                        Point neg = new Point(pTile.X + j, pTile.Y + k);
                        if (!valid(neg.X, neg.Y)) continue;
                        Tile tile = Main.tile[neg.X, neg.Y];
                        if (tile.active() && Main.tileSolid[tile.type] && !Main.tileSolidTop[tile.type]) return false;
                    }
                }
            }
            return true;
        }

        bool check(Point end, int x, int y) {
            if (x == end.X && y == end.Y) return true;
            Tile tile = Main.tile[x, y];
            if (tile.active() && Main.tileSolid[tile.type] && !Main.tileSolidTop[tile.type]) {
                return false;
            }
            return true;
            //if (x == end.X && y == end.Y) return true;
            //for (int i = 0; i < 8; i++) {
            //    if (x + dx[i] >= 0 && x + dx[i] < Main.maxTilesX && y + dy[i] >= 0 && y + dy[i] < Main.maxTilesY) {
            //        Tile tile = Main.tile[x + dx[i], y + dy[i]];
            //        if (tile.active() && Main.tileSolid[tile.type] && !Main.tileSolidTop[tile.type]) {
            //            return false;
            //        }
            //    }
            //}
            return true;
        }
        private struct Node : IComparable {
            public int x, y, s;
            public double d;
            public Node(int x, int y, int s, double d) {
                this.x = x;
                this.y = y;
                this.s = s;
                this.d = d;
            }
            public int CompareTo(object obj) {
                Node other = (Node)obj;
                return (s + d).CompareTo(other.s + other.d);
            }
        }
        private double cal(int x, int y, Point b) {
            return Math.Abs(x - b.X) + Math.Abs(y - b.Y);
            return Math.Sqrt((x - b.X) * (x - b.X) + (y - b.Y) * (y - b.Y));
        }

        void search() {
            PriorityQueue<Node> Q = new PriorityQueue<Node>(10000);
            for (int i = 0; i < 100; i++)
                for (int j = 0; j < 100; j++) {
                    dist[i, j] = 0x3f3f3f3f;
                    prev[i, j] = 0;
                }
            Point cur = projectile.Center.ToTileCoordinates();
            Point end = target.Center.ToTileCoordinates();
            dist[50, 50] = 0;
            Q.Push(new Node(50, 50, 0, cal(cur.X, cur.Y, end)));
            while (!Q.Empty) {
                Node x = Q.Pop();
                if (x.x - 50 + cur.X == end.X && x.y - 50 + cur.Y == end.Y) break;
                for (int i = 0; i < 4; i++) {
                    int nx = x.x + dx[i], ny = x.y + dy[i];
                    if (!valid(nx, ny)) continue;
                    if (!check(end, nx + cur.X - 50, ny + cur.Y - 50)) continue;
                    if (dist[x.x, x.y] + 1 < dist[nx, ny]) {
                        dist[nx, ny] = dist[x.x, x.y] + 1;
                        prev[nx, ny] |= x.x;
                        prev[nx, ny] |= x.y << 10;
                        Q.Push(new Node(nx, ny, dist[nx, ny], cal(nx + cur.X - 50, ny + cur.Y - 50, end)));
                    }
                }
            }
            list.Clear();
            Point end1 = new Point(end.X - cur.X + 50, end.Y - cur.Y + 50);
            int x1 = prev[end1.X, end1.Y] & 1023;
            int y1 = ((prev[end1.X, end1.Y] >> 10) & 1023);
            //Main.NewText($"Complete! {end1} : {dist[end1.X, end1.Y]} -> {x1}, {y1}");
            if (dist[end1.X, end1.Y] == 0x3f3f3f3f)
                return;
            while (true) {
                Vector2 pos = new Vector2((cur.X + end1.X - 50) * 16 + 8, (cur.Y + end1.Y - 50) * 16 + 8);
                list.Add(pos);
                int x = prev[end1.X, end1.Y] & 1023;
                int y = ((prev[end1.X, end1.Y] >> 10) & 1023);
                if (x == end1.X && y == end1.Y) break;
                if (x == 50 && y == 50) {
                    break;
                }
                end1 = new Point(x, y);
            }
            Vector2 end2 = new Vector2((cur.X + end1.X - 50) * 16 + 8, (cur.Y + end1.Y - 50) * 16 + 8);
            projectile.velocity = Vector2.Normalize(end2 - projectile.Center) * 10f;
            if (!Collision.CanHitLine(projectile.Center, 0, 0, projectile.Center + Vector2.Normalize(end2 - projectile.Center) * 24f, 0, 0)) {
                for (float r = 0; r < MathHelper.TwoPi; r += MathHelper.Pi / 2f) {
                    Vector2 unit = (projectile.velocity.ToRotation() + r).ToRotationVector2();
                    if (Collision.CanHitLine(projectile.Center, 0, 0, projectile.Center + unit * 24, 0, 0)) {
                        projectile.velocity = (projectile.velocity * 2 + unit * 16) / 3f;
                        return;
                    }
                }
            }
        }
        private NPC target;
        public override void AI() {
            // 火焰粒子特效
            Dust dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height
                , MyDustId.Fire, 0f, 0f, 100, default, 3f);
            // 粒子特效不受重力
            dust.noGravity = true;
            dust.velocity *= 0;
            dust.position = projectile.Center;
            float maxDis = 800f;
            target = null;
            //projectile.velocity *= 0f;
            // 选取最近npc，如果target是null说明没有临近的敌人
            foreach (var npc in Main.npc) {
                if (npc.active && !npc.friendly && npc.value > 0 && !npc.dontTakeDamage) {
                    float dis = Vector2.Distance(npc.Center, projectile.Center);
                    if (dis < maxDis) {
                        maxDis = dis;
                        target = npc;
                    }
                }
            }
            if (target != null) {
                search();
            }
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor) {
            if (target != null)
                Drawing.DrawLine(spriteBatch, projectile.Center - Main.screenPosition, target.Center - Main.screenPosition, 4, 2f, Color.White);
            foreach (var pos in list) {
                spriteBatch.Draw(Main.magicPixel, pos - Main.screenPosition,
                    new Rectangle(0, 0, 1, 1), Color.White, 0f, new Vector2(0.5f, 0.5f), new Vector2(8, 8), SpriteEffects.None, 0f);
            }
        }
    }
}
