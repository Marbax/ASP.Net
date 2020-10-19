using InternetShop.Identity.Model;
using Microsoft.AspNet.Identity.EntityFramework;

namespace InternetShop.Identity.Store
{
    public class AppRoleStore : RoleStore<AppRole, int, AppUserRole>
    {
        public AppRoleStore(AppDbContext context) : base(context) { }
    }

}