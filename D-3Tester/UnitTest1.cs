using D_3.Core;
using D_3.DataSource;
using D_3.Models;
using D_3.Models.Business;
using D_3.Models.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace D_3Tester
{
    [TestFixture]
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        public DateTime ArrangeDate = DateTime.Now;
        /// <summary>
        /// 测试课程属性优先级 group>v2>v1
        /// </summary>
        [Test]
        public void TestGroupV2V1()
        {
            var testModel = new TestDataModel()
            {
                ArrangeDate = ArrangeDate,
                occupiedClassroomArrangementEntities = new List<D_3.Models.Entities.ClassroomArrangementEntity>() { },
                ClassroomsEntities = new List<ClassroomEntity>() {
                new ClassroomEntity(){
                     ClassroomCode="01",
                      ClassroomId="CR001",
                       SchoolId="SH001",
                        SpaceId="S01",
                         TeachType= D_3.Models.ETeachType.Group,
                          TeachRange=new D_3.Models.ETeachType[]{
                           D_3.Models.ETeachType.Group,
                            D_3.Models.ETeachType.V1,
                             D_3.Models.ETeachType.V2
                          }
                }
            },
                CourseArrangementsEntities = new List<CourseArrangementEntity>()
            {            new CourseArrangementEntity(){
                 CourseId="YuWen01",
                   EndTime=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 10:00:00"),
                    OperateDate=DateTime.Now.AddDays(new Random().Next(1,4)),
                     SchoolId="SH001",
                      SpaceId="S01",
                       StartTime=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 8:00:00"),
                        TeacherId="T01",
                         TeachType= ETeachType.Group
                },new CourseArrangementEntity(){
                 CourseId="YuWen02",
                   EndTime=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 10:00:00"),
                    OperateDate=DateTime.Now.AddDays(new Random().Next(1,4)),
                     SchoolId="SH001",
                      SpaceId="S01",
                       StartTime=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 8:00:00"),
                        TeacherId="T01",
                         TeachType= ETeachType.V2
                },new CourseArrangementEntity(){
                 CourseId="YuWen03",
                   EndTime=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 10:00:00"),
                    OperateDate=DateTime.Now.AddDays(new Random().Next(1,4)),
                     SchoolId="SH001",
                      SpaceId="S01",
                       StartTime=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 8:00:00"),
                        TeacherId="T01",
                         TeachType= ETeachType.V1
                }

            }
            };
            var (classroomArrangements, courseArrangementNeedToDos) = arrange(testModel);
            //判定
            Assert.AreEqual(classroomArrangements.Count, 1);
            Assert.AreEqual(courseArrangementNeedToDos.Count, 2);
            Assert.AreEqual(classroomArrangements[0].CourseId, "YuWen01");
        }

        /// <summary>
        /// 标准大于非标准
        /// </summary>
        [Test]
        public void StandardGraterthanNoStardard()
        {
            var testModel = new TestDataModel()
            {
                ArrangeDate = ArrangeDate,
                occupiedClassroomArrangementEntities = new List<D_3.Models.Entities.ClassroomArrangementEntity>() { },
                ClassroomsEntities = new List<ClassroomEntity>() {
                new ClassroomEntity(){
                     ClassroomCode="01",
                      ClassroomId="CR001",
                       SchoolId="SH001",
                        SpaceId="S01",
                         TeachType= D_3.Models.ETeachType.Group,
                          TeachRange=new D_3.Models.ETeachType[]{
                           D_3.Models.ETeachType.Group,
                            D_3.Models.ETeachType.V1,
                             D_3.Models.ETeachType.V2
                          }
                }
            },
                CourseArrangementsEntities = new List<CourseArrangementEntity>()
            {            new CourseArrangementEntity(){
                 CourseId="YuWen01",
                   EndTime=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 10:00:00"),
                    OperateDate=DateTime.Now.AddDays(new Random().Next(1,4)),
                     SchoolId="SH001",
                      SpaceId="S01",
                       StartTime=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 8:00:00"),
                        TeacherId="T01",
                         TeachType= ETeachType.Group
                },new CourseArrangementEntity(){
                 CourseId="YuWen02",
                   EndTime=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 9:00:00"),
                    OperateDate=DateTime.Now.AddDays(new Random().Next(1,4)),
                     SchoolId="SH001",
                      SpaceId="S01",
                       StartTime=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 7:00:00"),
                        TeacherId="T01",
                         TeachType= ETeachType.V2
                }
            }
            };
            var (classroomArrangements, courseArrangementNeedToDos) = arrange(testModel);
            //判定
            Assert.AreEqual(classroomArrangements.Count, 1);
            Assert.AreEqual(courseArrangementNeedToDos.Count, 1);
            Assert.AreEqual(classroomArrangements[0].CourseId, "YuWen01");
        }

        /// <summary>
        /// 标准大于非标（即使连续）
        /// </summary>
        [Test]
        public void StandardGraterthanNoStardardSerial()
        {
            var testModel = new TestDataModel()
            {
                ArrangeDate = ArrangeDate,
                occupiedClassroomArrangementEntities = new List<D_3.Models.Entities.ClassroomArrangementEntity>() { },
                ClassroomsEntities = new List<ClassroomEntity>() {
                new ClassroomEntity(){
                     ClassroomCode="01",
                      ClassroomId="CR001",
                       SchoolId="SH001",
                        SpaceId="S01",
                         TeachType= D_3.Models.ETeachType.Group,
                          TeachRange=new D_3.Models.ETeachType[]{
                           D_3.Models.ETeachType.Group,
                            D_3.Models.ETeachType.V1,
                             D_3.Models.ETeachType.V2
                          }
                }
            },
                CourseArrangementsEntities = new List<CourseArrangementEntity>()
            {            new CourseArrangementEntity(){
                 CourseId="YuWen01",
                   EndTime=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 10:00:00"),
                    OperateDate=DateTime.Now.AddDays(new Random().Next(1,4)),
                     SchoolId="SH001",
                      SpaceId="S01",
                       StartTime=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 8:00:00"),
                        TeacherId="T01",
                         TeachType= ETeachType.Group
                },new CourseArrangementEntity(){
                 CourseId="YuWen02",
                   EndTime=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 8:00:00"),
                    OperateDate=DateTime.Now.AddDays(new Random().Next(1,4)),
                     SchoolId="SH001",
                      SpaceId="S01",
                       StartTime=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 7:00:00"),
                        TeacherId="T01",
                         TeachType= ETeachType.V2
                },new CourseArrangementEntity(){
                 CourseId="YuWen03",
                   EndTime=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 9:00:00"),
                    OperateDate=DateTime.Now.AddDays(new Random().Next(1,4)),
                     SchoolId="SH001",
                      SpaceId="S01",
                       StartTime=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 8:00:00"),
                        TeacherId="T01",
                         TeachType= ETeachType.V1
                }

            }
            };
            var (classroomArrangements, courseArrangementNeedToDos) = arrange(testModel);
            //判定
            Assert.AreEqual(classroomArrangements.Count, 1);
            Assert.AreEqual(courseArrangementNeedToDos.Count, 2);
            Assert.AreEqual(classroomArrangements[0].CourseId, "YuWen01");
        }
        /// <summary>
        /// 教室排课已占用
        /// </summary>
        [Test]
        public void TestOccupied()
        {
            var testModel = new TestDataModel()
            {
                ArrangeDate = ArrangeDate,
                occupiedClassroomArrangementEntities = new List<D_3.Models.Entities.ClassroomArrangementEntity>() {
                     new ClassroomArrangementEntity(){
                         ADate= ArrangeDate,
                          ClassroomId="CR001",
                           CourseArrangingId=Guid.NewGuid(),
                            CourseEndTime=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 10:00:00"),
                             CourseStartTime=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 8:00:00"),
                              CourseId="",
                               SchoolId="SH001",
                                 SpaceId="S01"
                     }
                },
                ClassroomsEntities = new List<ClassroomEntity>() {
                new ClassroomEntity(){
                     ClassroomCode="01",
                      ClassroomId="CR001",
                       SchoolId="SH001",
                        SpaceId="S01",
                         TeachType= D_3.Models.ETeachType.Group,
                          TeachRange=new D_3.Models.ETeachType[]{
                           D_3.Models.ETeachType.Group,
                            D_3.Models.ETeachType.V1,
                             D_3.Models.ETeachType.V2
                          }
                }
            },
                CourseArrangementsEntities = new List<CourseArrangementEntity>()
            {            new CourseArrangementEntity(){
                 CourseId="YuWen01",
                   EndTime=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 10:00:00"),
                    OperateDate=DateTime.Now.AddDays(new Random().Next(1,4)),
                     SchoolId="SH001",
                      SpaceId="S01",
                       StartTime=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 8:00:00"),
                        TeacherId="T01",
                         TeachType= ETeachType.Group
                },new CourseArrangementEntity(){
                 CourseId="YuWen02",
                   EndTime=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 10:00:00"),
                    OperateDate=DateTime.Now.AddDays(new Random().Next(1,4)),
                     SchoolId="SH001",
                      SpaceId="S01",
                       StartTime=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 8:00:00"),
                        TeacherId="T01",
                         TeachType= ETeachType.V2
                },new CourseArrangementEntity(){
                 CourseId="YuWen03",
                   EndTime=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 10:00:00"),
                    OperateDate=DateTime.Now.AddDays(new Random().Next(1,4)),
                     SchoolId="SH001",
                      SpaceId="S01",
                       StartTime=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 8:00:00"),
                        TeacherId="T01",
                         TeachType= ETeachType.V1
                }

            }
            };
            var (classroomArrangements, courseArrangementNeedToDos) = arrange(testModel);
            //判定
            Assert.AreEqual(classroomArrangements.Count, 0);
            Assert.AreEqual(courseArrangementNeedToDos.Count, 3);
        }
        /// <summary>
        /// 最大4连标
        /// </summary>
        [Test]
        public void TestMaxSerial4()
        {
            var testModel = new TestDataModel()
            {
                ArrangeDate = ArrangeDate,
                occupiedClassroomArrangementEntities = new List<D_3.Models.Entities.ClassroomArrangementEntity>()
                {
                },
                ClassroomsEntities = new List<ClassroomEntity>() {
                new ClassroomEntity(){
                     ClassroomCode="01",
                      ClassroomId="CR001",
                       SchoolId="SH001",
                        SpaceId="S01",
                         TeachType= D_3.Models.ETeachType.Group,
                          TeachRange=new D_3.Models.ETeachType[]{
                           D_3.Models.ETeachType.Group,
                            D_3.Models.ETeachType.V1,
                             D_3.Models.ETeachType.V2
                          }
                }
            },
                CourseArrangementsEntities = new List<CourseArrangementEntity>()
            {            new CourseArrangementEntity(){
                 CourseId="YuWen01",
                   EndTime=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 10:00:00"),
                    OperateDate=DateTime.Now.AddDays(new Random().Next(1,4)),
                     SchoolId="SH001",
                      SpaceId="S01",
                       StartTime=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 8:00:00"),
                        TeacherId="T01",
                         TeachType= ETeachType.Group
                },new CourseArrangementEntity(){
                 CourseId="YuWen02",
                   EndTime=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 12:00:00"),
                    OperateDate=DateTime.Now.AddDays(new Random().Next(1,4)),
                     SchoolId="SH001",
                      SpaceId="S01",
                       StartTime=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 10:00:00"),
                        TeacherId="T01",
                         TeachType= ETeachType.Group
                },new CourseArrangementEntity(){
                 CourseId="YuWen03",
                   EndTime=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 15:00:00"),
                    OperateDate=DateTime.Now.AddDays(new Random().Next(1,4)),
                     SchoolId="SH001",
                      SpaceId="S01",
                       StartTime=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 13:00:00"),
                        TeacherId="T01",
                         TeachType= ETeachType.Group
                },new CourseArrangementEntity(){
                 CourseId="YuWen03",
                   EndTime=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 17:00:00"),
                    OperateDate=DateTime.Now.AddDays(new Random().Next(1,4)),
                     SchoolId="SH001",
                      SpaceId="S01",
                       StartTime=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 15:00:00"),
                        TeacherId="T01",
                         TeachType= ETeachType.Group
                },new CourseArrangementEntity(){
                 CourseId="YuWen03",
                   EndTime=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 19:00:00"),
                    OperateDate=DateTime.Now.AddDays(new Random().Next(1,4)),
                     SchoolId="SH001",
                      SpaceId="S01",
                       StartTime=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 17:00:00"),
                        TeacherId="T01",
                         TeachType= ETeachType.Group
                },new CourseArrangementEntity(){
                 CourseId="YuWen03",
                   EndTime=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 20:30:00"),
                    OperateDate=DateTime.Now.AddDays(new Random().Next(1,4)),
                     SchoolId="SH001",
                      SpaceId="S01",
                       StartTime=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 19:00:00"),
                        TeacherId="T01",
                         TeachType= ETeachType.Group
                }

            }
            };

            var arrangeDate = testModel.ArrangeDate;
            //排课情况
            var courseArrangementEntities = testModel.CourseArrangementsEntities;
            //教室配备
            var classroomEntities = testModel.ClassroomsEntities;
            //教室已排班情况
            var classroomArrangementEntities = testModel.occupiedClassroomArrangementEntities;
            //排课排序
            var sortedCourseArrangement = new CourseArranger().Arrange(courseArrangementEntities);

            //判定
            Assert.AreEqual(sortedCourseArrangement.Values[0].SerialLevel, 4);
            Assert.AreEqual(sortedCourseArrangement.Values[1].SerialLevel, 2);
        }



        //todo d-3之内的，如果已排班的记录有出表，从待定表入
        //todo d-3之内的，当日新增排课，先走d-3，没有的话再入待定表

        /// <summary>
        /// 执行完整排班逻辑
        /// </summary>
        /// <returns></returns>
        public (List<ClassroomArrangement>, List<CourseArrangement>) arrange(TestDataModel testData)
        {
            var arrangeDate = testData.ArrangeDate;
            //排课情况
            var courseArrangementEntities = testData.CourseArrangementsEntities;
            //教室配备
            var classroomEntities = testData.ClassroomsEntities;
            //教室已排班情况
            var classroomArrangementEntities = testData.occupiedClassroomArrangementEntities;
            //排课排序
            var sortedCourseArrangement = new CourseArranger().Arrange(courseArrangementEntities);
            //结果
            return new ClassroomArranger(arrangeDate, sortedCourseArrangement, classroomEntities, classroomArrangementEntities).Arrange();


        }
    }
}