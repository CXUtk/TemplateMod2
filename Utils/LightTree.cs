using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.Utilities;

namespace TemplateMod2.Utils {
    public class LightTree {
        private class Node {
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
        private UnifiedRandom random;
        public LightTree(UnifiedRandom random) {
            cnt = 0;
            root = null;
            this.random = random;
        }
        private Vector2 target;
        private int cnt;
        private List<Vector2> keyPoints;


        public void Generate(Vector2 pos, Vector2 vel, Vector2 target) {
            // 根节点生成，朝向0，粗细1，长度随机50中选
            root = new Node(0, 1f, rand() * 50f);
            keyPoints = new List<Vector2>();
            this.target = target;
            root = _build(root, pos, vel, true);
            // Main.NewText($"生成了一个{cnt}个节点的树状结构");
        }
        private Node _build(Node node, Vector2 pos, Vector2 vel, bool root) {
            keyPoints.Add(pos);
            cnt++;
            if (node.size < 0.1f || node.length < 1 || Vector2.Distance(pos, target) < 10) return node;
            var r2 = (target - pos).ToRotation() - vel.ToRotation();
            var r = r2 + rand(MathHelper.Pi / 4f);
            var unit = (vel.ToRotation() + r).ToRotationVector2();
            Node rchild = new Node(r, node.size * 0.9f, node.length);
            // 闪电树主节点（树干）
            node.children.Add(_build(rchild, pos + unit * node.length, unit, root));
            if (root) {
                if (rand() > 0.75f) {
                    for (int i = 0; i < 1; i++) {
                        r = rand(MathHelper.Pi / 3f);
                        unit = (vel.ToRotation() + r).ToRotationVector2();
                        Node child = new Node(r, rand() * node.size * 0.6f, node.length * 0.6f);
                        node.children.Add(_build(child, pos + unit * node.length, unit, false));
                    }
                }
            }
            return node;
        }
        //private Node _build2(Node node, Vector2 pos, Vector2 vel, bool isMain, Vector2 target) {
        //    cnt++;
        //    keyPoints.Add(pos);
        //    // 终止条件：树枝太细了，或者太短了
        //    if (node.size < 0.1f || node.length < 1) return node;
        //    var r2 = (target - pos).ToRotation() - vel.ToRotation();
        //    var r = r2 + rand(MathHelper.Pi / 4f);
        //    Vector2 unit = (vel.ToRotation() + r).ToRotationVector2();
        //    Node main = new Node(rand(r), node.size * 0.95f, node.length);
        //    node.children.Add(_build(main, pos + unit * node.length, unit, isMain, target));
        //    // 只有较小的几率出分支
        //    if (rand() > 0.9f) {
        //        // 生成分支的时候长度变化不大，但是大小变化很大
        //        r = rand(MathHelper.Pi / 3f);
        //        unit = (vel.ToRotation() + r).ToRotationVector2();
        //        Node child = new Node(r, node.size * 0.6f, node.length);
        //        node.children.Add(_build(child, pos + unit * node.length, unit, false, target));
        //    }
        //    return node;
        //}


        //private Node _build(Node node, Vector2 pos, Vector2 vel, bool root) {
        //    keyPoints.Add(pos);
        //    cnt++;
        //    if (node.size < 0.1f || node.length < 1) return node;
        //    var r2 = (target - pos).ToRotation() - vel.ToRotation();
        //    var r = r2 + rand(MathHelper.Pi / 4f);
        //    var unit = (vel.ToRotation() + r).ToRotationVector2();
        //    Node rchild = new Node(r, node.size * 0.9f, node.length);
        //    // 闪电树主节点（树干）
        //    node.children.Add(_build(rchild, pos + unit * node.length, unit, root));
        //    if (root) {
        //        if (rand() > 0.8f) {
        //            for (int i = 0; i < 1; i++) {
        //                r = rand(MathHelper.Pi / 3f);
        //                unit = (vel.ToRotation() + r).ToRotationVector2();
        //                Node child = new Node(r, rand() * node.size * 0.6f, node.length * 0.6f);
        //                node.children.Add(_build(child, pos + unit * node.length, unit, false));
        //            }
        //        }
        //    }
        //    return node;
        //}




        private float rand() {
            double u = -2 * Math.Log(random.NextDouble());
            double v = 2 * Math.PI * random.NextDouble();
            return (float)Math.Max(0, Math.Sqrt(u) * Math.Cos(v) * 0.3 + 0.5);
        }

        private float rand(float range) {
            return random.NextFloatDirection() * range;
        }

        //private Node _build(Node node, Vector2 pos, Vector2 vel, bool root) {
        //    keyPoints.Add(pos);
        //    cnt++;
        //    if (node.size < 0.1f || node.length < 1) return node;
        //    var r2 = (target - pos).ToRotation() - vel.ToRotation();
        //    var r = r2 + rand(MathHelper.Pi / 4f);
        //    var unit = (vel.ToRotation() + r).ToRotationVector2();
        //    Node rchild = new Node(r, node.size * 0.9f, node.length);
        //    闪电树主节点（树干）
        //    node.children.Add(_build(rchild, pos + unit * node.length, unit, root));
        //    if (root) {
        //        if (rand() > 0.8f) {
        //            for (int i = 0; i < 1; i++) {
        //                r = rand(MathHelper.Pi / 3f);
        //                unit = (vel.ToRotation() + r).ToRotationVector2();
        //                Node child = new Node(r, rand() * node.size * 0.6f, node.length * 0.6f);
        //                node.children.Add(_build(child, pos + unit * node.length, unit, false));
        //            }
        //        }
        //    }
        //    return node;
        //}

        public void Draw(SpriteBatch sb, Vector2 pos, Vector2 vel) {
            sb.End();
            sb.Begin(SpriteSortMode.Deferred, BlendState.Additive);
            _draw(sb, pos, vel, root, Color.Cyan * 0.4f, 5f);
            _draw(sb, pos, vel, root, Color.White * 0.6f, 3f);
            sb.End();
            sb.Begin();
        }

        public void SpawnDust(Vector2 pos, Vector2 vel) {
            _dust(pos, vel, root);
        }

        private void _draw(SpriteBatch sb, Vector2 pos, Vector2 vel, Node node, Color c, float factor) {
            // 树枝实际的方向向量
            Vector2 unit = (vel.ToRotation() + node.rad).ToRotationVector2();
            // 类似激光的线性绘制方法，绘制出树枝
            for (float i = 0; i <= node.length; i += 0.3f)
                sb.Draw(Main.magicPixel, pos + unit * i, new Rectangle(0, 0, 1, 1), c, 0,
                    new Vector2(0.5f, 0.5f), Math.Max(node.size * factor, 0.3f), SpriteEffects.None, 0f);
            // 递归到子节点进行绘制
            foreach (var child in node.children) {
                // 传递给子节点真实的位置和方向向量
                _draw(sb, pos + unit * node.length, unit, child, c, factor);
            }
        }

        public void Tile(Vector2 pos, Vector2 vel) {
            _tile(pos, vel, root);
        }

        private void _tile(Vector2 pos, Vector2 vel, Node node) {
            // 树枝实际的方向向量
            Vector2 unit = (vel.ToRotation() + node.rad).ToRotationVector2();
            // 类似激光的线性绘制方法，绘制出树枝
            for (float i = 0; i <= node.length * 4; i += 8f) {
                Point p = (pos + unit * i).ToTileCoordinates();
                Main.tile[p.X, p.Y] = new Tile();
                Main.tile[p.X, p.Y].type = TileID.WoodBlock;
                Main.tile[p.X, p.Y].active(true);
                WorldGen.SquareTileFrame(p.X, p.Y);
            }
            // 递归到子节点进行绘制
            foreach (var child in node.children) {
                // 传递给子节点真实的位置和方向向量
                _tile(pos + unit * node.length * 4, unit, child);
            }
        }

        private void _dust(Vector2 pos, Vector2 vel, Node node) {
            float r = vel.ToRotation();
            Vector2 unit = (r + node.rad).ToRotationVector2();
            for (float i = 0; i <= node.length; i += 4f) {
                var dust = Dust.NewDustDirect(pos + unit * i, 0, 0,
                    MyDustId.DemonTorch, 0, 0, 100, Color.White, 1);
                dust.noGravity = true;
                dust.velocity *= 0;
                dust.position = pos + unit * i;
            }
            foreach (var child in node.children) {
                _dust(pos + unit * node.length, unit, child);
            }
        }

        public bool Check(Rectangle hitbox) {
            foreach (var pt in keyPoints)
                if (hitbox.Contains(pt.ToPoint())) return true;
            return false;
        }
    }
}

//using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Graphics;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Terraria;
//using Terraria.Utilities;

//namespace TemplateMod2.Utils {
//    public class LightTree {
//        private class Node {
//            public float rad, size, length;
//            public List<Node> children;
//            public Node(float rad, float size, float length) {
//                this.rad = rad;
//                this.size = size;
//                this.length = length;
//                this.children = new List<Node>();
//            }
//        };
//        private Node root;
//        private UnifiedRandom random;
//        public LightTree(UnifiedRandom random) {
//            cnt = 0;
//            root = null;
//            this.random = random;
//        }
//        private Vector2 target;
//        private int cnt;
//        private List<Vector2> keyPoints;


//        public void Generate(Vector2 pos, Vector2 vel, Vector2 target) {
//            keyPoints = new List<Vector2>();
//            root = new Node(0, 1f, rand() * LEN);
//            this.target = target;
//            root = _build(root, pos, vel, true);
//            // Main.NewText($"生成了一个{cnt}个节点的树状结构");
//        }
//        private float rand() {
//            double u = -2 * Math.Log(random.NextDouble());
//            double v = 2 * Math.PI * random.NextDouble();
//            return (float)Math.Max(0, Math.Sqrt(u) * Math.Cos(v) * 0.3 + 0.5);
//        }

//        private float rand(float range) {
//            return random.NextFloatDirection() * range;
//        }

//        private Node _build(Node node, Vector2 pos, Vector2 vel, bool root) {
//            keyPoints.Add(pos);
//            cnt++;
//            if (node.size < 0.1f || node.length < 1) return node;
//            var r2 = (target - pos).ToRotation() - vel.ToRotation();
//            var r = r2 + rand(MathHelper.Pi / 4f);
//            var unit = (vel.ToRotation() + r).ToRotationVector2();
//            Node rchild = new Node(r, node.size * 0.9f, node.length);
//            // 闪电树主节点（树干）
//            node.children.Add(_build(rchild, pos + unit * node.length, unit, root));
//            if (root) {
//                if (rand() > 0.8f) {
//                    for (int i = 0; i < 1; i++) {
//                        r = rand(MathHelper.Pi / 3f);
//                        unit = (vel.ToRotation() + r).ToRotationVector2();
//                        Node child = new Node(r, rand() * node.size * 0.6f, node.length * 0.6f);
//                        node.children.Add(_build(child, pos + unit * node.length, unit, false));
//                    }
//                }
//            }
//            return node;
//        }

//        public void Draw(SpriteBatch sb, Vector2 pos, Vector2 vel) {
//            _draw(sb, pos, vel, root);
//        }

//        public void SpawnDust(Vector2 pos, Vector2 vel) {
//            _dust(pos, vel, root);
//        }

//        private void _draw(SpriteBatch sb, Vector2 pos, Vector2 vel, Node node) {
//            Vector2 unit = (vel.ToRotation() + node.rad).ToRotationVector2();
//            for (float i = 0; i <= node.length; i += 0.04f) {
//                sb.Draw(Main.magicPixel, pos + unit * i, new Rectangle(0, 0, 1, 1), Color.White * 0.5f, 0,
//                    new Vector2(0.5f, 0.5f), Math.Max(node.size * 7, 0.2f), SpriteEffects.None, 0f);
//            }
//            foreach (var child in node.children) {
//                _draw(sb, pos + unit * node.length, unit, child);
//            }
//        }

//        private void _dust(Vector2 pos, Vector2 vel, Node node) {
//            float r = vel.ToRotation();
//            Vector2 unit = (r + node.rad).ToRotationVector2();
//            for (float i = 0; i <= node.length; i += 4f) {
//                var dust = Dust.NewDustDirect(pos + unit * i, 0, 0,
//                    MyDustId.DemonTorch, 0, 0, 100, Color.White, 1);
//                dust.noGravity = true;
//                dust.velocity *= 0;
//                dust.position = pos + unit * i;
//            }
//            foreach (var child in node.children) {
//                _dust(pos + unit * node.length, unit, child);
//            }
//        }

//        public bool Check(Rectangle hitbox) {
//            foreach (var pt in keyPoints)
//                if (hitbox.Contains(pt.ToPoint())) return true;
//            return false;
//        }
//    }
//}
