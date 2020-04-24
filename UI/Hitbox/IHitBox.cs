using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateMod2.UI.Hitbox {
    public interface IHitBox {
        bool Contains(Vector2 point);
        bool Intersects(IHitBox hitBox);
        Rectangle GetOuterRectangle();
        void Draw(SpriteBatch sb);
    }
}
