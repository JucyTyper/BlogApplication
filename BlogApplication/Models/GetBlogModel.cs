namespace BlogApplication.Models
{
    public class GetBlogModel
    {
        public BlogModel blog { get; set; } = new BlogModel();
        public List<string> tags { get; set; } = new List<string>();
        public GetUserModel? getUser { get; set; } 
    }
}
