using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;

namespace TemplateMod2.UI {
    public static class Drawing {
        public static void DrawStraightLine(SpriteBatch sb, Vector2 p1, Vector2 p2, int lineWidth = 1, Color color = default(Color)) {
            Vector2 dir = p2 - p1;
            float dis = dir.Length();
            dir.Normalize();
            for (int i = 0; i <= dis; i++) {
                sb.Draw(Main.magicPixel, p1, new Rectangle(0, 0, 1, 1), color, 0, new Vector2(0.5f, 0.5f), lineWidth, SpriteEffects.None, 0f);
                p1 += dir;
            }
        }
        public static void StrokeRect(SpriteBatch sb, Rectangle rect, int lineWidth = 1, Color color = default(Color)) {
            DrawStraightLine(sb, rect.TopLeft(), rect.TopRight(), lineWidth, color);
            DrawStraightLine(sb, rect.TopRight(), rect.BottomRight(), lineWidth, color);
            DrawStraightLine(sb, rect.BottomRight(), rect.BottomLeft(), lineWidth, color);
            DrawStraightLine(sb, rect.BottomLeft(), rect.TopLeft(), lineWidth, color);
        }
        public static void StrokePolygon(SpriteBatch sb, List<Vector2> points, int lineWidth = 1, Color color = default(Color)) {
            for (int i = 0; i < points.Count; i++) {
                DrawStraightLine(sb, points[i], points[(i + 1) % points.Count], lineWidth, color);
            }
        }
    }
}
