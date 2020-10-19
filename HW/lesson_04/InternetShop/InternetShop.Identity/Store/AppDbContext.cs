using InternetShop.Identity.Manager;
using InternetShop.Identity.Model;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace InternetShop.Identity.Store
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, int, AppUserLogin, AppUserRole, AppUserClaim>
    {
        public AppDbContext() : base("ShopIdentity")
        {
            if (!Database.Exists("ShopIdentity"))
                Database.SetInitializer<AppDbContext>(new DbInit());
        }

        public static AppDbContext Create()
        {
            return new AppDbContext();
        }
    }

    public class DbInit : DropCreateDatabaseIfModelChanges<AppDbContext>
    {
        private async Task SeedAsync(AppDbContext context)
        {
            if (!context.Roles.Any(r => r.Name == "AppAdmin"))
            {
                var store = new AppRoleStore(context);
                var manager = new AppRoleManager(store);
                var role = new AppRole { Name = "AppAdmin" };

                await manager.CreateAsync(role);
                var roleManager = new AppRole { Name = "AppManager" };
                await manager.CreateAsync(roleManager);
            }
            if (!context.Users.Any(u => u.UserName.ToLower() == "admin"))
            {
                var store = new AppUserStore(context);
                var manager = new AppUserManager(store);
                var user = new AppUser
                {
                    FirstName = "Ad",
                    LastName = "Min",
                    UserName = "admin",
                    SubdivisionId = 0,
                    Email = "admin@admin.com",
                    EmailConfirmed = true,
                    PhoneNumber = "0123456789",
                    PhoneNumberConfirmed = true
                };

                var res = await manager.CreateAsync(user, "admin1"); // min 6 anywhere

                await manager.AddToRoleAsync(user.Id, "AppAdmin");
                await manager.AddToRoleAsync(user.Id, "AppManager");

                await manager.AddClaimAsync(user.Id, new Claim(ClaimTypes.GivenName, "A Person"));
                await manager.AddClaimAsync(user.Id, new Claim(ClaimTypes.Gender, "Man"));
                await manager.AddClaimAsync(user.Id, new Claim(ClaimTypes.DateOfBirth, "01.01.2001"));
            }
            if (!context.Users.Any(u => u.UserName.ToLower() == "manager"))
            {
                var store = new AppUserStore(context);
                var manager = new AppUserManager(store);
                var userGreo1 = new AppUser
                {
                    FirstName = "Mana",
                    LastName = "Ger",
                    UserName = "manager",
                    SubdivisionId = 1,
                    Email = "manager@manager.com",
                    EmailConfirmed = true,
                    PhoneNumber = "0123456789",
                    PhoneNumberConfirmed = true
                };

                await manager.CreateAsync(userGreo1, "manager");

                await manager.AddToRoleAsync(userGreo1.Id, "AppManager");
            }
        }

        protected override void Seed(AppDbContext context)
        {
            Task.Run(async () => { await SeedAsync(context); }).Wait();
            base.Seed(context);
        }
    }
}