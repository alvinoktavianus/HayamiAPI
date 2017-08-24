using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HayamiAPI.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public string UserRole { get; set; }
        public string UserImg { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdDate { get; set; }
        public string UserToken { get; set; }
        [Index(IsUnique = true), MaxLength(100)]
        public string UserEmail { get; set; }
    }
}