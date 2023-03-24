using System.ComponentModel.DataAnnotations;

namespace BlogApplication.Models
{
    public class TagsModel
    {
        [Key]
        public Guid TagId { get; set; } = Guid.NewGuid();
        public string TagName { get; set; } = string.Empty;
        public string blogId { get; set; } = string.Empty;
    }
}
