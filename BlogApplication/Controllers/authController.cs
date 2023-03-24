using BlogApplication.Models;
using BlogApplication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BlogApplication.Controllers
{
    [Route("api/vi/[controller]")]
    [ApiController]
    public class authController : ControllerBase
    {
        private readonly IAuthService AuthService;

        public authController(IAuthService AuthService)
        {
            this.AuthService = AuthService;
        }
        [HttpPost]
        [Route("login")]
        public IActionResult Login(LoginModel model)
        {
            var response = AuthService.loginUser(model);
            return Ok(response);
        }
        [HttpPost]
        [Route("googleLogin")]
        public IActionResult GoogleLogin(GoogleToken token)
        {
            var response = AuthService.GoogleAuth(token.token);
            return Ok(response);
        }
        [HttpPost]
        [Authorize]
        [Route("logout")]
        public IActionResult LogOut()
        {
            string token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last()!;
            var response = AuthService.logout(token);
            return Ok();
        }
    }
}
