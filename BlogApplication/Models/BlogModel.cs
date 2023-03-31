using System.ComponentModel.DataAnnotations;

namespace BlogApplication.Models
{
    public class BlogModel
    {
        [Key]
        public Guid blogId { get; set; } = Guid.NewGuid();
        public Guid createrId { get; set; } = Guid.Empty;
        public string title { get; set; } = string.Empty;
        public DateTime createdAt { get; set; } = DateTime.Now;
        public DateTime updatedAt { get; set; } = DateTime.Now;
        public string content { get; set; } = string.Empty;
        public string previewImage { get; set; } = string.Empty;
        public int likes { get; set; } = 0;
        public int dislikes { get; set; } = 0;
        public int views { get; set; } = 0;
        public bool isBlocked { get; set; } = false;
        public bool isDeleted { get; set; } = false;
    }
}
