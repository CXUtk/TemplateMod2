using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace TemplateMod2.UI.Components {
    public class UIImageButton : UIElement {
        public Texture2D Texture { get; set; }
        public float TextureScale { get; set; }
        public SizeStyle SizeStyle { get; set; }
        public Color DefaultColor { get; set; }
        public Color MouseOverColor { get; set; }
        public float TextureRotation { get; set; }

        private Color _color;
        private bool _isMouseOver;
        private int _timer;


        public UIImageButton() : base() {
            SizeStyle = SizeStyle.Inline;
            TextureScale = 1f;
            Texture = Main.magicPixel;
            DefaultColor = Color.White;
            TextureRotation = 0f;

            this.OnMouseEnter += UIImageButton_OnMouseEnter;
            this.OnMouseOut += UIImageButton_OnMouseOut;
        }

        private void UIImageButton_OnMouseOut(Events.UIMouseEvent e, UIElement sender) {
            _isMouseOver = false;
        }

        private void UIImageButton_OnMouseEnter(Events.UIMouseEvent e, UIElement sender) {
            _isMouseOver = true;
            Main.PlaySound(12);
        }

        public override void UpdateSelf(GameTime gameTime) {
            base.UpdateSelf(gameTime);
            if (SizeStyle == SizeStyle.Inline) {
                Size = Texture.Size();
            }
            if (_isMouseOver && _timer < 15)
                _timer++;
            else if (!_isMouseOver && _timer > 0)
                _timer--;
            float factor = _timer / 15f;
            this._color = Color.Lerp(DefaultColor, MouseOverColor, factor);
        }

        public override void DrawSelf(SpriteBatch sb) {
            base.DrawSelf(sb);
            sb.Draw(Texture, new Vector2(Width / 2, Height / 2), null, _color, TextureRotation, Texture.Size() * 0.5f, new Vector2(1, 1), SpriteEffects.None, 0f);
        }
    }
}
