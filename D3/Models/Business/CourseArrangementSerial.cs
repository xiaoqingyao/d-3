using System;
using System.Collections.Generic;
using System.Text;

namespace D_3.Models.Business
{
    /// <summary>
    /// 连续的排课
    /// </summary>
    public class CourseArrangementSerial : CourseArrangement
    {
        

        /// <summary>
        /// 下一节课
        /// </summary>
        public CourseArrangementSerial Next { get; set; }


    }
}
