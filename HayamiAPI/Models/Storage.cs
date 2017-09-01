using System;
using System.Collections.Generic;

namespace HayamiAPI.Models
{
    public class Storage
    {
        public int StorageID { get; set; }
        public string StorageName { get; set; }
        public int StorageCapacity { get; set; }
        public int StorageStock { get; set; }
        public int StoragePrior { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdDate { get; set; }
        public string UpdatedBy { get; set; }
        
        // Relationship definition
        public ICollection<ProductDt> ProductDts { get; set; }
    }
}