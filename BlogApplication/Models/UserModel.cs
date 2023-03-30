using System.ComponentModel.DataAnnotations;

namespace BlogApplication.Models
{
    public class UserModel
    {
        [Key]
        public Guid UserId { get; set; } = Guid.NewGuid();
        public string firstName { get; set; } = string.Empty;
        public string lastName { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public byte[] password { get; set; } = new byte[] {};
        public long phoneNo { get; set; } = 0;
        public DateTime dateOfBirth { get; set; } = DateTime.Now;
        public DateTime created { get; set; } = DateTime.Now;
        public DateTime updated { get; set; } = DateTime.Now;
        public DateTime lastActive { get; set; } = DateTime.Now;
        public bool isDeleted { get; set; } = false;
        public bool isAdmin { get; set; } = false;
        public string ProfileImagePath { get; set; } = string.Empty;
    }
}

