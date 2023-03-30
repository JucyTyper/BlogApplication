using Azure;
using BlogApplication.Data;
using BlogApplication.Models;
using ChatApp.Models;

namespace BlogApplication.Services
{
    public class HubService :IHubService
    {
        private readonly blogAppDatabase _db;
        public HubService(blogAppDatabase _db)
        {
            this._db = _db;
        }
        // ------------------- A Function to Like and Dislike blogs ------->>
        public ResponseModel likeAndDistlike(string Id,int type)
        {
            try
            {
                //Fething blog
                var blog = _db.Blogs.Where(x => x.blogId == new Guid(Id)).FirstOrDefault();
                if (type == 1)
                {
                    // Increasing Like
                    blog!.likes = blog.likes + 1;
                    _db.SaveChanges();
                    return new ResponseModel(blog.likes);
                }
                else
                {
                    // Increasing Dislike
                    blog!.dislikes = blog.dislikes + 1;
                    _db.SaveChanges();
                    return new ResponseModel(blog.dislikes);
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel(500, ex.Message, false);
            }
        }
        //---------- A Function to get active notices ---------->>
        public ResponseModel GetNotice()
        {
            try
            {
                //Fetching notices
                var Notices = _db.notices.Where(x => x.CreationTime.AddDays(1) > DateTime.Now  && x.isDeleted == false).Select(x => x).ToList();
                //Checking if notices exist
                if (Notices.Count() == 0)
                {
                    return new ResponseModel(404, "Notice Not found", false);
                }
                return new ResponseModel("Active Notice",Notices);
            }
            catch (Exception ex)
            {
                return new ResponseModel(500, ex.Message, false);
            }
        }
    }
}
