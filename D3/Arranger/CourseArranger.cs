using D_3.Models.Business;
using D_3.Models.Entities;
using D_3.RoleManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace D_3.Arranger
{
    /// <summary>
    /// 排课管理类
    /// </summary>
    public class CourseArranger
    {

        /// <summary>
        /// 排课记录
        /// </summary>
        private List<CourseArrangement> _courseArrangement { get; set; } = new List<CourseArrangement>();

        /// <summary>
        /// 排序优先级后的排课记录
        /// </summary>
        private SortedList<int, CourseArrangement> _sortedCourseArrangement { get; set; } = new SortedList<int, CourseArrangement>();


        public CourseArranger()
        { }
        public SortedList<int, CourseArrangement> Arrange(IEnumerable<CourseArrangementEntity> courseArrangementEntity)
        {
            //判定排课的性质
            var arrangements = initData(courseArrangementEntity);
            //合并连续课
            MergeSerialCourseArrangements(arrangements);
            //根据权限排序
            SortCourseArrangements();
            return _sortedCourseArrangement;
        }
        /// <summary>
        /// 完成课程类型判定
        /// </summary>
        private List<CourseArrangement> initData(IEnumerable<CourseArrangementEntity> courseArrangementEntities)
        {
            if (courseArrangementEntities == null || courseArrangementEntities.Count() == 0)
            {
                return null;
            }
            var courseArrangements = new List<CourseArrangement>();

            foreach (var entity in courseArrangementEntities)
            {
                //是否是标段
                var isStandard = RoleReferee.IsStandard(entity.dtLessonBeginReal, entity.dtLessonEndReal);

                //是否是连续
                bool isSerial = false;
                var compareList = courseArrangementEntities.Where(p => p.sTeacherCode == entity.sTeacherCode && p.onClassCampusCode == entity.onClassCampusCode && p.onClassVenueId == entity.onClassVenueId);
                if (isStandard)
                {
                    foreach (var compare in compareList)
                    {
                        var arrSerial = RoleReferee.IsSerial(entity.dtLessonBeginReal, entity.dtLessonEndReal, compare.dtLessonBeginReal, compare.dtLessonEndReal);
                        isSerial = arrSerial[0];
                        if (isSerial)
                        {
                            break;
                        }
                    }
                }
                var courseArrangement = buildCourseArrangement(entity, isSerial, isStandard);
                courseArrangements.Add(courseArrangement);
            }
            return courseArrangements;
        }

        /// <summary>
        /// 合并连续排课
        /// </summary>
        /// <param name="arrangements"></param>
        private void MergeSerialCourseArrangements(List<CourseArrangement> arrangements)
        {
            if (arrangements == null || arrangements.Count == 0)
            {
                return;
            }
            var notSerialArrangements = arrangements.Where(p => p.CourseArrangementType == Models.ECourseArrangementType.Normal || p.CourseArrangementType == Models.ECourseArrangementType.Standard);
            _courseArrangement = _courseArrangement.Concat(notSerialArrangements.ToList()).ToList();

            var serialArrangements = arrangements.Where(p => p.CourseArrangementType == Models.ECourseArrangementType.SerialStandard).OrderBy(p => p.dtLessonBeginReal);
            //正序找每个课程的下一节课
            foreach (var arrangement in serialArrangements)
            {
                if (mergedCourseArrangementSerialIds.Contains(arrangement.courseArrangingId))
                {
                    continue;
                }
                CourseArrangementSerial serialarrangement = (CourseArrangementSerial)arrangement;
                var compareList = serialArrangements.Where(p => p.sTeacherCode == arrangement.sTeacherCode && p.onClassCampusCode == arrangement.onClassCampusCode && p.onClassVenueId == arrangement.onClassVenueId && !mergedCourseArrangementSerialIds.Contains(p.courseArrangingId));
                getNextSerialCourse(serialarrangement, serialarrangement, compareList);
                _courseArrangement.Add(serialarrangement);
            }
        }

        /// <summary>
        /// 排序
        /// </summary>
        private void SortCourseArrangements()
        {
            int keyIndex = 0;
            var courseList = _courseArrangement.OrderByDescending(p => p.teachType).ThenByDescending(p => p.CourseArrangementType).ThenByDescending(p => p.SerialLevel).ThenBy
                (p => p.EarliestMergeDate).ThenBy(p => p.dtPKDateTime).ToList();
            foreach (var arrangement in courseList)
            {
                if (arrangement is CourseArrangementSerial)
                {
                    var arrangementSerial = arrangement as CourseArrangementSerial;
                    for (int i = 0; i < arrangementSerial.SerialLevel; i++)
                    {
                        _sortedCourseArrangement.Add(keyIndex, arrangementSerial);
                        if (arrangementSerial.Next != null)
                        {
                            arrangementSerial = arrangementSerial.Next;
                        }
                    }
                }
                else
                {
                    _sortedCourseArrangement.Add(keyIndex, arrangement);
                }
                keyIndex++;
            }
        }

        List<int> mergedCourseArrangementSerialIds = new List<int>();//记录已经处理过的连课id
        /// <summary>
        /// 获取下一个连课
        /// </summary>
        /// <param name="root">连课中最早的课</param>
        /// <param name="arrangement">当前判定课程</param>
        /// <param name="compareList">课程对照列表</param>
        private void getNextSerialCourse(CourseArrangementSerial root, CourseArrangementSerial arrangement, IEnumerable<CourseArrangement> compareList)
        {
            foreach (var compare in compareList)
            {
                var arrSerial = RoleReferee.IsSerial(arrangement.dtLessonBeginReal, arrangement.dtLessonEndReal, compare.dtLessonBeginReal, compare.dtLessonEndReal);
                var isSerial = arrSerial[0];
                var isCurrentFront = arrSerial[1];
                if (isSerial)
                {
                    root.EarliestMergeDate = arrangement.EarliestMergeDate < compare.EarliestMergeDate ? arrangement.EarliestMergeDate : compare.EarliestMergeDate;
                }
                if (isSerial && isCurrentFront)
                {
                    var nextCourseArrangement = (CourseArrangementSerial)compare;
                    mergedCourseArrangementSerialIds.Add(nextCourseArrangement.courseArrangingId);
                    arrangement.Next = nextCourseArrangement;
                    getNextSerialCourse(root, nextCourseArrangement, compareList);
                    root.SerialLevel++;
                }
            }
        }

        /// <summary>
        /// build排课业务对象
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="isSerial"></param>
        /// <param name="isStandard"></param>
        /// <returns></returns>
        private CourseArrangement buildCourseArrangement(CourseArrangementEntity entity, bool isSerial, bool isStandard)
        {
            CourseArrangement arrangement = AutoMapperConfig.Mapper.Map<CourseArrangement>(entity);

            if (isSerial && isStandard)
            {
                arrangement = AutoMapperConfig.Mapper.Map<CourseArrangementSerial>(entity);
                arrangement.CourseArrangementType = Models.ECourseArrangementType.SerialStandard;
            }
            else if (isStandard)
            {
                arrangement.CourseArrangementType = Models.ECourseArrangementType.Standard;
            }
            else
            {
                arrangement.CourseArrangementType = Models.ECourseArrangementType.Normal;
            }
            return arrangement;
        }
    }
}
