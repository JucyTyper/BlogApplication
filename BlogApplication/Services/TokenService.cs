using Azure;
using BlogApplication.Data;
using BlogApplication.Models;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BlogApplication.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration configuration;
        private readonly blogAppDatabase _db;
        ResponseModel response = new ResponseModel();

        public TokenService(IConfiguration configuration,blogAppDatabase _db)
        {
            this.configuration = configuration;
            this._db = _db;
        }
        public string CreateToken(string email,string Id,int type)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email,email),
                new Claim(ClaimTypes.Sid,Id),
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(configuration.GetSection("jwt:Key").Value!));
            var cred = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            JwtSecurityToken token;
            if (type== 1)
            {
                token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(5),
                signingCredentials: cred
                );
            }
            else if (type == 3)
            {
                claims.Add(new Claim(ClaimTypes.Role, "Admin"));
                token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred
                );
            }
            else
            {
                token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: cred
                );
            }
            
            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return jwt;
        }
        public void BlackListToken(string token)
        {
            var BLToken = new BlackListTokenModel
            {
                token = token
            };
            _db.BLTokens.Add(BLToken);
            _db.SaveChanges();
        }
        public ResponseModel CheckToken(string token)
        {
            try
            {
                var tokenCheck = _db.BLTokens.Where(x => x.token == token).Count();
                if (tokenCheck != 0)
                {
                    response.StatusCode = 400;
                    response.Message = "Invalid Token";
                    response.IsSuccess = false;
                    return response;
                }
                return response;
            }
            catch (Exception ex)
            {
                response.StatusCode = 500;
                response.Message = ex.Message;
                response.IsSuccess = false;
                return response;
            }
        }
    }
}
