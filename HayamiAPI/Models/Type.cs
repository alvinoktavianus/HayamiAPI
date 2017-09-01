using System;
using System.Collections.Generic;

namespace HayamiAPI.Models
{
    public class Type
    {
        public int TypeID { get; set; }
        public string TypeName { get; set; }
        public decimal TypePrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdDate { get; set; }
        public string UpdatedBy { get; set; }

        // Relationship definition
        public virtual ICollection<ProductHd> ProductHds { get; set; }
    }
}