using System;
using System.Collections.Generic;
using System.Text;

namespace D_3.Models.Entities
{
    /// <summary>
    /// 教室配置信息
    /// </summary>
    public class ClassroomEntity : BaseEntity
    {
        /// <summary>
        /// 序号
        /// </summary>
        public int id { get; set; } 
        /// <summary>
        /// 校区
        /// </summary>
        public string campusCode { get; set; }
        /// <summary>
        /// 教学点
        /// </summary>
        public string venueId { get; set; }
        /// <summary>
        /// 教室id
        /// </summary>
        public int roomId { get; set; }
        /// <summary>
        /// 教室教学属性标签
        /// </summary>
        public string roomAttributeId { get; set; }
        /// <summary>
        /// 是否启用启动排课
        /// </summary>
        public bool canArrange { get; set; }
        /// <summary>
        /// 教室编号
        /// </summary>
        public string roomCode { get; set; }
        /// <summary>
        /// 可容纳人数
        /// </summary>
        public int capacityNum { get; set; }
        /// <summary>
        /// 教室属性
        /// </summary>
        public ETeachType TeachType { get; set; }
        /// <summary>
        /// 可教学范围
        /// </summary>
        public ETeachType[] TeachRange { get; set; }
        /// <summary>
        /// 是否是专属教室
        /// </summary>
        public bool IsExclusive { get; set; }
        /// <summary>
        /// 所属老师
        /// </summary>
        public string exclusiveTeacherCode { get; set; }
        /// <summary>
        /// 是否优先 1是 0否
        /// </summary>
        public int Priority { get; set; }
        /// <summary>
        /// 楼层
        /// </summary>
        public string floor { get; set; }
        /// <summary>
        /// 备注
        /// </summary>
        public string remark { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime createTime { get; set; }
        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime updateTime { get; set; }
    }
}
