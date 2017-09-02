using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HayamiAPI.Models
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public string CustCode { get; set; }
        public string CustName { get; set; }
        public string CustAddr { get; set; }
        public string CustCity { get; set; }
        public string CustPosCode { get; set; }
        public string CustPhone { get; set; }
        [Index(IsUnique = true), MaxLength(100)]
        public string CustEmail { get; set; }
        public string CustExp { get; set; }
        public string CustDesc { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdDate { get; set; }
        public string UpdatedBy { get; set; }
        public int CounterID { get; set; }

        // Relationship definition
        //[ForeignKey("CounterID")]
        //public Counter Counter { get; set; }

    }
}