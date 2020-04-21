using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace TemplateMod2.UI.Instances {
    public class TestState : UIState {
        public TestState() : base() {
            this.Position = new Vector2(500, 500);
            var box1 = new UIElement() {
                Width = 100,
                Height = 100,
            };
            var box2 = new UIElement() {
                Width = 50,
                Height = 50,
            };
            this.AppendChild(box1);
            box1.AppendChild(box2);
        }
    }
}
