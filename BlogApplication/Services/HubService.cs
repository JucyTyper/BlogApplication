using Azure;
using BlogApplication.Data;
using BlogApplication.Models;
using ChatApp.Models;

namespace BlogApplication.Services
{
    public class HubService :IHubService
    {
        private readonly IConfiguration configuration;
        private readonly IPasswordService passwordService;
        private readonly IValidationService validationService;
        private readonly ITokenService tokenService;
        private readonly blogAppDatabase _db;
        // blogAppDatabase blogAppDatabase1 = new blogAppDatabase();
        UserResponse DataOut = new UserResponse();
        ResponseModel response = new ResponseModel();
        public HubService(blogAppDatabase _db, IConfiguration configuration, IPasswordService passwordService, ITokenService tokenService, IValidationService validationService)
        {
            this.tokenService = tokenService;
            this._db = _db;
            this.configuration = configuration;
            this.passwordService = passwordService;
            this.validationService = validationService;
        }
        public ResponseModel likeAndDistlike(string Id,int type)
        {
            try
            {
                var blog = _db.Blogs.Where(x => x.blogId == new Guid(Id)).FirstOrDefault();
                if (type == 1)
                {
                    blog!.likes = blog.likes + 1;
                    response.Data = blog.likes;
                }
                else if (type == 2)
                {
                    blog!.dislikes = blog.dislikes + 1;
                    response.Data = blog.dislikes;
                }
                _db.SaveChanges();
                response.Message = "like and dislike status updated";
                return response;
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = ex.Message;
                response.IsSuccess = false;
                return response;
            }
        }
    }
}
