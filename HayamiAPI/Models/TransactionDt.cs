using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HayamiAPI.Models
{
    public class TransactionDt
    {
        [Key]
        public int TransDtID { get; set; }
        public int TransHdID { get; set; }
        public int ProductHdID { get; set; }
        public string ProductSize { get; set; }
        public decimal TotalPrice { get; set; }
        public int Qty { get; set; }
        public int QtyOri { get; set; }
        public int ReceiveQty { get; set; }
        [MaxLength(1)]
        public string AddDiscountType { get; set; }
        public decimal AddDiscountValue { get; set; }
        public string AddDiscountDesc { get; set; }
        public int DiscountID { get; set; }
        public DateTime CreatedAt { get; set; }
        public string RejectDesc { get; set; }
        public DateTime ActionDate { get; set; }
        [MaxLength(1)]
        public string FgStatus { get; set; }
        [MaxLength(1)]
        public string FgStatusStr { get; set; }
        public DateTime UpdDate { get; set; }
        public string UpdatedBy { get; set; }

        [ForeignKey("TransHdID")]
        public TransactionHd TransactionHd { get; set; }
        //[ForeignKey("DiscountID")]
        //public Discount Dicsount { get; set; }
    }
}