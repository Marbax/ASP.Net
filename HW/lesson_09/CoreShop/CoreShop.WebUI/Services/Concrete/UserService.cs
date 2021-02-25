using System.Collections.Generic;
using CoreShop.Domain.Abstract;
using CoreShop.Domain.Entities;
using System.Linq;
using System.Security.Claims;
using System;
using System.Text;
using CoreShop.WebUi.Services.Abstract;
using CoreShop.WebUI.Helpers;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using System.Transactions;

namespace CoreShop.WebUi.Services.Services
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;
        private readonly IRepository<User> _uRepo;
        public UserService(IOptions<AppSettings> appSettings, IRepository<User> uRepo)
        {
            _appSettings = appSettings.Value;
            _uRepo = uRepo;
        }
        public User Authenticate(string login, string password)
        {
            var user = _uRepo.GetAll().SingleOrDefault(x => x.Login == login && x.Password == password);
            if (user == null)
                return null;

            using (var trans = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Name, user.Name));
                claims.Add(new Claim(ClaimTypes.Role, user.Role));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new System.Security.Claims.ClaimsIdentity(claims),
                    Expires = DateTime.UtcNow.AddYears(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                user.Token = tokenHandler.WriteToken(token);

                _uRepo.Save();
                trans.Complete();
            }
            user.Password = null;
            return user;
        }

        public IEnumerable<User> GetAll()
        {
            return _uRepo.GetAll().Select(x =>
            {
                x.Password = null;
                return x;
            });
        }

    }
}