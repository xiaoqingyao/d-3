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
        
    }
}
