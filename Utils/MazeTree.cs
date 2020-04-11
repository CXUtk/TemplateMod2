using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terraria;
using Terraria.ID;
using Terraria.Map;
using Terraria.Utilities;

namespace TemplateMod2.Utils {
    public class MazeTree {
        private class Node : IComparable {
            public int dir, x, y, p;
            public Node[] children = new Node[4];
            public Node(int dir, int x, int y, int p) {
                this.dir = dir;
                this.x = x;
                this.y = y;
                this.p = p;
            }

            public int CompareTo(object obj) {
                var node = (Node)obj;
                return p.CompareTo(node.p);
            }
        };

        // 左右上下
        private static int[] dx = { -1, 1, 0, 0 };
        private static int[] dy = { 0, 0, -1, 1 };
        private Node root;
        private UnifiedRandom random;
        private int maxX, maxY;
        private bool[,] vis;
        private int cnt;
        public MazeTree(UnifiedRandom random) {
            root = null;
            this.random = random;
        }

        private bool check(int x, int y) {
            return x >= 0 && x < maxX && y >= 0 && y < maxY && !vis[x, y];
        }

        private void swap(ref int a, ref int b) {
            int t = a;
            a = b;
            b = t;
        }

        public void Build(int x, int y) {
            maxX = x;
            maxY = y;
            vis = new bool[x, y];
            for (int i = 0; i < x; i++) {
                for (int j = 0; j < y; j++) {
                    vis[i, j] = false;
                }
            }
            root = new Node(-1, 0, 0, 0);
            vis[0, 0] = true;
            PriorityQueue<Node> Q = new PriorityQueue<Node>(x * y);
            Q.Push(root);
            while (!Q.Empty) {
                var cur = Q.Top;
                Q.Pop();
                for (int i = 0; i < 4; i++) {
                    int nx = cur.x + dx[i], ny = cur.y + dy[i];
                    Node nd = new Node(i, nx, ny, -Math.Abs(nx - maxX) - Math.Abs(ny - maxY));
                    if (!check(nd.x, nd.y)) continue;
                    vis[nd.x, nd.y] = true;
                    cur.children[i] = nd;
                    Q.Push(nd);
                }
            }
        }




        public void Tile(int x, int y) {
            _tile(root, x, y);
        }

        //private void _tile(Node node, int x, int y) {
        //    // 把3x3单元格填满
        //    for (int i = -1; i <= 1; i++) {
        //        for (int j = -1; j <= 1; j++) {
        //            Main.tile[x + i, y + j] = new Tile {
        //                type = TileID.WoodBlock,
        //            };
        //            Main.tile[x + i, y + j].active(true);
        //        }
        //    }
        //    // 中间的清除掉
        //    Main.tile[x, y] = new Tile();
        //    // 如果不在根节点
        //    if (node.dir != -1) {
        //        // 获取方向的反向
        //        int d = node.dir ^ 1;
        //        // 挖空
        //        for (int i = 1; i < 3; i++)
        //            Main.tile[x + dx[d] * i, y + dy[d] * i] = new Tile();
        //    }
        //    // 顺着每个非空子节点继续构造迷宫
        //    for (int i = 0; i < 4; i++) {
        //        Node child = node.children[i];
        //        if (child == null) continue;
        //        // 因为单元格变成3x3，所以这里的坐标也要扩大三倍，总体迷宫范围扩大9倍
        //        _tile(child, x + dx[child.dir] * 3, y + dy[child.dir] * 3);
        //    }
        //}

        private void _tile(Node node, int x, int y) {
            for (int i = -2; i <= 2; i++) {
                for (int j = -2; j <= 2; j++) {
                    Main.tile[x + i, y + j] = new Tile {
                        type = TileID.WoodBlock,
                    };
                    Main.tile[x + i, y + j].active(true);
                }
            }
            if (node.dir != -1) {
                int d = node.dir ^ 1;
                for (int i = -1; i < 7; i++) {
                    Main.tile[x + dx[d] * i, y + dy[d] * i] = new Tile();
                    if (d >= 2) {
                        Main.tile[x + dx[d] * i + 1, y + dy[d] * i] = new Tile();
                        Main.tile[x + dx[d] * i - 1, y + dy[d] * i] = new Tile();
                    } else {
                        Main.tile[x + dx[d] * i, y + dy[d] * i + 1] = new Tile();
                        Main.tile[x + dx[d] * i, y + dy[d] * i - 1] = new Tile();
                    }
                }
            }
            for (int i = 0; i < 4; i++) {
                Node child = node.children[i];
                if (child == null) continue;
                _tile(child, x + dx[child.dir] * 5, y + dy[child.dir] * 5);
            }
        }

        //private void _tile(Node node, int x, int y) {
        //    if (node.dir == -1) {
        //        for (int i = -1; i <= 1; i++) {
        //            for (int j = -1; j <= 1; j++) {
        //                Main.tile[x + i, y + j] = new Tile();
        //            }
        //        }
        //    } else {
        //        for (int i = -1; i <= 1; i++) {
        //            for (int j = -1; j <= 1; j++) {
        //                Main.tile[x + i, y + j] = new Tile {
        //                    type = TileID.WoodBlock,
        //                };
        //                Main.tile[x + i, y + j].active(true);
        //            }
        //        }
        //        if (node.dir < 2) {
        //            int d = node.dir;
        //            for (int i = -1 + d; i <= -1 + d + 1; i++) {
        //                Main.tile[x + i, y] = new Tile();
        //            }
        //            if (node.dirp >= 2) {
        //                d = (node.dirp == 2) ? 1 : -1;
        //                Main.tile[x, y + d] = new Tile();
        //            } else {
        //                for (int i = -1; i <= 1; i++) {
        //                    Main.tile[x + i, y] = new Tile();
        //                }
        //            }
        //        } else {
        //            int d = (node.dir == 2) ? 0 : 1;
        //            for (int i = -1 + d; i <= -1 + d + 1; i++) {
        //                Main.tile[x, y + i] = new Tile();
        //            }
        //            if (node.dirp < 2) {
        //                d = (node.dirp == 0) ? 1 : -1;
        //                Main.tile[x + d, y] = new Tile();
        //            } else {
        //                for (int i = -1; i <= 1; i++) {
        //                    Main.tile[x, y + i] = new Tile();
        //                }
        //            }
        //        }
        //    }
        //    for (int i = 0; i < 4; i++) {
        //        Node child = node.children[i];
        //        if (child == null) continue;
        //        _tile(child, x + dx[child.dir] * 3, y + dy[child.dir] * 3);
        //    }
        //}
    }
}
