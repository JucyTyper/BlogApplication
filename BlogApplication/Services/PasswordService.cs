using BlogApplication.Data;
using BlogApplication.Models;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Data;
using ChatApp.Models;

namespace BlogApplication.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly ITokenService tokenService;
        private readonly blogAppDatabase _db;
        private readonly IConfiguration configuration;
        private readonly IValidationService validationService;
        UserResponse DataOut = new UserResponse();
        
        // Calling Constructor
        public PasswordService(IConfiguration configuration,blogAppDatabase _db,ITokenService tokenService, IValidationService validationService)
        {
            this.tokenService = tokenService;
            this._db = _db;
            this.configuration = configuration;
            this.validationService = validationService;
        }
        //---------------A function to generate passwordHash------------->>
        public byte[] CreatePasswordHash(string password, byte[] salt)
        {
            var hmac = new HMACSHA512(salt);
            //creatinh password Hash
            var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return passwordHash;
        }
        //----------------A Function to verify hash password ------------->>
        public bool VerifyPasswordHash(string password, byte[] passwordHash)
        {
            //Getting salt
            byte[] salt = Encoding.ASCII.GetBytes(configuration.GetSection("Password:salt").Value!);
            using (var hmac = new HMACSHA512(salt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
        //----------------A Function to Change Password------------>>
        public ResponseModel changePassword(string id, ChangePasswordModel repass)
        {
            try
            {
                //Checking Validity of the password
                var validation = validationService.CheckValidationPassword(repass.newPassword);
                if (validation.isSuccess == false)
                    return validation;

                Guid Id = new Guid(id);
                //Getting user data
                var _user = _db.users.Where(x =>
                 (x.UserId == Id)).Select(x => x);
                if (_user.Count() == 0)
                {
                    return new ResponseModel(404, "User NotFound", false);
                }
                //Verifying password
                if (!VerifyPasswordHash(repass.oldPassword, _user.First().password))
                {
                    return new ResponseModel(400, "Wrong Old Password", false);
                }

                byte[] salt = Encoding.ASCII.GetBytes(configuration.GetSection("Password:salt").Value!);
                //Generating password hash and saving it
                _user.First().password = CreatePasswordHash(repass.newPassword, salt);
                // Saving Password
                _db.SaveChanges();

                return new ResponseModel("Password Changed");
            }
            catch (Exception ex)
            {
                return new ResponseModel(500, ex.Message, false);
            }
        }
        //------------------ A Function for forget password ------------->>
        public ResponseModel ForgetPassword(ForgetPasswordModel mail)
        {
            //Checking Email
            var _user = _db.users.Where(x => x.email == mail.email).Select(x => x);
            if (_user.Count() == 0)
            {
                return new ResponseModel(404, "Email Not Found", false);
            }
            string token = tokenService.CreateToken(mail.email,_user.First().UserId.ToString(), 1);
            // Create a new UriBuilder object with the original link
            UriBuilder builder = new UriBuilder("http://localhost:4200/redirect");

            // Encode the JWT token as a URL-safe string
            string encodedToken = WebUtility.UrlEncode(token);

            // Add the encoded JWT token as a query string parameter
            builder.Query = "token=" + encodedToken;

            // Get the modified link as a string
            string modifiedLink = builder.ToString();
            string text = "<table width=\"100%\" cellspacing=\"0\" cellpadding=\"0\">\r\n    <tr>\r\n        <td>\r\n            <table cellspacing=\"0\" cellpadding=\"0\">\r\n                <tr>\r\n                    <td style=\"border-radius: 2px;\" bgcolor=\"#ED2939\">\r\n                        <a href=" + modifiedLink + " style=\"padding: 8px 12px; border: 1px solid #ED2939;border-radius: 2px;font-family: Helvetica, Arial, sans-serif;font-size: 14px; color: #ffffff;text-decoration: none;font-weight:bold;display: inline-block;\">\r\n                            Verify Yourself\r\n                        </a>\r\n                    </td>\r\n                </tr>\r\n            </table>\r\n        </td>\r\n    </tr>\r\n</table>";

            MailMessage message = new MailMessage();
            message.IsBodyHtml = true;
            // set the sender and recipient email addresses
            message.From = new MailAddress("Verification.mail@blogapp.chicmic.co.in");
            message.To.Add(new MailAddress(mail.email));

            // set the subject and body of the email
            message.Subject = "Verify your account";
            message.Body = "To verify your reset password attempt click on the button below " + text;

            // create a new SmtpClient object
            SmtpClient client = new SmtpClient();

            string adminEmail = configuration.GetSection("EmailCred:Email").Value!;
            string adminPassword = configuration.GetSection("EmailCred:password").Value!;

            // set the SMTP server credentials and port
            client.Credentials = new NetworkCredential(adminEmail, adminPassword);
            client.Host = "mail.chicmic.co.in";
            client.Port = 587;
            client.EnableSsl = true;
            // send the email
            client.Send(message);

            return new ResponseModel("Verification Email Sent");
        }
        //-------------------- A Function To reset Password ---------------->>
        public ResponseModel ResetPassword(string userid, ResetPasswordModel cred,string token)
        {
            try
            {
                //Checking password pattern
                var validation = validationService.CheckValidationPassword(cred.newPassword);
                if (validation.isSuccess == false)
                    return validation;

                Guid Id = new Guid(userid);
                //fetching user Data
                var _user = _db.users.Where(x =>
                 (x.UserId == Id)).Select(x => x);
                if (_user.Count() == 0)
                {
                    return new ResponseModel(404, "User NotFound", false);
                }

                byte[] salt = Encoding.ASCII.GetBytes(configuration.GetSection("Password:salt").Value!);
                //Generating password hash and saving it
                _user.First().password = CreatePasswordHash(cred.newPassword, salt);
                _db.SaveChanges();
                //Blacklisting token
                tokenService.BlackListToken(token);

                return new ResponseModel("Password Changed");
            }
            catch (Exception ex)
            {
                return new ResponseModel(500, ex.Message, false);
            }
        }
    }
}
