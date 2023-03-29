namespace BlogApplication.Models
{
    public class UpdateUserModel
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public long phoneNo { get; set; }
        public DateTime dateOfBirth { get; set; } = DateTime.Now;
    }
}
