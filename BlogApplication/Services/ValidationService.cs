using Azure;
using BlogApplication.Models;
using System.Text.RegularExpressions;

namespace BlogApplication.Services
{
    public class ValidationService : IValidationService
    {
        // ---------- Regex Patterns ---------------->>
        private static string passwordPattern = "^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[#?!@$%^&*-]).{8,}$";
        private static string emailPattern = "^[a-z0-9._%+-]+@[a-z0-9.-]+\\.[a-z]{2,4}$";
        private static string phonePattern = "^[6-9]\\d{9}$";

        // ---------- Function to Check Password ---------------->>
        public ResponseModel CheckValidationPassword(string cred)
        {
            // Matching input with regex pattern
            if (!Regex.IsMatch(cred,passwordPattern))
            {
                return new ResponseModel(400, "Enter Valid Password", false);
            }
            return new ResponseModel();
        }
        // ---------- Function to Check Email ---------------->>
        public ResponseModel CheckValidationEmail(string cred)
        {
            // Matching input with regex pattern
            if (!Regex.IsMatch(cred, emailPattern))
            {
                return new ResponseModel(400, "Enter Valid Email", false);
            }
            return new ResponseModel();
        }
        // ---------- Function to Check Phone Number ---------------->>
        public ResponseModel CheckValidationPhoneNo(string cred)
        {
            // Matching input with regex pattern
            if (!Regex.IsMatch(cred, phonePattern))
            {
                return new ResponseModel(400, "Enter Valid Phone Number", false);
            }
            return new ResponseModel();
        }
        // ---------- Function to Check Age ---------------->>
        public ResponseModel CheckValidationAge(DateTime DOB)
        {
            TimeSpan ageTimeSpan = DateTime.Now - DOB;
            int age = (int)(ageTimeSpan.Days / 365.25);

            // Performing DOB validation here based specific requirements
            //for age less than 12
            if (age < 12)
            {
                return new ResponseModel(400, "Not allowed to register. User is underage.Must be atleast 12 years old", false);
            }
            //for age greater than 130
            else if (age > 130)
            {
                return new ResponseModel(400, "Not allowed to register. User is overage.Must be atmost 130 years old", false);
            }
            return new ResponseModel();
        }
    }
}
