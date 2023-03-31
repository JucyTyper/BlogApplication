namespace BlogApplication.Models
{
    public class TotalLikeDislikeModel
    {
        public Guid blogId { get; set; }
        public int likes { get; set; } = 0;
        public int dislikes { get; set; } = 0;
        public TotalLikeDislikeModel()
        {

        }
        public TotalLikeDislikeModel(Guid blogId,int likes, int dislikes)
        {
            this.blogId = blogId;
            this.likes = likes;
            this.dislikes = dislikes;
        }
    }
}
