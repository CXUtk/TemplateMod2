using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateMod2.UI.Events;
using Terraria;

namespace TemplateMod2.UI {
    public class UIElement {
        public delegate void MouseEvent(UIMouseEvent e, UIElement sender);

        public static bool DEBUG_MODE = true;

        /// <summary>
        /// UI元素是否处于激活状态，如果不激活则不会显示也不会响应任何事件
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// UI元素是否能见，如果不能见就不绘制，但是会响应事件
        /// </summary>
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
        /// 该UI节点的基准点位置，计算位置旋转等时会以此位置为原点
        /// X和Y的值一般为0到1的浮点数，代表节点的比例位置
        /// </summary>
        public Vector2 Pivot { get; set; }

        /// <summary>
        /// UI元素的锚点，也就是其基准点相对于父节点的位置
        /// X和Y的值一般为0到1的浮点数，代表父节点的比例位置
        /// </summary>
        public Vector2 AnchorPoint { get; set; }

        /// <summary>
        /// 该UI元素的宽度
        /// </summary>
        public int Width { get { return _width; } set { _width = value; calculateScreenPos(); } }

        /// <summary>
        /// 该UI元素的高度
        /// </summary>
        public int Height { get { return _height; } set { _height = value; calculateScreenPos(); } }

        /// <summary>
        /// 该UI元素相对于自己基准点的位置，不是左上角
        /// </summary>
        public Vector2 Position { get { return _position; } set { _position = value; calculateScreenPos(); } }

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


        public event MouseEvent OnMouseOver;
        public event MouseEvent OnMouseOut;

        private Vector2 _screenTopLeft;
        private Vector2 _position;
        private int _width, _height;



        public void MouseOver(UIMouseEvent e) {
            OnMouseOver?.Invoke(e, this);
            if (!BlockPropagation)
                Parent?.MouseOver(e);
        }

        public void MouseOut(UIMouseEvent e) {
            OnMouseOut?.Invoke(e, this);
            if (!BlockPropagation)
                Parent?.MouseOut(e);
        }



        public UIElement ElementAt(Vector2 pos) {
            Main.NewText(this.HitboxScreen);
            foreach (var child in Children) {
                if (child.IsActive && child.HitboxScreen.Contains(pos.ToPoint()))
                    return child.ElementAt(pos);
            }
            if (HitboxScreen.Contains(pos.ToPoint())) return this;
            return null;
        }



        public Vector2 TopLeft {
            get {
                return new Vector2(_position.X - Width * Pivot.X, _position.Y - Height * Pivot.Y);
            }
        }


        public Rectangle HitboxScreen {
            get {
                return new Rectangle((int)(_screenTopLeft.X),
                    (int)(_screenTopLeft.Y), Width, Height);
            }
        }

        public Vector2 PivotPos {
            get {
                return _screenTopLeft + new Vector2(Width * Pivot.X, Height * Pivot.Y);
            }
        }

        private Vector2 getBasePosScreen() {
            Vector2 pos = (Parent == null) ? Vector2.Zero : Parent._screenTopLeft;
            int pw = Main.screenWidth, ph = Main.screenHeight;
            if (Parent != null) {
                pw = Parent.Width;
                ph = Parent.Height;
            }
            pos += new Vector2(pw * AnchorPoint.X, ph * AnchorPoint.Y);
            return pos;
        }

        private void calculateScreenPos() {
            _screenTopLeft = getBasePosScreen() + Position - new Vector2(Width * Pivot.X, Height * Pivot.Y);
        }

        public void Recalculate() {
            calculateScreenPos();
            RecalculateChildren();
        }

        public void RecalculateChildren() {
            foreach (var element in Children) {
                element.Recalculate();
            }
        }

        public UIElement() {
            Pivot = new Vector2(0.5f, 0.5f);
            AnchorPoint = Vector2.Zero;
            Position = new Vector2(0, 0);
            Parent = null;
            Children = new List<UIElement>();
            IsActive = true;
            IsVisible = true;
            BlockPropagation = true;

            Recalculate();
        }

        public void AppendChild(UIElement element) {
            element.SplitFromParent();
            element.Parent = this;
            Children.Add(element);
            element.Recalculate();
        }

        public void RemoveChild(UIElement element) {
            Children.Remove(element);
        }

        public UIElement GetChildByName(string name) {
            return Children.FirstOrDefault((element) => element.Name.Equals(name));
        }

        public void SplitFromParent() {
            Parent?.RemoveChild(this);
        }

        public virtual void Draw(SpriteBatch sb) {
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


        public virtual void Update(GameTime gameTime) {
            foreach (var child in Children) {
                if (child.IsActive)
                    child.Update(gameTime);
            }
        }


        public override string ToString() {
            return $"Type: {GetType().Name}, Name: {Name}";
        }
    }
}
