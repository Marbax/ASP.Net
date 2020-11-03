using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using jwt_sample.Helpers;
using jwt_sample.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace jwt_sample.Services
{
    public class UserService : IUserService
    {
        private List<User> users = new List<User>
        {
            new User {Id = 1, Name = "Alex", Lastname = "Klar", Login = "admin", Password = "admin"}
        };

        private readonly AppSettings appSettings;

        public UserService(IOptions<AppSettings> appSettings)
        {
            this.appSettings = appSettings.Value;
        }
        public User Authenticate(string login, string password)
        {
            var user = users.SingleOrDefault(x => x.Login == login && x.Password == password);
            if (user == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.Role, "admin")
                }),
                Expires = DateTime.UtcNow.AddMinutes(10),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            user.Password = null;
            return user;
        }

        public IEnumerable<User> GetAll()
        {
            return users.Select(x => {
                x.Password = null;
                return x;
            });
        }
    }
}