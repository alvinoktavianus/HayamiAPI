using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HayamiAPI.Models
{
    public class TransactionHd
    {
        public TransactionHd()
        {
            TransactionDts = new List<TransactionDt>();
        }

        [Key]
        public int TransHdID { get; set; }
        public string TransNo { get; set; }
        public DateTime TransDate { get; set; }
        public int CounterID { get; set; }
        public int CustomerID { get; set; }
        [MaxLength(1)]
        public string FgStatus { get; set; }
        public decimal TotalDiscount { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdDate { get; set; }
        public string UpdatedBy { get; set; }

        //[ForeignKey("CounterID")]
        //public Counter Counter { get; set; }
        //[ForeignKey("CustomerID")]
        //public Customer Customer { get; set; }
        public virtual ICollection<TransactionDt> TransactionDts { get; set; }
    }
}