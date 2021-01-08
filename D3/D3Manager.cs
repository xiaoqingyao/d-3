using D_3.Arranger;
using D_3.DataSource;
using D_3.Models.Business;
using D3.DataSource;
using D3.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D3
{
    public static class D3Manager
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        public static bool D3(int year, int month, int day)
        {
            try
            {
                //基础数据初始化
                var strArrangeDate = $"{year}-{month}-{day}";
                var dataManager = new DataManager();
                var classrooms = dataManager.GetClassrooms();
                var courses = dataManager.GetCourseArrangement(year, month, day);
                var occupiedClassroomArrangement = dataManager.GetClassroomArrangement(year, month, day);
                var classroomArrangeData = new ClassroomArrangeParameterModel(DateTime.Parse(strArrangeDate), courses, classrooms, occupiedClassroomArrangement);

                //课程排序
                var sortedCourseArrangement = new CourseArranger().Arrange(classroomArrangeData.CourseArrangementsEntities);
                //排班
                List<CourseArrangementQueueEntity> courseArrangementQueue = null;//待定排课记录
                List<LogSortedClassroomEntity> logSortedClassroomEntities = null;//教室排序日志
                var classroomArrangeResult = new ClassroomArranger(classroomArrangeData.ArrangeDate, sortedCourseArrangement, classroomArrangeData.ClassroomsEntities, classroomArrangeData.OccupiedClassroomArrangementEntities).Arrange(out courseArrangementQueue, out logSortedClassroomEntities);

                //结果入库
                var d3arrangeRel = new ClassroomArrangeResultModel(sortedCourseArrangement, classroomArrangeResult, courseArrangementQueue, logSortedClassroomEntities);
                dataManager.D3ToDb(d3arrangeRel);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
            return true;
        }
    }
}
