using D_3.Arranger;
using D_3.DataSource;
using D_3.Models;
using D_3.Models.Business;
using D_3.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using D3.Models.Entities;

namespace D_3Tester
{
    /// <summary>
    /// 测试排教室
    /// </summary>
    [TestClass]
    public class ClassroomArrangeTest
    {
        public DateTime ArrangeDate = DateTime.Now;
        /// <summary>
        /// 测试教室可授课类型与课程类型匹配
        /// </summary>
        [TestMethod]
        public void TestTeachType()
        {
            var testModel = new ClassroomArrangeParameterModel()
            {
                ArrangeDate = ArrangeDate,
                OccupiedClassroomArrangementEntities = new List<D_3.Models.Entities.ClassroomArrangementEntity>() { },
                ClassroomsEntities = new List<ClassroomEntity>() {
                new ClassroomEntity(){
                     roomId=1,
                      
                       campusCode="SH001",
                        venueId="S01",
                         TeachType= D_3.Models.ETeachType.Group,
                          TeachRange=new D_3.Models.ETeachType[]{
                           D_3.Models.ETeachType.Group
                          }
                },
                new ClassroomEntity(){
                     roomId=2,
                      
                       campusCode="SH001",
                        venueId="S01",
                         TeachType= D_3.Models.ETeachType.Group,
                          TeachRange=new D_3.Models.ETeachType[]{
                           D_3.Models.ETeachType.V2
                          }
                },new ClassroomEntity(){
                     roomId=3,
                     
                       campusCode="SH001",
                        venueId="S01",
                         TeachType= D_3.Models.ETeachType.Group,
                          TeachRange=new D_3.Models.ETeachType[]{
                            D_3.Models.ETeachType.V1
                          }
                }
            },
                CourseArrangementsEntities = new List<CourseArrangementEntity>()
            {            new CourseArrangementEntity(){
                 courseArrangingId=1,
                   dtLessonEndReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 10:00:00"),
                    dtPKDateTime=DateTime.Now.AddDays(new Random().Next(1,4)),
                     onClassCampusCode="SH001",
                      onClassVenueId="S01",
                       dtLessonBeginReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 8:00:00"),
                        sTeacherCode="T01",
                         teachType= ETeachType.Group
                },new CourseArrangementEntity(){
                   courseArrangingId=2,
                   dtLessonEndReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 10:00:00"),
                    dtPKDateTime=DateTime.Now.AddDays(new Random().Next(1,4)),
                     onClassCampusCode="SH001",
                      onClassVenueId="S01",
                       dtLessonBeginReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 8:00:00"),
                        sTeacherCode="T01",
                         teachType= ETeachType.V2
                },new CourseArrangementEntity(){
                  courseArrangingId=3,
                   dtLessonEndReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 10:00:00"),
                    dtPKDateTime=DateTime.Now.AddDays(new Random().Next(1,4)),
                     onClassCampusCode="SH001",
                      onClassVenueId="S01",
                       dtLessonBeginReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 8:00:00"),
                        sTeacherCode="T01",
                         teachType= ETeachType.V1
                }

            }
            };
            List<CourseArrangementQueueEntity> courseArrangementNeedToDos;
            var classroomArrangements = arrange(testModel, out courseArrangementNeedToDos);
            //判定
            Assert.AreEqual(classroomArrangements.Count, 3);
            Assert.AreEqual(courseArrangementNeedToDos.Count, 0);

            Assert.AreEqual(classroomArrangements.Where(p => p.roomId == 3).FirstOrDefault().courseArrangingId, "YuWen03");
            Assert.AreEqual(classroomArrangements.Where(p => p.roomId == 2).FirstOrDefault().courseArrangingId, "YuWen02");
            Assert.AreEqual(classroomArrangements.Where(p => p.roomId == 1).FirstOrDefault().courseArrangingId, "YuWen01");
        }

        /// <summary>
        /// 测试教室可授课类型包含的情况
        /// </summary>
         [TestMethod]
        public void TestTeachRange()
        {
            var testModel = new ClassroomArrangeParameterModel()
            {
                ArrangeDate = ArrangeDate,
                OccupiedClassroomArrangementEntities = new List<D_3.Models.Entities.ClassroomArrangementEntity>() { },
                ClassroomsEntities = new List<ClassroomEntity>() {
                new ClassroomEntity(){
                     roomId=1,
                      
                       campusCode="SH001",
                        venueId="S01",
                         TeachType= D_3.Models.ETeachType.Group,
                          TeachRange=new D_3.Models.ETeachType[]{
                           D_3.Models.ETeachType.Group,
                            D_3.Models.ETeachType.V1,
                             D_3.Models.ETeachType.V2
                          }
                },
                new ClassroomEntity(){
                     roomId=2,
                      
                       campusCode="SH001",
                        venueId="S01",
                         TeachType= D_3.Models.ETeachType.Group,
                          TeachRange=new D_3.Models.ETeachType[]{
                           D_3.Models.ETeachType.V1
                          }
                }
            },
                CourseArrangementsEntities = new List<CourseArrangementEntity>()
            {            new CourseArrangementEntity(){
                 courseArrangingId=1,
                   dtLessonEndReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 9:10:00"),
                    dtPKDateTime=DateTime.Now.AddDays(new Random().Next(1,4)),
                     onClassCampusCode="SH001",
                      onClassVenueId="S01",
                       dtLessonBeginReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 7:10:00"),
                        sTeacherCode="T03",
                         teachType= ETeachType.Group
                },new CourseArrangementEntity(){
                   courseArrangingId=2,
                   dtLessonEndReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 12:00:00"),
                    dtPKDateTime=DateTime.Now.AddDays(new Random().Next(1,4)),
                     onClassCampusCode="SH001",
                      onClassVenueId="S01",
                       dtLessonBeginReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 10:00:00"),
                        sTeacherCode="T02",
                         teachType= ETeachType.V2
                },new CourseArrangementEntity(){
                  courseArrangingId=3,
                   dtLessonEndReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 10:00:00"),
                    dtPKDateTime=DateTime.Now.AddDays(new Random().Next(1,4)),
                     onClassCampusCode="SH001",
                      onClassVenueId="S01",
                       dtLessonBeginReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 8:00:00"),
                        sTeacherCode="T01",
                         teachType= ETeachType.V1
                }
            }
            };
            List<CourseArrangementQueueEntity> courseArrangementNeedToDos;
            var classroomArrangements = arrange(testModel, out courseArrangementNeedToDos);
            //判定
            Assert.AreEqual(classroomArrangements.Count, 3);
            Assert.AreEqual(courseArrangementNeedToDos.Count, 0);

            Assert.AreEqual(classroomArrangements.Where(p => p.roomId == 2).FirstOrDefault().courseArrangingId, "YuWen03");
            Assert.AreEqual(classroomArrangements.Where(p => p.roomId == 1).Count(), 2);
        }

        /// <summary>
        /// 专属教室优先
        /// </summary>
         [TestMethod]
        public void TestExclusive()
        {
            var testModel = new ClassroomArrangeParameterModel()
            {
                ArrangeDate = ArrangeDate,
                OccupiedClassroomArrangementEntities = new List<D_3.Models.Entities.ClassroomArrangementEntity>() { },
                ClassroomsEntities = new List<ClassroomEntity>() {
                new ClassroomEntity(){
                     roomId=1,
                      
                       campusCode="SH001",
                        venueId="S01",
                         TeachType= D_3.Models.ETeachType.Group,
                          TeachRange=new D_3.Models.ETeachType[]{
                            D_3.Models.ETeachType.V1
                          },
                          IsExclusive=true,
                           exclusiveTeacherCode="T01"
                },
                new ClassroomEntity(){
                     roomId=2,
                      
                       campusCode="SH001",
                        venueId="S01",
                         TeachType= D_3.Models.ETeachType.Group,
                          TeachRange=new D_3.Models.ETeachType[]{
                           D_3.Models.ETeachType.V2,
                           D_3.Models.ETeachType.Group
                          }
                }
            },
                CourseArrangementsEntities = new List<CourseArrangementEntity>()
            {            new CourseArrangementEntity(){
                 courseArrangingId=1,
                   dtLessonEndReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 10:00:00"),
                    dtPKDateTime=DateTime.Now.AddMinutes(2),
                     onClassCampusCode="SH001",
                      onClassVenueId="S01",
                       dtLessonBeginReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 8:00:00"),
                        sTeacherCode="T01",
                         teachType= ETeachType.Group//进教室2
                },   new CourseArrangementEntity(){
                   courseArrangingId=2,
                   dtLessonEndReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 10:00:00"),
                    dtPKDateTime=DateTime.Now.AddMinutes(1),
                     onClassCampusCode="SH001",
                      onClassVenueId="S01",
                       dtLessonBeginReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 8:00:00"),
                        sTeacherCode="T02",
                         teachType= ETeachType.V1//进待定表
                },   new CourseArrangementEntity(){
                  courseArrangingId=3,
                   dtLessonEndReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 10:00:00"),
                    dtPKDateTime=DateTime.Now.AddMinutes(2),
                     onClassCampusCode="SH001",
                      onClassVenueId="S01",
                       dtLessonBeginReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 8:00:00"),
                        sTeacherCode="T01",
                         teachType= ETeachType.V1//进教室1
                }
            }
            };
            List<CourseArrangementQueueEntity> courseArrangementNeedToDos;
            var classroomArrangements = arrange(testModel, out courseArrangementNeedToDos);
            Assert.AreEqual(classroomArrangements.Where(p => p.roomId == 1).FirstOrDefault().courseArrangingId, "YuWen03");
            Assert.AreEqual(classroomArrangements.Where(p => p.roomId == 2).FirstOrDefault().courseArrangingId, "YuWen01");
            Assert.AreEqual(courseArrangementNeedToDos.Count, 1);
        }

        /// <summary>
        /// 教室授课类型与排课一对一匹配优先
        /// </summary>
         [TestMethod]
        public void TestTeachTypeEqual()
        {
            var testModel = new ClassroomArrangeParameterModel()
            {
                ArrangeDate = ArrangeDate,
                OccupiedClassroomArrangementEntities = new List<D_3.Models.Entities.ClassroomArrangementEntity>() { },
                ClassroomsEntities = new List<ClassroomEntity>() {
                new ClassroomEntity(){
                     roomId=1,
                      
                       campusCode="SH001",
                        venueId="S01",
                         TeachType= D_3.Models.ETeachType.Group,
                          TeachRange=new D_3.Models.ETeachType[]{
                            D_3.Models.ETeachType.V1
                          }
                },
                new ClassroomEntity(){
                     roomId=2,
                      
                       campusCode="SH001",
                        venueId="S01",
                         TeachType= D_3.Models.ETeachType.Group,
                          TeachRange=new D_3.Models.ETeachType[]{
                           D_3.Models.ETeachType.V1,
                           D_3.Models.ETeachType.Group
                          }
                }
            },
                CourseArrangementsEntities = new List<CourseArrangementEntity>()
            {            new CourseArrangementEntity(){
                 courseArrangingId=1,
                   dtLessonEndReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 10:00:00"),
                    dtPKDateTime=DateTime.Now.AddMinutes(2),
                     onClassCampusCode="SH001",
                      onClassVenueId="S01",
                       dtLessonBeginReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 8:00:00"),
                        sTeacherCode="T01",
                         teachType= ETeachType.V1//进教室2
                }
            }
            };
            List<CourseArrangementQueueEntity> courseArrangementNeedToDos;
            var classroomArrangements = arrange(testModel, out courseArrangementNeedToDos);
            Assert.AreEqual(classroomArrangements.Where(p => p.roomId == 1).FirstOrDefault().courseArrangingId, "YuWen01");
            Assert.AreEqual(classroomArrangements.Where(p => p.roomId == 2).Count(), 0);
        }

        /// <summary>
        /// 教室已经给老师有过排课的教室，优先进入
        /// </summary>
         [TestMethod]
        public void TestHasSiblingCourse()
        {
            var testModel = new ClassroomArrangeParameterModel()
            {
                ArrangeDate = ArrangeDate,
                OccupiedClassroomArrangementEntities = new List<D_3.Models.Entities.ClassroomArrangementEntity>() { },
                ClassroomsEntities = new List<ClassroomEntity>() {
                new ClassroomEntity(){
                     roomId=1,
                      
                       campusCode="SH001",
                        venueId="S01",
                         TeachType= D_3.Models.ETeachType.Group,
                          TeachRange=new D_3.Models.ETeachType[]{
                            D_3.Models.ETeachType.V1,
                            D_3.Models.ETeachType.V2
                          }
                },
                new ClassroomEntity(){
                     roomId=2,
                      
                       campusCode="SH001",
                        venueId="S01",
                         TeachType= D_3.Models.ETeachType.Group,
                          TeachRange=new D_3.Models.ETeachType[]{
                           D_3.Models.ETeachType.V1,
                           D_3.Models.ETeachType.Group
                          }
                }
            },
                CourseArrangementsEntities = new List<CourseArrangementEntity>()
            {            new CourseArrangementEntity(){
                             courseArrangingId=1,
                               dtLessonEndReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 10:00:00"),
                                dtPKDateTime=DateTime.Now.AddMinutes(0),
                                 onClassCampusCode="SH001",
                                  onClassVenueId="S01",
                                   dtLessonBeginReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 8:00:00"),
                                    sTeacherCode="T01",
                                     teachType= ETeachType.Group//进教室2
                        },
                         new CourseArrangementEntity(){
                               courseArrangingId=2,
                               dtLessonEndReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 11:00:00"),
                                dtPKDateTime=DateTime.Now.AddMinutes(2),
                                 onClassCampusCode="SH001",
                                  onClassVenueId="S01",
                                   dtLessonBeginReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 10:00:00"),
                                    sTeacherCode="T01",
                                     teachType= ETeachType.V1//进教室2
                        },new CourseArrangementEntity(){
                              courseArrangingId=3,
                               dtLessonEndReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 11:00:00"),
                                dtPKDateTime=DateTime.Now.AddMinutes(1),
                                 onClassCampusCode="SH001",
                                  onClassVenueId="S01",
                                   dtLessonBeginReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 10:00:00"),
                                    sTeacherCode="T02",
                                     teachType= ETeachType.V1//进教室1
                        }
            }
            };
            List<CourseArrangementQueueEntity> courseArrangementNeedToDos;
            var classroomArrangements = arrange(testModel, out courseArrangementNeedToDos);
            Assert.AreEqual(classroomArrangements.Where(p => p.roomId == 2).Count(), 2);
            Assert.AreEqual(classroomArrangements.Where(p => p.roomId == 1).FirstOrDefault().courseArrangingId, "YuWen03");
        }

        /// <summary>
        /// 测试教室优先属性
        /// </summary>
         [TestMethod]
        public void TestIsTop()
        {
            var testModel = new ClassroomArrangeParameterModel()
            {
                ArrangeDate = ArrangeDate,
                OccupiedClassroomArrangementEntities = new List<D_3.Models.Entities.ClassroomArrangementEntity>() { },
                ClassroomsEntities = new List<ClassroomEntity>() {
                new ClassroomEntity(){
                     roomId=1,
                      
                       campusCode="SH001",
                        venueId="S01",
                         TeachType= D_3.Models.ETeachType.Group,
                          TeachRange=new D_3.Models.ETeachType[]{
                            D_3.Models.ETeachType.V1,
                            D_3.Models.ETeachType.V2
                          }
                },
                new ClassroomEntity(){
                     roomId=2,
                      
                       campusCode="SH001",
                        venueId="S01",
                         TeachType= D_3.Models.ETeachType.Group,
                          TeachRange=new D_3.Models.ETeachType[]{
                           D_3.Models.ETeachType.V1,
                           D_3.Models.ETeachType.Group
                          },Priority=1
                }
            },
                CourseArrangementsEntities = new List<CourseArrangementEntity>()
            {            new CourseArrangementEntity(){
                             courseArrangingId=1,
                               dtLessonEndReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 10:00:00"),
                                dtPKDateTime=DateTime.Now.AddMinutes(0),
                                 onClassCampusCode="SH001",
                                  onClassVenueId="S01",
                                   dtLessonBeginReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 8:00:00"),
                                    sTeacherCode="T01",
                                     teachType= ETeachType.V1//进教室2
                        } 
            }
            };
            List<CourseArrangementQueueEntity> courseArrangementNeedToDos;
            var classroomArrangements = arrange(testModel,out courseArrangementNeedToDos);
            Assert.AreEqual(classroomArrangements.Where(p => p.roomId == 2).Count(),1);
            Assert.AreEqual(classroomArrangements.Where(p => p.roomId == 1).Count(),0);
        }

        public List<ClassroomArrangementEntity> arrange(ClassroomArrangeParameterModel testData, out List<CourseArrangementQueueEntity> courseArrangementNeedToDos)
        {
            var arrangeDate = testData.ArrangeDate;
            //排课情况
            var courseArrangementEntities = testData.CourseArrangementsEntities;
            //教室配备
            var classroomEntities = testData.ClassroomsEntities;
            //教室已排班情况
            var classroomArrangementEntities = testData.OccupiedClassroomArrangementEntities;
            //排课排序
            var sortedCourseArrangement = new CourseArranger().Arrange(courseArrangementEntities);
            List<LogSortedClassroomEntity> logSortedClassroomEntities;
            //结果
            return new ClassroomArranger(arrangeDate, sortedCourseArrangement, classroomEntities, classroomArrangementEntities).Arrange(out courseArrangementNeedToDos,out logSortedClassroomEntities);
        }
    }
}
