using System;
using System.Collections.Generic;
using System.Text;

namespace D_3.Models.Entities
{
    /// <summary>
    /// 排班结果
    /// </summary>
    public class ClassroomArrangementEntity : BaseEntity
    {
        /// <summary>
        /// 学校
        /// </summary>
        public string SchoolId { get; set; }
        /// <summary>
        /// 教学点
        /// </summary>
        public string SpaceId { get; set; }
        /// <summary>
        /// 教室
        /// </summary>
        public string ClassroomId { get; set; }
        /// <summary>
        /// 排课
        /// </summary>
        public Guid CourseArrangingId { get; set; }
        /// <summary>
        /// 上课日期
        /// </summary>
        public DateTime ADate { get; set; }
        /// <summary>
        /// 课程开始时间
        /// </summary>
        public DateTime CourseStartTime { get; set; }
        /// <summary>
        /// 课程结束时间
        /// </summary>
        public DateTime CourseEndTime { get; set; }

        //冗余数据

        public string CourseId { get; set; }


    }
}
