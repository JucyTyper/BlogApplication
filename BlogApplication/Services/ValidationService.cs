using Azure;
using BlogApplication.Models;
using System.Text.RegularExpressions;

namespace BlogApplication.Services
{
    public class ValidationService : IValidationService
    {
        private static string passwordPattern = "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$";
        private static string emailPattern = "^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$";
        private static string phonePattern = "^[6-9]\\d{9}$";

        ResponseModel response = new ResponseModel();
        public ResponseModel CheckValidationPassword(string cred)
        {
            if (!Regex.IsMatch(cred,passwordPattern))
            {
                response.StatusCode = 400;
                response.Message = "Enter Valid password";
                response.IsSuccess = false;
                return response;
            }
            return response;
        }
        public ResponseModel CheckValidationEmail(string cred)
        {
            if (!Regex.IsMatch(cred, emailPattern))
            {
                response.StatusCode = 400;
                response.Message = "Enter Valid Email";
                response.IsSuccess = false;
                return response;
            }
            return response;
        }
        public ResponseModel CheckValidationPhoneNo(string cred)
        {
            if (!Regex.IsMatch(cred, phonePattern))
            {
                response.StatusCode = 400;
                response.Message = "Enter Valid Phone Number";
                response.IsSuccess = false;
                return response;
            }
            return response;
        }
        public ResponseModel CheckValidationAge(DateTime DOB)
        {
            TimeSpan ageTimeSpan = DateTime.Now - DOB;
            int age = (int)(ageTimeSpan.Days / 365.25);

            // Performing DOB validation here based specific requirements
            //for age less than 12
            if (age < 12)
            {
                response.StatusCode = 400;
                response.Message = "Not allowed to register. User is underage.Must be atleast 12 years old";
                response.IsSuccess = false;
                return response;
            }
            //for age greater than 130
            else if (age > 130)
            {
                response.StatusCode = 400;
                response.Message = "Not allowed to register. User is overage.Must be atmost 130 years old";
                response.IsSuccess = false;
                return response;
            }
            return response;
        }
    }
}
