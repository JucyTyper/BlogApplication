using BlogApplication.Data;
using BlogApplication.Models;
using ChatApp.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

namespace BlogApplication.Services
{
    public class BlogService: IBlogService
    {
        private readonly IConfiguration configuration;
        private readonly IPasswordService passwordService;
        private readonly IValidationService validationService;
        private readonly ITokenService tokenService;
        private readonly blogAppDatabase _db;
        // blogAppDatabase blogAppDatabase1 = new blogAppDatabase();
        UserResponse DataOut = new UserResponse();
        ResponseModel response = new ResponseModel();
        public BlogService(blogAppDatabase _db, IConfiguration configuration, IPasswordService passwordService, ITokenService tokenService, IValidationService validationService)
        {
            this.tokenService = tokenService;
            this._db = _db;
            this.configuration = configuration;
            this.passwordService = passwordService;
            this.validationService = validationService;
        }
        public object CreateBlog(string id, AddBlogModel blog)
        {
            try
            {
                var _blog = new BlogModel
                {
                    createrId = new Guid(id),
                    content = blog.content,
                    title= blog.title,
                    previewImage=blog.previewImage
                };
                _db.Blogs.Add(_blog);
                foreach (var tag in blog.tags )
                {
                    var tagEntity = new TagsModel
                    {
                        TagName = tag.tagName,
                        blogId = _blog.blogId
                    };
                    _db.Tags.Add(tagEntity);
                }
                _db.SaveChanges();
                response.Message = "Blog Added";
                response.Data = _blog;
                return response;
            }
            catch(Exception ex)
            {
                response.StatusCode = 500;
                response.Message = ex.Message!;
                response.IsSuccess = false;
                return response;
            }
        }
        public object GetBlogs(Guid id, string searchString)
        {
            try
            {
                var resdata = new List<GetBlogModel>();
                List<Guid> tags = _db.Tags.Where(x => x.TagName == searchString|| searchString == string.Empty).Select(x => x.blogId).ToList();
                if (tags.Count > 0)
                {
                    foreach (var bid in tags)
                    {
                        var tagblog = _db.Blogs.Where(x => x.blogId == bid).Select(x => x).First();
                        var blogTag = _db.Tags.Where(x => x.blogId == bid).Select(x => x.TagName).ToList();
                        var creator = _db.users.Where(x => x.UserId == tagblog.createrId).Select(x => x).First();
                        var getBlog = new GetBlogModel
                        {
                            blog = tagblog,
                            tags = blogTag,
                            getUser = new GetUserModel(creator)
                        };
                        resdata.Add(getBlog);
                    }
                }
                var titleBlog = _db.Blogs.Where(x => (x.blogId == id || id == Guid.Empty) && (x.isDeleted == false) && ((EF.Functions.Like(x.title, "%" + searchString + "%") || searchString == string.Empty))).Select(x => x).OrderByDescending(x => x.createdAt).ToList();
                foreach (var blog in titleBlog)
                {
                    var blogTag = _db.Tags.Where(x => x.blogId == blog.blogId).Select(x => x.TagName).ToList();
                    var creator = _db.users.Where(x => x.UserId == blog.createrId).Select(x => x).First();
                    var getBlog = new GetBlogModel
                    {
                        blog = blog,
                        tags = blogTag,
                        getUser = new GetUserModel(creator)
                    };
                    resdata.Add(getBlog);
                }
                response.Message = " searched blogs";
                response.Data = resdata;
                return response;
                
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = ex.Message!;
                response.IsSuccess = false;
                return response;
            }
        }
        public ResponseModel updateBlog(Guid id,UpdateBlogModel blog)
        {
            try
            {
                var _blog = _db.Blogs.Where(x=>x.blogId == id ).FirstOrDefault();
                if (blog.title != string.Empty)
                    _blog!.title = blog.title;
                if (blog.content != string.Empty)
                    _blog!.content = blog.content;
                if (blog.previewImage != string.Empty)
                    _blog!.previewImage = blog.previewImage;
                _blog!.updatedAt = DateTime.Now;
                _db.SaveChanges();
                response.Message = "Blog updated successfully";
                response.Data = _blog;
                return response;
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = ex.Message!;
                response.IsSuccess = false;
                return response;
            }
        }
        public ResponseModel DeleteBlog(Guid id)
        {
            try
            {
                var _blog = _db.Blogs.Where(x => x.blogId == id).FirstOrDefault();
                _blog!.isDeleted = true;
                _db.SaveChanges();
                response.Message = "Blog updated successfully";
                response.Data = _blog;
                return response;
            }
            catch(Exception ex)
            {
                response.StatusCode = 500;
                response.Message = ex.Message!;
                response.IsSuccess = false;
                return response;
            }
        }
        public ResponseModel GetMyBlogs(string Id)
        {
            try
            {
                var id = new Guid(Id);
                var resdata = new List<GetBlogModel>();
                var _blog = _db.Blogs.Where(x => x.createrId == id).Select(x=>x).ToList();
                var creator = _db.users.Where(x => x.UserId == id).Select(x => x).First();
                foreach (var blog in _blog)
                {
                    var blogTag = _db.Tags.Where(x => x.blogId == blog.blogId).Select(x => x.TagName).ToList();
                    var getBlog = new GetBlogModel
                    {
                        blog = blog,
                        tags = blogTag,
                        getUser = new GetUserModel(creator)
                    };
                    resdata.Add(getBlog);
                }
                response.Message = "My Blogs";
                response.Data = resdata;
                return response;

            }
            catch(Exception ex)
            {
                response.StatusCode = 500;
                response.Message = ex.Message!;
                response.IsSuccess = false;
                return response;
            }
        }
    }
}
