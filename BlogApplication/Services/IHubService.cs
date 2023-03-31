using BlogApplication.Models;

namespace BlogApplication.Services
{
    public interface IHubService
    {
        public ResponseModel GetNotice();
        public ResponseModel likeAndDistlike(string blogId, string userId, int type);
    }
}
