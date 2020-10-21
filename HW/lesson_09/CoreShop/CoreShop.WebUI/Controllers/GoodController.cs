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
    public class GoodController : Controller
    {
        private readonly ILogger<GoodController> _logger;
        private readonly IRepository<Good> _gRepo;
        private readonly IRepository<Category> _cRepo;
        private readonly IRepository<Manufacturer> _mRepo;
        public GoodController(ILogger<GoodController> logger, IRepository<Good> gRepo, IRepository<Category> cRepo, IRepository<Manufacturer> mRepo)
        {
            _logger = logger;
            _gRepo = gRepo;
            _cRepo = cRepo;
            _mRepo = mRepo;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var list = _gRepo.GetAll().ToList();
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
            var g = await _gRepo.GetAsync(id);
            var gvm = g == null ? new GoodEditVM() : new GoodEditVM(g);
            gvm.Categories = await _cRepo.GetAllAsync().ToListAsync();
            gvm.Manufacturers = await _mRepo.GetAllAsync().ToListAsync();
            return View(gvm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(GoodEditVM gvm)
        {
            if (ModelState.IsValid)
            {
                using (var trans = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
                {
                    var g = await _gRepo.GetAsync(gvm.GoodId);
                    if (g != null)
                    {
                        g.GoodName = gvm.GoodName;
                        g.Price = gvm.Price;
                        g.GoodCount = gvm.GoodCount;
                        g.CategoryId = gvm.CategoryId;
                        g.ManufacturerId = gvm.ManufacturerId;
                        TempData["Message"] = $"{gvm.GoodName} has been modified.";
                    }
                    else
                    {
                        _gRepo.Add(gvm.Good);
                        TempData["Message"] = $"{gvm.GoodName} has been added.";
                    }
                    _gRepo.Save();
                    trans.Complete();
                }
                return RedirectToAction("Index");
            }
            else
                return View(gvm);
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int id)
        {
            var g = await _gRepo.GetAsync(id);
            if (g == null)
                return RedirectToAction("Error");

            using (var trans = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
            {
                _gRepo.Delete(g);
                _gRepo.Save();
                trans.Complete();
                TempData["Message"] = $"{g.GoodName} has been deleted.";
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