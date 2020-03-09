using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.UI;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.UI;

namespace TemplateMod2 {
    public class TestScreenShaderData : ScreenShaderData {
        public TestScreenShaderData(string passName) : base(passName) {
        }

        public TestScreenShaderData(Ref<Effect> shader, string passName) : base(shader, passName) {
        }

        public override void Apply() {
            this.Shader.Parameters["uEffectPos"].SetValue(Main.MouseScreen);
            UseIntensity(TemplateMod2.Strength);
            UseProgress(TemplateMod2.Progress);
            UseImage(TemplateMod2.Instance.GetTexture("Images/noise1"), 0, SamplerState.LinearClamp);
            base.Apply();

        }

    }
}
