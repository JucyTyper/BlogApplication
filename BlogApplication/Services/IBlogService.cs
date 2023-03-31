using BlogApplication.Models;

namespace BlogApplication.Services
{
    public interface IBlogService
    {
        public ResponseModel CreateBlog(string id ,AddBlogModel blog);
        public ResponseModel GetBlogs(Guid id, string searchString);
        public ResponseModel updateBlog(Guid id, UpdateBlogModel blog);
        public ResponseModel DeleteBlog(Guid id);
        public ResponseModel GetMyBlogs(string id);
        public ResponseModel GetFamousTags();
        public ResponseModel RecommendedBlogs();
        public ResponseModel UnblockBlog(Guid blogId);
        public ResponseModel BlockBlog(Guid blogId);
    }
}
