using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using CoreShop.Domain.Abstract;
using CoreShop.Domain.Entities;
using CoreShop.WebUi.Services.Abstract;
using CoreShop.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CoreShop.WebUI.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;
        private readonly IRepository<User> _uRepo;
        private readonly IUserService _uServ;
        public UserController(ILogger<UserController> logger, IRepository<User> uRepo, IUserService uServ)
        {
            _logger = logger;
            _uRepo = uRepo;
            _uServ = uServ;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var list = await _uRepo.GetAllAsync().ToListAsync();
            return View(list);
        }

        [HttpGet]
        public IActionResult CreateNew()
        {
            return RedirectToAction("Edit", 0);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var c = await _uRepo.GetAsync(id);
            c = c ?? new User();
            return View(c);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(User inUser)
        {
            if (ModelState.IsValid)
            {
                using (var trans = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
                {
                    var user = await _uRepo.GetAsync(inUser.Id);
                    if (user != null)
                    {
                        user.Name = inUser.Login;
                        user.Name = inUser.Token;
                        user.Name = inUser.Password;
                        user.Name = inUser.Name;
                        user.Name = inUser.Lastname;
                        TempData["Message"] = $"{user.Name} has been modified.";
                    }
                    else
                    {
                        _uRepo.Add(inUser);
                        TempData["Message"] = $"{inUser.Name} has been added.";
                    }
                    _uRepo.Save();
                    trans.Complete();
                }
                return RedirectToAction("Index");
            }
            else
                return View(inUser);
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int id)
        {
            var user = await _uRepo.GetAsync(id);
            if (user == null)
                return RedirectToAction("Error");

            using (var trans = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
            {
                _uRepo.Delete(user);
                _uRepo.Save();
                trans.Complete();
                TempData["Message"] = $"{user.Name} has been deleted.";
            }
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}