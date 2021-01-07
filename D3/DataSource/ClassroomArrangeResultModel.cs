using D_3.Models.Business;
using D_3.Models.Entities;
using D_3.RoleManager;
using D3.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D3.DataSource
{
    public class ClassroomArrangeResultModel
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sortedCourseArrangement">课程排序结果</param>
        /// <param name="classroomArrangements">排班结果</param>
        /// <param name="courseArrangementNeedToDo">待定表</param>
        /// <param name="logSortedClassroomEntities">班级排班记录</param>
        public ClassroomArrangeResultModel(SortedList<int, CourseArrangement> sortedCourseArrangement, List<ClassroomArrangementEntity> classroomArrangements, List<CourseArrangementQueueEntity> courseArrangementQueue, List<LogSortedClassroomEntity> logSortedClassroomEntities)
        {
            this.CourseArrangementQueue = courseArrangementQueue;
            this.ClassroomArrangements = classroomArrangements;
            this.LogSortedClassroomEntities = logSortedClassroomEntities;
            if (sortedCourseArrangement == null || sortedCourseArrangement.Count == 0)
            {
                throw new Exception("课程排序不能为空");
            }
            foreach (var item in sortedCourseArrangement)
            {
                var sortCourseLog = AutoMapperConfig.Mapper.Map<LogSortedCourseArrangementEntity>(item.Value);
                sortCourseLog.sortIndex = item.Key;
                LogSortedCourseArrangement.Add(sortCourseLog);
            }
        }
        /// <summary>
        /// 排班结果（教室监控）
        /// </summary>
        public List<ClassroomArrangementEntity> ClassroomArrangements { get; set; }
        /// <summary>
        /// 待定表
        /// </summary>
        public List<CourseArrangementQueueEntity> CourseArrangementQueue { get; set; }
        /// <summary>
        /// 课程排序日志
        /// </summary>        
        public List<LogSortedCourseArrangementEntity> LogSortedCourseArrangement { get; set; } = new List<LogSortedCourseArrangementEntity>();
        //教室排序日志
        public List<LogSortedClassroomEntity> LogSortedClassroomEntities { get; set; }
    }
}
