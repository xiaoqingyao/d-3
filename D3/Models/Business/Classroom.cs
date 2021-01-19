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
        public List<CourseArrangementEntity> OccupiedCourseArrangement { get; set; } = new List<CourseArrangementEntity>();

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
        private bool isMatch(CourseArrangementEntity courseArrangement, out string message)
        {

            message = "成功";
            //专属教室验证
            if (this.IsExclusive && this.exclusiveTeacherCode != courseArrangement.steacherCode)
            {
                message = "专属教室不匹配";
                return false;
            }
            if (OccupiedRanges.Count == 0)
            {
                return true;
            }
            //时间段重叠验证
            var startTime = courseArrangement.dtLessonBeginReal;
            var endTime = courseArrangement.dtLessonEndReal;



            //var isMatch = OccupiedRanges.Where(p =>
            //                                    (endTime > p.dtFrom && startTime < p.dtTo)
            //                                    || (startTime <= p.dtFrom && endTime >= p.dtFrom)
            //                                    || (startTime >= p.dtFrom && endTime <= p.dtTo)
            //                                    || (startTime <= p.dtTo && endTime >= p.dtTo)
            //                                    ).Count() == 0;
            var isMatch = OccupiedRanges.Where(p => (startTime < p.dtTo && endTime > p.dtFrom)).Count() == 0;
            if (!isMatch)
            {
                message = "上课时间段冲突";
            }
            return isMatch;
        }
        /// <summary>
        /// 增加教室占用
        /// </summary>
        /// <param name="courseArrangement"></param>
        /// <returns></returns>
        public bool AddCourseArrangement(CourseArrangementEntity courseArrangement, out string message)
        {
            //if (this.roomId==13)
            //{

            //}
            message = string.Empty;
            var isMatch = this.isMatch(courseArrangement, out message);
            if (!isMatch)
            {
                return false;
            }
            //else { 
                
            //}
            this.OccupiedCourseArrangement.Add(courseArrangement);
            this.OccupiedRanges.Add(new ClassroomDateRange(courseArrangement.dtLessonBeginReal, courseArrangement.dtLessonEndReal));
            return true;
        }

        /// <summary>
        /// 当日已有该教师排课
        /// </summary>
        /// <returns></returns>
        public bool HasSiblingCourseThisDay(CourseArrangementEntity courseArrangement)
        {
            if (this.OccupiedCourseArrangement == null || this.OccupiedCourseArrangement.Count == 0)
                return false;
            foreach (var ocCourse in OccupiedCourseArrangement)
            {
                if (ocCourse.steacherCode == courseArrangement.steacherCode)
                {
                    return true;
                }
            }
            return false;
        }
    }

}
