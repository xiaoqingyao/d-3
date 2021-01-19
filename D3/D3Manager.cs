using D_3.Arranger;
using D_3.DataSource;
using D_3.Models.Business;
using D_3.Models.Entities;
using D_3.RoleManager;
using D3.DataSource;
using D3.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VIP.SystemService.DB;

namespace D3
{
    public static class D3Manager
    {
        /// <summary>
        /// 每日计算D-3规则，预排教室
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        public async static Task<bool> D3(int year, int month, int day)
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
                var classroomArrangeResult = new ClassroomArranger(classroomArrangeData.ArrangeDate, sortedCourseArrangement, classroomArrangeData.ClassroomsEntities, classroomArrangeData.OccupiedClassroomArrangementEntities)
                    .Arrange(out courseArrangementQueue, out logSortedClassroomEntities);
                //入库
                var d3arrangeRel = new ClassroomArrangeResultModel(sortedCourseArrangement, classroomArrangeResult, courseArrangementQueue, logSortedClassroomEntities);
                var executeCount = await dataManager.D3ToDb(d3arrangeRel);
                return executeCount > 0;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        /// <summary>
        /// D-3内计算
        /// </summary>
        /// <param name="campusCode">校区</param>
        /// <param name="venueId">教学点</param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        public async static Task<bool> WithinD3(string campusCode, string venueId, int year, int month, int day)
        {
            //已排教室表
            var occupiedArrangement = new DataManager().GetClassroomArrangement(year, month, day, campusCode, venueId);
            if (occupiedArrangement==null|| occupiedArrangement.Count()==0)
            {
                //未执行过d-3，不执行d-3内
                //todo 写日志记录提醒，没有执行d-3就触发了d-3内
                return false;
            }
            //当期待定表
            //数据获取方式：未入教室的课时核录数据，都认为是待定表数据。
            var expectids = occupiedArrangement.Select(p => p.sClasscode);
            var courseArrangement = new DataManager().GetCourseArrangement(year, month, day, campusCode, venueId, expectids.ToArray());
            //待定表按照排课时间排序
            var sortedCourseArrangement = courseArrangement.OrderBy(p => p.dtPKDateTime).ToList();
            //获取所有教室
            var classrooms = new DataManager().GetClassrooms(campusCode, venueId);
            //排班待定表
            List<CourseArrangementQueueEntity> courseQueue = null;//新待定
            List<LogSortedClassroomEntity> sortedClassroomEntitie = null;//教室排列日志
            var classroomArrangement = new ClassroomArranger(DateTime.Parse($"{year}-{month}-{day}"), sortedCourseArrangement, classrooms, occupiedArrangement).Arrange(out courseQueue, out sortedClassroomEntitie);

            //更新数据库
            var executeCount = await new DataManager().D3ToDb(new ClassroomArrangeResultModel(sortedCourseArrangement, classroomArrangement, courseQueue, sortedClassroomEntitie));
            return executeCount > 0;
        }
        /// <summary>
        /// 释放已排班的排课
        /// </summary>
        /// <param name="roomId">教室id</param>
        /// <param name="courseArrangementId">排课id</param>
        /// <param name="deleteReson">原因</param>
        public async static Task<bool> FreeClassroomArrangement(int? roomId, int? courseArrangementId, string deleteReson)
        {
            //todo添加释放逻辑判断，针对1-1之外的，如果还存在课，就不释放教室
            //释放并返回释放的排班顺序
            var classroomArrangement = new DataManager().FreeClassroomArrangement(roomId, courseArrangementId, deleteReson);

            //数据合并分组，调用d-3内逻辑
            var groupClassroomArrangement = from s in classroomArrangement
                                            group s by new { s.campusCode, s.venueId, s.dtDateRealYear, s.dtDateRealMonth, s.dtDateRealDay } into g
                                            select new { g.Key.campusCode, g.Key.venueId, g.Key.dtDateRealYear, g.Key.dtDateRealMonth, g.Key.dtDateRealDay };
            if (groupClassroomArrangement != null && groupClassroomArrangement.Count() > 0)
            {
                foreach (var whind3Item in groupClassroomArrangement)
                {
                    await WithinD3(whind3Item.campusCode, whind3Item.venueId, whind3Item.dtDateRealYear, whind3Item.dtDateRealMonth, whind3Item.dtDateRealDay);
                }
            }
            return classroomArrangement.Count() > 0;
        }
    }
}
