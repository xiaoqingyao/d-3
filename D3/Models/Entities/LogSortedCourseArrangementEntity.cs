using D_3.Models.Business;
using D_3.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D3.Models.Entities
{
    /// <summary>
    /// 过程处理记录：D-3规则计算过程中，对课程先后的排序结果
    /// </summary>
    public class LogSortedCourseArrangementEntity : CourseArrangement
    {
        /// <summary>
        /// 唯一id
        /// </summary>
        public int logId { get; set; }
        public int sortIndex { get; set; }
    }
}
