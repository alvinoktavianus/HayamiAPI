using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HayamiAPI.Models
{
    public class Discount
    {
        public int DiscountID { get; set; }
        public int DiscDivide { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdDate { get; set; }
        public string UpdatedBy { get; set; }
        public int CustomerID { get; set; }
        
        // Relationship definition
        [ForeignKey("CustomerID")]
        public Customer Customer { get; set; }
    }
}