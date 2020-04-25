using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateMod2.UI.Components {
    public class UIPanel : UIElement {
        public Texture2D PanelTexture {
            get; set;
        }
        public Texture2D PanelBorderTexture {
            get; set;
        }
        public Vector2 CornerSize {
            get; set;
        }
        public Color Color {
            get; set;
        }
        public UIPanel() : base() {
            PanelTexture = Drawing.PanelDefaultBackTexture;
            PanelBorderTexture = null;
            CornerSize = new Vector2(8f, 8f);
            Color = Color.White;
        }
        public override void DrawSelf(SpriteBatch sb) {
            base.DrawSelf(sb);
            Drawing.DrawAdvBox(sb, 0, 0, Width, Height, Color, PanelTexture, CornerSize);
            if (PanelBorderTexture != null)
                Drawing.DrawAdvBox(sb, 0, 0, Width, Height, Color, PanelBorderTexture, CornerSize);
        }
    }
}
