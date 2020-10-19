using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Security.Claims;
using System.Threading.Tasks;

namespace InternetShop.Identity.Model
{
    public class AppUser : IdentityUser<int, AppUserLogin, AppUserRole, AppUserClaim>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int SubdivisionId { get; set; }

    }

    public class AppUserRole : IdentityUserRole<int> { }

    public class AppUserClaim : IdentityUserClaim<int> { }

    public class AppUserLogin : IdentityUserLogin<int> { }
}
