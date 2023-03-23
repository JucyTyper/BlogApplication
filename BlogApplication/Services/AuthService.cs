using Azure;
using BlogApplication.Data;
using BlogApplication.Models;
using ChatApp.Models;
using System.Data;

namespace BlogApplication.Services
{
    public class AuthService : IAuthService
    {
        
        private readonly IConfiguration configuration;
        private readonly IPasswordService passwordService;
        private readonly ITokenService tokenService;
        private readonly blogAppDatabase _db;
        // blogAppDatabase blogAppDatabase1 = new blogAppDatabase();
        UserResponse DataOut = new UserResponse();
        ResponseModel response = new ResponseModel();
        public AuthService(blogAppDatabase _db, IConfiguration configuration, IPasswordService passwordService, ITokenService tokenService)
        {
            this._db = _db;
            this.configuration = configuration;
            this.passwordService = passwordService;
            this.tokenService = tokenService;
        }
        public object loginUser(LoginModel user)
        {
            var _user = _db.users.Where(x => x.email == user.email).Select(x => x);
            if (_user.Count() == 0)
            {
                response.StatusCode = 404;
                response.Message = "User doesn't Exist";
                response.IsSuccess = false;
                return response;
            }

            if (!passwordService.VerifyPasswordHash(user.password, _user.First().password))
            {
                response.StatusCode = 404;
                response.Message = "wrong Password";
                response.IsSuccess = false;
                return response;
            }
            var token = tokenService.CreateToken(user.email,_user.First().UserId.ToString());
            DataOut.Token = token;
            DataOut.Name = _user.First().firstName;
            DataOut.Email = _user.First().email;
            DataOut.UserID = _user.First().UserId;
            response.Data = DataOut;
            return response;
        }
    }     
}