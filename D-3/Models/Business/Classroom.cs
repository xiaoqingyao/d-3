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
        private bool isMatch(CourseArrangement courseArrangement)
        {
            if (OccupiedRanges.Count == 0)
            {
                return true;
            }
            var startTime = courseArrangement.StartTime;
            var endTime = courseArrangement.EndTime;

            //TimeSpan tsStart = new TimeSpan(startTime.Ticks);
            //TimeSpan tsEnd = new TimeSpan(endTime.Ticks);
            //TimeSpan ts = tsEnd.Subtract(tsStart).Duration();

            var isMatch = OccupiedRanges.Where(p => (startTime >= p.dtFrom && endTime <= p.dtTo)
                                                || (startTime >= p.dtFrom && endTime <= p.dtTo)
                                                || (startTime >= p.dtFrom && endTime >= p.dtTo)
                                                ).Count() == 0;

            return isMatch;
        }
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
    }

}
