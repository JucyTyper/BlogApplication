using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using BlogApplication.Models;
using BlogApplication.Services;

namespace BlogApplication.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class blogHub :Hub
    {
        private readonly IHubService hubService;
        public blogHub(IHubService hubService)
        {

            this.hubService = hubService;
        }
        public override Task OnConnectedAsync()
        {
            Clients.Caller.SendAsync("refreshNotice");
            return base.OnConnectedAsync();
        }
        public object sendNotice(string notice)
        {
            try
            {
                Clients.All.SendAsync("refreshNotice");
                return new ResponseModel("Notice Set", notice);
            }
            catch(Exception ex)
            {
                return new ResponseModel(500, ex.Message, false);
            }
        }
        public object likeAndDislike(string Id,int type)
        {
            var response = hubService.likeAndDistlike(Id,type);
            return response;
        }
        public object GetNotice()
        {
            return hubService.GetNotice();
        }
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}
