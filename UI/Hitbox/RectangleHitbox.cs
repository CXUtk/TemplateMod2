using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace TemplateMod2.UI.Hitbox {
    public class RectangleHitbox : IHitBox {
        private Rectangle _rectangle;
        public RectangleHitbox(Rectangle rect) {
            _rectangle = rect;
        }
        public bool Contains(Vector2 point) {
            return _rectangle.Contains(point.ToPoint());
        }

        public void Draw(SpriteBatch sb) {
            Drawing.StrokeRect(sb, _rectangle, 1, Color.Red);
        }

        public Rectangle GetOuterRectangle() {
            return _rectangle;
        }

        public bool Intersects(IHitBox hitBox) {
            return false;
        }
    }
}
