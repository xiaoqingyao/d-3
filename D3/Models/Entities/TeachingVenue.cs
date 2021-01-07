using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D3.Models.Entities
{
    /// <summary>
    /// 教学点
    /// </summary>
    public class TeachingVenue
    {
        /// <summary>
        /// 编号
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 校区编码
        /// </summary>
        public string campusCode { get; set; }
        /// <summary>
        /// 教学点唯一标识ID，对照宙斯配置
        /// </summary>
        public int venueId { get; set; }
        /// <summary>
        /// 教学点名称
        /// </summary>
        public string venueName { get; set; }
        /// <summary>
        /// 是否开启规则
        /// </summary>
        public bool isOnRule { get; set; }
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
