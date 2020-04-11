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
            //npc.position += npc.velocity * 10;
        }
        public override void PostDraw(NPC npc, SpriteBatch spriteBatch, Color drawColor) {
            spriteBatch.End();
            spriteBatch.Begin();
        }
        public override bool PreDraw(NPC npc, SpriteBatch spriteBatch, Color drawColor) {
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.NonPremultiplied);
            TemplateMod2.npcEffect.Parameters["uTime"].SetValue((float)Main.time);
            TemplateMod2.npcEffect.Parameters["uImageSize"].SetValue(Main.npcTexture[npc.type].Size());
            TemplateMod2.npcEffect.CurrentTechnique.Passes["Edge"].Apply();
            return true;
        }
    }
}
