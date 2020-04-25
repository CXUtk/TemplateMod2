using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace TemplateMod2.UI {
    public class UIState : UIElement {
        public UIState() : base() {
            Pivot = new Vector2(0, 0);
            SizeFactor = new Vector2(1, 1);
            IsVisible = false;

            Initialize();
            Recalculate();
        }

        public virtual void Initialize() { }
    }
}
