using System;
using System.Collections.Generic;
using System.Text;

namespace D_3.Models.Entities
{
    /// <summary>
    /// 排课结果（学员课时核录）
    /// </summary>
    public class CourseArrangementEntity
    {
        /// <summary>
        /// 排课课程记录唯一
        /// </summary>
        public int courseArrangingId { get; set; }
        /// <summary>
        /// 学校
        /// </summary>
        public string onClassCampusCode { get; set; }
        /// <summary>
        /// 教学点
        /// </summary>
        public string onClassVenueId { get; set; }
        /// <summary>
        /// 教师id sTeacherCode
        /// </summary>
        public string sTeacherCode { get; set; }
        /// <summary>
        /// 开始时间
        /// </summary>
        public DateTime dtLessonBeginReal { get; set; }
        /// <summary>
        /// 结束时间
        /// </summary>
        public DateTime dtLessonEndReal { get; set; }
        /// <summary>
        /// 授课类型
        /// </summary>
        public ETeachType teachType { get; set; }
        /// <summary>
        /// 排课时间
        /// </summary>
        public DateTime dtPKDateTime { get; set; }

    }
}
