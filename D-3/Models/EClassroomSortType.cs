using System;
using System.Collections.Generic;
using System.Text;

namespace D_3.Models
{
    /// <summary>
    /// 教室排序类型
    /// </summary>
    public enum EClassroomSortType
    {
        /// <summary>
        /// 专属教室
        /// </summary>
        Exclusive = 0x01,
        /// <summary>
        /// 与课程教学属性一对一匹配
        /// </summary>
        TeachTypeEqual = Exclusive << 1,
        /// <summary>
        /// 包含兄弟排课：当日该老师在该教室有过上课记录
        /// </summary>
        HasSiblingsCourse = TeachTypeEqual << 1,
        /// <summary>
        /// 教室优先
        /// </summary>
        IsTop = HasSiblingsCourse << 1,
        /// <summary>
        /// 包含课程教学属性
        /// </summary>
        TeachTypeContain = IsTop << 1,
    }
}
