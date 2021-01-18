using System;
using System.Collections.Generic;
using System.Text;

namespace D_3.Models.Entities
{
    /// <summary>
    /// 排班结果
    /// </summary>
    public class ClassroomArrangementEntity
    {
        /// <summary>
        /// 记录id/主键
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 排课id/课程id(对应学员课时核录id)
        /// </summary>
        public string sClasscode { get; set; }
 
        /// <summary>
        /// 学校
        /// </summary>
        public string campusCode { get; set; }
        /// <summary>
        /// 教学点
        /// </summary>
        public string venueId { get; set; }
        /// <summary>
        /// 教室id
        /// </summary>
        public int roomId { get; set; }
        /// <summary>
        /// 教室编号
        /// </summary>
        public string roomNum { get; set; }        
        /// <summary>
        /// 上课日期
        /// </summary>
        public DateTime dtPKDateTime { get; set; }
        /// <summary>
        /// 课程开始时间
        /// </summary>
        public DateTime dtLessonBeginReal { get; set; }
        /// <summary>
        /// 课程结束时间
        /// </summary>
        public DateTime dtLessonEndReal { get; set; }
        /// <summary>
        /// 课程id
        /// </summary>
        //public int courseId { get; set; }
        public bool isDelete { get; set; }
        public string deleteReason { get; set; }
        /// <summary>
        /// 上课年
        /// </summary>
        public int dtDateRealYear { get; set; }
        /// <summary>
        /// 上课月
        /// </summary>
        public int dtDateRealMonth { get; set; }
        /// <summary>
        /// 上课日
        /// </summary>
        public int dtDateRealDay { get; set; }
    }
}
