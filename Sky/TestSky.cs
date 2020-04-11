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
    public class TestSky : CustomSky {
        private bool _isActive;
        public override void Activate(Vector2 position, params object[] args) {


            _isActive = true;
        }

        public override void Deactivate(params object[] args) {
            _isActive = false;
        }

        public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth) {
        }

        public override bool IsActive() {
            return _isActive;
        }

        public override void Reset() {
            _isActive = false;
        }

        public override void Update(GameTime gameTime) {
        }

    }
}
