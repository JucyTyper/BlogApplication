using BlogApplication.Models;
using BlogApplication.Data;

namespace BlogApplication.Services
{
    public interface IUserService
    {
        public object RegisterUser(RegisterUserModel user);
    }
}
