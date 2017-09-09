using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace HayamiAPI.Models
{
    public class ProductHd
    {
        public ProductHd()
        {
            ProductDts = new List<ProductDt>();
        }

        public int ProductHdID { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ProductDesc { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdDate { get; set; }
        public string UpdatedBy { get; set; }
        public string ImagePath1 { get; set; }
        public string ImagePath2 { get; set; }
        public string ImagePath3 { get; set; }
        public string ImagePath4 { get; set; }
        public string ImagePath5 { get; set; }
        public int TypeID { get; set; }
        public int ModelID { get; set; }
        
        // Relationship definition
        [ForeignKey("TypeID")]
        public Type Type { get; set; }
        [ForeignKey("ModelID")]
        public Model Model { get; set; }
        public virtual ICollection<ProductDt> ProductDts { get; set; }
        public virtual ICollection<TransactionDt> TransactionDts { get; set; }
        
    }
}