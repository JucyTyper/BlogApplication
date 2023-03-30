using BlogApplication.Models;

namespace BlogApplication.Services
{
    public interface IPasswordService
    {
        public byte[] CreatePasswordHash(string password, byte[] salt);
        public bool VerifyPasswordHash(string password, byte[] passwordHash);
        public ResponseModel changePassword(string id, ChangePasswordModel repass);
        public ResponseModel ForgetPassword(ForgetPasswordModel mail);
        public ResponseModel ResetPassword(string id, ResetPasswordModel cred,string token);
    }
}
