using System.Collections.Generic;
using jwt_sample.Models;

namespace jwt_sample.Services
{
    public interface IUserService
    {
         User Authenticate(string login, string password);
        IEnumerable<User> GetAll();
    }
}