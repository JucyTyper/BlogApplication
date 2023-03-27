﻿using BlogApplication.Models;

namespace BlogApplication.Services
{
    public interface IBlogService
    {
        public object CreateBlog(string id ,AddBlogModel blog);
        public object GetBlogs(Guid id, string searchString);
        public ResponseModel updateBlog(Guid id, UpdateBlogModel blog);
        public ResponseModel DeleteBlog(Guid id);
        public ResponseModel GetMyBlogs(string id);
    }
}