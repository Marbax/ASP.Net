using InternetShop.Identity.Model;
using Microsoft.AspNet.Identity.EntityFramework;

namespace InternetShop.Identity.Store
{
    public class AppUserStore : UserStore<AppUser, AppRole, int, AppUserLogin, AppUserRole, AppUserClaim>
    {
        public AppUserStore(AppDbContext context) : base(context) { }
    }

}