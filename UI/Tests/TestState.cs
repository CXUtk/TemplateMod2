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
    public class TestState : UIState {
        public override void Initialize() {
            base.Initialize();
            var box1 = new UIWindow() {
                Name = "a",
                Size = new Vector2(0, 0),
                AnchorPoint = new Vector2(0.5f, 0.5f),
                SizeFactor = new Vector2(0.5f, 0.5f),
                //Rotation = 0.5f,
                //Overflow = OverflowType.Hidden,
            };
            box1.OnClose += Box1_OnClose;
            var box12 = new UILabel() {
                Name = "label",
                Text = "哈哈哈哈",
                IsLargeText = false,
                AnchorPoint = new Vector2(0.5f, 0.5f),
                //Rotation = 0.5f
            };
            var box2 = new UIButton() {
                Size = new Vector2(100, 50),
                AnchorPoint = new Vector2(0f, 0f),
                Pivot = new Vector2(0f, 0f),
            };
            var box3 = new UIButton() {
                Size = new Vector2(50, 50),
                AnchorPoint = new Vector2(0f, 1f),
                Pivot = new Vector2(0f, 1f),
            };
            var box5 = new UIButton() {
                Size = new Vector2(50, 50),
                AnchorPoint = new Vector2(1f, 1f),
                Pivot = new Vector2(1f, 1f),
            };
            var box6 = new UIButton() {
                Size = new Vector2(50, 50),
                AnchorPoint = new Vector2(0f, 0.5f),
            };
            AppendChild(box1);
            box1.AppendChild(box12);
            box1.AppendChild(box2);
            box1.AppendChild(box3);
            box1.AppendChild(box5);
            box1.AppendChild(box6);
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
