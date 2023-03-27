using Azure;
using BlogApplication.Data;
using BlogApplication.Models;
using ChatApp.Models;
using Google.Apis.Auth;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.Text;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Model;

namespace BlogApplication.Services
{
    public class AuthService : IAuthService
    {
        
        private readonly IConfiguration configuration;
        private readonly IPasswordService passwordService;
        private readonly ITokenService tokenService;
        private readonly IValidationService validationService;
        private readonly blogAppDatabase _db;
        // blogAppDatabase blogAppDatabase1 = new blogAppDatabase();
        UserResponse DataOut = new UserResponse();
        ResponseModel response = new ResponseModel();
        public AuthService(blogAppDatabase _db, IConfiguration configuration, IPasswordService passwordService, ITokenService tokenService, IValidationService validationService)
        {
            this._db = _db;
            this.configuration = configuration;
            this.passwordService = passwordService;
            this.tokenService = tokenService;
            this.validationService = validationService;
        }
        public object loginUser(LoginModel user)
        {
            var validation = validationService.CheckValidationEmail(user.email);
            if (validation.IsSuccess == false)
                return validation;
            validation = validationService.CheckValidationPassword(user.password);
            if (validation.IsSuccess == false)
                return validation;
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
            var token = tokenService.CreateToken(user.email,_user.First().UserId.ToString(),2);
            DataOut.Token = token;
            DataOut.Name = _user.First().firstName;
            DataOut.Email = _user.First().email;
            DataOut.UserID = _user.First().UserId;
            response.Data = DataOut;
            return response;
        }
        public object GoogleAuth(string Token)
        {
            try
            {
                var settings = new GoogleJsonWebSignature.ValidationSettings()
                {
                    Audience = new List<string> { configuration.GetSection("Google:ClientID").Value! }
                };
                var GoogleUser = GoogleJsonWebSignature.ValidateAsync(Token);
                var _user = _db.users.Where(x => x.email == GoogleUser.Result.Email).Select(x => new { x.UserId, x.email, x.firstName, x.lastName, x.dateOfBirth, x.created, x.lastActive, x.phoneNo, x.updated });

                if (_user.Count() != 0)
                {
                    string Authtoken = tokenService.CreateToken(_user.First().email,_user.First().UserId.ToString(),2);
                    DataOut.Token = Authtoken;
                    DataOut.Name = GoogleUser.Result.GivenName;
                    DataOut.Email = GoogleUser.Result.Email;
                    DataOut.UserID = _user.First().UserId;
                    DataOut.profilePicPath = "";
                    response.Message = "user Logged in";
                    response.IsSuccess = true;
                    response.Data = DataOut;
                    return response;
                }
                byte[] salt = Encoding.ASCII.GetBytes(configuration.GetSection("Password:salt").Value!);
                var user = new UserModel();
                user.email = GoogleUser.Result.Email;
                user.password = passwordService.CreatePasswordHash("",salt);
                user.firstName = GoogleUser.Result.GivenName;
                user.lastName = GoogleUser.Result.FamilyName;
                user.dateOfBirth = DateTime.MinValue;
                user.phoneNo = 0;
                string token = tokenService.CreateToken(user.email, user.UserId.ToString(),2);
                DataOut.Token = token;
                DataOut.Name = user.firstName;
                DataOut.Email = user.email;
                DataOut.UserID = user.UserId;
                DataOut.profilePicPath = user.ProfileImagePath;
                response.Data = DataOut;
                _db.users.Add(user);
                _db.SaveChanges();
                return response;
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = ex.Message;
                response.IsSuccess = false;
                return response;
            }
        }
        public object logout(string token)
        {
            try
            {
                tokenService.BlackListToken(token);
                response.Message = "User Logged Out";
                return response;
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = ex.Message;
                response.IsSuccess = false;
                return response;
            }
        }

    }     
}