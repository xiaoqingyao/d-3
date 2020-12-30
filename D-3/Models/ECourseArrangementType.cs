using System;
using System.Collections.Generic;
using System.Text;

namespace D_3.Models
{
    /// <summary>
    /// 排课类型
    /// </summary>
    public enum ECourseArrangementType
    {
        /// <summary>
        /// 普通排课
        /// </summary>
        Normal = 0x01,
        /// <summary>
        /// 连续的排课 2
        /// </summary>
        Serial = Normal << 1,
        /// <summary>
        /// 标准时段  4
        /// </summary>
        Standard = Serial << 1,
        /// <summary>
        /// 连标   8
        /// </summary>
        SerialStandard = Serial | Standard
    }
}
