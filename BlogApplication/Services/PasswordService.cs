﻿using Azure;
using BlogApplication.Data;
using BlogApplication.Models;
using System.Configuration;
using System.Net.Mail;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace BlogApplication.Services
{
    public class PasswordService : IPasswordService
    {
        private readonly ITokenService tokenService;
        private readonly blogAppDatabase _db;
        private readonly IConfiguration configuration;
        ResponseModel response = new ResponseModel();
        public PasswordService(IConfiguration configuration,blogAppDatabase _db,ITokenService tokenService)
        {
            this.tokenService = tokenService;
            this._db = _db;
            this.configuration = configuration;
        }
        //A function to generate passwordHash
        public byte[] CreatePasswordHash(string password, byte[] salt)
        {
            var hmac = new HMACSHA512(salt);
            var passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            return passwordHash;
        }
        public bool VerifyPasswordHash(string password, byte[] passwordHash)
        {
            byte[] salt = Encoding.ASCII.GetBytes(configuration.GetSection("Password:salt").Value!);
            using (var hmac = new HMACSHA512(salt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                return computedHash.SequenceEqual(passwordHash);
            }
        }
        public object changePassword(string id, ChangePasswordModel repass)
        {
            try
            {
                Guid Id = new Guid(id);
                var _user = _db.users.Where(x =>
                 (x.UserId == Id)).Select(x => x);
                if (_user.Count() == 0)
                {
                    response.StatusCode = 404;
                    response.Message = "User Not Found";
                    response.IsSuccess = false;
                    return response;
                }
                if (!VerifyPasswordHash(repass.oldPassword, _user.First().password))
                {
                    response.StatusCode = 400;
                    response.Message = "wrong Old Password";
                    response.IsSuccess = false;
                    return response;
                }
                byte[] salt = Encoding.ASCII.GetBytes(configuration.GetSection("Password:salt").Value!);
                //Generating password hash and saving it
                _user.First().password = CreatePasswordHash(repass.newPassword, salt);
                _db.SaveChanges();
                response.IsSuccess = true;
                response.StatusCode = 200;
                response.Message = "password Changed";
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
        public object ForgetPassword(ForgetPasswordModel mail)
        {
            var _user = _db.users.Where(x => x.email == mail.email).Select(x => x);
            if (_user.Count() == 0)
            {
                response.StatusCode = 404;
                response.Message = "Email Not Found";
                response.IsSuccess = false;
                return response;
            }
            string token = tokenService.CreateToken(mail.email,"temp");
            // Create a new UriBuilder object with the original link
            UriBuilder builder = new UriBuilder(mail.url);

            // Encode the JWT token as a URL-safe string
            string encodedToken = WebUtility.UrlEncode(token);

            // Add the encoded JWT token as a query string parameter
            builder.Query = "token=" + encodedToken;

            // Get the modified link as a string
            string modifiedLink = builder.ToString();

            MailMessage message = new MailMessage();
            // set the sender and recipient email addresses
            message.From = new MailAddress("ajay.joshi@chatapp.chicmic.co.in");
            message.To.Add(new MailAddress(mail.email));

            // set the subject and body of the email
            message.Subject = "Verify your account";
            message.Body = "Please verify your reset password attempt. Your one time link for verification is " + modifiedLink;

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

            response.StatusCode = 200;
            response.Message = "Verification Email Sent";
            response.IsSuccess = true;
            return response;
        }
    }
}