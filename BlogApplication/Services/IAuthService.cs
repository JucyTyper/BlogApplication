using BlogApplication.Models;

namespace BlogApplication.Services
{
    public interface IAuthService
    {
        public ResponseModel loginUser(LoginModel user);
        public ResponseModel GoogleAuth(string Token);
        public ResponseModel logout(string token);
    }
}
