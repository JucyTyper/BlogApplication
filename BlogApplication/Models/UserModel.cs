using System.ComponentModel.DataAnnotations;

namespace BlogApplication.Models
{
    public class UserModel
    {
        [Key]
        public Guid UserId { get; set; } = Guid.NewGuid();
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public byte[] password { get; set; }
        public long phoneNo { get; set; }
        public DateTime dateOfBirth { get; set; }
        public DateTime created { get; set; } = DateTime.Now;
        public DateTime updated { get; set; } = DateTime.Now;
        public DateTime lastActive { get; set; } = DateTime.Now;
        public bool isDeleted { get; set; } = false;
        public string ProfileImagePath { get; set; } = string.Empty;
    }
}

