using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HayamiAPI.Models
{
    public class TransactionReturDt
    {
        [Key]
        public int TransReturDtID { get; set; }
        public int TransReturHdID { get; set; }
        public int ProductHdID { get; set; }
        [MaxLength(10)]
        public string ProductSize { get; set; }
        public int ReturQty { get; set; }
        [MaxLength(1)]
        public string ReturType { get; set; }
        [MaxLength(1)]
        public string ReturStatus { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdDate { get; set; }
        [MaxLength(50)]
        public string UpdatedBy { get; set; }

        [ForeignKey("TransReturHdID")]
        public TransactionReturHd transactionReturHd { get; set; }
    }
}