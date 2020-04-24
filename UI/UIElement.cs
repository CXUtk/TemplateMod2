using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TemplateMod2.UI.Events;
using TemplateMod2.UI.Hitbox;
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
        public int Width { get { return _width; } set { _width = value; } }

        /// <summary>
        /// 该UI元素的高度
        /// </summary>
        public int Height { get { return _height; } set { _height = value; } }

        /// <summary>
        /// 该UI元素与于自身锚点的相对位置
        /// </summary>
        public Vector2 Position { get { return _position; } set { _position = value; } }

        /// <summary>
        /// UI元素绕基准点旋转的弧度，注意，如果设置了旋转，就不要设置溢出隐藏了
        /// </summary>
        public float Rotation { get; set; }

        /// <summary>
        /// UI元素的放大倍率
        /// </summary>
        public Vector2 Scale { get; set; }

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
        private Vector2 _realPosition;
        private QuadrilateralHitbox _selfHitbox;
        private Matrix _selfTransform;

        private int _width, _height;
        private RasterizerState _selfRasterizerState;



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
            foreach (var child in Children) {
                if (child.IsActive && child._selfHitbox.Contains(pos))
                    return child.ElementAt(pos);
            }
            if (_selfHitbox.Contains(pos)) return this;
            return null;
        }


        public Vector2 TopLeftScreen {
            get {
                return _screenTopLeft;
            }
        }


        public Vector2 TopLeft {
            get {
                return new Vector2(_position.X - Width * Pivot.X, _position.Y - Height * Pivot.Y);
            }
        }


        public Rectangle InnerRectangleScreen {
            get {
                return new Rectangle((int)(_screenTopLeft.X),
                    (int)(_screenTopLeft.Y), Width, Height);
            }
        }

        public IHitBox ScreenHitBox {
            get {
                return _selfHitbox;
            }
        }

        public Vector2 PivotPosScreen {
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


        public void Recalculate() {
            _screenTopLeft = getBasePosScreen() + Position - new Vector2(Width * Pivot.X, Height * Pivot.Y);
            _realPosition = (Parent == null) ? Position : new Vector2(Parent.Width * AnchorPoint.X, Parent.Height * AnchorPoint.Y)
                + Position - new Vector2(Width * Pivot.X, Height * Pivot.Y);
            _selfTransform = Main.UIScaleMatrix;
            if (Parent != null) _selfTransform = Parent._selfTransform;
            _selfTransform = ApplyTransform(_selfTransform);
            _selfHitbox.Reset(Width, Height);
            _selfHitbox.Transform(_selfTransform);
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
            Scale = new Vector2(1f, 1f);
            Parent = null;
            Children = new List<UIElement>();
            IsActive = true;
            IsVisible = true;
            BlockPropagation = true;
            Rotation = 0;
            _selfRasterizerState = new RasterizerState() {
                CullMode = CullMode.None,
                ScissorTestEnable = true,
            };
            _selfHitbox = new QuadrilateralHitbox();
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


        public Rectangle GetClippingRectangle(SpriteBatch sb) {
            return Rectangle.Intersect(sb.GraphicsDevice.ScissorRectangle, _selfHitbox.GetOuterRectangle());
        }

        private Matrix ApplyTransform(Matrix prev) {
            Matrix curTransform = Matrix.CreateTranslation(new Vector3(_realPosition.X + Width * Pivot.X, _realPosition.Y + Height * Pivot.Y, 0)) * prev;
            curTransform = Matrix.CreateScale(Scale.X, Scale.Y, 1f) * curTransform;
            curTransform = Matrix.CreateRotationZ(Rotation) * curTransform;
            curTransform = Matrix.CreateTranslation(new Vector3(-Width * Pivot.X, -Height * Pivot.Y, 0f)) * curTransform;
            return curTransform;
        }
        public virtual void Draw(SpriteBatch sb) {
            if (DEBUG_MODE) {
                sb.End();
                sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicClamp,
                    DepthStencilState.None, _selfRasterizerState, null, Main.UIScaleMatrix);
                _selfHitbox.Draw(sb);
                Drawing.StrokeRect(sb, GetClippingRectangle(sb), 1, Color.Yellow);
            }
            Rectangle scissorRectangle = sb.GraphicsDevice.ScissorRectangle;
            if (IsVisible) {
                sb.End();
                sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicClamp,
                DepthStencilState.None, _selfRasterizerState, null, _selfTransform);
                DrawSelf(sb);
            }
            if (Overflow == OverflowType.Hidden) {
                sb.End();
                sb.GraphicsDevice.ScissorRectangle = Rectangle.Intersect(sb.GraphicsDevice.ScissorRectangle, GetClippingRectangle(sb));
                sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicClamp,
                    DepthStencilState.None, _selfRasterizerState, null, _selfTransform);
            }
            foreach (var child in Children) {
                if (child.IsActive) {
                    child.Draw(sb);
                }
            }
            if (Overflow == OverflowType.Hidden) {
                sb.End();
                var defaultstate = sb.GraphicsDevice.RasterizerState;
                sb.GraphicsDevice.ScissorRectangle = scissorRectangle;
                sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicClamp,
                    DepthStencilState.None, defaultstate, null, _selfTransform);
            }
        }

        public virtual void DrawSelf(SpriteBatch sb) {
            if (UIElement.DEBUG_MODE) {
                sb.Draw(Main.magicPixel, new Rectangle(1, 1, Width - 2, Height - 2), Color.White * 0.4f);
            }
        }

        public virtual void UpdateSelf(GameTime gameTime, Matrix uiMatrix) { }

        public void Update(GameTime gameTime, Matrix uiMatrix) {
            UpdateSelf(gameTime, uiMatrix);
            foreach (var child in Children) {
                if (child.IsActive) {
                    child.Update(gameTime, uiMatrix);
                }
            }
        }


        public override string ToString() {
            return $"Type: {GetType().Name}, Name: {Name}";
        }
    }
}
