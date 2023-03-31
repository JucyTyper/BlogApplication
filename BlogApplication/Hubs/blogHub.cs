using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using BlogApplication.Models;
using BlogApplication.Services;
using BlogApplication.Data;

namespace BlogApplication.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class blogHub :Hub
    {
        private readonly blogAppDatabase _db;
        private readonly IHubService hubService;
        // Calling constructor for dependency injections
        public blogHub(IHubService hubService,blogAppDatabase _db)
        {
            this._db = _db;
            this.hubService = hubService;
        }
        // ----- Overriding an existion function of on connect----->>
        public override Task OnConnectedAsync()
        {
            // sending refresh notice message to user 
            Clients.Caller.SendAsync("refreshNotice");
            return base.OnConnectedAsync();
        }
        //-------------- A method to send Notice to all users ---------->>
        public object sendNotice(string notice)
        {
            try
            {
                // creating anotice entity
                var Notice = new NoticeModel(notice);
                // Saving Notice in Database
                _db.notices.Add(Notice);
                _db.SaveChanges();
                // Sending refresh notice message to all
                Clients.All.SendAsync("refreshNotice");
                return new ResponseModel("Notice Send Successfully", Notice);
            }
            catch(Exception ex)
            {
                return new ResponseModel(500, ex.Message, false);
            }
        }
        // ------------- A function to like and Dislike a blog---------->>
        public object likeAndDislike(string blogId,string userId,int type)
        {
            // getting response of liking and disliking
            var response = hubService.likeAndDistlike(blogId,userId,type);
            return response;
        }
        // --------------- A function to Get active notice ------------>> 
        public object GetNotice()
        {
            return hubService.GetNotice();
        }
        // ------------------ overriding On DisConnected function ---------->>
        public override Task OnDisconnectedAsync(Exception? exception)
        {
            return base.OnDisconnectedAsync(exception);
        }
    }
}
