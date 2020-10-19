using InternetShop.Identity.Model;
using InternetShop.WebUI.Infrastructure;
using InternetShop.WebUI.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

/// <summary>
//--------------------------------------------------------------------------------------------------------------------
//  Добавить в проект магазина аутентификацию.                                                                       +
//  Есть одна роль на данный момент - админ.Логин и пароль - admin.                                                  +(pass - admin1 , coz pass can't be shorter then 6 sym)
//  При просмотре списка товаров, кнопки редактирования, 
//      добавления и удаления отображаются только тогда, когда пользователь аутентифицирован.                        +
//  Реализовать кнопку входа в систему и выхода из неё.                                                              +
//--------------------------------------------------------------------------------------------------------------------
/// </summary>
namespace InternetShop.WebUI.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<AppUser, int> _userManager;

        public AuthController() : this(Startup.UserManagerFactory.Invoke()) { }

        public AuthController(UserManager<AppUser, int> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult LogIn(string returnUrl) => View(new LoginVM { ReturnUrl = returnUrl });

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> LogIn(LoginVM model)
        {
            if (_userManager.Users.Count() < 1)
            {
                ModelState.AddModelError("", "There no users.");
                return View(model);
            }

            if (!ModelState.IsValid)
                return View();

            var user = await _userManager.FindAsync(model.UserName, model.Password);

            if (user != null)
            {
                var identity = await _userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);

                GetAuthenticationManager().SignIn(identity);
                return Redirect(model.ReturnUrl ?? "/Admin/Goods");
            }
            ModelState.AddModelError("", "Invalid username or password");
            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        private async Task SignIn(AppUser user)
        {
            var identity = await _userManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            identity.AddClaim(new Claim(ClaimTypes.Name, user.FirstName));
            identity.AddClaim(new Claim(ClaimTypes.Surname, user.LastName));

            GetAuthenticationManager().SignIn(identity);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            GetAuthenticationManager().SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Goods", "Admin");
        }

        private IAuthenticationManager GetAuthenticationManager() => Request.GetOwinContext().Authentication;

    }
}