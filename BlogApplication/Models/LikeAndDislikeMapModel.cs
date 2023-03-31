using System.ComponentModel.DataAnnotations;

namespace BlogApplication.Models
{
    public class LikeAndDislikeMapModel
    {
        [Key]
        public Guid activityId { get; set; } = Guid.NewGuid();
        public Guid blogId { get; set; }
        public Guid userId { get; set; }
        public bool isLiked { get; set; } = false;
        public bool isDisliked { get; set; } = false;
    }
}
