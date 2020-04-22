using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Terraria;

namespace TemplateMod2.UI.Instances {
    public class TestState : UIState {
        public TestState() : base() {
            var box1 = new UIElement() {
                Name = "a",
                Width = 200,
                Height = 200,
                Position = new Vector2(500, 400),

            };
            var box12 = new UIElement() {
                Name = "a",
                Width = 200,
                Height = 200,
                Position = new Vector2(650, 400),
                Rotation = 0.8f,
            };
            var box2 = new UIElement() {
                Width = 50,
                Height = 50,
                AnchorPoint = new Vector2(0f, 0f),
                Pivot = new Vector2(0f, 0f),
            };
            var box3 = new UIElement() {
                Width = 50,
                Height = 50,
                AnchorPoint = new Vector2(0f, 1f),
                Pivot = new Vector2(0f, 1f),
            };
            var box4 = new UIElement() {
                Width = 50,
                Height = 50,
                AnchorPoint = new Vector2(1f, 0f),
                Pivot = new Vector2(1f, 0f),
                BlockPropagation = false,
            };
            var box5 = new UIElement() {
                Width = 50,
                Height = 50,
                AnchorPoint = new Vector2(1f, 1f),
                Pivot = new Vector2(1f, 1f),
            };
            var box6 = new UIElement() {
                Width = 50,
                Height = 50,
                AnchorPoint = new Vector2(0f, 0.5f),
            };
            box2.OnMouseOver += Box1_OnMouseOver;
            box3.OnMouseOver += Box1_OnMouseOver;
            box4.OnMouseOver += Box1_OnMouseOver;
            box5.OnMouseOver += Box1_OnMouseOver;
            box6.OnMouseOver += Box1_OnMouseOver;

            box2.OnMouseOut += Box1_OnMouseOut;
            box3.OnMouseOut += Box1_OnMouseOut;
            box4.OnMouseOut += Box1_OnMouseOut;
            box5.OnMouseOut += Box1_OnMouseOut;
            box6.OnMouseOut += Box1_OnMouseOut;
            AppendChild(box1);
            AppendChild(box12);
            box1.AppendChild(box2);
            box1.AppendChild(box3);
            box1.AppendChild(box4);
            box1.AppendChild(box5);
            box1.AppendChild(box6);
        }

        private void Box1_OnMouseOver(Events.UIMouseEvent e, UIElement sender) {
            sender.Width = 200;
        }

        private void Box1_OnMouseOut(Events.UIMouseEvent e, UIElement sender) {
            sender.Width = 50;
        }

        public override void Update(GameTime gameTime) {
            base.Update(gameTime);
            // this.GetChildByName("a").Rotation += 0.03f;
            //Main.NewText();
        }
    }
}
