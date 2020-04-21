using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemplateMod2.UI {
    /// <summary>
    /// 决定该UI元素的部分处于父节点容器之外的行为
    /// </summary>
    public enum OverflowType {
        /// <summary>
        /// 直接溢出
        /// </summary>
        Overflow,
        /// <summary>
        /// 隐藏溢出部分
        /// </summary>
        Hidden,
    }
}
