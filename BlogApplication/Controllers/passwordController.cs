using BlogApplication.Models;
using BlogApplication.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace BlogApplication.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class passwordController : ControllerBase
    {
        private readonly IPasswordService passwordService;

        public passwordController(IPasswordService passwordService)
        {
            this.passwordService = passwordService;
        }
        [HttpPost]
        [Route("forgetPassword")]
        public IActionResult ForgetPasssword(ForgetPasswordModel mail)
        {
            var response = passwordService.ForgetPassword(mail);
            return Ok(response);
        }

        [HttpPut]
        [Route("changePassword")]
        public IActionResult ChangePassword(ChangePasswordModel repass)
        { 
            var user = HttpContext.User;
            var userId = user.FindFirst(ClaimTypes.Sid)?.Value;
            var response = passwordService.changePassword(userId, repass);
            return Ok(response);
        }
    }
}
