using BlogApplication.Models;

namespace BlogApplication.Services
{
    public interface ITokenService
    {
        public string CreateToken(string email,string id);
    }
}
