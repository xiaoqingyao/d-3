using D_3.Arranger;
using D_3.DataSource;
using D_3.Models;
using D_3.Models.Business;
using D_3.Models.Entities;
using D3.Models.Entities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace D_3Tester
{
    /// <summary>
    /// �����ſ����
    /// </summary>
    [TestClass]
    public class CourseArrangeTest
    {


        public DateTime ArrangeDate = DateTime.Now;
        /// <summary>
        /// ���Կγ��������ȼ� group>v2>v1
        /// </summary>
        [TestMethod]
        public void TestGroupV2V1()
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
                        steacherCode="T01",
                         nTutorType= ETeachType.Group
                },new CourseArrangementEntity(){
                 courseArrangingId=2,
                   dtLessonEndReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 10:00:00"),
                    dtPKDateTime=DateTime.Now.AddDays(new Random().Next(1,4)),
                     onClassCampusCode="SH001",
                      onClassVenueId="S01",
                       dtLessonBeginReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 8:00:00"),
                        steacherCode="T01",
                         nTutorType= ETeachType.V2
                },new CourseArrangementEntity(){
                courseArrangingId=3,
                   dtLessonEndReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 10:00:00"),
                    dtPKDateTime=DateTime.Now.AddDays(new Random().Next(1,4)),
                     onClassCampusCode="SH001",
                      onClassVenueId="S01",
                       dtLessonBeginReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 8:00:00"),
                        steacherCode="T01",
                         nTutorType= ETeachType.V1
                }

            }
            };
            List<CourseArrangementQueueEntity> courseArrangementNeedToDos;
            var classroomArrangements = arrange(testModel, out courseArrangementNeedToDos);
            //�ж�
            Assert.AreEqual(classroomArrangements.Count, 1);
            Assert.AreEqual(courseArrangementNeedToDos.Count, 2);
            Assert.AreEqual(classroomArrangements[0].courseId, 1);
        }

        /// <summary>
        /// ��׼���ڷǱ�׼
        /// </summary>
        [TestMethod]
        public void StandardGraterthanNoStardard()
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
                        steacherCode="T01",
                         nTutorType= ETeachType.Group
                },new CourseArrangementEntity(){
                 courseArrangingId=2,
                   dtLessonEndReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 9:00:00"),
                    dtPKDateTime=DateTime.Now.AddDays(new Random().Next(1,4)),
                     onClassCampusCode="SH001",
                      onClassVenueId="S01",
                       dtLessonBeginReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 7:00:00"),
                        steacherCode="T01",
                         nTutorType= ETeachType.V2
                }
            }
            };
            List<CourseArrangementQueueEntity> courseArrangementNeedToDos;
            var classroomArrangements = arrange(testModel, out courseArrangementNeedToDos);
            //�ж�
            Assert.AreEqual(classroomArrangements.Count, 1);
            Assert.AreEqual(courseArrangementNeedToDos.Count, 1);
            Assert.AreEqual(classroomArrangements[0].courseId, 1);
        }

        /// <summary>
        /// ��׼���ڷǱ꣨��ʹ������
        /// </summary>
        [TestMethod]
        public void StandardGraterthanNoStardardSerial()
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
                        steacherCode="T01",
                         nTutorType= ETeachType.Group
                },new CourseArrangementEntity(){
                 courseArrangingId=2,
                   dtLessonEndReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 8:00:00"),
                    dtPKDateTime=DateTime.Now.AddDays(new Random().Next(1,4)),
                     onClassCampusCode="SH001",
                      onClassVenueId="S01",
                       dtLessonBeginReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 7:00:00"),
                        steacherCode="T01",
                         nTutorType= ETeachType.V2
                },new CourseArrangementEntity(){
                courseArrangingId=3,
                   dtLessonEndReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 9:00:00"),
                    dtPKDateTime=DateTime.Now.AddDays(new Random().Next(1,4)),
                     onClassCampusCode="SH001",
                      onClassVenueId="S01",
                       dtLessonBeginReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 8:00:00"),
                        steacherCode="T01",
                         nTutorType= ETeachType.V1
                }
            }
            };
            List<CourseArrangementQueueEntity> courseArrangementNeedToDos;
            var classroomArrangements = arrange(testModel, out courseArrangementNeedToDos);
            //�ж�
            Assert.AreEqual(classroomArrangements.Count, 2);
            Assert.AreEqual(courseArrangementNeedToDos.Count, 1);
            Assert.AreEqual(courseArrangementNeedToDos[0].courseArrangingId, 3);
        }
        /// <summary>
        /// �����ſ���ռ��
        /// </summary>
        [TestMethod]
        public void TestOccupied()
        {
            var testModel = new ClassroomArrangeParameterModel()
            {
                ArrangeDate = ArrangeDate,
                OccupiedClassroomArrangementEntities = new List<D_3.Models.Entities.ClassroomArrangementEntity>() {
                     new ClassroomArrangementEntity(){
                         dtPKDateTime= ArrangeDate,
                           courseArrangingId=1,
                            dtLessonEndReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 10:00:00"),
                             dtLessonBeginReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 8:00:00"),
                              courseId=1,
                               campusCode="SH001",
                                 venueId="S01",roomId=1
                     }
                },
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
                        steacherCode="T01",
                         nTutorType= ETeachType.Group
                },new CourseArrangementEntity(){
                 courseArrangingId=2,
                   dtLessonEndReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 10:00:00"),
                    dtPKDateTime=DateTime.Now.AddDays(new Random().Next(1,4)),
                     onClassCampusCode="SH001",
                      onClassVenueId="S01",
                       dtLessonBeginReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 8:00:00"),
                        steacherCode="T01",
                         nTutorType= ETeachType.V2
                },new CourseArrangementEntity(){
                courseArrangingId=3,
                   dtLessonEndReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 10:00:00"),
                    dtPKDateTime=DateTime.Now.AddDays(new Random().Next(1,4)),
                     onClassCampusCode="SH001",
                      onClassVenueId="S01",
                       dtLessonBeginReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 8:00:00"),
                        steacherCode="T01",
                         nTutorType= ETeachType.V1
                }

            }
            };
            List<CourseArrangementQueueEntity> courseArrangementNeedToDos;
            var classroomArrangements = arrange(testModel, out courseArrangementNeedToDos);
            //�ж�
            Assert.AreEqual(classroomArrangements.Count, 0);
            Assert.AreEqual(courseArrangementNeedToDos.Count, 3);
        }
        /// <summary>
        /// ���4����
        /// </summary>
        [TestMethod]
        public void TestMaxSerial4()
        {
            var testModel = new ClassroomArrangeParameterModel()
            {
                ArrangeDate = ArrangeDate,
                OccupiedClassroomArrangementEntities = new List<D_3.Models.Entities.ClassroomArrangementEntity>()
                {
                },
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
                        steacherCode="T01",
                         nTutorType= ETeachType.Group
                },new CourseArrangementEntity(){
                 courseArrangingId=2,
                   dtLessonEndReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 12:00:00"),
                    dtPKDateTime=DateTime.Now.AddDays(new Random().Next(1,4)),
                     onClassCampusCode="SH001",
                      onClassVenueId="S01",
                       dtLessonBeginReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 10:00:00"),
                        steacherCode="T01",
                         nTutorType= ETeachType.Group
                },new CourseArrangementEntity(){
                courseArrangingId=3,
                   dtLessonEndReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 15:00:00"),
                    dtPKDateTime=DateTime.Now.AddDays(new Random().Next(1,4)),
                     onClassCampusCode="SH001",
                      onClassVenueId="S01",
                       dtLessonBeginReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 13:00:00"),
                        steacherCode="T01",
                         nTutorType= ETeachType.Group
                },new CourseArrangementEntity(){
                courseArrangingId=4,
                   dtLessonEndReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 17:00:00"),
                    dtPKDateTime=DateTime.Now.AddDays(new Random().Next(1,4)),
                     onClassCampusCode="SH001",
                      onClassVenueId="S01",
                       dtLessonBeginReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 15:00:00"),
                        steacherCode="T01",
                         nTutorType= ETeachType.Group
                },new CourseArrangementEntity(){
                courseArrangingId=5,
                   dtLessonEndReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 19:00:00"),
                    dtPKDateTime=DateTime.Now.AddDays(new Random().Next(1,4)),
                     onClassCampusCode="SH001",
                      onClassVenueId="S01",
                       dtLessonBeginReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 17:00:00"),
                        steacherCode="T01",
                         nTutorType= ETeachType.Group
                },new CourseArrangementEntity(){
                courseArrangingId=6,
                   dtLessonEndReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 20:30:00"),
                    dtPKDateTime=DateTime.Now.AddDays(new Random().Next(1,4)),
                     onClassCampusCode="SH001",
                      onClassVenueId="S01",
                       dtLessonBeginReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 19:00:00"),
                        steacherCode="T01",
                         nTutorType= ETeachType.Group
                }

            }
            };

            var arrangeDate = testModel.ArrangeDate;
            //�ſ����
            var courseArrangementEntities = testModel.CourseArrangementsEntities;
            //�����䱸
            var classroomEntities = testModel.ClassroomsEntities;
            //�������Ű����
            var classroomArrangementEntities = testModel.OccupiedClassroomArrangementEntities;
            //�ſ�����
            var sortedCourseArrangement = new CourseArranger().Arrange(courseArrangementEntities);

            //�ж�
            Assert.AreEqual(((CourseArrangementSerial)sortedCourseArrangement[0]).SerialLevel, 4);
            Assert.AreEqual(((CourseArrangementSerial)sortedCourseArrangement[1]).SerialLevel, 2);
        }

        /// <summary>
        /// ����������ڱ�
        /// </summary>
        [TestMethod]
        public void TestSerialStandardGreaterthanStandard()
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
                }
            },
                CourseArrangementsEntities = new List<CourseArrangementEntity>()
            {        new CourseArrangementEntity(){
                 courseArrangingId=2,
                   dtLessonEndReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 12:00:00"),
                    dtPKDateTime=DateTime.Now.AddDays(new Random().Next(1,4)),
                     onClassCampusCode="SH001",
                      onClassVenueId="S01",
                       dtLessonBeginReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 10:00:00"),
                        steacherCode="T01",
                         nTutorType= ETeachType.Group
                },new CourseArrangementEntity(){
                courseArrangingId=3,
                   dtLessonEndReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 10:00:00"),
                    dtPKDateTime=DateTime.Now.AddDays(new Random().Next(1,4)),
                     onClassCampusCode="SH001",
                      onClassVenueId="S01",
                       dtLessonBeginReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 8:00:00"),
                        steacherCode="T02",
                         nTutorType= ETeachType.Group
                },    new CourseArrangementEntity(){
                 courseArrangingId=4,
                   dtLessonEndReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 10:00:00"),
                    dtPKDateTime=DateTime.Now.AddDays(new Random().Next(1,4)),
                     onClassCampusCode="SH001",
                      onClassVenueId="S01",
                       dtLessonBeginReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 8:00:00"),
                        steacherCode="T01",
                         nTutorType= ETeachType.Group
                }

            }
            };
            List<CourseArrangementQueueEntity> courseArrangementNeedToDos;
            var classroomArrangements = arrange(testModel, out courseArrangementNeedToDos);
            //�ж�
            Assert.AreEqual(classroomArrangements.Count, 1);
            Assert.AreEqual(courseArrangementNeedToDos.Count, 1);
            Assert.AreEqual(classroomArrangements[0].courseId, 4);
        }

        /// <summary>
        /// �����������ϴ����������
        /// </summary>
        [TestMethod]
        public void TestSerialLevelGreaterThanSerial()
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
                }
            },
                CourseArrangementsEntities = new List<CourseArrangementEntity>()
            {            new CourseArrangementEntity(){
                 courseArrangingId=1,
                   dtLessonEndReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 15:00:00"),
                    dtPKDateTime=DateTime.Now.AddDays(new Random().Next(1,4)),
                     onClassCampusCode="SH001",
                      onClassVenueId="S01",
                       dtLessonBeginReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 13:00:00"),
                        steacherCode="T01",
                         nTutorType= ETeachType.Group
                },new CourseArrangementEntity(){
                 courseArrangingId=2,
                   dtLessonEndReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 17:00:00"),
                    dtPKDateTime=DateTime.Now.AddDays(new Random().Next(1,4)),
                     onClassCampusCode="SH001",
                      onClassVenueId="S01",
                       dtLessonBeginReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 15:00:00"),
                        steacherCode="T01",
                         nTutorType= ETeachType.Group
                } ,     new CourseArrangementEntity(){
                courseArrangingId=3,
                   dtLessonEndReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 15:00:00"),
                    dtPKDateTime=DateTime.Now.AddDays(new Random().Next(1,4)),
                     onClassCampusCode="SH001",
                      onClassVenueId="S01",
                       dtLessonBeginReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 13:00:00"),
                        steacherCode="T02",
                         nTutorType= ETeachType.Group
                },new CourseArrangementEntity(){
                 courseArrangingId=4,
                   dtLessonEndReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 17:00:00"),
                    dtPKDateTime=DateTime.Now.AddDays(new Random().Next(1,4)),
                     onClassCampusCode="SH001",
                      onClassVenueId="S01",
                       dtLessonBeginReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 15:00:00"),
                        steacherCode="T02",
                         nTutorType= ETeachType.Group
                },new CourseArrangementEntity(){
                 courseArrangingId=5,
                   dtLessonEndReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 19:00:00"),
                    dtPKDateTime=DateTime.Now.AddDays(new Random().Next(1,4)),
                     onClassCampusCode="SH001",
                      onClassVenueId="S01",
                       dtLessonBeginReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 17:00:00"),
                        steacherCode="T02",
                         nTutorType= ETeachType.Group
                }

            }
            };
            List<CourseArrangementQueueEntity> courseArrangementNeedToDos;
            var classroomArrangements = arrange(testModel, out courseArrangementNeedToDos);
            //�ж�
            Assert.AreEqual(classroomArrangements.Count, 1);
            Assert.AreEqual(courseArrangementNeedToDos.Count, 1);
            Assert.AreEqual(classroomArrangements[0].courseId, 3);
        }

        /// <summary>
        /// ��ͬ�����������������
        /// </summary>
        [TestMethod]
        public void TestSerialLevelCompare()
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
                }
            },
                CourseArrangementsEntities = new List<CourseArrangementEntity>()
            {
                      new CourseArrangementEntity(){
                 courseArrangingId=1,
                   dtLessonEndReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 15:00:00"),
                    dtPKDateTime=DateTime.Now.AddMinutes(1),
                     onClassCampusCode="SH001",
                      onClassVenueId="S01",
                       dtLessonBeginReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 13:00:00"),
                        steacherCode="T01",
                         nTutorType= ETeachType.Group
                },new CourseArrangementEntity(){
                 courseArrangingId=2,
                   dtLessonEndReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 17:00:00"),
                    dtPKDateTime=DateTime.Now.AddMinutes(1),
                     onClassCampusCode="SH001",
                      onClassVenueId="S01",
                       dtLessonBeginReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 15:00:00"),
                        steacherCode="T01",
                         nTutorType= ETeachType.Group
                },new CourseArrangementEntity(){
                courseArrangingId=3,
                   dtLessonEndReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 19:00:00"),
                    dtPKDateTime=DateTime.Now.AddMinutes(1),
                     onClassCampusCode="SH001",
                      onClassVenueId="S01",
                       dtLessonBeginReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 17:00:00"),
                        steacherCode="T01",
                         nTutorType= ETeachType.Group
                },
                    new CourseArrangementEntity(){
                 courseArrangingId=4,
                   dtLessonEndReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 15:00:00"),
                     dtPKDateTime=DateTime.Now.AddMinutes(2),
                     onClassCampusCode="SH001",
                      onClassVenueId="S01",
                       dtLessonBeginReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 13:00:00"),
                        steacherCode="T02",
                         nTutorType= ETeachType.Group
                },new CourseArrangementEntity(){
                 courseArrangingId=5,
                   dtLessonEndReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 17:00:00"),
                      dtPKDateTime=DateTime.Now.AddMinutes(2),
                     onClassCampusCode="SH001",
                      onClassVenueId="S01",
                       dtLessonBeginReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 15:00:00"),
                        steacherCode="T02",
                         nTutorType= ETeachType.Group
                },new CourseArrangementEntity(){
                 courseArrangingId=6,
                   dtLessonEndReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 19:00:00"),
                     dtPKDateTime=DateTime.Now.AddMinutes(0),
                     onClassCampusCode="SH001",
                      onClassVenueId="S01",
                       dtLessonBeginReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 17:00:00"),
                        steacherCode="T02",
                         nTutorType= ETeachType.Group
                }

            }
            };
            List<CourseArrangementQueueEntity> courseArrangementNeedToDos;
            var classroomArrangements = arrange(testModel, out courseArrangementNeedToDos);
            //�ж�
            Assert.AreEqual(classroomArrangements.Count, 1);
            Assert.AreEqual(courseArrangementNeedToDos.Count, 1);
            Assert.AreEqual(classroomArrangements[0].courseId, 4);
        }

        /// <summary>
        /// �ſ�ʱ��
        /// </summary>
        [TestMethod]
        public void TestOperateDateCompare()
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
                }
            },
                CourseArrangementsEntities = new List<CourseArrangementEntity>()
            {
                      new CourseArrangementEntity(){
                 courseArrangingId=1,
                   dtLessonEndReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 15:00:00"),
                    dtPKDateTime=DateTime.Now.AddMinutes(3),
                     onClassCampusCode="SH001",
                      onClassVenueId="S01",
                       dtLessonBeginReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 13:00:00"),
                        steacherCode="T01",
                         nTutorType= ETeachType.Group
                },new CourseArrangementEntity(){
                 courseArrangingId=4,
                   dtLessonEndReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 15:00:00"),
                     dtPKDateTime=DateTime.Now.AddMinutes(2),
                     onClassCampusCode="SH001",
                      onClassVenueId="S01",
                       dtLessonBeginReal=DateTime.Parse($"{ArrangeDate.ToString("yyyy-MM-dd")} 13:00:00"),
                        steacherCode="T02",
                         nTutorType= ETeachType.Group
                }
            }
            };
            List<CourseArrangementQueueEntity> courseArrangementNeedToDos;
            var classroomArrangements = arrange(testModel, out courseArrangementNeedToDos);
            //�ж�
            Assert.AreEqual(classroomArrangements.Count, 1);
            Assert.AreEqual(courseArrangementNeedToDos.Count, 1);
            Assert.AreEqual(classroomArrangements[0].courseId, 4);
        }


        //todo d-3֮�ڵģ�������Ű�ļ�¼�г����Ӵ�������
        //todo d-3֮�ڵģ����������ſΣ�����d-3��û�еĻ����������


        public List<ClassroomArrangementEntity> arrange(ClassroomArrangeParameterModel testData, out List<CourseArrangementQueueEntity> courseArrangementNeedToDos)
        {
            var arrangeDate = testData.ArrangeDate;
            //�ſ����
            var courseArrangementEntities = testData.CourseArrangementsEntities;
            //�����䱸
            var classroomEntities = testData.ClassroomsEntities;
            //�������Ű����
            var classroomArrangementEntities = testData.OccupiedClassroomArrangementEntities;
            //�ſ�����
            var sortedCourseArrangement = new CourseArranger().Arrange(courseArrangementEntities);
            List<LogSortedClassroomEntity> logSortedClassroomEntities;
            //���
            return new ClassroomArranger(arrangeDate, sortedCourseArrangement, classroomEntities, classroomArrangementEntities).Arrange(out courseArrangementNeedToDos, out logSortedClassroomEntities);
        }
    }
}