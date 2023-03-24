using System.ComponentModel.DataAnnotations;

namespace BlogApplication.Models
{
    public class BlackListTokenModel
    {
        [Key]
        public Guid tokenId { get; set; } = Guid.NewGuid();
        public string token { get; set; } = string.Empty;
    }
}
