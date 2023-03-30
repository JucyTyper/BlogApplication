using System.ComponentModel.DataAnnotations;

namespace BlogApplication.Models
{
    public class RegisterUserModel
    {
        public string firstName { get; set; } = string.Empty;
        public string lastName { get; set; } = string.Empty;
        public string email { get; set; } = string.Empty;
        public string password { get; set; }= string.Empty;
        public long phoneNo { get; set; }
        public DateTime dateOfBirth { get; set; }
    }
}
