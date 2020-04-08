using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using TemplateMod2.Utils;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace TemplateMod2.Projectiles {
    public class LightTreePro : ModProjectile {
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("神秘弹幕");
        }
        public override void SetDefaults() {
            projectile.width = 1;
            projectile.height = 1;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.light = 0.1f;
            projectile.timeLeft = 30;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
        }

        public override bool ShouldUpdatePosition() {
            return false;
        }
        private LightTree tree;
        public override void AI() {
            if (projectile.ai[0] % 6 == 0) {
                tree = new LightTree(Main.rand);
                float maxDis = 1000f;
                NPC target = null;
                foreach (var npc in Main.npc) {
                    if (npc.active && !npc.friendly && npc.type != NPCID.TargetDummy && !npc.dontTakeDamage) {
                        float dis = Vector2.Distance(npc.Center, projectile.Center);
                        if (dis < maxDis) {
                            maxDis = dis;
                            target = npc;
                        }
                    }
                }
                Vector2 pos = projectile.Center + projectile.velocity * 100f;
                if (target != null)
                    pos = target.Center;
                tree.Generate(projectile.Center, projectile.velocity, pos);
            }
            projectile.ai[0]++;
        }
        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox) {
            return tree.Check(targetHitbox);
        }


        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
            base.OnHitNPC(target, damage, knockback, crit);
            target.immune[projectile.owner] = 0;
            for (int i = 0; i < 2; i++) {
                var dust = Dust.NewDustDirect(target.position, target.width, target.height, MyDustId.ElectricCyan, 0, 0, 100, Color.White, 0.3f);
                dust.noGravity = true;
                dust.velocity *= 1.5f;
            }
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor) {
            tree.Draw(spriteBatch, projectile.Center - Main.screenPosition, projectile.velocity);
        }
    }
}
