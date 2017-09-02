using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HayamiAPI.Models
{
    public class Counter
    {
        public Counter()
        {
            CreatedAt = DateTime.Now;
            UpdDate = DateTime.Now;
        }

        public int CounterID { get; set; }
        public string CounterName { get; set; }
        public string CounterAddr { get; set; }
        public string CounterCity { get; set; }
        public string CounterPosCode { get; set; }
        public string CounterPhone { get; set; }
        [Index(IsUnique = true), MaxLength(100)]
        public string CounterEmail { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdDate { get; set; }
        public string UpdatedBy { get; set; }
        
    }
}