using BlogApplication.Models;

namespace BlogApplication.Services
{
    public interface IAuthService
    {
        public object loginUser(LoginModel user);
        public object GoogleAuth(string Token);
        public object logout(string token);
    }
}
