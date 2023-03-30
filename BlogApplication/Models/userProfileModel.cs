namespace ChatApp.Models
{
    public class userProfileModel
    {
        public Guid UserId { get; set; } 
        public string Email { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public long PhoneNo { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string ProfileImagePath { get; set; } = string.Empty;

    }
}
