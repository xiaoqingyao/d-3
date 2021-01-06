using D_3.Models.Entities;
using D_3.RoleManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace D_3.Models.Business
{
    public class Classroom : ClassroomEntity
    {
        /// <summary>
        /// 已占用排课
        /// </summary>
        public List<CourseArrangement> OccupiedCourseArrangement { get; set; } = new List<CourseArrangement>();

        /// <summary>
        /// 已占用的时间段
        /// </summary>
        public List<ClassroomDateRange> OccupiedRanges { get; set; } = new List<ClassroomDateRange>();

        /// <summary>
        /// 教室的排序依据
        /// </summary>
        public EClassroomSortType SortType = EClassroomSortType.TeachTypeContain;

        public class ClassroomDateRange
        {
            public ClassroomDateRange(DateTime df, DateTime dt)
            {
                dtFrom = df;
                dtTo = dt;
            }
            public DateTime dtFrom { get; }
            public DateTime dtTo { get; }
        }
        /// <summary>
        /// 排课入教室验证
        /// </summary>
        /// <param name="courseArrangement"></param>
        /// <returns></returns>
        private bool isMatch(CourseArrangement courseArrangement)
        {


            //专属教室验证
            if (this.IsExclusive && this.ExclusiveTeacherId != courseArrangement.TeacherId)
            {
                return false;
            }
            if (OccupiedRanges.Count == 0)
            {
                return true;
            }
            //时间段重叠验证
            var startTime = courseArrangement.StartTime;
            var endTime = courseArrangement.EndTime;



            //var isMatch = OccupiedRanges.Where(p =>
            //                                    (endTime > p.dtFrom && startTime < p.dtTo)
            //                                    || (startTime <= p.dtFrom && endTime >= p.dtFrom)
            //                                    || (startTime >= p.dtFrom && endTime <= p.dtTo)
            //                                    || (startTime <= p.dtTo && endTime >= p.dtTo)
            //                                    ).Count() == 0;
            var isMatch = OccupiedRanges.Where(p => (startTime < p.dtTo && endTime > p.dtFrom)).Count() == 0;

            return isMatch;
        }
        /// <summary>
        /// 增加教室占用
        /// </summary>
        /// <param name="courseArrangement"></param>
        /// <returns></returns>
        public bool AddCourseArrangement(CourseArrangement courseArrangement)
        {
            var isMatch = this.isMatch(courseArrangement);
            if (!isMatch)
            {
                return false;
            }
            this.OccupiedCourseArrangement.Add(courseArrangement);
            this.OccupiedRanges.Add(new ClassroomDateRange(courseArrangement.StartTime, courseArrangement.EndTime));
            return true;
        }

        /// <summary>
        /// 当日已有该教师排课
        /// </summary>
        /// <returns></returns>
        public bool HasSiblingCourseThisDay(CourseArrangement courseArrangement)
        {
            if (this.OccupiedCourseArrangement == null || this.OccupiedCourseArrangement.Count == 0)
                return false;
            foreach (var ocCourse in OccupiedCourseArrangement)
            {
                if (ocCourse.TeacherId == courseArrangement.TeacherId)
                {
                    return true;
                }
            }
            return false;
        }
    }

}
