using D_3.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D3.Models.Entities
{
    /// <summary>
    /// 过程处理记录：D-3计算过程中，针对排课教室的排序情况
    /// </summary>
    public class LogSortedClassroomEntity: ClassroomEntity
    {
        public int logId { get; set; }

        public string sClasscode { get; set; }
        public DateTime dtLessonBeginReal { get; set; }
        /// <summary>
        /// 是否加入成功
        /// </summary>
        public bool isSuccess { get; set; }
        /// <summary>
        /// 原因
        /// </summary>
        public string message { get; set; }
        /// <summary>
        /// 序号
        /// </summary>
        public int sortIndex { get; set; }
    }
}
