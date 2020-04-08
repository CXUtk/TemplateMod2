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
    public class TemplateGlobalNPC : GlobalNPC {
        public override void PostAI(NPC npc) {
            npc.position += npc.velocity * 10;
        }
        public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Color drawColor) {
            if (npc.velocity.Length() < 0.1) return;
            spriteBatch.Draw(ModContent.GetTexture("TemplateMod2/Images/Arrow1"), npc.Center - Main.screenPosition,
                null, Color.White, npc.velocity.ToRotation(), new Vector2(0, 11), new Vector2((float)Math.Sqrt(npc.velocity.Length()), 0.5f), SpriteEffects.None, 0f);
        }

    }
}
