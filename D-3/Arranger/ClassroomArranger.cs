using D_3.Models.Business;
using D_3.Models.Entities;
using D_3.RoleManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace D_3.Core
{
    /// <summary>
    /// 排班管理类
    /// </summary>
    public class ClassroomArranger
    {
        /// <summary>
        /// 处理日期
        /// </summary>
        private DateTime _arrangeDate { get; set; }
        /// <summary>
        /// 排课情况
        /// </summary>
        private SortedList<int, CourseArrangement> _sortedCourseArrangement;
        /// <summary>
        /// 可用教室
        /// </summary>
        private List<Classroom> _classrooms;
        /// <summary>
        /// 当期已排课程情况
        /// </summary>
        private List<ClassroomArrangementEntity> _occupiedClassroomArrangement { get; set; }

        public ClassroomArranger(DateTime arrangeDate, SortedList<int, CourseArrangement> sortedCourseArrangement, List<ClassroomEntity> classroomEntities, List<ClassroomArrangementEntity> occupiedClassroomArrangement = null)
        {
            this._arrangeDate = arrangeDate;
            this._sortedCourseArrangement = sortedCourseArrangement;
            this._classrooms = AutoMapperConfig.Mapper.Map<List<Classroom>>(classroomEntities);
            _occupiedClassroomArrangement = occupiedClassroomArrangement;
        }

        public (List<ClassroomArrangement>, List<CourseArrangement>) Arrange()
        {
            if (_classrooms == null || _classrooms.Count == 0 || _sortedCourseArrangement == null || _sortedCourseArrangement.Count == 0)
            {
                return (null, null);
            }

            //初始化当期排课对教室的占用
            if (_occupiedClassroomArrangement != null && _occupiedClassroomArrangement.Count > 0)
            {
                foreach (var occupiedArrange in _occupiedClassroomArrangement)
                {
                    var classroom = _classrooms.Where(p => p.ClassroomId == occupiedArrange.ClassroomId).FirstOrDefault();
                    if (classroom == null)
                    {
                        continue;
                    }
                    classroom.OccupiedRanges.Add(
                        new Classroom.ClassroomDateRange(occupiedArrange.CourseStartTime, occupiedArrange.CourseEndTime)
                    );
                }
            }
          
            //开始分配教室
            List<CourseArrangement> courseTobeDone = new List<CourseArrangement>();//待定表

            foreach (var courseArrangementKv in _sortedCourseArrangement)
            {
                var courseArrangement = courseArrangementKv.Value;
                var classrooms = _classrooms.Where(p => p.SchoolId == courseArrangement.SchoolId && p.SpaceId == courseArrangement.SpaceId);

                bool isSuccess = false;
                foreach (var classroom in classrooms)
                {
                    isSuccess = classroom.AddCourseArrangement(courseArrangement);
                    if (isSuccess)
                    {
                        break;
                    }
                }
                if (!isSuccess)
                {
                    courseTobeDone.Add(courseArrangement);
                }
            }
            //形成教室排课记录表
            List<ClassroomArrangement> classroomArrangements = new List<ClassroomArrangement>();
            foreach (var classroom in _classrooms)
            {
                if (classroom.OccupiedCourseArrangement.Count == 0)
                {
                    continue;
                }
                foreach (var courseAggrangement in classroom.OccupiedCourseArrangement)
                {
                    classroomArrangements.Add(new ClassroomArrangement()
                    {
                        ADate = this._arrangeDate,
                        ClassroomId = classroom.ClassroomId,
                        CourseArrangingId = courseAggrangement.GId,
                        CourseStartTime = courseAggrangement.StartTime,
                        CourseEndTime = courseAggrangement.EndTime,
                        GId = Guid.NewGuid(),
                        SchoolId = courseAggrangement.SchoolId,
                        SpaceId = courseAggrangement.SpaceId,
                        CourseId = courseAggrangement.CourseId
                    });
                }
            }
            return (classroomArrangements, courseTobeDone);
        }

    }
}
