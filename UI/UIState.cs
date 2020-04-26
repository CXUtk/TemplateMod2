using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace TemplateMod2.UI {
    public class UIState : UIElement, IComparable {
        internal long TimeGetFocus { get; set; }
        internal float ZIndex { get; set; }
        public UIState() : base() {
            Pivot = new Vector2(0, 0);
            SizeFactor = new Vector2(1, 1);
            IsVisible = false;
            ZIndex = 0f;

            Initialize();
            Recalculate();
        }

        public int CompareTo(object obj) {
            var other = obj as UIState;
            if (ZIndex != other.ZIndex) return ZIndex.CompareTo(other.ZIndex);
            return TimeGetFocus.CompareTo(other.TimeGetFocus);
        }

        public virtual void Initialize() { }
    }
}
