using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using TemplateMod2.UI.Components;
using Terraria;

namespace TemplateMod2.UI.Instances {
    public class TestState : UIState {
        public override void Initialize() {
            base.Initialize();
            var box1 = new UIPanel() {
                Name = "a",
                Size = new Vector2(200, 200),
                Position = new Vector2(500, 400),
                //Rotation = 0.5f,
                //Overflow = OverflowType.Hidden,
            };
            var box12 = new UILabel() {
                Name = "label",
                Text = "哈哈哈哈",
                IsLargeText = false,
                AnchorPoint = new Vector2(0.5f, 0.5f),
                //Rotation = 0.5f
            };
            var box2 = new UITextButton() {
                Size = new Vector2(100, 50),
                AnchorPoint = new Vector2(0f, 0f),
                Pivot = new Vector2(0f, 0f),
            };
            var box3 = new UITextButton() {
                Size = new Vector2(50, 50),
                AnchorPoint = new Vector2(0f, 1f),
                Pivot = new Vector2(0f, 1f),
            };
            var box4 = new UITextButton() {
                Size = new Vector2(50, 50),
                AnchorPoint = new Vector2(1f, 0f),
                Pivot = new Vector2(1f, 0f),
                BlockPropagation = false,
            };
            var box5 = new UITextButton() {
                Size = new Vector2(50, 50),
                AnchorPoint = new Vector2(1f, 1f),
                Pivot = new Vector2(1f, 1f),
            };
            var box6 = new UITextButton() {
                Size = new Vector2(50, 50),
                AnchorPoint = new Vector2(0f, 0.5f),
            };
            AppendChild(box1);
            box1.AppendChild(box12);
            box1.AppendChild(box2);
            box1.AppendChild(box3);
            box1.AppendChild(box4);
            box1.AppendChild(box5);
            box1.AppendChild(box6);
        }

        public override void UpdateSelf(GameTime gameTime) {
            base.UpdateSelf(gameTime);
            //var box1 = GetChildByName("a");
            //box1.Rotation -= 1f * (float)gameTime.ElapsedGameTime.TotalSeconds;
            //box1.GetChildByName("label").Rotation += 3.14f * (float)gameTime.ElapsedGameTime.TotalSeconds;
        }
    }
}
