using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace HayamiAPI.Models
{
    public class TransactionReturHd
    {
        [Key]
        public int TransReturHdID { get; set; }
        public int CounterID { get; set; }
        [MaxLength(25)]
        public string TransReturNo { get; set; }
        [MaxLength(1)]
        public string ReturStatus { get; set; }
        [MaxLength(255)]
        public string ReturDesc { get; set; }
        public DateTime? ActionDate { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdDate { get; set; }
        public string UpdatedBy { get; set; }

        public virtual ICollection<TransactionReturDt> TransactionReturDts { get; set; }
    }
}