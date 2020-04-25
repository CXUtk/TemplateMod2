using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace TemplateMod2.UI.Hitbox {
    public class QuadrilateralHitbox : IHitBox {
        private Vector2[] _points;

        /// <summary>
        /// 逆时针多边形顶点序列
        /// </summary>
        /// <param name="points"></param>
        public QuadrilateralHitbox() {
            _points = new Vector2[4];
        }

        public void Reset(int w, int h) {
            _points[0] = Vector2.Zero;
            _points[1] = new Vector2(0, h);
            _points[2] = new Vector2(w, h);
            _points[3] = new Vector2(w, 0);
        }

        public void Transform(Matrix matrix) {
            for (int i = 0; i < _points.Length; i++) {
                _points[i] = Vector2.Transform(_points[i], matrix);
            }
        }

        public static bool ToLeft(Vector2 p1, Vector2 p2, Vector2 point) {
            Vector2 v1 = p2 - p1, v2 = point - p1;
            double cross = v1.X * v2.Y - v1.Y * v2.X;
            return cross < 0;
        }

        public bool Contains(Vector2 point) {
            // Main.NewText(ToLeft(new Vector2(0, 0), new Vector2(1, 1), new Vector2(0, 1)));
            for (int i = 0; i < _points.Length; i++) {
                if (!ToLeft(_points[i], _points[(i + 1) % _points.Length], point)) return false;
            }
            return true;
        }

        public void Draw(SpriteBatch sb) {
            Drawing.StrokePolygon(sb, _points.ToList(), 1, Color.Lime);
        }

        public bool Intersects(IHitBox hitBox) {
            return false;
        }

        public Rectangle GetOuterRectangle() {
            float minX = int.MaxValue, maxX = 0;
            float minY = int.MaxValue, maxY = 0;
            foreach (var pt in _points) {
                minX = Math.Min(minX, pt.X);
                minY = Math.Min(minY, pt.Y);
                maxX = Math.Max(maxX, pt.X);
                maxY = Math.Max(maxY, pt.Y);
            }
            return new Rectangle((int)minX, (int)minY, (int)(maxX - minX + 1), (int)(maxY - minY + 1));
        }

        public override string ToString() {
            StringBuilder sb = new StringBuilder();
            foreach (var p in _points) {
                sb.Append(p.ToString());
                sb.Append(",");
            }
            return sb.ToString();
        }
    }
}
