using BlogApplication.Models;
using BlogApplication.Data;

namespace BlogApplication.Services
{
    public interface IUserService
    {
        public ResponseModel RegisterUser(RegisterUserModel user);
        public ResponseModel GetUser(Guid id, string name, string email,int pageNo);
        public ResponseModel UpdateUser(Guid id, UpdateUserModel user);
        public ResponseModel AddNotice(string notice);
        public ResponseModel RemoveNotice(Guid Id);
        public ResponseModel GetNotice();
    }
}
