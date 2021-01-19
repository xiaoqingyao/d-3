using D3;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VIP.SystemService.DB;
using Dapper;

namespace D3UnitTest
{
    /// <summary>
    /// D-3整体测试
    /// </summary>
    [TestClass]
    public class D3Test
    {
        /// <summary>
        /// d-3测试调用
        /// </summary>
        [TestMethod]
        public void TestD3EveryDay()
        {
            var rel = D3Manager.D3(2021, 1, 7);
            Assert.AreEqual(true, rel.Result);
            rel = D3Manager.D3(2021, 1, 7);
            Assert.AreEqual(false, rel.Result);
            rel = D3Manager.WithinD3("VPVP036", "19", 2021, 1, 7);
            Assert.AreEqual(false, rel.Result);
        }
        /// <summary>
        /// 释放教室（前提是执行完d-3)
        /// </summary>
        [TestMethod]
        public void TestFreeClassRoom()
        {
            using (var conn = Conn.getConn(Conn.getConnStr()))
            {
                int dyClassroomid = conn.QueryFirstOrDefault<int>("select assignClassroomId from V_BS_StudentLessonClassroom where [assignClassroomStatus]!=0 and assignClassroomId!=0");
                var rel = D3Manager.FreeClassroomArrangement(dyClassroomid, null, "测试删除");
                //释放后有成功进入
                Assert.AreEqual(true, rel.Result);
                //再次执行d-3内，不可以再有成功记录
                rel = D3Manager.WithinD3("VPVP036", "19", 2021, 1, 7);
                Assert.AreEqual(false, rel.Result);
            }
        }
        /// <summary>
        /// 添加教室（前提是执行完d-3)
        /// </summary>
        [TestMethod]
        public void TestAddClassRoom()
        {
            using (var conn = Conn.getConn(Conn.getConnStr()))
            {
                string insertsql = @"
 insert into Zeus_Classroom(
[campusCode]
,[venueId]
,[roomId]
,[roomCode]
,[roomAttributeId]
,[canArrange]
,[capacityNum]
,[teachingAttributes]
,[floor]
,[priority]
,[isExclusive]
,[exclusiveTeacherCode]
,[remark]
,[isDeleted]
,[createTime]
,[updateTime])
select top 1
'VPVP036'
,19
,99999
,[roomCode]
,[roomAttributeId]
,[canArrange]
,[capacityNum]
,'1,2,7'
,[floor]
,[priority]
,0
,''
,'lvcc'
,[isDeleted]
,[createTime]
,[updateTime]
from
Zeus_Classroom  
order by teachingAttributes desc
";
                  conn.Execute(insertsql);
                //新增教室，执行d-3成功
                var rel = D3Manager.WithinD3("VPVP036", "19", 2021, 1, 7);
                Assert.AreEqual(true, rel.Result);
                //再次执行d-3失败
                rel = D3Manager.WithinD3("VPVP036", "19", 2021, 1, 7);
                Assert.AreEqual(false, rel.Result);
                
              
               int dyClassroomid = conn.QueryFirstOrDefault<int>("select roomid from Zeus_Classroom where remark='lvcc'");
                //删除教室
                conn.Execute("delete from Zeus_Classroom where remark='lvcc'");
                rel = D3Manager.FreeClassroomArrangement(dyClassroomid, null, "删除了教室信息释放");
                //释放教室应成功
                Assert.AreEqual(true, rel.Result);
                //执行d-3应该为false,因为释放逻辑里包含d-3内
                rel = D3Manager.WithinD3("VPVP036", "19", 2021, 1, 7);
                Assert.AreEqual(false, rel.Result);
            }
        }

        //调课-增加课-有匹配
        //调课-增加课-没有匹配
        //调课-释放课-有匹配进入
        //调课-时方可-无匹配进入
    }
}
