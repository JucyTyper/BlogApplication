using Azure;
using BlogApplication.Data;
using BlogApplication.Models;
using ChatApp.Models;
using Google.Apis.Auth;
using System.Data;
using System.Text;

namespace BlogApplication.Services
{
    public class AuthService : IAuthService
    {
        
        private readonly IConfiguration configuration;
        private readonly IPasswordService passwordService;
        private readonly ITokenService tokenService;
        private readonly IValidationService validationService;
        private readonly blogAppDatabase _db;
        UserResponse DataOut = new UserResponse();
        public AuthService(blogAppDatabase _db, IConfiguration configuration, IPasswordService passwordService, ITokenService tokenService, IValidationService validationService)
        {
            this._db = _db;
            this.configuration = configuration;
            this.passwordService = passwordService;
            this.tokenService = tokenService;
            this.validationService = validationService;
        }
        // -------------------- A function to login user --------------->>
        public ResponseModel loginUser(LoginModel user)
        {
            try
            {
                // Checking email validity
                var validation = validationService.CheckValidationEmail(user.email);
                if (validation.isSuccess == false)
                    return validation;
                //Checking Password Validity
                validation = validationService.CheckValidationPassword(user.password);
                if (validation.isSuccess == false)
                    return validation;
                //Checking if email exist
                var _user = _db.users.Where(x => x.email == user.email).Select(x => x);
                if (_user.Count() == 0)
                {
                    return new ResponseModel(404, "User Not Found", false);
                }
                // verifying password
                if (!passwordService.VerifyPasswordHash(user.password, _user.First().password))
                {
                    return new ResponseModel(400, "Wrong Password", false);
                }
                //Checking if the user is admin
                if (_user.First().isAdmin == true)
                {
                    var AdminToken = tokenService.CreateToken(user.email, _user.First().UserId.ToString(), 3);
                    DataOut.Token = AdminToken;
                    DataOut.Name = _user.First().firstName;
                    DataOut.Email = _user.First().email;
                    DataOut.UserID = _user.First().UserId;
                    DataOut.isAdmin = _user.First().isAdmin;
                    return new ResponseModel(DataOut);
                }
                // For normal user
                var token = tokenService.CreateToken(user.email, _user.First().UserId.ToString(), 2);
                DataOut.Token = token;
                DataOut.Name = _user.First().firstName;
                DataOut.Email = _user.First().email;
                DataOut.UserID = _user.First().UserId;
                DataOut.isAdmin = _user.First().isAdmin; 
                DataOut.isBlocked = _user.First().isBlocked;
                return new ResponseModel( DataOut);
            }
            catch(Exception ex)
            {
                return new ResponseModel(500, ex.Message, false);
            }
        }
        //--------------- A function for Google Authorization ----------------->>
        public ResponseModel GoogleAuth(string Token)
        {
            try
            {
                // Validating Google token
                var settings = new GoogleJsonWebSignature.ValidationSettings()
                {
                    Audience = new List<string> { configuration.GetSection("Google:ClientID").Value! }
                };
                // Getting user payload
                var GoogleUser = GoogleJsonWebSignature.ValidateAsync(Token);
                //Checking if user exist
                var _user = _db.users.Where(x => x.email == GoogleUser.Result.Email).Select(x =>  x);

                if (_user.Count() != 0)
                {
                    string Authtoken = tokenService.CreateToken(_user.First().email,_user.First().UserId.ToString(),2);
                    DataOut.Token = Authtoken;
                    DataOut.Name = GoogleUser.Result.GivenName;
                    DataOut.Email = GoogleUser.Result.Email;
                    DataOut.UserID = _user.First().UserId;
                    DataOut.profilePicPath = _user.First().ProfileImagePath;
                    return new ResponseModel("User Logged in",DataOut);
                }
                // creating a new user
                byte[] salt = Encoding.ASCII.GetBytes(configuration.GetSection("Password:salt").Value!);
                var user = new UserModel();
                user.email = GoogleUser.Result.Email;
                user.password = passwordService.CreatePasswordHash("",salt);
                user.firstName = GoogleUser.Result.GivenName;
                user.lastName = GoogleUser.Result.FamilyName;
                user.dateOfBirth = DateTime.MinValue;
                user.phoneNo = 0;
                // creating token
                string token = tokenService.CreateToken(user.email, user.UserId.ToString(),2);
                DataOut.Token = token;
                DataOut.Name = user.firstName;
                DataOut.Email = user.email;
                DataOut.UserID = user.UserId;
                DataOut.profilePicPath = user.ProfileImagePath;
                // Saving data
                _db.users.Add(user);
                _db.SaveChanges();
                return new ResponseModel(DataOut);
            }
            catch (Exception ex)
            {
                return new ResponseModel(500, ex.Message, false);
            }
        }
        //------------------ A Function To LogOut User ------------------->>
        public ResponseModel logout(string token)
        {
            try
            {
                //Blacklisting Token
                tokenService.BlackListToken(token);
                return new ResponseModel("User Logged Out");
            }
            catch (Exception ex)
            {
                return new ResponseModel(500, ex.Message, false);
            }
        }
    }     
}