using BlogApplication.Models;

namespace BlogApplication.Services
{
    public interface ITokenService
    {
        public string CreateToken(string email,string id,int type);
        public void BlackListToken(string token);
        public ResponseModel CheckToken(string token);
    }
}
