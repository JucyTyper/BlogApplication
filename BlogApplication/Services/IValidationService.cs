using BlogApplication.Models;

namespace BlogApplication.Services
{
    public interface IValidationService
    {
        public ResponseModel CheckValidationPassword(string cred);
        public ResponseModel CheckValidationPhoneNo(string cred);
        public ResponseModel CheckValidationEmail(string cred);
        public ResponseModel CheckValidationAge(DateTime DOB);
    }
}
