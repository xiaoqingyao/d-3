using D_3.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace D_3.Models.Business
{
    /// <summary>
    /// 排课业务类
    /// </summary>
    public class CourseArrangement : CourseArrangementEntity
    {
        public CourseArrangement()
        {
            SerialLevel = 1;
            EarliestMergeDate = OperateDate;
        }
        /// <summary>
        /// 排课类型 连标 非连标
        /// </summary>
        public ECourseArrangementType CourseArrangementType { get; set; }
        /// <summary>
        /// 连续数  N连 N连标
        /// </summary>
        public int SerialLevel { get; set; } = 1;
        /// <summary>
        /// 形成连课最早时间（最后一节课的排课时间）
        /// </summary>
        public DateTime EarliestMergeDate { get; set; }


        public bool IsSiblings(CourseArrangement targetCourseArrangement)
        {
            if (this == targetCourseArrangement)
            {
                return true;
            }
            if (targetCourseArrangement == null)
            {
                return false;
            }
            //todo
            return true;
        }

    }
}
