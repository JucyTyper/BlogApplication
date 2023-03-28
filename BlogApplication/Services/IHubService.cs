using BlogApplication.Models;

namespace BlogApplication.Services
{
    public interface IHubService
    {
        public ResponseModel likeAndDistlike(string Id, int type);
    }
}
