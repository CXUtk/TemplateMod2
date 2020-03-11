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
            projectile.width = 6;
            projectile.height = 6;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.light = 0.1f;
            projectile.timeLeft = 2;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
        }
        LightTree tree;

        public override void AI() {
            if (projectile.ai[0] == 0) {
                tree = new LightTree();
                tree.Generate(projectile.Center, projectile.velocity);
                projectile.ai[0] = 1;
            }

        }

        public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox) {
            return tree.Check(targetHitbox);
        }


        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit) {
            base.OnHitNPC(target, damage, knockback, crit);
            target.immune[projectile.owner] = 0;
        }

        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor) {
            tree.Draw(spriteBatch, projectile.Center - Main.screenPosition, projectile.velocity);
        }
    }
}
