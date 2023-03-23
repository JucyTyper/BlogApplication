using BlogApplication.Models;

namespace BlogApplication.Services
{
    public interface IPasswordService
    {
        public byte[] CreatePasswordHash(string password, byte[] salt);
        public bool VerifyPasswordHash(string password, byte[] passwordHash);
        public object changePassword(string id, ChangePasswordModel repass);
        public object ForgetPassword(ForgetPasswordModel mail);
    }
}
