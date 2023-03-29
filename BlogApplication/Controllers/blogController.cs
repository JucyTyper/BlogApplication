﻿using BlogApplication.Models;
using BlogApplication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class blogController : ControllerBase
    {
        private readonly IBlogService blogService;

        public blogController(IBlogService blogService)
        {
            this.blogService = blogService;
        }

        [HttpPost]
        [Authorize]
        public IActionResult CreateBlog(AddBlogModel blog)
        {
            var user1 = HttpContext.User;
            var id = user1.FindFirst(ClaimTypes.Sid)?.Value;
            var response = blogService.CreateBlog(id!,blog);
            return Ok(response);
        }
        [HttpGet]
        public IActionResult GetBlog(Guid id, string? searchString, Guid createrId)
        {
            var response = blogService.GetBlogs(id, searchString!,createrId);
            return Ok(response);
        }
        [HttpPut]
        public IActionResult UpdateBlog(Guid id, [FromBody] UpdateBlogModel blog)
        {
            var response = blogService.updateBlog(id,blog);
            return Ok(response);
        }
        [HttpDelete]
        public IActionResult DeteteBlog(Guid id)
        {
            var response = blogService.DeleteBlog(id);
            return Ok(response);
        }
        [HttpGet]
        [Route("myBlogs")]
        [Authorize]
        public IActionResult GetMyBlog()
        {
            var user1 = HttpContext.User;
            var id = user1.FindFirst(ClaimTypes.Sid)?.Value;
            var response = blogService.GetMyBlogs(id!);
            return Ok(response);
        }
        [HttpPost]
        [Route("likeAndDislike")]
        public IActionResult likeAndDislike([FromBody] LikeDislikeModel type)
        {
            var response = blogService.likeAndDislike(type.id, type.type);
            return Ok(response);
        }
        [HttpGet]
        [Route("famousTags")]
        public IActionResult GetFamousTags()
        {
            var response = blogService.GetFamousTags();
            return Ok(response);
        }
        [HttpGet]
        [Route("randomBlogs")]
        public IActionResult RandomBlogs()
        {
            var response = blogService.RecommendedBlogs();
            return Ok(response);
        }
    }
}
