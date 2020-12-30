using System;
using System.Collections.Generic;
using System.Text;

namespace D_3.Models.Entities
{
    /// <summary>
    /// 排课结果
    /// </summary>
    public class CourseArrangementEntity : BaseEntity
    {
        /// <summary>
        /// 课程id
        /// </summary>
        public string CourseId { get; set; }
        /// <summary>
        /// 学校
        /// </summary>
        public string SchoolId { get; set; }
        /// <summary>
        /// 教学点
        /// </summary>
        public string SpaceId { get; set; }
        /// <summary>
        /// 教师id
        /// </summary>
        public string TeacherId { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime StartTime { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime EndTime { get; set; }
        /// <summary>
        /// 上课类型
        /// </summary>
        public ETeachType TeachType { get; set; }

        /// <summary>
        /// 排课时间
        /// </summary>
        public DateTime OperateDate { get; set; }

    }
}
