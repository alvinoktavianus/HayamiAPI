using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace HayamiAPI.Models
{
    [Table("Types")]
    public class Types
    {
        public int TypeID { get; set; }
        public string TypeName { get; set; }
        public decimal TypePrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdDate { get; set; }
        public string UpdatedBy { get; set; }
    }
}