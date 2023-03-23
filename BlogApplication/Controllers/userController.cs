using BlogApplication.Models;
using BlogApplication.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}
