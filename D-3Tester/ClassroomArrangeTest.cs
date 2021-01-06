using D_3.Core;
using D_3.DataSource;
using D_3.Models;
using D_3.Models.Business;
using D_3.Models.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace D_3Tester
{
    /// <summary>
    /// 测试排教室
    /// </summary>
    [TestFixture]
    public class ClassroomArrangeTest
    {
        public DateTime ArrangeDate = DateTime.Now;
        /// <summary>
        /// 测试教室可授课类型与课程类型匹配
        /// </summary>
        [Test]
        public void TestTeachType()
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
                           D_3.Models.ETeachType.Group
                          }
                },
                new ClassroomEntity(){
                     ClassroomCode="02",
                      ClassroomId="CR002",
                       SchoolId="SH001",
                        SpaceId="S01",
                         TeachType= D_3.Models.ETeachType.Group,
                          TeachRange=new D_3.Models.ETeachType[]{
                           D_3.Models.ETeachType.V2
                          }
                },new ClassroomEntity(){
                     ClassroomCode="03",
                      ClassroomId="CR003",
                       SchoolId="SH001",
                        SpaceId="S01",
                         TeachType= D_3.Models.ETeachType.Group,
                          TeachRange=new D_3.Models.ETeachType[]{
                            D_3.Models.ETeachType.V1
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
            Assert.AreEqual(classroomArrangements.Count, 3);
            Assert.AreEqual(courseArrangementNeedToDos.Count, 0);

            Assert.AreEqual(classroomArrangements.Where(p => p.ClassroomId == "CR003").FirstOrDefault().CourseId, "YuWen03");
            Assert.AreEqual(classroomArrangements.Where(p => p.ClassroomId == "CR002").FirstOrDefault().CourseId, "YuWen02");
            Assert.AreEqual(classroomArrangements.Where(p => p.ClassroomId == "CR001").FirstOrDefault().CourseId, "YuWen01");
        }

        /// <summary>
        /// 测试教室可授课类型包含的情况
        /// </summary>
        [Test]
        public void TestTeachRange()
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
                },
                new ClassroomEntity(){
                     ClassroomCode="02",
                      ClassroomId="CR002",
                       SchoolId="SH001",
                        SpaceId="S01",
                         TeachType= D_3.Models.ETeachType.Group,
                          TeachRange=new D_3.Models.ETeachType[]{
                           D_3.Models.ETeachType.V1
                          }
                }
            },
                CourseArrangementsEntities = new List<CourseArrangementEntity>()
            {            new CourseArrangementEntity(){
                 CourseId="YuWen01",
                   EndTime=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 9:10:00"),
                    OperateDate=DateTime.Now.AddDays(new Random().Next(1,4)),
                     SchoolId="SH001",
                      SpaceId="S01",
                       StartTime=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 7:10:00"),
                        TeacherId="T03",
                         TeachType= ETeachType.Group
                },new CourseArrangementEntity(){
                 CourseId="YuWen02",
                   EndTime=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 12:00:00"),
                    OperateDate=DateTime.Now.AddDays(new Random().Next(1,4)),
                     SchoolId="SH001",
                      SpaceId="S01",
                       StartTime=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 10:00:00"),
                        TeacherId="T02",
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
            Assert.AreEqual(classroomArrangements.Count, 3);
            Assert.AreEqual(courseArrangementNeedToDos.Count, 0);

            Assert.AreEqual(classroomArrangements.Where(p => p.ClassroomId == "CR002").FirstOrDefault().CourseId, "YuWen03");
            Assert.AreEqual(classroomArrangements.Where(p => p.ClassroomId == "CR001").Count(), 2);
        }

        /// <summary>
        /// 专属教室优先
        /// </summary>
        [Test]
        public void TestExclusive()
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
                            D_3.Models.ETeachType.V1
                          },
                          IsExclusive=true,
                           ExclusiveTeacherId="T01"
                },
                new ClassroomEntity(){
                     ClassroomCode="02",
                      ClassroomId="CR002",
                       SchoolId="SH001",
                        SpaceId="S01",
                         TeachType= D_3.Models.ETeachType.Group,
                          TeachRange=new D_3.Models.ETeachType[]{
                           D_3.Models.ETeachType.V2,
                           D_3.Models.ETeachType.Group
                          }
                }
            },
                CourseArrangementsEntities = new List<CourseArrangementEntity>()
            {            new CourseArrangementEntity(){
                 CourseId="YuWen01",
                   EndTime=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 10:00:00"),
                    OperateDate=DateTime.Now.AddMinutes(2),
                     SchoolId="SH001",
                      SpaceId="S01",
                       StartTime=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 8:00:00"),
                        TeacherId="T01",
                         TeachType= ETeachType.Group//进教室2
                },   new CourseArrangementEntity(){
                 CourseId="YuWen02",
                   EndTime=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 10:00:00"),
                    OperateDate=DateTime.Now.AddMinutes(1),
                     SchoolId="SH001",
                      SpaceId="S01",
                       StartTime=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 8:00:00"),
                        TeacherId="T02",
                         TeachType= ETeachType.V1//进待定表
                },   new CourseArrangementEntity(){
                 CourseId="YuWen03",
                   EndTime=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 10:00:00"),
                    OperateDate=DateTime.Now.AddMinutes(2),
                     SchoolId="SH001",
                      SpaceId="S01",
                       StartTime=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 8:00:00"),
                        TeacherId="T01",
                         TeachType= ETeachType.V1//进教室1
                }
            }
            };
            var (classroomArrangements, courseArrangementNeedToDos) = arrange(testModel);
            Assert.AreEqual(classroomArrangements.Where(p => p.ClassroomId == "CR001").FirstOrDefault().CourseId, "YuWen03");
            Assert.AreEqual(classroomArrangements.Where(p => p.ClassroomId == "CR002").FirstOrDefault().CourseId, "YuWen01");
            Assert.AreEqual(courseArrangementNeedToDos.Count, 1);
        }

        /// <summary>
        /// 教室授课类型与排课一对一匹配优先
        /// </summary>
        [Test]
        public void TestTeachTypeEqual()
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
                            D_3.Models.ETeachType.V1
                          }
                },
                new ClassroomEntity(){
                     ClassroomCode="02",
                      ClassroomId="CR002",
                       SchoolId="SH001",
                        SpaceId="S01",
                         TeachType= D_3.Models.ETeachType.Group,
                          TeachRange=new D_3.Models.ETeachType[]{
                           D_3.Models.ETeachType.V1,
                           D_3.Models.ETeachType.Group
                          }
                }
            },
                CourseArrangementsEntities = new List<CourseArrangementEntity>()
            {            new CourseArrangementEntity(){
                 CourseId="YuWen01",
                   EndTime=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 10:00:00"),
                    OperateDate=DateTime.Now.AddMinutes(2),
                     SchoolId="SH001",
                      SpaceId="S01",
                       StartTime=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 8:00:00"),
                        TeacherId="T01",
                         TeachType= ETeachType.V1//进教室2
                }
            }
            };
            var (classroomArrangements, courseArrangementNeedToDos) = arrange(testModel);
            Assert.AreEqual(classroomArrangements.Where(p => p.ClassroomId == "CR001").FirstOrDefault().CourseId, "YuWen01");
            Assert.AreEqual(classroomArrangements.Where(p => p.ClassroomId == "CR002").Count(), 0);
        }

        /// <summary>
        /// 教室已经给老师有过排课的教室，优先进入
        /// </summary>
        [Test]
        public void TestHasSiblingCourse()
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
                            D_3.Models.ETeachType.V1,
                            D_3.Models.ETeachType.V2
                          }
                },
                new ClassroomEntity(){
                     ClassroomCode="02",
                      ClassroomId="CR002",
                       SchoolId="SH001",
                        SpaceId="S01",
                         TeachType= D_3.Models.ETeachType.Group,
                          TeachRange=new D_3.Models.ETeachType[]{
                           D_3.Models.ETeachType.V1,
                           D_3.Models.ETeachType.Group
                          }
                }
            },
                CourseArrangementsEntities = new List<CourseArrangementEntity>()
            {            new CourseArrangementEntity(){
                             CourseId="YuWen01",
                               EndTime=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 10:00:00"),
                                OperateDate=DateTime.Now.AddMinutes(0),
                                 SchoolId="SH001",
                                  SpaceId="S01",
                                   StartTime=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 8:00:00"),
                                    TeacherId="T01",
                                     TeachType= ETeachType.Group//进教室2
                        },
                         new CourseArrangementEntity(){
                             CourseId="YuWen02",
                               EndTime=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 11:00:00"),
                                OperateDate=DateTime.Now.AddMinutes(2),
                                 SchoolId="SH001",
                                  SpaceId="S01",
                                   StartTime=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 10:00:00"),
                                    TeacherId="T01",
                                     TeachType= ETeachType.V1//进教室2
                        },new CourseArrangementEntity(){
                             CourseId="YuWen03",
                               EndTime=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 11:00:00"),
                                OperateDate=DateTime.Now.AddMinutes(1),
                                 SchoolId="SH001",
                                  SpaceId="S01",
                                   StartTime=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 10:00:00"),
                                    TeacherId="T02",
                                     TeachType= ETeachType.V1//进教室1
                        }
            }
            };
            var (classroomArrangements, courseArrangementNeedToDos) = arrange(testModel);
            Assert.AreEqual(classroomArrangements.Where(p => p.ClassroomId == "CR002").Count(), 2);
            Assert.AreEqual(classroomArrangements.Where(p => p.ClassroomId == "CR001").FirstOrDefault().CourseId, "YuWen03");
        }

        /// <summary>
        /// 测试教室优先属性
        /// </summary>
        [Test]
        public void TestIsTop()
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
                            D_3.Models.ETeachType.V1,
                            D_3.Models.ETeachType.V2
                          }
                },
                new ClassroomEntity(){
                     ClassroomCode="02",
                      ClassroomId="CR002",
                       SchoolId="SH001",
                        SpaceId="S01",
                         TeachType= D_3.Models.ETeachType.Group,
                          TeachRange=new D_3.Models.ETeachType[]{
                           D_3.Models.ETeachType.V1,
                           D_3.Models.ETeachType.Group
                          },IsTop=true
                }
            },
                CourseArrangementsEntities = new List<CourseArrangementEntity>()
            {            new CourseArrangementEntity(){
                             CourseId="YuWen01",
                               EndTime=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 10:00:00"),
                                OperateDate=DateTime.Now.AddMinutes(0),
                                 SchoolId="SH001",
                                  SpaceId="S01",
                                   StartTime=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 8:00:00"),
                                    TeacherId="T01",
                                     TeachType= ETeachType.V1//进教室2
                        } 
            }
            };
            var (classroomArrangements, courseArrangementNeedToDos) = arrange(testModel);
            Assert.AreEqual(classroomArrangements.Where(p => p.ClassroomId == "CR002").Count(),1);
            Assert.AreEqual(classroomArrangements.Where(p => p.ClassroomId == "CR001").Count(),0);
        }

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
