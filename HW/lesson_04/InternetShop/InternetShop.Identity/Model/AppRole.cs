using Microsoft.AspNet.Identity.EntityFramework;

namespace InternetShop.Identity.Model
{
    public class AppRole : IdentityRole<int, AppUserRole>
    {
        public AppRole() { }
        public AppRole(string name) { Name = name; }
    }
}
