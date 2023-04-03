using BlogApplication.Models;
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
            return StatusCode(response.statusCode, response);
        }
        [HttpGet]
        public IActionResult GetBlog(Guid id, string? searchString)
        {
            var response = blogService.GetBlogs(id, searchString!);
            return StatusCode(response.statusCode, response);
        }
        [HttpPut]
        [Authorize]
        public IActionResult UpdateBlog(Guid id, [FromBody] UpdateBlogModel blog)
        {
            var response = blogService.updateBlog(id,blog);
            return StatusCode(response.statusCode, response);
        }
        [HttpDelete]
        [Authorize]
        public IActionResult DeteteBlog(Guid id)
        {
            var response = blogService.DeleteBlog(id);
            return StatusCode(response.statusCode, response);
        }
        [HttpGet]
        [Route("myBlogs")]
        [Authorize]
        public IActionResult GetMyBlog()
        {
            var user1 = HttpContext.User;
            var id = user1.FindFirst(ClaimTypes.Sid)?.Value;
            var response = blogService.GetMyBlogs(id!);
            return StatusCode(response.statusCode, response);
        }
        [HttpGet]
        [Route("famousTags")]
        public IActionResult GetFamousTags()
        {
            var response = blogService.GetFamousTags();
            return StatusCode(response.statusCode, response);
        }
        [HttpGet]
        [Route("randomBlogs")]
        public IActionResult RandomBlogs()
        {
            var response = blogService.RecommendedBlogs();
            return StatusCode(response.statusCode,response);
        }
        [HttpGet]
        [Route("userBlogs")]
        [Authorize(Roles = "Admin")]
        public IActionResult userBlog(Guid id)
        {
            var response = blogService.GetMyBlogs(id.ToString()!);
            return StatusCode(response.statusCode, response);
        }
        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("blockBlog")]
        public IActionResult BlockBlog(Guid id,int type)
        {
            var response = blogService.BlockBlog(id,type);
            return StatusCode(response.statusCode, response);
        }
    }
}
