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
        public bool isDelete { get; set; }
        public string deleteReason { get; set; }
    }
}
