using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateMod2.UI.Events {
    public class UIActionEvent : UIEvent {
        public UIActionEvent(UIElement element, TimeSpan timestamp)
            : base(element, timestamp) {
        }
    }
}
