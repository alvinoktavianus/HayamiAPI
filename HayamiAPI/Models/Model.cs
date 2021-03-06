﻿using System;
using System.Collections.Generic;

namespace HayamiAPI.Models
{
    public class Model
    {
        public int ModelID { get; set; }
        public string ModelName { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdDate { get; set; }
        public string UpdatedBy { get; set; }
        
        public ICollection<ProductHd> ProductHds { get; set; }
    }
}