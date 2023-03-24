using System.ComponentModel.DataAnnotations;

namespace BlogApplication.Models
{
    public class BlogImageModel
    {
        [Key]
        public Guid imageId { get; set; } = Guid.NewGuid();
        public string imageName { get; set; } = string.Empty;
        public Guid blogId { get; set; } = Guid.Empty;
        public string imagePath { get; set; } = string.Empty;
    }
}
