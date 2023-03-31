namespace BlogApplication.Models
{
    public class GetUserModel
    {
        public Guid UserId { get; set; } = Guid.NewGuid();
        public string firstName { get; set; } = string.Empty;
        public string lastName { get; set; }= string.Empty;
        public string email { get; set; } = string.Empty;
        public long phoneNo { get; set; } = 0;
        public DateTime dateOfBirth { get; set; } = DateTime.MinValue;
        public string ProfileImagePath { get; set; } = string.Empty;
        public bool isAdmin { get; set; } = false;
        public bool isBlocked { get; set; } = false;
        public GetUserModel() { }
        public GetUserModel(UserModel user)
        {
            this.UserId = user.UserId;
            this.phoneNo = user.phoneNo;
            this.email = user.email;
            this.firstName = user.firstName;
            this.lastName = user.lastName;
            this.dateOfBirth = user.dateOfBirth;
            this.ProfileImagePath= user.ProfileImagePath;
            this.isAdmin = user.isAdmin;
            this.isBlocked = user.isBlocked;
        }
    }
}
