using System.ComponentModel.DataAnnotations;

namespace BlogApplication.Models
{
    public class BlogTagMap
    {
        [Key]
        public Guid mapId { get; set; } = Guid.NewGuid();
        public Guid tagId { get; set; } = new Guid();
        public Guid blogId { get; set; } = new Guid();
        public bool isDeleted { get; set; } = false;
    }
}
