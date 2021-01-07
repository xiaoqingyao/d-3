using D_3.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace D_3.DataSource
{
    /// <summary>
    /// D-3数据处理入参模板
    /// </summary>
    public class ClassroomArrangeParameterModel
    {
        
        public ClassroomArrangeParameterModel() {
            //todo 要删除空构造方法
        }
        public ClassroomArrangeParameterModel(DateTime arrangeDate, IEnumerable<CourseArrangementEntity> courseArrangementsEntities, IEnumerable<ClassroomEntity> classroomEntities, IEnumerable<ClassroomArrangementEntity> occupiedClassroomArrangementEntities)
        {
            if (courseArrangementsEntities == null || courseArrangementsEntities.Count() == 0)
            {
                throw new Exception("待排课程不能为空");
            }
            if (classroomEntities == null || classroomEntities.Count() == 0)
            {
                throw new Exception("可用教室不能为空");
            }
            this.ArrangeDate = arrangeDate;
            this.CourseArrangementsEntities = courseArrangementsEntities;
            this.ClassroomsEntities = classroomEntities;
            OccupiedClassroomArrangementEntities = occupiedClassroomArrangementEntities;
        }
        /// <summary>
        /// 处理日期
        /// </summary>
        public DateTime ArrangeDate { get; set; }

        /// <summary>
        /// 排课记录数据源
        /// </summary>
        public IEnumerable<CourseArrangementEntity> CourseArrangementsEntities { get; set; }
        /// <summary>
        /// 教室配备数据源
        /// </summary>
        public IEnumerable<ClassroomEntity> ClassroomsEntities { get; set; }
        /// <summary>
        /// 当期已排班情况
        /// </summary>
        public IEnumerable<ClassroomArrangementEntity> OccupiedClassroomArrangementEntities = new List<ClassroomArrangementEntity>();

    }
}

