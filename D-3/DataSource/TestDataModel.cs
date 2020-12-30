using D_3.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace D_3.DataSource
{
    /// <summary>
    /// 测试数据模板
    /// </summary>
    public  class TestDataModel
    {
        public  DateTime ArrangeDate { get; set; }
        
        /// <summary>
        /// 排课记录数据源
        /// </summary>
        public  List<CourseArrangementEntity> CourseArrangementsEntities = new List<CourseArrangementEntity>()
        {
        };
        /// <summary>
        /// 教室配备数据源
        /// </summary>
        public  List<ClassroomEntity> ClassroomsEntities = new List<ClassroomEntity>()
        {
        };
        /// <summary>
        /// 当期已排班情况
        /// </summary>
        public  List<ClassroomArrangementEntity> occupiedClassroomArrangementEntities = new List<ClassroomArrangementEntity>()
        {
        };

    }
}

