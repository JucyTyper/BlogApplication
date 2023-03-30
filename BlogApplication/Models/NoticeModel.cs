using System.ComponentModel.DataAnnotations;

namespace BlogApplication.Models
{
    public class NoticeModel
    {
        [Key]
        public Guid noticeId { get; set; } = Guid.NewGuid();
        public string noticeData { get; set; } = string.Empty;
        public DateTime CreationTime { get; set; } = DateTime.Now;
        public bool isDeleted { get; set; } = false;
        public NoticeModel(Guid noticeId, string noticeData, DateTime creationTime)
        {
            this.noticeId = noticeId;
            this.noticeData = noticeData;
            CreationTime = creationTime;
        }
        public NoticeModel( string noticeData)
        {
            this.noticeData = noticeData;
        }
        public NoticeModel()
        {

        }
    }
}
