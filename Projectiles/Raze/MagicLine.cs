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

namespace TemplateMod2.Projectiles.Raze {
    public class MagicLine : ModProjectile {

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
            projectile.extraUpdates = 100;
        }

        public override void AI() {
            Dust dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height
                , MyDustId.RedTrans, 0f, 0f, 100, default, 2f);
            dust.noGravity = true;
            dust.velocity *= 0;
            dust.position = projectile.Center;
        }
    }
}
