using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HayamiAPI.Models
{
    public class ProductDt
    {
        public int ProductDtID { get; set; }
        public string ProductSize { get; set; }
        public int ProductQty { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdDate { get; set; }
        public string UpdatedBy { get; set; }
        public int ProductHdID { get; set; }

        // Relationship definition
        [ForeignKey("ProductHdID")]
        public virtual ProductHd ProductHd { get; set; }
    }
}