using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace TemplateMod2 {
    public class TemplateGlobaProjectile : GlobalProjectile {
        public override void PostDraw(Projectile projectile, SpriteBatch spriteBatch, Color lightColor) {
            if (projectile.velocity.Length() < 0.1) return;
            //spriteBatch.Draw(ModContent.GetTexture("TemplateMod2/Images/Arrow1"), projectile.Center - Main.screenPosition,
            //  null, Color.White, projectile.velocity.ToRotation(), new Vector2(0, 11), new Vector2((float)Math.Sqrt(projectile.velocity.Length()), 0.5f), SpriteEffects.None, 0f);
        }
    }
}
