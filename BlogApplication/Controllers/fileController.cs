using Azure;
using BlogApplication.Services;
using ChatApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Security.Claims;

namespace BlogApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class fileController : ControllerBase
    {
        private readonly IFileService fileService;
        public fileController(IFileService fileservice)
        {
            this.fileService = fileservice;
        }
        [HttpPost]
        [Route("image")]
        [Authorize]
        [DisableRequestSizeLimit]
        public IActionResult uploadImage([FromForm] fileUpload ImageFile)
        {
            var user1 = HttpContext.User;
            var id = user1.FindFirst(ClaimTypes.Sid)?.Value;
            var response = fileService.UploadImage(id!, ImageFile);
            return StatusCode(response.statusCode, response);
        }
    }
}
