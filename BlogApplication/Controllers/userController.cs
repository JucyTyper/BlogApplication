using BlogApplication.Models;
using BlogApplication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogApplication.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class userController : ControllerBase
    {
        private readonly IUserService userService;

        public userController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost]
        [Route("registration")]
        public IActionResult RegisterUser(RegisterUserModel user)
        {
            var response = userService.RegisterUser(user);
            return StatusCode(response.statusCode, response);
        }
        [HttpGet]
        [Authorize]
        [Route("myProfile")]
        public IActionResult userProfile()
        {
            var user = HttpContext.User;
            var id = user.FindFirst(ClaimTypes.Sid)?.Value;
            var response = userService.GetUser(new Guid(id!),null!,null!,1);
            return StatusCode(response.statusCode, response);
        }
        [HttpGet]
        public IActionResult GetUser(Guid id,string? email,string? name,int pageNo)
        {
            var response = userService.GetUser(id,name!,email!,pageNo);
            return StatusCode(response.statusCode, response);
        }
        [HttpPut]
        public IActionResult UpdateUser(Guid id, [FromBody] UpdateUserModel user)
        {
            var response = userService.UpdateUser(id, user);
            return StatusCode(response.statusCode, response);
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("removeNotice")]
        public IActionResult removeNotice(Guid Id)
        {
            var response = userService.RemoveNotice(Id);
            return StatusCode(response.statusCode, response);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("GetNotice")]
        public IActionResult getNotice()
        {
            var response = userService.GetNotice();
            return StatusCode(response.statusCode, response);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("blockUser")]
        public IActionResult BlockUser(Guid Id)
        {
            var response = userService.BlockUser(Id);
            return StatusCode(response.statusCode, response);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("unblockUser")]
        public IActionResult UnBlockUser(Guid Id)
        {
            var response = userService.UnblockUser(Id);
            return StatusCode(response.statusCode, response);
        }
    }
}
