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
    public class UIStateMachine {
        public int ActiveStateNumber => uiRunningStack.Count;
        private List<UIState> uiRunningStack = new List<UIState>();
        private UIElement _previousHoverElement;

        public UIStateMachine() {

        }

        public void Add<T>(T state) where T : UIState {
            uiRunningStack.Add(state);
        }

        public void Remove<T>(T state) where T : UIState {
            uiRunningStack.Remove(state);
        }

        public void Update(GameTime gameTime) {
            // 响应鼠标事件的时候一定是从后往前，前端的窗口一定是第一个响应鼠标事件的
            int sz = uiRunningStack.Count;
            UIElement hoverElement = null;
            for (int i = sz - 1; i >= 0; i--) {
                var state = uiRunningStack[i];
                if (state.IsActive) {
                    var element = state.ElementAt(Main.MouseScreen);
                    if (element != state) {
                        hoverElement = element;
                        break;
                    }
                }
            }
            bool mouseLeftDown = Main.mouseLeft && Main.hasFocus;
            if (hoverElement != null)
                hoverElement.MouseOver(new UIMouseEvent(hoverElement, gameTime.TotalGameTime, Main.MouseScreen));
            if (_previousHoverElement != null && hoverElement != _previousHoverElement)
                _previousHoverElement.MouseOut(new UIMouseEvent(_previousHoverElement, gameTime.TotalGameTime, Main.MouseScreen));
            //if (mouseLeftDown && hoverElement != null)
            //    hoverElement.MouseDown(new UIMouseEvent(hoverElement, gameTime.TotalGameTime, Main.MouseScreen));
            //if (Main.mouseLeftRelease && hoverElement != null)
            //    hoverElement.MouseUp(new UIMouseEvent(hoverElement, gameTime.TotalGameTime, Main.MouseScreen));
            _previousHoverElement = hoverElement;
            foreach (var state in uiRunningStack) {
                if (state.IsActive) {
                    state.Update(gameTime, Main.UIScaleMatrix);
                    state.Recalculate();
                }
            }
        }

        public void Draw(SpriteBatch sb) {


            // 绘制一定要从前往后，维持父子关系
            foreach (var state in uiRunningStack) {
                if (state.IsActive) {
                    state.Draw(sb);
                }
            }
        }

        //public void RegisterState<T>() where T : UIState {
        //    var name = typeof(T).FullName;
        //    if (stateDict.ContainsKey(name)) throw new ArgumentException("这个状态已经注册过了");
        //    var state = (T)Activator.CreateInstance(typeof(T), new[] { this });
        //    uiStates.Add(state);
        //    stateDict.Add(name, uiStates.Count);
        //}
    }
}
