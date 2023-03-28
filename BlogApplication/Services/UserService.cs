using Azure;
using BlogApplication.Data;
using BlogApplication.Models;
using ChatApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace BlogApplication.Services
{
    public class UserService:IUserService
    {
        private readonly IConfiguration configuration;
        private readonly IPasswordService passwordService;
        private readonly IValidationService validationService;
        private readonly ITokenService tokenService;
        private readonly blogAppDatabase _db;
       // blogAppDatabase blogAppDatabase1 = new blogAppDatabase();
        UserResponse DataOut = new UserResponse();
        ResponseModel response = new ResponseModel();
        public UserService(blogAppDatabase _db, IConfiguration configuration,IPasswordService passwordService, ITokenService tokenService,IValidationService validationService)
        {
            this.tokenService = tokenService;
            this._db = _db;
            this.configuration = configuration;
            this.passwordService = passwordService;
            this.validationService = validationService;
        }
        public object RegisterUser(RegisterUserModel user)
        {
            try
            {
                //checking if the user filled all entities 
                if (user.dateOfBirth == DateTime.MinValue || user.email == string.Empty || user.firstName == string.Empty || user.lastName == string.Empty)
                {
                    response.StatusCode = 400;
                    response.IsSuccess = false;
                    response.Message = "Fill all details";
                    return response;
                }

                var validation = validationService.CheckValidationPhoneNo(user.phoneNo.ToString());
                if (validation.IsSuccess == false)
                    return validation;
                validation = validationService.CheckValidationEmail(user.email);
                if (validation.IsSuccess == false)
                    return validation;
                validation = validationService.CheckValidationPassword(user.password);
                if (validation.IsSuccess == false)
                    return validation;
                validation = validationService.CheckValidationAge(user.dateOfBirth);
                if (validation.IsSuccess == false)
                    return validation;

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
                DataOut.Token = tokenService.CreateToken(user.email,userModel.UserId.ToString(),2);
                DataOut.Name = userModel.firstName;
                DataOut.Email = userModel.email;
                DataOut.UserID = userModel.UserId;
                DataOut.profilePicPath = userModel.ProfileImagePath;
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
        public ResponseModel GetUser(Guid id,string searchString,string email)
        {
            try
            {
                var user = _db.users.Where(x => (x.UserId == id || id == Guid.Empty) && (x.isDeleted == false) && (EF.Functions.Like(x.firstName, "%" + searchString + "%") || EF.Functions.Like(x.lastName, "%" + searchString + "%") || EF.Functions.Like(x.firstName + " " + x.lastName, "%" + searchString + "%") || searchString == null) &&
                (x.email == email || email == null)).Select(x => x);
                if(user.Count() == 0) 
                {
                    response.StatusCode = 404;
                    response.Message = "user Not Found" ;
                    response.IsSuccess = false;
                    return response;
                }
                var resUser = new List<GetUserModel>();
                foreach(var item in user)
                {
                    var users = new GetUserModel(item);
                    resUser.Add(users);
                }
                response.Data = resUser;
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
