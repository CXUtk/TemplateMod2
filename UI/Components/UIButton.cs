using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace TemplateMod2.UI.Components {
    public abstract class UIButtonBase : UIElement {

    }
    public class UIButton : UIPanel {
        public string Text { get; set; }
        public bool DrawPanel { get; set; }
        public bool IsLargeText { get; set; }
        public Color PanelDefaultColor { get; set; }
        public Color PanelMouseOverColor { get; set; }
        public Color TextDefaultColor { get; set; }
        public Color TextMouseOverColor { get; set; }

        private UILabel _label;
        private bool _isMouseOver;
        private int _timer;

        private void SyncToLabel() {
            _label.Text = this.Text;
            _label.AnchorPoint = new Vector2(0.5f, 0.5f);
            _label.IsLargeText = this.IsLargeText;
            _label.TextColor = this.TextDefaultColor;
            _label.BlockPropagation = false;
            _label.NoEvent = true;
        }
        public UIButton() : base() {
            Text = "按钮";
            DrawPanel = true;
            PanelDefaultColor = Color.Gray * 1.2f;
            PanelMouseOverColor = Color.White;

            TextDefaultColor = Color.White;
            TextMouseOverColor = Color.Yellow;
            Color = PanelDefaultColor;

            _label = new UILabel();
            SyncToLabel();
            this.AppendChild(_label);
            this.OnMouseEnter += UITextButton_OnMouseEnter;
            this.OnMouseOut += UITextButton_OnMouseOut;
        }

        private void UITextButton_OnMouseOut(Events.UIMouseEvent e, UIElement sender) {
            _isMouseOver = false;
        }

        private void UITextButton_OnMouseEnter(Events.UIMouseEvent e, UIElement sender) {
            _isMouseOver = true;
            Main.PlaySound(12);
        }
        public override void UpdateSelf(GameTime gameTime) {
            base.UpdateSelf(gameTime);
            SyncToLabel();
            if (_isMouseOver && _timer < 15)
                _timer++;
            else if (!_isMouseOver && _timer > 0)
                _timer--;
            float factor = _timer / 15f;
            this.Color = Color.Lerp(PanelDefaultColor, PanelMouseOverColor, factor);
            //this.Scale = new Vector2(1 + _timer / 100f, 1 + _timer / 100f);
            this._label.TextColor = Color.Lerp(TextDefaultColor, TextMouseOverColor, factor);
        }

        public override void DrawSelf(SpriteBatch sb) {
            if (DrawPanel)
                base.DrawSelf(sb);
        }
    }
}
