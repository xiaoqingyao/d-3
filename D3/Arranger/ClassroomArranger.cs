using D_3.Models.Business;
using D_3.Models.Entities;
using D_3.RoleManager;
using D3.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace D_3.Arranger
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
        private List<CourseArrangementEntity> _sortedCourseArrangement;
        /// <summary>
        /// 可用教室
        /// </summary>
        private List<Classroom> _classrooms;
        /// <summary>
        /// 当期已排课程情况
        /// </summary>
        private IEnumerable<ClassroomArrangementEntity> _occupiedClassroomArrangement { get; set; }

        public ClassroomArranger(DateTime arrangeDate, List<CourseArrangementEntity> sortedCourseArrangement, IEnumerable<ClassroomEntity> classroomEntities, IEnumerable<ClassroomArrangementEntity> occupiedClassroomArrangement = null)
        {
            this._arrangeDate = arrangeDate;
            this._sortedCourseArrangement = sortedCourseArrangement;
            this._classrooms = AutoMapperConfig.Mapper.Map<List<Classroom>>(classroomEntities);
            _occupiedClassroomArrangement = occupiedClassroomArrangement;
        }

        public List<ClassroomArrangementEntity> Arrange(out List<CourseArrangementQueueEntity> courseQueue, out List<LogSortedClassroomEntity> sortedClassroomEntities)
        {
            //形成教室排课记录表
            List<ClassroomArrangementEntity> classroomArrangements = new List<ClassroomArrangementEntity>();
            courseQueue = new List<CourseArrangementQueueEntity>();//待定表
            sortedClassroomEntities = new List<LogSortedClassroomEntity>();
            if (_classrooms == null || _classrooms.Count == 0 || _sortedCourseArrangement == null || _sortedCourseArrangement.Count == 0)
            {
                return classroomArrangements;
            }

            //初始化当期排课对教室的占用
            if (_occupiedClassroomArrangement != null && _occupiedClassroomArrangement.Count() > 0)
            {
                foreach (var occupiedArrange in _occupiedClassroomArrangement)
                {
                    var classroom = _classrooms.Where(p => p.roomId == occupiedArrange.roomId).FirstOrDefault();
                    if (classroom == null)
                    {
                        continue;
                    }
                    classroom.OccupiedRanges.Add(
                        new Classroom.ClassroomDateRange(occupiedArrange.dtLessonBeginReal, occupiedArrange.dtLessonEndReal)
                    );
                }
            }

            //开始分配教室
            foreach (var courseArrangement in _sortedCourseArrangement)
            {
                //根据课程排序教室
                var sortedClassrooms = sortClassRooms(courseArrangement);
                bool isSuccess = false;
                int sortIndex = 0;
                foreach (var classroom in sortedClassrooms)
                {
                    sortIndex++;
                    //记录教室排序和匹配情况
                    var logSortedClassroomEntity = AutoMapperConfig.Mapper.Map<LogSortedClassroomEntity>(classroom);
                    string message = string.Empty;
                    if (!isSuccess)
                    {
                        //尝试排课入该教室
                        isSuccess = classroom.AddCourseArrangement(courseArrangement, out message);
                    }
                    logSortedClassroomEntity.sortIndex = sortIndex;
                    logSortedClassroomEntity.isSuccess = isSuccess;
                    logSortedClassroomEntity.courseArrangingId = courseArrangement.courseArrangingId;
                    logSortedClassroomEntity.message = message;
                    sortedClassroomEntities.Add(logSortedClassroomEntity);
                }
                if (!isSuccess)
                {
                    var courseArrangementQueue = AutoMapperConfig.Mapper.Map<CourseArrangementQueueEntity>(courseArrangement);
                    courseQueue.Add(courseArrangementQueue);
                }
            }

            //生成排班结果
            foreach (var classroom in _classrooms)
            {
                if (classroom.OccupiedCourseArrangement.Count == 0)
                {
                    continue;
                }
                foreach (var courseAggrangement in classroom.OccupiedCourseArrangement)
                {
                    classroomArrangements.Add(new ClassroomArrangementEntity()
                    {
                        dtPKDateTime = this._arrangeDate,
                        roomId = classroom.roomId,
                        courseArrangingId = courseAggrangement.courseArrangingId,
                        dtLessonBeginReal = courseAggrangement.dtLessonBeginReal,
                        dtLessonEndReal = courseAggrangement.dtLessonEndReal,
                        campusCode = courseAggrangement.onClassCampusCode,
                        venueId = courseAggrangement.onClassVenueId,
                        courseId = courseAggrangement.courseArrangingId,
                        dtDateRealYear = courseAggrangement.dtLessonBeginReal.Year,
                        dtDateRealMonth = courseAggrangement.dtLessonBeginReal.Month,
                        dtDateRealDay = courseAggrangement.dtLessonBeginReal.Day,
                        roomNum = classroom.roomCode
                    });
                }
            }
            return classroomArrangements;
        }

        /// <summary>
        /// 根据排课数据确定教室优先级
        /// </summary>
        /// <param name="courseArrangement"></param>
        /// <returns></returns>
        public List<Classroom> sortClassRooms(CourseArrangementEntity courseArrangement)
        {
            if (_classrooms == null || _classrooms.Count == 0)
            {
                return null;
            }
            var classrooms = _classrooms.Where(p => p.campusCode == courseArrangement.onClassCampusCode && p.venueId == courseArrangement.onClassVenueId && p.TeachRange.Contains(courseArrangement.nTutorType));
            foreach (var classroom in classrooms)
            {
                if (classroom.IsExclusive)
                {
                    classroom.SortType = Models.EClassroomSortType.Exclusive;
                }
                else if (classroom.TeachRange.Count() == 1)
                {
                    classroom.SortType = Models.EClassroomSortType.TeachTypeEqual;
                }
                else if (classroom.HasSiblingCourseThisDay(courseArrangement))
                {
                    classroom.SortType = Models.EClassroomSortType.HasSiblingsCourse;
                }
                else if (classroom.Priority > 0)
                {
                    classroom.SortType = Models.EClassroomSortType.IsTop;
                }
                else
                {
                    classroom.SortType = Models.EClassroomSortType.TeachTypeContain;
                }
            }
            return classrooms.OrderBy(p => p.SortType).ToList();
        }
    }
}
