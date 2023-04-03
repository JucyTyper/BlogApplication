using BlogApplication.Data;
using BlogApplication.Models;
using Microsoft.EntityFrameworkCore;


namespace BlogApplication.Services
{
    public class BlogService: IBlogService
    {
        private readonly blogAppDatabase _db;
        // calling Constructor
        public BlogService(blogAppDatabase _db)
        {
            this._db = _db;
        }
        // ---------------- A Function to create blog ----------->>
        public ResponseModel CreateBlog(string id, AddBlogModel blog)
        {
            try
            {
                var user = _db.users.Where(x => x.UserId == new Guid(id)).ToList();
                if(user.Count == 0 ||user!.First().isBlocked == true)
                {
                    return new ResponseModel(404,"user is either blocked or not exist",false);
                }
                //creating a blog bodel and populating it 
                var _blog = new BlogModel
                {
                    createrId = new Guid(id),
                    content = blog.content,
                    title= blog.title,
                    previewImage=blog.previewImage
                };
                //Adding in DattaBase
                _db.Blogs.Add(_blog);
                //Adding Tags in the blogs
                foreach (var tag in blog.tags )
                {
                    //Checking if tag alredy exist or not
                    var checkTag = _db.Tags.Where(x=>x.TagName == tag.tagName && x.isDeleted == false).ToList();
                    //Creating A new Tag
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
                    //Adding Existing Tag
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
                // Saving Data in Database
                _db.SaveChanges();
                return new ResponseModel("Blog Added ", _blog);
            }
            catch(Exception ex)
            {
                return new ResponseModel(500, ex.Message, false);
            }
        }
        // -------------- A Function To Get Blogs ------------>>
        public ResponseModel GetBlogs(Guid id, string searchString)
        {
            try
            {
                var resdata = new List<GetBlogModel>();
                //Getting All tagID corresponding to Given TagName
                var tagId = _db.Tags.Where(x => (x.TagName == searchString|| searchString == string.Empty)&&(x.isDeleted == false)).Select(x => x.TagId).ToList();
                if(tagId.Count() > 0) 
                {
                    //getting all blog ids corresponding to the tag id
                    var tags = _db.blogTagMaps.Where(x => x.tagId == tagId.First() && x.isDeleted == false).Select(x => x.blogId).ToList();
                    if (tags.Count > 0)
                    {
                        //Getting All Blogs corresponding to tags
                        foreach (var bid in tags)
                        {
                            //fetching blogs
                            var tagblog = _db.Blogs.Where(x => x.blogId == bid && x.isDeleted == false && x.isBlocked == false).Select(x => x).First();
                            //Fetching All tags
                            var blogTag = _db.blogTagMaps.Where(x => x.blogId == bid && x.isDeleted == false).Select(x => x.tagId).ToList();
                            List<string> tagNames = new List<string>();
                            //Getting Tags
                            foreach (var TagId in blogTag)
                            {
                                var tagName = _db.Tags.Where(x => x.TagId == TagId).Select(x => x.TagName).First();
                                tagNames.Add(tagName);
                            }
                            //Gating creator of the blog
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
                // Getting Blogs based on name and id and email
                var titleBlog = _db.Blogs.Where(x => (x.blogId == id || id == Guid.Empty) && (x.isDeleted == false) && x.isBlocked == false && ((EF.Functions.Like(x.title, "%" + searchString + "%") || searchString == string.Empty))).Select(x => x).OrderByDescending(x => x.createdAt).ToList();
                if (titleBlog.Count == 0)
                {
                    return new ResponseModel(404, "Blog Not Found", false);
                }
                foreach (var blog in titleBlog)
                {
                    //Getting all tag maps
                    var blogTag = _db.blogTagMaps.Where(x => x.blogId == blog.blogId && x.isDeleted == false).Select(x => x.tagId).ToList();
                    List<string> tagNames = new List<string>();
                    //getting all tags of blog
                    foreach (var TagId in blogTag)
                    {
                        var tagName = _db.Tags.Where(x => x.TagId == TagId).Select(x => x.TagName).First();
                        tagNames.Add(tagName);
                    }
                    //getting creator data 
                    var creator = _db.users.Where(x => x.UserId == blog.createrId).Select(x => x).First();
                    var getBlog = new GetBlogModel
                    {
                        blog = blog,
                        tags = tagNames,
                        getUser = new GetUserModel(creator)
                    };
                    resdata.Add(getBlog);
                }
                //if no blog exist of asked details
                if (resdata.Count == 0)
                {
                    return new ResponseModel(404, "Blog Not Found", false);
                }
                return new ResponseModel("Searched Blogs", resdata);
            }
            catch (Exception ex)
            {
                return new ResponseModel(500, ex.Message, false);
            }
        }
        //------------------- A Function To Update Blogs ---------------->>
        public ResponseModel updateBlog(Guid id,UpdateBlogModel blog)
        {
            try
            {
                // Getting blog to update
                var _blog = _db.Blogs.Where(x=>x.blogId == id && x.isDeleted == false && x.isBlocked == false ).FirstOrDefault();
                //Updating title
                if (blog.title != string.Empty)
                    _blog!.title = blog.title;
                //Updating content
                if (blog.content != string.Empty)
                    _blog!.content = blog.content;
                //Updating preview Image
                if (blog.previewImage != string.Empty)
                    _blog!.previewImage = blog.previewImage;
                //Updating Tags
                if (blog.tags.Count != 0)
                {
                    foreach(var tag in blog.tags)
                    {
                        //Checking if tags exist
                        var tags = _db.Tags.Where(x => x.TagName == tag.tagName && x.isDeleted == false).Select(x => x).ToList();
                        if(tags.Count() != 0)
                        {
                            //Getting tag maps
                            var tagMap = _db.blogTagMaps.Where(x => x.tagId == tags.First().TagId && x.blogId == id).ToList();
                            //adding tags
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
                            //Creating tags
                            var tagEntity = new TagsModel
                            {
                                TagName = tag.tagName,
                            };
                            _db.Tags.Add(tagEntity);
                            //creating tag map 
                            var tagMap = new BlogTagMap
                            {
                                tagId = tagEntity.TagId,
                                blogId = id
                            };
                            //adding tag map in database
                            _db.blogTagMaps.Add(tagMap);
                        }
                        _db.SaveChanges();
                    }
                }
                // updation time
                _blog!.updatedAt = DateTime.Now;
                _db.SaveChanges();
                // Getting creator of the blog
                var creator = _db.users.Where(x => x.UserId == _blog.createrId).Select(x => x).First();
                List<string> tagNames = new List<string>();
                var blogTag = _db.blogTagMaps.Where(x => x.blogId == _blog.blogId && x.isDeleted == false).Select(x => x.tagId).ToList();
                //creating response
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
                return new ResponseModel("Blog updated successfully", getBlog);
            }
            catch (Exception ex)
            {
                return new ResponseModel(500, ex.Message, false);
            }
        }
        //------------------- A function to Delete Blogs ---------------->>
        public ResponseModel DeleteBlog(Guid id)
        {
            try
            {
                // Getting Blog
                var _blog = _db.Blogs.Where(x => x.blogId == id && x.isDeleted==false ).ToList();
                // Checking if blog exist
                if(_blog.Count == 0)
                {
                    return new ResponseModel(404, "Blog Not Found", false);
                }
                //Deleting blog
                _blog!.First().isDeleted = true;
                // Deleting Corresponding tag maps
                var blogTag = _db.blogTagMaps.Where(x => x.blogId == id).Select(x=>x);
                foreach (var TagId in blogTag)
                {
                    TagId.isDeleted = true;
                }
                // Saving data 
                _db.SaveChanges();
                return new ResponseModel("Blog Deleted successfully");
            }
            catch(Exception ex)
            {
                return new ResponseModel(500, ex.Message, false);
            }
        }
        // -------------------- A function to get users Blog ---------->>
        public ResponseModel GetMyBlogs(string Id)
        {
            try
            {
                var id = new Guid(Id);
                var resdata = new List<GetBlogModel>();
                //Fething blogs
                var _blog = _db.Blogs.Where(x => x.createrId == id && x.isDeleted == false ).Select(x=>x).ToList();
                // Geting user Data
                var creator = _db.users.Where(x => x.UserId == id).Select(x => x).First();
                //for each blog getting tags
                foreach (var blog in _blog)
                {
                    var blogTag = _db.blogTagMaps.Where(x => x.blogId == blog.blogId).Select(x => x.tagId).ToList();
                    List<string> tagsList = new List<string>();
                    // creating a response view of the blog 
                    foreach (var tagId in blogTag)
                    {
                        // fetching all tags
                        var tagName = _db.Tags.Where(x => x.TagId == tagId).Select(x => x.TagName).ToList();
                        tagsList.Add(tagName!.First());
                    }
                    var getBlog = new GetBlogModel
                    {
                        blog = blog,
                        tags = tagsList,
                        getUser = new GetUserModel(creator)
                    };
                    // adding all blogs in list
                    resdata.Add(getBlog);
                }
                return new ResponseModel("My Blogs", resdata);
            }
            catch(Exception ex)
            {
                return new ResponseModel(500, ex.Message, false);
            }
        }
        //------------------ A Function to Get Famous Tags -------------->>
        public ResponseModel GetFamousTags() 
        {
            try
            {
                // Getting tags based on no. of occurances
                var tagsId = _db.blogTagMaps.Where(x=>x.isDeleted == false).GroupBy(x => x.tagId).OrderByDescending(g => g.Count()).Take(5).Select(g => g.Key).ToList();
                // Adding tag name in list
                List<string> tagsList = new List<string>();
                foreach(var tagId in tagsId)
                {
                    var tagName = _db.Tags.Where(x => x.TagId == tagId).Select(x=>x.TagName).ToList();
                    tagsList.Add(tagName.First());
                }
                return new ResponseModel("Famous Tags", tagsList);
            }
            catch(Exception ex)
            {
                return new ResponseModel(500, ex.Message, false);
            }
        }
        // ---------------- A Function to get Recommended Blogs ----------------->>
        public ResponseModel RecommendedBlogs()
        {
            try
            {
                // getting blogs
                var tagByBlog = _db.Blogs.Where(x=>x.isDeleted == false && x.isBlocked == false).OrderBy(n => Guid.NewGuid()).Take(5);
                return new ResponseModel("Recommended blogs", tagByBlog);
            }
            catch (Exception ex)
            {
                return new ResponseModel(500, ex.Message, false);
            }
        }
        // ------------- A function to block blog----------->>
        public ResponseModel BlockBlog(Guid blogId,int type)
        {
            try
            {
                // Fetching user
                var Blogs = _db.Blogs.Where(x => x.blogId == blogId && x.isDeleted == false ).Select(x => x).ToList();
                // Checking if blog exist
                if (Blogs.Count() == 0)
                {
                    return new ResponseModel(404, "Blog Not found", false);
                }
                if (type == 1)
                {
                    if (Blogs.First().isBlocked == true)
                    {
                        return new ResponseModel("blog already Blocked");
                    }
                    //Blocking blog
                    Blogs.First().isBlocked = true;
                    _db.SaveChanges();
                    // Returning response
                    return new ResponseModel("blog Blocked");
                }
                else
                {
                    var user = _db.users.Where(x => x.UserId == Blogs.First().createrId && x.isDeleted == false && x.isBlocked == false ).Select(x => x).ToList();
                    //Checking if user is blocked
                    if (user.Count() == 0)
                    {
                        return new ResponseModel("Blog creater is blocked");
                    }
                    //checking if user in already uncblocked
                    if (Blogs.First().isBlocked == false)
                    {
                        return new ResponseModel("blog already unblocked");
                    }
                    //Blocking blog
                    Blogs.First().isBlocked = false;
                    _db.SaveChanges();
                    // Returning response
                    return new ResponseModel("blog Blocked");
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel(500, ex.Message, false);
            }
        }
    }
}