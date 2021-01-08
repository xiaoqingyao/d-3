using D_3.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using VIP.SystemService.DB;
using Dapper;
using D3.DataSource;
using System.Data.SqlClient;

namespace D_3.DataSource
{
    /// <summary>
    /// 数据管理
    /// </summary>
    public class DataManager
    {
        /// <summary>
        /// 获取教室
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ClassroomEntity> GetClassrooms()
        {
            using (var conn = Conn.getConn(Conn.getConnStr()))
            {
                //获取规则：教学点启用自动排教室的规则；教室可用
                string sql = @"select * from Zeus_Classroom cr inner join Zeus_TeachingVenue venue on cr.venueId=venue.id where venue.isOnRule=1 and cr.canArrange=1 and cr.isDeleted=0  ";
                return conn.Query<ClassroomEntity>(sql, commandTimeout: 10);
            }
        }
        /// <summary>
        /// 获取排课信息（学员课时核录）
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <returns></returns>
        public IEnumerable<CourseArrangementEntity> GetCourseArrangement(int year, int month, int day)
        {
            var date = $"{year}-{month}-{day}";
            using (var conn = Conn.getConn(Conn.getConnStr()))
            {
                //获取规则：教学点启用自动排教室的规则；教室可用
                string sql = @"
                                select helu.id,sStudentCode,nTutorType,dtPKDateTime,dtLessonBeginReal,dtDateReal,sClasscode,dtLessonEndReal,lessonroom.onClassVenueId,lessonroom.onClassVenueId
                                    from view_VB_StudentLessonHeLu helu
                                         inner join V_BS_StudentLessonClassroom lessonroom on helu.id=lessonroom.lessonId and lessonroom.isOccupyClassroom=1 
                                    where nTutorType=1--授课类型
                                    and nAudit=0--未核录
                                    and nStatus!=3--非缺勤记录
                                    and dtDateReal=@dtDateReal
                                    union all
                                    select  helu.id,helu.sStudentCode,helu.nTutorType,helu.dtPKDateTime,helu.dtLessonBeginReal,helu.dtDateReal,helu.sClasscode,dtLessonEndReal,lessonroom.onClassVenueId,lessonroom.onClassVenueId
                                    from view_VB_StudentLessonHeLu helu
		                                 inner join V_BS_Class cls on helu.sClasscode=cls.sCode and cls.fullClass=1
                                         inner join V_BS_StudentLessonClassroom lessonroom on helu.id=lessonroom.lessonId and lessonroom.isOccupyClassroom=1
                                    where nTutorType in (2,7)--授课类型
                                    and nAudit=0--未核录
                                    and nStatus!=3--非缺勤记录
                                    and dtDateReal=@dtDateReal
                            ";
                return conn.Query<CourseArrangementEntity>(sql, param: new { dtDateReal = date }, commandTimeout: 10);
            }
        }

        /// <summary>
        /// 获取已排课信息(占用情况）
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="campusCode">校区</param>
        /// <param name="venueId">教学点</param>
        /// <returns></returns>
        public IEnumerable<ClassroomArrangementEntity> GetClassroomArrangement(int year, int month, int day, string campusCode = null, string venueId = null)
        {
            using (var conn = Conn.getConn(Conn.getConnStr()))
            {
                //获取规则：教学点启用自动排教室的规则；教室可用
                string sql = @"
                               select * from  V_BS_D3ClassroomArrangement where dtDateReal=@dtDateReal
                            ";
                if (!string.IsNullOrEmpty(campusCode))
                {
                    sql += " and campusCode=@campusCode";
                }
                if (!string.IsNullOrEmpty(venueId))
                {
                    sql += " and venueId=@venueId";
                }
                return conn.Query<ClassroomArrangementEntity>(sql, param: new { dtDateReal = $"{year}-{month}-{day}", campusCode = @campusCode, @venueId = @venueId }, commandTimeout: 10);
            }
        }

        /// <summary>
        /// 保存到数据库
        /// </summary>
        /// <param name="d3DbResultModel"></param>
        public void D3ToDb(ClassroomArrangeResultModel d3DbResultModel)
        {
            using (var conn = Conn.getConn(Conn.getConnStr()))
            {
                SqlTransaction trans = conn.BeginTransaction();
                //保存排班结果
                conn.ExecuteAsync(@"insert into V_BS_D3ClassroomArrangement(
                        courseArrangingId
                        ,campusCode
                        ,venueId
                        ,roomId
                        ,roomNum
                        ,dtDateReal
                        ,dtLessonBeginReal
                        ,dtLessonEndReal
                        ,courseId)
                        values(
                        @courseArrangingId
                        ,@campusCode
                        ,@venueId
                        ,@roomId
                        ,@roomNum
                        ,@dtDateReal
                        ,@dtLessonBeginReal
                        ,@dtLessonEndReal
                        ,@courseId)", param: d3DbResultModel.ClassroomArrangements, transaction: trans);
                //回写学员课时核录todo
                //保存待定表
                conn.ExecuteAsync(@"insert into [V_BS_D3CourseArrangementQueue](
                                   queueIndex
                                  ,courseArrangingId
                                  ,onClassCampusCode
                                  ,onClassVenueId
                                  ,dtLessonBeginReal
                                  ,dtLessonEndReal
                                  ,teachType
                                  ,dtPKDateTime)
                                    values(
                                   @queueIndex
                                  ,@courseArrangingId
                                  ,@onClassCampusCode
                                  ,@onClassVenueId
                                  ,@dtLessonBeginReal
                                  ,@dtLessonEndReal
                                  ,@teachType
                                  ,@dtPKDateTime
                                    )",
                    param: d3DbResultModel.CourseArrangementQueue, transaction: trans);
                //保存排班日志
                conn.ExecuteAsync(@"insert into [V_BS_D3LogSortedClassroom](
                                 [courseArrangingId]
                                ,[isSuccess]
                                ,[message]
                                ,[sortIndex]
                                ,[roomId]
                                ,[roomCode]
                                ,[Priority]
                                ,[floor]
                                ,[campusCode]
                                ,[venueId]  
                                )
                                values(
                                 @courseArrangingId
                                ,@isSuccess
                                ,@message
                                ,@sortIndex
                                ,@roomId
                                ,@roomCode
                                ,@Priority
                                ,@floor
                                ,@campusCode
                                ,@venueId    
                                    )",
                    param: d3DbResultModel.LogSortedClassroomEntities, transaction: trans);
                //保存排课日志
                conn.ExecuteAsync(@"insert into [V_BS_D3LogSortedCourseArrangement](
                                   [CourseArrangementType]
                                  ,[SerialLevel]
                                  ,[EarliestMergeDate]
                                  ,[id]
                                  ,[onClassCampusCode]
                                  ,[onClassVenueId]
                                  ,[sTeacherCode]
                                  ,[dtLessonBeginReal]
                                  ,[dtLessonEndReal]
                                  ,[TeachType]
                                  ,[dtPKDateTime])
                                values(
                                   @CourseArrangementType
                                  ,@SerialLevel
                                  ,@EarliestMergeDate
                                  ,@id
                                  ,@onClassCampusCode
                                  ,@onClassVenueId
                                  ,@sTeacherCode
                                  ,@dtLessonBeginReal
                                  ,@dtLessonEndReal
                                  ,@TeachType
                                  ,@dtPKDateTime     
                                    )",
                    param: d3DbResultModel.LogSortedCourseArrangement, transaction: trans);
                trans.Commit();
            }
        }
    }
}

