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
        /// ���Կγ��������ȼ� group>v2>v1
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
            //�ж�
            Assert.AreEqual(classroomArrangements.Count, 1);
            Assert.AreEqual(courseArrangementNeedToDos.Count, 2);
            Assert.AreEqual(classroomArrangements[0].CourseId, "YuWen01");
        }

        /// <summary>
        /// ��׼���ڷǱ�׼
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
            //�ж�
            Assert.AreEqual(classroomArrangements.Count, 1);
            Assert.AreEqual(courseArrangementNeedToDos.Count, 1);
            Assert.AreEqual(classroomArrangements[0].CourseId, "YuWen01");
        }

        /// <summary>
        /// ��׼���ڷǱ꣨��ʹ������
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
            //�ж�
            Assert.AreEqual(classroomArrangements.Count, 1);
            Assert.AreEqual(courseArrangementNeedToDos.Count, 2);
            Assert.AreEqual(classroomArrangements[0].CourseId, "YuWen01");
        }
        /// <summary>
        /// �����ſ���ռ��
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
            //�ж�
            Assert.AreEqual(classroomArrangements.Count, 0);
            Assert.AreEqual(courseArrangementNeedToDos.Count, 3);
        }
        /// <summary>
        /// ���4����
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
            //�ſ����
            var courseArrangementEntities = testModel.CourseArrangementsEntities;
            //�����䱸
            var classroomEntities = testModel.ClassroomsEntities;
            //�������Ű����
            var classroomArrangementEntities = testModel.occupiedClassroomArrangementEntities;
            //�ſ�����
            var sortedCourseArrangement = new CourseArranger().Arrange(courseArrangementEntities);

            //�ж�
            Assert.AreEqual(sortedCourseArrangement.Values[0].SerialLevel, 4);
            Assert.AreEqual(sortedCourseArrangement.Values[1].SerialLevel, 2);
        }



        //todo d-3֮�ڵģ�������Ű�ļ�¼�г����Ӵ�������
        //todo d-3֮�ڵģ����������ſΣ�����d-3��û�еĻ����������

        /// <summary>
        /// ִ�������Ű��߼�
        /// </summary>
        /// <returns></returns>
        public (List<ClassroomArrangement>, List<CourseArrangement>) arrange(TestDataModel testData)
        {
            var arrangeDate = testData.ArrangeDate;
            //�ſ����
            var courseArrangementEntities = testData.CourseArrangementsEntities;
            //�����䱸
            var classroomEntities = testData.ClassroomsEntities;
            //�������Ű����
            var classroomArrangementEntities = testData.occupiedClassroomArrangementEntities;
            //�ſ�����
            var sortedCourseArrangement = new CourseArranger().Arrange(courseArrangementEntities);
            //���
            return new ClassroomArranger(arrangeDate, sortedCourseArrangement, classroomEntities, classroomArrangementEntities).Arrange();


        }
    }
}