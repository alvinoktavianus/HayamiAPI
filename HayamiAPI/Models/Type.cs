using System;

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
    }
}