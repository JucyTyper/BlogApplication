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
            return Ok(response);
        }
        [HttpGet]
        [Authorize]
        [Route("myProfile")]
        public IActionResult userProfile()
        {
            var user = HttpContext.User;
            var id = user.FindFirst(ClaimTypes.Sid)?.Value;
            var response = userService.GetUser(new Guid(id!),null!,null!);
            return Ok(response);
        }
        [HttpGet]
        public IActionResult GetUser(Guid id,string? email,string? name)
        {
            var response = userService.GetUser(id,name!,email!);
            return Ok(response);
        }
    }
}
