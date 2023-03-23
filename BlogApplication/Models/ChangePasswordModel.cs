namespace BlogApplication.Models
{
    public class ChangePasswordModel
    {
        public string newPassword { get; set; } = string.Empty;
        public string oldPassword { get; set; } = string.Empty;
    }
}
