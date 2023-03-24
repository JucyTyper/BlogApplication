﻿using BlogApplication.Models;
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
        private readonly ITokenService tokenService;
        private readonly IPasswordService passwordService;

        public passwordController(IPasswordService passwordService,ITokenService tokenService)
        {
            this.tokenService = tokenService;
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
        [Authorize]
        [Route("changePassword")]
        public IActionResult ChangePassword(ChangePasswordModel repass)
        {
            string token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last()!;
            var temp = tokenService.CheckToken(token);
            if (temp.IsSuccess == false)
                return Ok(temp);
            var user = HttpContext.User;
            var userId = user.FindFirst(ClaimTypes.Sid)?.Value;
            var response = passwordService.changePassword(userId!, repass);
            return Ok(response);
        }
        [HttpPut]
        [Authorize]
        [Route("resetPassword")]
        public IActionResult ResetPassword(ResetPasswordModel repass)
        {
            string token = HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last()!;
            var temp = tokenService.CheckToken(token);
            if (temp.IsSuccess == false)
                return Ok(temp);
            var user = HttpContext.User;
            var id = user.FindFirst(ClaimTypes.Sid)?.Value;
            var test = id!.Split("xtxtx").Last();
            if(test != "Brush")
            {
                return BadRequest();
            }
            var response = passwordService.ResetPassword(id!, repass,token);
            return Ok(response);
        }
    }
}
