﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using TemplateMod2.Projectiles;
using TemplateMod2.Utils;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace TemplateMod2.Projectiles {
    public class ProjProj : ModProjectile {
        private int timer = 0;
        public override void SetStaticDefaults() {
            DisplayName.SetDefault("神秘弹幕");
        }
        public override void SetDefaults() {
            projectile.width = 6;
            projectile.height = 6;
            projectile.aiStyle = -1;
            projectile.friendly = true;
            projectile.light = 0.1f;
            projectile.timeLeft = 200;
            projectile.ignoreWater = true;
            projectile.tileCollide = false;
            projectile.penetrate = -1;
            projectile.scale = 3f;
            projectile.alpha = 255;
            projectile.extraUpdates = 0;
        }
        const float CENTER_DIS = 60f;
        const float RADIUS = 50f;
        public override void AI() {
            int type = (projectile.ai[1] > 0) ? MyDustId.DemonTorch : MyDustId.RedTorch;
            // 火焰粒子特效
            Dust dust = Dust.NewDustDirect(projectile.position, projectile.width, projectile.height
                , type, 0f, 0f, 100, default(Color), 3f);
            // 粒子特效不受重力
            dust.noGravity = true;
            dust.velocity *= 0;
            dust.position = projectile.Center;
            float factor = (200f - projectile.timeLeft) / 200f;
            factor *= factor;
            projectile.ai[0]++;
            if (projectile.ai[0] > 60 && projectile.ai[0] < 75) {
                projectile.velocity *= 0.95f;
            } else if (projectile.ai[0] >= 75) {
                Player player = Main.player[projectile.owner];
                Vector2 unit = Vector2.Normalize(player.Center - projectile.Center).RotatedBy(1.5f);
                projectile.velocity = unit * factor * 50f;
            }

            base.AI();
        }
        public override void PostDraw(SpriteBatch spriteBatch, Color lightColor) {
            //spriteBatch.Draw(Main.magicPixel, new Rectangle((int)(projectile.Hitbox.X - Main.screenPosition.X),
            //    (int)(projectile.Hitbox.Y - Main.screenPosition.Y), projectile.Hitbox.Width, projectile.Hitbox.Height),
            //    null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 0f);
            //float rot = projectile.ai[1];
            //Vector2 center = projectile.Center - Main.screenPosition + rot.ToRotationVector2() * CENTER_DIS;
            //spriteBatch.Draw(Main.magicPixel, center, new Rectangle(0, 0, 1, 1),
            //    Color.White, 0f, new Vector2(0.5f, 0.5f), new Vector2(1, 1), SpriteEffects.None, 0f);
            //for (float r = 0; r < MathHelper.TwoPi; r += MathHelper.TwoPi / 32f) {
            //    spriteBatch.Draw(Main.magicPixel, center + r.ToRotationVector2() * RADIUS, new Rectangle(0, 0, 1, 1),
            //        Color.White, 0f, new Vector2(0.5f, 0.5f), new Vector2(1, 1), SpriteEffects.None, 0f);
            //}
            //spriteBatch.Draw(Main.magicPixel, center + projectile.ai[0].ToRotationVector2() * RADIUS, new Rectangle(0, 0, 1, 1),
            //        Color.LimeGreen, 0f, new Vector2(0.5f, 0.5f), new Vector2(5, 5), SpriteEffects.None, 0f);
        }
    }
}
