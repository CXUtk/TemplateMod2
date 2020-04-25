using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateMod2.UI.Components {
    public class UITextButton : UIPanel {
        public string Text { get; set; }
        public bool DrawPanel { get; set; }
        public bool IsLargeText { get; set; }
        public Color TextColor { get; set; }
        public Color DefaultColor { get; set; }
        public Color MouseMoveColor { get; set; }
        public Color MouseDownColor { get; set; }

        private UILabel _label;
        private bool _isMouseOver;
        private int _timer;

        private void SyncToLabel() {
            _label.Text = this.Text;
            _label.AnchorPoint = new Vector2(0.5f, 0.5f);
            _label.IsLargeText = this.IsLargeText;
            _label.TextColor = this.TextColor;
            _label.BlockPropagation = false;
            _label.NoEvent = true;
        }
        public UITextButton() : base() {
            Text = "按钮";
            DrawPanel = true;
            TextColor = Color.White;
            DefaultColor = Color.WhiteSmoke;
            Color = DefaultColor;
            MouseMoveColor = Color.White;
            MouseDownColor = Color.White;
            _label = new UILabel();
            SyncToLabel();
            this.AppendChild(_label);
            this.OnMouseOver += UITextButton_OnMouseOver;
            this.OnMouseOut += UITextButton_OnMouseOut;
        }

        private void UITextButton_OnMouseOut(Events.UIMouseEvent e, UIElement sender) {
            _isMouseOver = false;
        }

        private void UITextButton_OnMouseOver(Events.UIMouseEvent e, UIElement sender) {
            _isMouseOver = true;
        }
        public override void UpdateSelf(GameTime gameTime) {
            base.UpdateSelf(gameTime);
            SyncToLabel();
            if (_isMouseOver && _timer < 15)
                _timer++;
            else if (!_isMouseOver && _timer > 0)
                _timer--;
            float factor = _timer / 15f;
            this.Color = Color.Lerp(DefaultColor, MouseMoveColor, factor);
            this.Scale = new Vector2(1 + _timer / 100f, 1 + _timer / 100f);
        }

        public override void DrawSelf(SpriteBatch sb) {
            if (DrawPanel)
                base.DrawSelf(sb);
        }
    }
}
