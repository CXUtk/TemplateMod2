using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TemplateMod2.Projectiles;
using TemplateMod2.Utils;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace TemplateMod2.Projectiles {
    public class GravProj : ModProjectile {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("重力弹幕");
        }
        public override void SetDefaults() {
            projectile.width = 16;
            projectile.height = 16;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.light = 0.1f;
            projectile.timeLeft = 400;
            projectile.ignoreWater = true;
            projectile.tileCollide = true;
            projectile.scale = 1f;
            projectile.alpha = 255;
            projectile.extraUpdates = 2;
        }

        bool valid(int x, int y) {
            return !(x < 0 || x >= Main.maxTilesX || y < 0 || y >= Main.maxTilesY);
        }
        bool CanHitLine(Vector2 start, Vector2 end) {
            Vector2 unit = Vector2.Normalize(end - start);
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

        bool check(NPC target, float rot, float r, out float closest) {
            Vector2 unit = (rot + r).ToRotationVector2();
            closest = 1.0f / 0.0f;
            for (int i = 0; i < 100; i += 8) {
                Vector2 cur = projectile.Center + unit * i;
                closest = Math.Min(closest, Vector2.Distance(cur, target.Center));
            }
            return CanHitLine(projectile.Center, projectile.Center + unit * 100);
        }
        private NPC target;
        public override void AI() {
            // 火焰粒子特效
            Dust dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height
                , MyDustId.Fire, 0f, 0f, 100, default(Color), 3f);
            // 粒子特效不受重力
            dust.noGravity = true;
            dust.velocity *= 0;
            dust.position = projectile.Center;
            float maxDis = 1000f;
            target = null;
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
            //if (!Collision.CanHit(projectile.position, projectile.width, projectile.height, target.position, target.width, target.height)) {
            if (target != null) {
                Vector2 aim = target.Center - projectile.Center;
                float rot = aim.ToRotation();
                if (!CanHitLine(projectile.Center, target.Center)) {
                    float final = 0f, minn = float.PositiveInfinity;
                    for (float r = -MathHelper.Pi / 2; r < MathHelper.Pi / 2; r += MathHelper.Pi / 10f) {
                        float d;
                        if (check(target, rot, r, out d)) {
                            if (d < minn) {
                                minn = d;
                                final = r;
                            }
                        }
                    }
                    if (minn != float.PositiveInfinity)
                        projectile.velocity = (rot + final).ToRotationVector2() * 10f;
                } else {
                    projectile.velocity = Vector2.Normalize(aim) * 10f;
                }
            }
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor) {
            if (target != null)
                Drawing.DrawLine(spriteBatch, projectile.Center - Main.screenPosition, target.Center - Main.screenPosition, 4, 2f, Color.White);
        }
    }
}
