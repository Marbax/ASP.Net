using System.Collections.Generic;
using CoreShop.Domain.Entities;

namespace CoreShop.WebUi.Services.Abstract
{
    public interface IUserService
    {
        User Authenticate(string login, string password);
        IEnumerable<User> GetAll();
    }
}