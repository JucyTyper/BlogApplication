using BlogApplication.Data;
using BlogApplication.Models;
using ChatApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace BlogApplication.Services
{
    public class UserService : IUserService
    {
        private readonly IConfiguration configuration;
        private readonly IPasswordService passwordService;
        private readonly IValidationService validationService;
        private readonly ITokenService tokenService;
        private readonly blogAppDatabase _db;
        UserResponse DataOut = new UserResponse();

        //Calling constructor
        public UserService(blogAppDatabase _db, IConfiguration configuration, IPasswordService passwordService, ITokenService tokenService, IValidationService validationService)
        {
            this.tokenService = tokenService;
            this._db = _db;
            this.configuration = configuration;
            this.passwordService = passwordService;
            this.validationService = validationService;
        }
        //--------------- A function to register user ---------->>
        public ResponseModel RegisterUser(RegisterUserModel user)
        {
            try
            {
                //checking if the user filled all entities 
                if (user.dateOfBirth == DateTime.MinValue || user.email == string.Empty || user.firstName == string.Empty || user.lastName == string.Empty)
                {
                    return new ResponseModel(400, "Fill All Details", false);
                }
                //Phone Number Validation
                var validation = validationService.CheckValidationPhoneNo(user.phoneNo.ToString());
                if (validation.isSuccess == false)
                    return validation;

                //Email Validation
                validation = validationService.CheckValidationEmail(user.email);
                if (validation.isSuccess == false)
                    return validation;

                //Password Validation
                validation = validationService.CheckValidationPassword(user.password);
                if (validation.isSuccess == false)
                    return validation;

                //Age Validation
                validation = validationService.CheckValidationAge(user.dateOfBirth);
                if (validation.isSuccess == false)
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

                //generating Response Data
                DataOut.Token = tokenService.CreateToken(user.email, userModel.UserId.ToString(), 2);
                DataOut.Name = userModel.firstName;
                DataOut.Email = userModel.email;
                DataOut.UserID = userModel.UserId;
                DataOut.isAdmin= userModel.isAdmin;
                DataOut.isBlocked = userModel.isBlocked ;
                DataOut.profilePicPath = userModel.ProfileImagePath;
                
                return new ResponseModel(200, "User added Successfully", DataOut, true);
            }
            catch (Exception ex)
            {
                return new ResponseModel(500, ex.Message, false);
            }
        }
        private void RegisterPassword(UserModel user, string Password)
        {
            //Getting Salt for password
            byte[] salt = Encoding.ASCII.GetBytes(configuration.GetSection("Password:salt").Value!);
            //Generating password hash and saving it
            user.password = passwordService.CreatePasswordHash(Password, salt);
        }
        //------------------- A function to get user data ----------------->>
        public ResponseModel GetUser(Guid id, string searchString, string email, int pageNo)
        {
            try
            {
                //Fetching user data
                var user = _db.users.Where(x => (x.UserId == id || id == Guid.Empty) && (x.isDeleted == false) && (EF.Functions.Like(x.firstName, "%" + searchString + "%") || EF.Functions.Like(x.lastName, "%" + searchString + "%") || EF.Functions.Like(x.firstName + " " + x.lastName, "%" + searchString + "%") || searchString == null) &&
                (x.email == email || email == null)).Select(x => x).Skip((pageNo - 1)*5).Take(5);
                //Checking if user exist
                if (user.Count() == 0)
                {
                    return new ResponseModel(404, "User NotFound", false);
                }
                //mapping user data to  get user model
                var resUser = new List<GetUserModel>();
                foreach (var item in user)
                {
                    var users = new GetUserModel(item);
                    resUser.Add(users);
                }

                return new ResponseModel(resUser);
            }
            catch (Exception ex)
            {
                return new ResponseModel(500, ex.Message, false);
            }
        }
        //--------------------- A Function to Update user Data---------->>
        public ResponseModel UpdateUser(Guid id, UpdateUserModel _user)
        {
            try
            {
                //Fetching user Data
                var user = _db.users.Where(x => (x.UserId == id || id == Guid.Empty) && (x.isDeleted == false)).Select(x => x).ToList();
                //Checking if user exist
                if (user.Count() == 0)
                {
                    return new ResponseModel(404, "User NotFound", false);
                }
                //Updating user Data
                user.First().firstName = _user.firstName;
                user.First().lastName = _user.lastName;
                user.First().dateOfBirth = _user.dateOfBirth;
                user.First().phoneNo = _user.phoneNo;

                var users = new GetUserModel(user.First());
                return new ResponseModel(users);
            }

            catch (Exception ex)
            {
                return new ResponseModel(500, ex.Message, false);
            }
        }
        // ------------------ A Function To Remove Notice ------------>>
        public ResponseModel RemoveNotice(Guid Id)
        {
            try
            {
                //Feting Notice
                var Notice = _db.notices.Where(x => x.noticeId == Id && x.isDeleted == false).Select(x => x);
                //Checking if notice exist
                if(Notice.Count() == 0)
                {
                    return new ResponseModel(404,"Notice Not found",false);
                }
                //Removing Notice
                Notice.First().isDeleted = true;
                // Saving Changes
                _db.SaveChanges();
                return new ResponseModel("Notice Deleted Successfully");
            }
            catch (Exception ex)
            {
                return new ResponseModel(500, ex.Message, false);
            }
        }
        // ------------ A Function to Get Notice ------------>>
        public ResponseModel GetNotice()
        {
            try
            {
                // Fetching All Notice
                var Notices = _db.notices.Where(x => x.isDeleted == false).Select(x => x).ToList();
                // Checking if notice exist
                if (Notices.Count() == 0)
                {
                    return new ResponseModel(404, "Notice Not found", false);
                }
                // Returning response
                return new ResponseModel("All Notices", Notices);
            }
            catch (Exception ex)
            {
                return new ResponseModel(500, ex.Message, false);
            }
        }
        // ------------- A function to block user----------->>
        public ResponseModel BlockUser(Guid userId, int type)
        {
            try
            {
                // Fetching user
                var user = _db.users.Where(x => x.UserId == userId && x.isDeleted == false).Select(x => x).ToList();
                // Checking if user exist
                if (user.Count() == 0)
                {
                    return new ResponseModel(404, "user Not found", false);
                }
                if(type == 1) 
                {
                    if (user.First().isBlocked == true)
                    {
                        return new ResponseModel("user already Blocked");
                    }
                    //Blocking User
                    user.First().isBlocked = true;
                    //Blocking All blogs of the user
                    var Blogs = _db.Blogs.Where(x => x.createrId == userId).Select(x => x).ToList();
                    foreach (var blog in Blogs)
                    {
                        blog.isBlocked = true;
                    }
                    _db.SaveChanges();
                    // Returning response
                    return new ResponseModel("User Blocked");
                }
                else
                {
                    if (user.First().isBlocked == false)
                    {
                        return new ResponseModel("user already Unblocked");
                    }
                    //ubBlocking User
                    user.First().isBlocked = false;
                    //unBlocking All blogs of the user
                    var Blogs = _db.Blogs.Where(x => x.createrId == userId && x.isDeleted == false).Select(x => x).ToList();
                    foreach (var blog in Blogs)
                    {
                        blog.isBlocked = false;
                    }
                    _db.SaveChanges();
                    // Returning response
                    return new ResponseModel("User Unblocked");
                }
            }
            catch (Exception ex)
            {
                return new ResponseModel(500, ex.Message, false);
            }
        }
    }
}