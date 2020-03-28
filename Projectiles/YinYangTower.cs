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
    public class YinYangTower : ModProjectile {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("阴 阳 怪 塔");
        }
        public override void SetDefaults() {
            projectile.width = 6;
            projectile.height = 6;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.light = 0.1f;
            projectile.timeLeft = 600;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.scale = 1f;
            projectile.alpha = 255;
            projectile.extraUpdates = 0;
        }

        public int Timer1 {
            get {
                return (int)projectile.ai[0];
            }
            set {
                projectile.ai[0] = value;
            }
        }

        public int Timer2 {
            get {
                return (int)projectile.ai[1];
            }
            set {
                projectile.ai[1] = value;
            }
        }
        public override void AI() {
            Main.dayTime = false;
            Main.time = 0;
            projectile.velocity *= 0f;
            for (float r = 6.28f; r > 0; r -= MathHelper.TwoPi / 10f) {
                float r2 = (float)Math.Cos(projectile.ai[0]);
                for (int i = -1; i <= 1; i += 2) {
                    Vector2 pos = projectile.Center +
                        new Vector2((float)Math.Cos(r + r2), (float)Math.Sin(r + r2)) * r * 10f * i;
                    Dust dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height
                    , i < 0 ? MyDustId.RedTorch : MyDustId.DemonTorch, 0f, 0f, 100, default(Color), 3f);
                    dust.noGravity = true;
                    dust.velocity *= 0;
                    dust.position = pos;
                }
            }
            Timer1++;
            float factor = Math.Min(1.0f, Timer1 / 300f);
            if (factor < 0.2f) return;
            Timer2++;
            int shootCD = (int)(10 + (1 - factor) * 20);
            if (Timer2 >= shootCD) {
                float maxDis = 1000f;
                NPC target = null;
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
                    projectile.ai[1] = 0;
                    Projectile.NewProjectile(projectile.Center, Vector2.Normalize(target.Center - projectile.Center) * 13f,
                    ModContent.ProjectileType<ProjProj>(), 100, 5f, projectile.owner, 0, Main.rand.Next(2));
                }
            }
        }
    }
}
