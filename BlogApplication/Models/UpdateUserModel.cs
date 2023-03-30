namespace BlogApplication.Models
{
    public class UpdateUserModel
    {
        public string firstName { get; set; } = string.Empty;
        public string lastName { get; set; } = string.Empty;
        public long phoneNo { get; set; }
        public DateTime dateOfBirth { get; set; } = DateTime.Now;
    }
}
