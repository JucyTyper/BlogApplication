using Azure;
using BlogApplication.Data;
using BlogApplication.Models;
using ChatApp.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
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
                    var checkTag = _db.Tags.Where(x=>x.TagName == tag.tagName && x.isDeleted == false).ToList();
                    if(checkTag.Count() ==0)
                    {
                        var tagEntity = new TagsModel
                        {
                            TagName = tag.tagName,
                        };
                        _db.Tags.Add(tagEntity);
                        var tagMap = new BlogTagMap
                        {
                            tagId = tagEntity.TagId,
                            blogId = _blog.blogId
                        };
                        _db.blogTagMaps.Add(tagMap);
                    }
                    else
                    {
                        var tagMap = new BlogTagMap
                        {
                            tagId = checkTag.First().TagId,
                            blogId = _blog.blogId
                        };
                        _db.blogTagMaps.Add(tagMap);
                    }
                }
                _db.SaveChanges();
                response.Message = "Blog Added";
                response.Data = _blog;
                return response;
            }
            catch(Exception ex)
            {
                response.StatusCode = 500;
                response.Message = ex.StackTrace!;
                response.IsSuccess = false;
                return response;
            }
        }
        public object GetBlogs(Guid id, string searchString,Guid createrId)
        {
            try
            {
                List<string> tagNames = new List<string>();
                var resdata = new List<GetBlogModel>();
                var tagId = _db.Tags.Where(x => (x.TagName == searchString|| searchString == string.Empty)&&(x.isDeleted == false)).Select(x => x.TagId).ToList();
                if(tagId.Count() > 0) 
                {
                    var tags = _db.blogTagMaps.Where(x => x.tagId == tagId.First() && x.isDeleted == false).Select(x => x.blogId).ToList();
                    if (tags.Count > 0)
                    {
                        foreach (var bid in tags)
                        {
                            var tagblog = _db.Blogs.Where(x => x.blogId == bid).Select(x => x).First();
                            var blogTag = _db.blogTagMaps.Where(x => x.blogId == bid && x.isDeleted == false).Select(x => x.tagId).ToList();
                            
                            foreach (var TagId in blogTag)
                            {
                                var tagName = _db.Tags.Where(x => x.TagId == TagId).Select(x => x.TagName).First();
                                tagNames.Add(tagName);
                            }
                            var creator = _db.users.Where(x => x.UserId == tagblog.createrId).Select(x => x).First();
                            var getBlog = new GetBlogModel
                            {
                                blog = tagblog,
                                tags = tagNames,
                                getUser = new GetUserModel(creator)
                            };
                            resdata.Add(getBlog);
                        }
                    }
                }
                var titleBlog = _db.Blogs.Where(x => (x.blogId == id || id == Guid.Empty)&& (x.blogId == createrId || createrId == Guid.Empty) && (x.isDeleted == false) && ((EF.Functions.Like(x.title, "%" + searchString + "%") || searchString == string.Empty))).Select(x => x).OrderByDescending(x => x.createdAt).ToList();
                foreach (var blog in titleBlog)
                {
                    var blogTag = _db.blogTagMaps.Where(x => x.blogId == blog.blogId && x.isDeleted == false).Select(x => x.tagId).ToList();
                    foreach (var TagId in blogTag)
                    {
                        var tagName = _db.Tags.Where(x => x.TagId == TagId).Select(x => x.TagName).First();
                        tagNames.Add(tagName);
                    }
                    var creator = _db.users.Where(x => x.UserId == blog.createrId).Select(x => x).First();
                    var getBlog = new GetBlogModel
                    {
                        blog = blog,
                        tags = tagNames,
                        getUser = new GetUserModel(creator)
                    };
                    resdata.Add(getBlog);
                }
                if (resdata.Count == 0)
                {
                    response.StatusCode = 404;
                    response.Message = "No Blog Found";
                    response.IsSuccess = false;
                    return response;
                }
                response.Message = " searched blogs";
                response.Data = resdata;
                return response;
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = ex.StackTrace!;
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
                if (blog.tags.Count != 0)
                {
                    foreach(var tag in blog.tags)
                    {
                        var tags = _db.Tags.Where(x => x.TagName == tag.tagName && x.isDeleted == false).Select(x => x).ToList();
                        if(tags.Count() != 0)
                        {
                            var tagMap = _db.blogTagMaps.Where(x => x.tagId == tags.First().TagId && x.blogId == id).ToList();
                            if(tagMap.Count() == 0)
                            {
                                var blogTagMap = new BlogTagMap
                                {
                                    tagId = tags.First().TagId,
                                    blogId = id
                                };
                                _db.blogTagMaps.Add(blogTagMap);
                            }
                        }
                        else
                        {
                            var tagEntity = new TagsModel
                            {
                                TagName = tag.tagName,
                            };
                            _db.Tags.Add(tagEntity);
                            var tagMap = new BlogTagMap
                            {
                                tagId = tagEntity.TagId,
                                blogId = id
                            };
                            _db.blogTagMaps.Add(tagMap);
                        }
                        _db.SaveChanges();
                    }
                }
                _blog!.updatedAt = DateTime.Now;
                _db.SaveChanges();
                var creator = _db.users.Where(x => x.UserId == _blog.createrId).Select(x => x).First();
                List<string> tagNames = new List<string>();
                var blogTag = _db.blogTagMaps.Where(x => x.blogId == _blog.blogId && x.isDeleted == false).Select(x => x.tagId).ToList();
                foreach (var TagId in blogTag)
                {
                    var tagName = _db.Tags.Where(x => x.TagId == TagId).Select(x => x.TagName).First();
                    tagNames.Add(tagName);
                }
                var getBlog = new GetBlogModel
                {
                    blog = _blog!,
                    tags = tagNames,
                    getUser = new GetUserModel(creator)
                };
                response.Message = "Blog updated successfully";
                response.Data = getBlog;
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
                var _blog = _db.Blogs.Where(x => x.blogId == id && x.isDeleted==false).ToList();
                if(_blog.Count == 0)
                {
                    response.StatusCode = 404;
                    response.Message = "No Blog Found";
                    response.IsSuccess = false;
                    return response;
                }
                _blog!.First().isDeleted = true;
                var blogTag = _db.blogTagMaps.Where(x => x.blogId == id).Select(x=>x);
                foreach (var TagId in blogTag)
                {
                    TagId.isDeleted = true;
                }
                _db.SaveChanges();
                response.Message = "Blog Deleted successfully";
                response.Data = "Blog Id " + id;
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
                var _blog = _db.Blogs.Where(x => x.createrId == id && x.isDeleted == false).Select(x=>x).ToList();
                var creator = _db.users.Where(x => x.UserId == id).Select(x => x).First();
                foreach (var blog in _blog)
                {
                    var blogTag = _db.blogTagMaps.Where(x => x.blogId == blog.blogId).Select(x => x.tagId).ToList();
                    List<string> tagsList = new List<string>();
                    foreach (var tagId in blogTag)
                    {
                        var tagName = _db.Tags.Where(x => x.TagId == tagId).Select(x => x.TagName).ToString();
                        tagsList.Add(tagName!);
                    }
                    var getBlog = new GetBlogModel
                    {
                        blog = blog,
                        tags = tagsList,
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
        public ResponseModel likeAndDislike(string Id, int type)
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
        public ResponseModel GetFamousTags() 
        {
            try
            {
                var tagsId = _db.blogTagMaps.Where(x=>x.isDeleted == false).GroupBy(x => x.tagId).OrderByDescending(g => g.Count()).Take(5).Select(g => g.Key).ToList();
                List<string> tagsList = new List<string>();
                foreach(var tagId in tagsId)
                {
                    var tagName = _db.Tags.Where(x => x.TagId == tagId).Select(x=>x.TagName).ToString();
                    tagsList.Add(tagName!);
                }
                response.Message = "Famous Tags";
                response.Data = tagsList;
                return response;
            }
            catch(Exception ex)
            {
                response.StatusCode = 500;
                response.Message = ex.Message;
                response.IsSuccess = false;
                return response;
            }
        }
        public ResponseModel RecommendedBlogs()
        {
            try
            {
                var tagByBlog = _db.Blogs.OrderBy(n => Guid.NewGuid()).Take(5);
                response.Message = "Random Blogs";
                response.Data = tagByBlog;
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
