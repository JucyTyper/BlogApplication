namespace BlogApplication.Models
{
    public class UpdateBlogModel
    {
        public string title { get; set; } = string.Empty;
        public string content { get; set; } = string.Empty;
        public string previewImage { get; set; } = string.Empty;
        public List<GetTags> tags { get; set; } = new List<GetTags>();
    }
}
