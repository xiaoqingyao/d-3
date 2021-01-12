using D_3.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using VIP.SystemService.DB;
using Dapper;
using D3.DataSource;
using System.Data.SqlClient;
using System.Linq;
using D3.Models.Entities;

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
        public IEnumerable<ClassroomEntity> GetClassrooms(string campusCode = null, string venueId = null)
        {
            string sql = @"select * from Zeus_Classroom cr inner join Zeus_TeachingVenue venue on cr.venueId=venue.id where venue.isOnRule=1 and cr.canArrange=1 and cr.isDeleted=0  ";
            if (!string.IsNullOrEmpty(venueId))
            {
                sql += " and cr.venueId=@venueId";
            }
            if (!string.IsNullOrEmpty(campusCode))
            {
                sql += " and cr.campusCode=@campusCode";
            }
            using (var conn = Conn.getConn(Conn.getConnStr()))
            {
                //获取规则：教学点启用自动排教室的规则；教室可用
                return conn.Query<ClassroomEntity>(sql, param: new { venueId = venueId, campusCode = campusCode }, commandTimeout: 10);
            }
        }
        /// <summary>
        /// 获取排课信息（学员课时核录）
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        /// <param name="campusCode">校区</param>
        /// <param name="venueId">教学点</param>
        /// <param name="exceptcourseArrangingIds">要排出的排课ids</param>
        /// <returns></returns>
        public IEnumerable<CourseArrangementEntity> GetCourseArrangement(int year, int month, int day, string campusCode = null, string venueId = null, int[] exceptcourseArrangingIds = null)
        {
            var date = $"{year}-{month}-{day}";
            using (var conn = Conn.getConn(Conn.getConnStr()))
            {
                //获取规则说明：教学点启用自动排教室；教室可用
                //1对1课程
                string sql1v1 = @"
                        select helu.id,sStudentCode,nTutorType,dtPKDateTime,dtLessonBeginReal,dtDateReal,sClasscode,dtLessonEndReal,lessonroom.onClassVenueId,lessonroom.onClassVenueId
                                    from view_VB_StudentLessonHeLu helu
                                         inner join V_BS_StudentLessonClassroom lessonroom on helu.id=lessonroom.lessonId and lessonroom.isOccupyClassroom=1 
                                    where nTutorType=1--授课类型
                                    and nAudit=0--未核录
                                    and nStatus!=3--非缺勤记录
                                    and dtDateReal=@dtDateReal
                    ";
                string sqlv2vGroup = @"
                                    select  helu.id,helu.sStudentCode,helu.nTutorType,helu.dtPKDateTime,helu.dtLessonBeginReal,helu.dtDateReal,helu.sClasscode,dtLessonEndReal,lessonroom.onClassVenueId,lessonroom.onClassVenueId
                                    from view_VB_StudentLessonHeLu helu
		                                 inner join V_BS_Class cls on helu.sClasscode=cls.sCode and cls.fullClass=1
                                         inner join V_BS_StudentLessonClassroom lessonroom on helu.id=lessonroom.lessonId and lessonroom.isOccupyClassroom=1
                                    where nTutorType in (2,7)--授课类型
                                    and nAudit=0--未核录
                                    and nStatus!=3--非缺勤记录
                                    and dtDateReal=@dtDateReal
                            ";
                string sqlAppend = string.Empty;
                if (!string.IsNullOrEmpty(campusCode))
                {
                    sqlAppend += " and lessonroom.onClassCampusCode=@onClassCampusCode";
                }
                if (!string.IsNullOrEmpty(venueId))
                {
                    sqlAppend += " and lessonroom.onClassVenueId=@onClassVenueId";
                }
                if (exceptcourseArrangingIds != null && exceptcourseArrangingIds.Length > 0)
                {
                    sqlAppend += " and helu.Id not in @exceptcourseArrangingIds";
                }
                if (!string.IsNullOrEmpty(sqlAppend))
                {
                    sql1v1 += sqlAppend;
                    sqlv2vGroup += sqlAppend;
                }
                var paramObj = new { dtDateReal = date, onClassCampusCode = campusCode, onClassVenueId = venueId, exceptcourseArrangingIds = exceptcourseArrangingIds };
                var courseArrangement1v1 = conn.Query<CourseArrangementEntity>(sql1v1, param: paramObj, commandTimeout: 10);
                var courseArrangementv2vGroup = conn.Query<CourseArrangementEntity>(sqlv2vGroup, param: paramObj, commandTimeout: 10);
                return courseArrangement1v1.Concat(courseArrangementv2vGroup);
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
                               select * from  V_BS_D3ClassroomArrangement where isDelete=0 and dtDateReal=@dtDateReal
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
        /// 保存D-3 排班数据
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
                        ,courseId
                        ,isDelete)
                        values(
                        @courseArrangingId
                        ,@campusCode
                        ,@venueId
                        ,@roomId
                        ,@roomNum
                        ,@dtDateReal
                        ,@dtLessonBeginReal
                        ,@dtLessonEndReal
                        ,@courseId
                        ,0)", param: d3DbResultModel.ClassroomArrangements, transaction: trans);
                //回写学员课时核录todo
                //保存待定表  （暂不维护了）
                //conn.ExecuteAsync(@"insert into [V_BS_D3CourseArrangementQueue](
                //                   queueIndex
                //                  ,courseArrangingId
                //                  ,onClassCampusCode
                //                  ,onClassVenueId
                //                  ,dtLessonBeginReal
                //                  ,dtLessonEndReal
                //                  ,teachType
                //                  ,dtPKDateTime
                //                    ,isDelete)
                //                    values(
                //                   @queueIndex
                //                  ,@courseArrangingId
                //                  ,@onClassCampusCode
                //                  ,@onClassVenueId
                //                  ,@dtLessonBeginReal
                //                  ,@dtLessonEndReal
                //                  ,@teachType
                //                  ,@dtPKDateTime
                //                    ,0
                //                    )",
                //    param: d3DbResultModel.CourseArrangementQueue, transaction: trans);
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

        /// <summary>
        /// 获取排班结果
        /// </summary>
        /// <param name="roomId">教室id</param>
        /// <param name="courseArrangementId">排课id</param>
        private IEnumerable<ClassroomArrangementEntity> getClassroomArrangement(int? roomId, int? courseArrangementId)
        {
            if (!roomId.HasValue && !courseArrangementId.HasValue)
            {
                return null;
            }

            using (var conn = Conn.getConn(Conn.getConnStr()))
            {
                //获取规则：教学点启用自动排教室的规则；教室可用
                string sql = "select * from V_BS_D3ClassroomArrangement where isDelete=0 and 1=1";
                if (roomId.HasValue)
                {
                    sql += " and roomId=@roomId";
                }
                if (courseArrangementId.HasValue)
                {
                    sql += " and courseArrangingId=@courseArrangingId";
                }
                return conn.Query<ClassroomArrangementEntity>(sql, param: new { roomId = roomId, courseArrangementId = courseArrangementId }, commandTimeout: 10);
            }
        }

        /// <summary>
        /// 释放教室
        /// </summary>
        /// <param name="roomId">教室id</param>
        /// <param name="courseArrangementId">排课id</param>
        /// <param name="Reason">释放原因</param>
        /// <returns>释放的教室结果</returns>
        public IEnumerable<ClassroomArrangementEntity> FreeClassroomArrangement(int? roomId, int? courseArrangementId, string deleteReason)
        {
            var arrangements = getClassroomArrangement(roomId, courseArrangementId);
            if (arrangements == null || arrangements.Count() == 0)
            {
                return arrangements;
            }
            using (var conn = Conn.getConn(Conn.getConnStr()))
            {
                var trans = conn.BeginTransaction();
                //释放教室
                string sql = "update V_BS_D3ClassroomArrangement set isDelete=1,deleteReason=@deleteReason where id in @ids";
                conn.Execute(sql, transaction: trans, param: new { deleteReason = deleteReason, ids = arrangements.Select(p => p.id).ToArray() }, commandTimeout: 10);
                //修改课节教室扩展表
                sql = "update V_BS_StudentLessonClassroom set assignClassroomStatus=0,assignClassroomId=0 where lessonId in @ids  ";
                conn.Execute(sql, transaction: trans, param: new { ids = arrangements.Select(p => p.id).ToArray() }, commandTimeout: 10);
            }
            return arrangements;
            //todo 如果
        }

        /// <summary>
        /// 获取待定表
        /// </summary>
        /// <param name="campusCode"></param>
        /// <param name="venueId"></param>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="day"></param>
        public IEnumerable<CourseArrangementQueueEntity> GetCourseArrangementQueue(string campusCode, string venueId, int year, int month, int day)
        {
            using (var conn = Conn.getConn(Conn.getConnStr()))
            {
                string sql = "select * from V_BS_D3CourseArrangementQueue where isDelete=0 and  campusCode=@campusCode and venueId=@venueId and year=@year and month=@month and day=@day";
                return conn.Query<CourseArrangementQueueEntity>(sql, param: new { campusCode = @campusCode, venueId = @venueId, year = @year, month = @month, day = @day }, commandTimeout: 10);
            }
        }
    }
}

