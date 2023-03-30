using BlogApplication.Data;
using BlogApplication.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BlogApplication.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration configuration;
        private readonly blogAppDatabase _db;
        // Calling Constructor
        public TokenService(IConfiguration configuration,blogAppDatabase _db)
        {
            this.configuration = configuration;
            this._db = _db;
        }
        // ------------------------- A Function to Create Token ------------->>
        public string CreateToken(string email,string Id,int type)
        {
            // Adding Claims
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email,email),
                new Claim(ClaimTypes.Sid,Id),
            };
            //Adding Key
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration.GetSection("jwt:Key").Value!));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            JwtSecurityToken token;

            // For Forget Password
            if (type== 1)
            {
                claims.Add(new Claim(ClaimTypes.Role, "ForgetPassword"));
                token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: cred
                );
            }
            // For Admin 
            else if (type == 3)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
                token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred
                );
            }
            // Normal Token
            else
            {
                token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred
                );
            }
            //Converting into string
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
        // ----------------- A Function to Blacklist Token ------->>
        public void BlackListToken(string token)
        {
            var BLToken = new BlackListTokenModel
            {
                token = token
            };
            _db.BLTokens.Add(BLToken);
            _db.SaveChanges();
        }
        // ----------------- A Function to Check Token ------->>
        public ResponseModel CheckToken(string token)
        {
            try
            {
                //Fetching token from blackList token
                var tokenCheck = _db.BLTokens.Where(x => x.token == token).Count();
                if (tokenCheck != 0)
                {
                    return new ResponseModel(400, "Invalid Token", false);
                }
                return new ResponseModel();
            }
            catch (Exception ex)
            {
                return new ResponseModel(500, ex.Message, false);
            }
        }
    }
}
