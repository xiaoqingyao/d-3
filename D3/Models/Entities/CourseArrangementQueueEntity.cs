using D_3.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D3.Models.Entities
{
    /// <summary>
    /// 排课待定表
    /// </summary>
    public class CourseArrangementQueueEntity : CourseArrangementEntity
    {
        /// <summary>
        /// 待定表顺序
        /// </summary>
        public int queueIndex { get; set; }
    }
}
