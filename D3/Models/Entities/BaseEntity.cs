using System;
using System.Collections.Generic;
using System.Text;

namespace D_3.Models.Entities
{
    public class BaseEntity
    {
        /// <summary>
        /// ID
        /// </summary>
        public Guid GId { get; set; } = Guid.NewGuid();
        public int EId { get; set; }
        //public DateTime CreateDate { get; set; }
        //public DateTime UpdateDate { get; set; }
        //public string Creator { get; set; }
        //public string UpdateUser { get; set; }

    }
}
