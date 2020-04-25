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
using Newtonsoft.Json;

namespace TemplateMod2.UI {
    public class UIElement {
        public delegate void MouseEvent(UIMouseEvent e, UIElement sender);

        public static bool DEBUG_MODE = false;


        #region 基础属性
        /// <summary>
        /// UI元素是否处于激活状态，如果不激活则不会显示也不会响应任何事件
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// UI元素是否能见，如果不能见就不绘制，但是会响应事件
        /// </summary>
        public bool IsVisible { get; set; }

        [JsonIgnore]
        /// <summary>
        /// 该UI节点的父节点
        /// </summary>
        public UIElement Parent { get; set; }

        /// <summary>
        /// 该UI节点的直接子节点
        /// </summary>
        private List<UIElement> Children { get; set; }

        /// <summary>
        /// 该UI节点的基准点位置，计算位置旋转等时会以此位置为原点，
        /// X和Y的值一般为0到1的浮点数，代表节点的比例位置
        /// </summary>
        public Vector2 Pivot { get; set; }

        /// <summary>
        /// UI元素的锚点，也就是其基准点相对于父节点的位置，
        /// X和Y的值一般为0到1的浮点数，代表父节点的比例位置
        /// </summary>
        public Vector2 AnchorPoint { get; set; }

        /// <summary>
        /// 该UI元素的宽度，高度
        /// </summary>
        public Vector2 Size { get; set; }

        /// <summary>
        /// 该UI元素相对于父节点的宽度，高度
        /// </summary>
        public Vector2 SizeFactor { get; set; }

        /// <summary>
        /// 该UI元素与于自身锚点的相对位置
        /// </summary>
        public Vector2 Position { get; set; }

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

        /// <summary>
        /// 这个UI元素是否会响应事件
        /// </summary>
        public bool NoEvent { get; set; }



        public int MarginLeft { get; set; }
        public int MarginRight { get; set; }
        public int MarginTop { get; set; }
        public int MarginBottom { get; set; }


        public int PaddingLeft { get; set; }
        public int PaddingRight { get; set; }
        public int PaddingTop { get; set; }
        public int PaddingBottom { get; set; }
        #endregion


        #region 事件
        public event MouseEvent OnMouseOver;
        public event MouseEvent OnMouseOut;
        public event MouseEvent OnMouseDown;
        public event MouseEvent OnMouseUp;
        public event MouseEvent OnClick;
        #endregion


        #region 派生属性
        public Rectangle OuterRectangleScreen {
            get {
                return _selfHitbox.GetOuterRectangle();
            }
        }
        public Rectangle BaseRectangleScreen {
            get {
                return new Rectangle((int)(_baseTopLeftScreen.X), (int)(_baseTopLeftScreen.Y), Width, Height);
            }
        }
        public Rectangle InnerRectangleScreen {
            get {
                return new Rectangle((int)(_baseTopLeftScreen.X), (int)(_baseTopLeftScreen.Y), Width, Height);
            }
        }

        protected int Width {
            get {
                return (int)(SizeFactor.X * getParentRect().Width + Size.X);
            }
        }
        protected int Height {
            get {
                return (int)(SizeFactor.Y * getParentRect().Height + Size.Y);
            }
        }
        #endregion


        private Rectangle getParentRect() {
            return Parent == null ? new Rectangle(0, 0, Main.screenWidth, Main.screenHeight) :
                Parent.InnerRectangleScreen;
        }

        private Vector2 getBaseRectScreen() {
            var rect = getParentRect();
            Vector2 pos = rect.TopLeft();
            pos += new Vector2(rect.Width * AnchorPoint.X, rect.Height * AnchorPoint.Y);
            return pos;
        }

        public virtual void Recalculate() {
            _baseTopLeftScreen = getBaseRectScreen() + Position - new Vector2(Width * Pivot.X, Height * Pivot.Y);
            _realPosition = (Parent == null) ? Position : new Vector2(Parent.Width * AnchorPoint.X, Parent.Height * AnchorPoint.Y)
                + Position - new Vector2(Width * Pivot.X, Height * Pivot.Y);

            _selfTransform = Main.UIScaleMatrix;
            if (Parent != null) _selfTransform = Parent._selfTransform;
            _selfTransform = ApplyTransform(_selfTransform);
            _selfHitbox.Reset(Width, Height);
            _selfHitbox.Transform(_selfTransform);
            RecalculateChildren();
        }


        private Vector2 _baseTopLeftScreen;
        private Vector2 _realPosition;
        private QuadrilateralHitbox _selfHitbox;
        private Matrix _selfTransform;

        private RasterizerState _selfRasterizerState;



        public void MouseOver(UIMouseEvent e) {
            // Main.NewText("进入");
            OnMouseOver?.Invoke(e, this);
            if (!BlockPropagation)
                Parent?.MouseOver(e);
        }

        public void MouseOut(UIMouseEvent e) {
            //Main.NewText("离开");
            OnMouseOut?.Invoke(e, this);
            if (!BlockPropagation)
                Parent?.MouseOut(e);
        }

        public void MouseDown(UIMouseEvent e) {
            //Main.NewText("按下");
            OnMouseDown?.Invoke(e, this);
            if (!BlockPropagation)
                Parent?.MouseDown(e);
        }

        public void MouseUp(UIMouseEvent e) {
            //Main.NewText("抬起");
            OnMouseUp?.Invoke(e, this);
            if (!BlockPropagation)
                Parent?.MouseUp(e);
        }
        public void MouseClick(UIMouseEvent e) {
            //Main.NewText("点击");
            OnClick?.Invoke(e, this);
            if (!BlockPropagation)
                Parent?.MouseClick(e);
        }

        public UIElement ElementAt(Vector2 pos) {
            UIElement target = null;
            foreach (var child in Children) {
                if (child.IsActive && child._selfHitbox.Contains(pos)) {
                    var tmp = child.ElementAt(pos);
                    if (tmp != null) {
                        target = tmp;
                        break;
                    }
                }
            }
            if (target != null) return target;
            if (_selfHitbox.Contains(pos) && !NoEvent) {
                return this;
            }
            return target;
        }



        public IHitBox ScreenHitBox {
            get {
                return _selfHitbox;
            }
        }

        public Vector2 PivotPosScreen {
            get {
                return _baseTopLeftScreen + new Vector2(Width * Pivot.X, Height * Pivot.Y);
            }
        }

        public void RecalculateChildren() {
            foreach (var element in Children) {
                element.Recalculate();
            }
        }

        public UIElement() {
            Name = "UI元素";
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
            NoEvent = false;
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
            return Rectangle.Intersect(sb.GraphicsDevice.ScissorRectangle, OuterRectangleScreen);
        }

        private Matrix ApplyTransform(Matrix prev) {
            Matrix curTransform = Matrix.CreateTranslation(new Vector3(_realPosition.X + Width * Pivot.X, _realPosition.Y + Height * Pivot.Y, 0)) * prev;
            curTransform = Matrix.CreateScale(Scale.X, Scale.Y, 1f) * curTransform;
            curTransform = Matrix.CreateRotationZ(Rotation) * curTransform;
            curTransform = Matrix.CreateTranslation(new Vector3(-Width * Pivot.X, -Height * Pivot.Y, 0f)) * curTransform;
            return curTransform;
        }
        public virtual void Draw(SpriteBatch sb) {
            Rectangle scissorRectangle = sb.GraphicsDevice.ScissorRectangle;
            if (IsVisible) {
                sb.End();
                sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp,
                    DepthStencilState.None, _selfRasterizerState, null, _selfTransform);
                DrawSelf(sb);
            }
            if (Overflow == OverflowType.Hidden) {
                sb.End();
                sb.GraphicsDevice.ScissorRectangle = Rectangle.Intersect(sb.GraphicsDevice.ScissorRectangle, GetClippingRectangle(sb));
                sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp,
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
                sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp,
                    DepthStencilState.None, defaultstate, null, _selfTransform);
            }
            if (DEBUG_MODE) {
                sb.End();
                sb.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.PointClamp,
                    DepthStencilState.None, _selfRasterizerState, null, Main.UIScaleMatrix);
                _selfHitbox.Draw(sb);
                Drawing.StrokeRect(sb, GetClippingRectangle(sb), 1, Color.Yellow);
            }
        }

        public virtual void DrawSelf(SpriteBatch sb) {
            if (UIElement.DEBUG_MODE) {
                sb.Draw(Main.magicPixel, new Rectangle(1, 1, Width - 2, Height - 2), Color.White * 0.4f);
            }
        }

        public virtual void UpdateSelf(GameTime gameTime) { }

        public void Update(GameTime gameTime) {
            UpdateSelf(gameTime);
            foreach (var child in Children) {
                if (child.IsActive) {
                    child.Update(gameTime);
                }
            }
        }

        public override string ToString() {
            return $"Type: {GetType().Name}, Name: {Name}";
        }
    }
}
