using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateMod2.UI.Events;

namespace TemplateMod2.UI {
    public class UIElement {
        public delegate void MouseEvent(UIMouseEvent e, UIElement sender);

        public static bool DEBUG_MODE = true;


        public bool IsActive { get; set; }

        public bool IsVisible { get; set; }

        /// <summary>
        /// 该UI节点的父节点
        /// </summary>
        public UIElement Parent { get; set; }

        /// <summary>
        /// 该UI节点的直接子节点
        /// </summary>
        private List<UIElement> Children { get; set; }

        /// <summary>
        /// 该UI节点的锚点位置，计算位置时会以此位置为原点
        /// X和Y的值一般为0到1的浮点数
        /// </summary>
        public Vector2 Pivot { get; set; }

        /// <summary>
        /// 该UI元素的宽度
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        /// 该UI元素的高度
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// 该UI元素相对于父节点锚点的位置
        /// </summary>
        public Vector2 Position {
            get {
                return _position;
            }
            set {
                _position = value;
                _screenPos = (Parent == null) ? _position : Parent._screenPos + _position;
            }
        }

        /// <summary>
        /// UI元素的拉伸倍率
        /// </summary>
        public Vector2 Scale { get; set; }

        /// <summary>
        /// UI元素绕基准点旋转的弧度
        /// </summary>
        public float Rotation { get; set; }

        /// <summary>
        /// UI元素的名字
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// UI元素的溢出行为
        /// </summary>
        public OverflowType Overflow { get; set; }

        /// <summary>
        /// UI元素是否阻止事件向其父元素传播
        /// </summary>
        public bool BlockPropagation { get; set; }

        private Vector2 _screenPos;
        private Vector2 _position;



        public Vector2 TopLeft {
            get {
                return new Vector2(_position.X - Width * Pivot.X, _position.Y - Height * Pivot.Y);
            }
        }


        public Rectangle HitboxScreen {
            get {
                return new Rectangle((int)(_screenPos.X - Width * Pivot.X),
                    (int)(_screenPos.Y - Height * Pivot.Y), Width, Height);
            }
        }

        private void recalculate() {
            _screenPos = (Parent == null) ? _position : Parent._screenPos + _position;
            foreach (var element in Children) {
                element.recalculate();
            }
        }

        public UIElement() {
            Pivot = new Vector2(0.5f, 0.5f);
            _position = Vector2.Zero;
            Parent = null;
            Children = new List<UIElement>();
            IsActive = true;
            IsVisible = true;

            recalculate();
        }

        public void AppendChild(UIElement element) {
            element.SplitFromParent();
            element.Parent = this;
            Children.Add(element);
            element.recalculate();
        }

        public void RemoveChild(UIElement element) {
            Children.Remove(element);
        }

        public void SplitFromParent() {
            Parent?.RemoveChild(this);
        }

        public void Draw(SpriteBatch sb) {
            if (!IsVisible) return;
            DrawSelf(sb);
            foreach (var child in Children) {
                if (child.IsActive && child.IsVisible)
                    child.Draw(sb);
            }
            if (UIElement.DEBUG_MODE) {
                Drawing.StrokeRect(sb, HitboxScreen, 1, Color.Red);
            }
        }

        public virtual void DrawSelf(SpriteBatch sb) { }


        public void Update(GameTime gameTime) {
            foreach (var child in Children) {
                if (child.IsActive)
                    child.Update(gameTime);
            }
        }
    }
}
