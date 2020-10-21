using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using CoreShop.Domain.Abstract;
using CoreShop.Domain.Entities;
using CoreShop.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CoreShop.WebUI.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly IRepository<Category> _cRepo;
        public CategoryController(ILogger<CategoryController> logger, IRepository<Category> cRepo)
        {
            _logger = logger;
            _cRepo = cRepo;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var list = await _cRepo.GetAllAsync().ToListAsync();
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
            var c = await _cRepo.GetAsync(id);
            c = c ?? new Category();
            return View(c);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Category inCat)
        {
            if (ModelState.IsValid)
            {
                using (var trans = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
                {
                    var cat = await _cRepo.GetAsync(inCat.CategoryId);
                    if (cat != null)
                    {
                        cat.CategoryName = inCat.CategoryName;
                        TempData["Message"] = $"{cat.CategoryName} has been modified.";
                    }
                    else
                    {
                        _cRepo.Add(inCat);
                        TempData["Message"] = $"{inCat.CategoryName} has been added.";
                    }
                    _cRepo.Save();
                    trans.Complete();
                }
                return RedirectToAction("Index");
            }
            else
                return View(inCat);
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int id)
        {
            var cat = await _cRepo.GetAsync(id);
            if (cat == null)
                return RedirectToAction("Error");

            using (var trans = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
            {
                _cRepo.Delete(cat);
                _cRepo.Save();
                trans.Complete();
                TempData["Message"] = $"{cat.CategoryName} has been deleted.";
            }
            return RedirectToAction("Index");
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}