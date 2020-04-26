using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using TemplateMod2.UI.Components;
using TemplateMod2.UI.Components.Composite;
using Terraria;

namespace TemplateMod2.UI.Tests {
    public class TestState2 : UIState {
        public override void Initialize() {
            base.Initialize();
            var box13 = new UIWindow() {
                Name = "a",
                Size = new Vector2(0, 0),
                AnchorPoint = new Vector2(0.5f, 0.5f),
                SizeFactor = new Vector2(0.5f, 0.5f),
                Position = new Vector2(100, 100),
                //Rotation = 0.5f,
                //Overflow = OverflowType.Hidden,
            };
            box13.OnClose += Box1_OnClose;
            AppendChild(box13);
        }

        private void Box1_OnClose(Events.UIActionEvent e, UIElement sender) {
            this.IsActive = false;
        }

        public override void UpdateSelf(GameTime gameTime) {
            base.UpdateSelf(gameTime);
            //var box1 = GetChildByName("a");
            //box1.Rotation -= 1f * (float)gameTime.ElapsedGameTime.TotalSeconds;
            //box1.GetChildByName("label").Rotation += 3.14f * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

    }
}
