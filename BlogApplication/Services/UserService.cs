using BlogApplication.Data;
using BlogApplication.Models;
using ChatApp.Models;
using System.Text;

namespace BlogApplication.Services
{
    public class UserService:IUserService
    {
        private readonly IConfiguration configuration;
        private readonly IPasswordService passwordService;
        private readonly ITokenService tokenService;
        private readonly blogAppDatabase _db;
       // blogAppDatabase blogAppDatabase1 = new blogAppDatabase();
        UserResponse DataOut = new UserResponse();
        ResponseModel response = new ResponseModel();
        public UserService(blogAppDatabase _db, IConfiguration configuration,IPasswordService passwordService, ITokenService tokenService)
        {
            this.tokenService = tokenService;
            this._db = _db;
            this.configuration = configuration;
            this.passwordService = passwordService;
        }
        public object RegisterUser(RegisterUserModel user)
        {
            try
            {
                // Creating and user entity and populating it 
                var userModel = new UserModel();
                userModel.firstName = user.firstName;
                userModel.lastName = user.lastName;
                userModel.email = user.email;
                userModel.phoneNo = user.phoneNo;
                userModel.dateOfBirth = user.dateOfBirth;
                //calling a userdefined function to save the password in hash
                RegisterPassword(userModel, user.password);
                //adding user Entity in Database
                _db.users.Add(userModel);
                //Saving changes in Database
                _db.SaveChanges();
                //Calling a user Defined Function to generate JWT token
                //generating Response
                response.StatusCode = 200;
                response.Message = "User added Successfully";
                DataOut.Token = tokenService.CreateToken(user.email,userModel.UserId.ToString());
                DataOut.Name = userModel.firstName;
                DataOut.Email = userModel.email;
                DataOut.UserID = userModel.UserId;
                response.Data = DataOut;
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
        private void RegisterPassword(UserModel user, string Password)
        {
            byte[] salt = Encoding.ASCII.GetBytes(configuration.GetSection("Password:salt").Value!);
            //Generating password hash and saving it
            user.password = passwordService.CreatePasswordHash(Password, salt);
        }
    }
}
