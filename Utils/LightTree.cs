using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;

namespace TemplateMod2.Utils {
    public class LightTree {
        const float PI = 3.1415926f;
        const float LEN = 50f;
        class Node {
            public float rad, size, length;
            public List<Node> children;
            public Node(float rad, float size, float length) {
                this.rad = rad;
                this.size = size;
                this.length = length;
                this.children = new List<Node>();
            }
        };
        private Node root;
        private int cnt;
        private List<Vector2> keyPoints;

        public LightTree() {
            cnt = 0;
            root = null;
        }
        public void Generate(Vector2 pos, Vector2 vel) {
            keyPoints = new List<Vector2>();
            root = new Node(0, 1f, (float)random() * 50f);
            root = _build(root, pos, vel);
            Main.NewText($"生成了一个{cnt}个节点的闪电树");
        }
        private double random() {
            var rand = Main.rand;
            double u = -2 * Math.Log(rand.NextDouble());
            double v = 2 * Math.PI * rand.NextDouble();
            return Math.Max(0, Math.Sqrt(u) * Math.Cos(v) * 0.3 + 0.5);
        }

        private Node _build(Node node, Vector2 pos, Vector2 vel) {
            if (node.size < 0.04 || node.length < 0.04) return node;
            keyPoints.Add(pos);
            cnt++;
            const int MAXN = 3;
            float rad = (float)(random() * PI * 2 / 6f - PI / 6f);
            var unit = (vel.ToRotation() + rad).ToRotationVector2();
            Node rchild = new Node(rad, (0.8f * node.size), 0.8f * node.length);
            node.children.Add(_build(rchild, pos + unit * node.length, unit));
            int size = (int)(MAXN * random() + 0.5);
            for (int i = 0; i < size; i++) {
                rad = (float)random() * PI * 2 / 3f - PI / 3f;
                unit = (vel.ToRotation() + rad).ToRotationVector2();
                Node child = new Node(rad, (float)(random() * node.size * 0.8f), (float)random() * node.length * 0.5f);
                node.children.Add(_build(child, pos + unit * node.length, unit));
            }
            return node;
        }

        public void Draw(SpriteBatch sb, Vector2 pos, Vector2 vel) {
            _draw(sb, pos, vel, root);
        }

        private void _draw(SpriteBatch sb, Vector2 pos, Vector2 vel, Node node) {
            float r = vel.ToRotation();
            Vector2 unit = (r + node.rad).ToRotationVector2();
            for (float i = 0; i <= node.length; i += 0.04f) {
                sb.Draw(Main.magicPixel, pos + unit * i, new Rectangle(0, 0, 1, 1), Color.White, 0,
                    new Vector2(0.5f, 0.5f), node.size * 2, SpriteEffects.None, 0f);
            }
            foreach (var child in node.children) {
                _draw(sb, pos + unit * node.length, unit, child);
            }
        }

        public bool Check(Rectangle hitbox) {
            foreach (var pt in keyPoints)
                if (hitbox.Contains(pt.ToPoint())) return true;
            return false;
        }
    }
}
