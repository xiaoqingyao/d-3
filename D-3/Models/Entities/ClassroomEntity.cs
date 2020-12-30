using System;
using System.Collections.Generic;
using System.Text;

namespace D_3.Models.Entities
{
    /// <summary>
    /// 教室配置信息
    /// </summary>
    public class ClassroomEntity: BaseEntity
    {
        public string ClassroomId { get; set; }
        /// <summary>
        /// 校区
        /// </summary>
        public string SchoolId { get; set; }
        /// <summary>
        /// 教学点
        /// </summary>
        public string SpaceId { get; set; }
        /// <summary>
        /// 教室编号
        /// </summary>
        public string ClassroomCode { get; set; }
        /// <summary>
        /// 教室属性
        /// </summary>
        public ETeachType TeachType { get; set; }
        /// <summary>
        /// 可教学范围
        /// </summary>
        public ETeachType[] TeachRange { get; set; }
    }
}
