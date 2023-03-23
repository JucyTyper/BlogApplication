using System.ComponentModel.DataAnnotations;

namespace BlogApplication.Models
{
    public class RegisterUserModel
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public long phoneNo { get; set; }
        public DateTime dateOfBirth { get; set; }
    }
}
