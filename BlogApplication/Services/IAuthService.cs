using BlogApplication.Models;

namespace BlogApplication.Services
{
    public interface IAuthService
    {
        public object loginUser(LoginModel user);
    }
}
