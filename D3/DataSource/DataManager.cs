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
using D_3.Models;
using System.Threading.Tasks;

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
            IList<ClassroomEntity> queryRel = null;
            using (var conn = Conn.getConn(Conn.getConnStr()))
            {
                //获取规则：教学点启用自动排教室的规则；教室可用
                queryRel = conn.Query<ClassroomEntity>(sql, param: new { venueId = venueId, campusCode = campusCode }, commandTimeout: 10).ToList();
            }
            foreach (var classroom in queryRel)
            {
                string[] sTeachRange = classroom.teachingAttributes?.Split(new char[] { ',' });
                List<ETeachType> lstTeachTypes = new List<ETeachType>();
                if (sTeachRange != null)
                {
                    foreach (var sTeachType in sTeachRange)
                    {
                        lstTeachTypes.Add((ETeachType)int.Parse(sTeachType));
                    }
                }
                classroom.TeachRange = lstTeachTypes.ToArray();
            }
            return queryRel;
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
        public IEnumerable<CourseArrangementEntity> GetCourseArrangement(int year, int month, int day, string campusCode = null, string venueId = null, string[] exceptcourseArrangingIds = null)
        {
            var date = $"{year}-{month}-{day}";
            using (var conn = Conn.getConn(Conn.getConnStr()))
            {
                //获取规则说明：教学点启用自动排教室；教室可用
                //1对1课程
                string sql1v1 = @"
                       select  helu.sClasscode,helu.steacherCode,helu.nTutorType,helu.dtPKDateTime,helu.dtLessonBeginReal,helu.dtDateReal,dtLessonEndReal,lessonroom.onClassCampusCode,lessonroom.onClassVenueId
                                    from view_VB_StudentLessonHeLu helu
                                         inner join V_BS_StudentLessonClassroom lessonroom on helu.id=lessonroom.lessonId and lessonroom.isOccupyClassroom=1 and lessonroom.assignClassroomStatus!=1 
                                    where nTutorType=1--授课类型
                                    --and nAudit=0--未核录
                                    --and nStatus!=3--非缺勤记录
                                    and dtDateReal=@dtDateReal
                    ";
                //1v2 小组课
                string sqlv2vGroup = @"
                    select sClasscode,steacherCode,nTutorType,max(dtPKDateTime) dtPKDateTime,dtLessonBeginReal,dtDateReal,dtLessonEndReal,onClassCampusCode,onClassVenueId
		                from (
			                select  helu.sClasscode,helu.steacherCode,helu.nTutorType,helu.dtPKDateTime,helu.dtLessonBeginReal,helu.dtDateReal,helu.dtLessonEndReal
					                ,lessonroom.onClassCampusCode,lessonroom.onClassVenueId 
					                from 
					                view_VB_StudentLessonHeLu helu 
					                inner join V_BS_StudentLessonClassroom lessonroom on helu.id=lessonroom.lessonId and lessonroom.isOccupyClassroom=1 and lessonroom.assignClassroomStatus!=1
					                inner join V_BS_Class cls on helu.sClasscode=cls.sCode and cls.fullClass=1
					                where helu.nTutorType !=1
							                --and helu.nAudit=0--未核录
							                --and helu.nStatus!=3--非缺勤记录
							                and helu.dtDateReal=@dtDateReal
                                            {0}
				                ) a 
				                group by  steacherCode,nTutorType,dtLessonBeginReal,dtDateReal,sClasscode,dtLessonEndReal
						                ,onClassCampusCode,onClassVenueId
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
                    sqlAppend += " and helu.sClasscode not in @exceptcourseArrangingIds";
                }

                sql1v1 += sqlAppend;
                sqlv2vGroup = string.Format(sqlv2vGroup, sqlAppend);

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
                               select * from  V_BS_D3ClassroomArrangement where isDelete=0 and dtDateRealYear=@dtDateRealYear and dtDateRealMonth=@dtDateRealMonth and dtDateRealDay=@dtDateRealDay
                            ";
                if (!string.IsNullOrEmpty(campusCode))
                {
                    sql += " and campusCode=@campusCode";
                }
                if (!string.IsNullOrEmpty(venueId))
                {
                    sql += " and venueId=@venueId";
                }
                return conn.Query<ClassroomArrangementEntity>(sql, param: new { dtDateRealYear = year, dtDateRealMonth = month, dtDateRealDay = day, campusCode = @campusCode, @venueId = @venueId }, commandTimeout: 10);
            }
        }

        /// <summary>
        /// 保存D-3 排班数据
        /// </summary>
        /// <param name="d3DbResultModel"></param>
        public async Task<int> D3ToDb(ClassroomArrangeResultModel d3DbResultModel)
        {
            int executeCount = 0;
            using (var conn = Conn.getConn(Conn.getConnStr()))
            {
                SqlTransaction trans = conn.BeginTransaction();
                try
                {
                    //保存排教室结果
                    executeCount = await conn.ExecuteAsync(@"insert into V_BS_D3ClassroomArrangement(
                        sClasscode
                        ,campusCode
                        ,venueId
                        ,roomId
                        ,roomNum
                        ,dtDateRealYear
                        ,dtDateRealMonth
                        ,dtDateRealDay
                        ,dtLessonBeginReal
                        ,dtLessonEndReal
                        ,isDelete)
                        values(
                        @sClasscode
                        ,@campusCode
                        ,@venueId
                        ,@roomId
                        ,@roomNum
                        ,@dtDateRealYear
                        ,@dtDateRealMonth
                        ,@dtDateRealDay
                        ,@dtLessonBeginReal
                        ,@dtLessonEndReal
                        ,0)", param: d3DbResultModel.ClassroomArrangements, transaction: trans);
                    //回写学员课时核录-成功
                    foreach (var classroomArrangement in d3DbResultModel.ClassroomArrangements)
                    {
                        await conn.ExecuteAsync(@"
                            update room set room.assignClassroomStatus=1,
				                            room.assignClassroomId=@assignClassroomId
                            from view_VB_StudentLessonHeLu helu 
				                            inner join V_BS_StudentLessonClassroom room on helu.id=room.lessonId
				                            where helu.sclasscode=@sClasscode  and helu.dtLessonBeginReal=@dtLessonBeginReal
                        ", param: new { assignClassroomId = classroomArrangement.roomId, sClasscode = classroomArrangement.sClasscode, dtLessonBeginReal = classroomArrangement.dtLessonBeginReal }, transaction: trans);
                    }
                    //回写课时核录扩展表-待定
                    foreach (var queue in d3DbResultModel.CourseArrangementQueue)
                    {
                        await conn.ExecuteAsync(@"
                         update room set room.assignClassroomStatus=2
                            from view_VB_StudentLessonHeLu helu 
				                            inner join V_BS_StudentLessonClassroom room on helu.id=room.lessonId
				                            where helu.sclasscode=@sClasscode  and helu.dtLessonBeginReal=@dtLessonBeginReal

                        ", param: new { sClasscode = queue.sClasscode, dtLessonBeginReal = queue.dtLessonBeginReal }, transaction: trans);
                    }
                    trans.Commit();
                }
                catch (Exception ex)
                {
                    //todo 记录日志
                    trans.Rollback();
                }
                finally
                {
                    if (conn.State != System.Data.ConnectionState.Closed)
                    {
                        conn.Close();
                    }
                }
            }
            if (executeCount == 0)
            {
                return 0;
            }
            using (var conn = Conn.getConn(Conn.getConnStr()))
            {
                try
                {
                    //保存排班日志
                    await conn.ExecuteAsync(@"insert into [V_BS_D3LogSortedClassroom](
                                 [sClasscode]
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
                                 @sClasscode
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
                            param: d3DbResultModel.LogSortedClassroomEntities);
                    //保存排课日志
                    await conn.ExecuteAsync(@"insert into [V_BS_D3LogSortedCourseArrangement](
                                   [CourseArrangementType]
                                  ,[SerialLevel]
                                  ,[EarliestMergeDate] 
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
                                  ,@onClassCampusCode
                                  ,@onClassVenueId
                                  ,@sTeacherCode
                                  ,@dtLessonBeginReal
                                  ,@dtLessonEndReal
                                  ,@nTutorType
                                  ,@dtPKDateTime     
                                    )",
                        param: d3DbResultModel.LogSortedCourseArrangement);
                }
                catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    if (conn.State != System.Data.ConnectionState.Closed)
                    {
                        conn.Close();
                    }
                }
                return executeCount;
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
                string sql = "select * from V_BS_D3ClassroomArrangement where isDelete=0 ";
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
            //todo添加释放逻辑判断，针对1-1之外的，如果还存在课，就不释放教室
            using (var conn = Conn.getConn(Conn.getConnStr()))
            {
                var trans = conn.BeginTransaction();
                //释放教室
                string sql = "update V_BS_D3ClassroomArrangement set isDelete=1,deleteReason=@deleteReason where id in @ids";
                conn.Execute(sql, transaction: trans, param: new { deleteReason = deleteReason, ids = arrangements.Select(p => p.id).ToArray() });
                //修改课节教室扩展表
                sql = "update V_BS_StudentLessonClassroom set assignClassroomStatus=0,assignClassroomId=0 where lessonId in @ids  ";
                conn.Execute(sql, transaction: trans, param: new { ids = arrangements.Select(p => p.id).ToArray() });
                trans.Commit();
            }
            return arrangements;
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

