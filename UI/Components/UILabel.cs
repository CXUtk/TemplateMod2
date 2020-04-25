using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace TemplateMod2.UI.Components {
    public class UILabel : UIElement {
        public string Text { get; set; }
        public Color TextColor { get; set; }
        public float TextScale { get; set; }
        public bool IsLargeText { get; set; }
        public SizeStyle SizeStyle { get; set; }

        public UILabel() {
            Text = "文字";
            TextScale = 1f;
            TextColor = Color.White;
            IsLargeText = false;
            SizeStyle = SizeStyle.Inline;
        }

        public override void UpdateSelf(GameTime gameTime) {
            var font = IsLargeText ? Main.fontDeathText : Main.fontMouseText;
            if (SizeStyle == SizeStyle.Inline)
                Size = new Vector2(font.MeasureString(Text).X, IsLargeText ? 42f : 18f) * TextScale;

            Recalculate();
            base.UpdateSelf(gameTime);
        }

        public override void DrawSelf(SpriteBatch sb) {
            var font = IsLargeText ? Main.fontDeathText : Main.fontMouseText;
            if (IsLargeText)
                Terraria.Utils.DrawBorderStringBig(sb, Text, Vector2.Zero, TextColor, TextScale);
            else
                Terraria.Utils.DrawBorderString(sb, Text, Vector2.Zero, TextColor, TextScale);
            base.DrawSelf(sb);
        }
    }
}
