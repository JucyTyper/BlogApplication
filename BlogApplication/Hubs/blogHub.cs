using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using BlogApplication.Models;
using BlogApplication.Data;
using BlogApplication.Services;
using ChatApp.Models;

namespace BlogApplication.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class blogHub :Hub
    {
        private readonly IHubService hubService;
        // blogAppDatabase blogAppDatabase1 = new blogAppDatabase();
        UserResponse DataOut = new UserResponse();
        ResponseModel response = new ResponseModel();
        public blogHub( IConfiguration configuration,IHubService hubService)
        {

            this.hubService = hubService;
        }
        public override Task OnConnectedAsync()
        {
            return base.OnConnectedAsync();
        }
        public object sendNotice(string notice)
        {
            try
            {
                Clients.All.SendAsync("Notice",notice);
                response.Message = "Notice Sent";
                response.Data= notice;
                return response;
            }
            catch(Exception ex)
            {
                response.StatusCode = 500;
                response.Message = ex.Message;
                response.IsSuccess = false;
                return response;
            }
        }
        public object likeAndDislike(string Id,int type)
        {
            var response = hubService.likeAndDistlike(Id,type);
            return response;
        }
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}
