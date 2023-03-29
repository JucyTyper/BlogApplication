namespace ChatApp.Models
{
    public class UserResponse
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public Guid UserID { get; set; } = Guid.Empty;
        public bool isAdmin { get; set; } = false;
        public string Token { get; set; } = string.Empty;
        public string profilePicPath { get; set; } = string.Empty;  
    }
}
