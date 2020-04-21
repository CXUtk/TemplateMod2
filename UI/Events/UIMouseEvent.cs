using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateMod2.UI.Events {
    /// <summary>
    /// 储存鼠标事件
    /// </summary>
    public class UIMouseEvent : UIEvent {
        public Vector2 MouseScreen { get; }
        public UIMouseEvent(UIElement element, TimeSpan timestamp, Vector2 mouseScreen)
            : base(element, timestamp) {
            MouseScreen = mouseScreen;
        }
    }
}
