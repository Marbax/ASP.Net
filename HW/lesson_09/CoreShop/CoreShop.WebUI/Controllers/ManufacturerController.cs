using System.Diagnostics;
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
    public class ManufacturerController : Controller
    {
        private readonly ILogger<ManufacturerController> _logger;
        private readonly IRepository<Manufacturer> _mRepo;
        public ManufacturerController(ILogger<ManufacturerController> logger, IRepository<Manufacturer> mRepo)
        {
            _logger = logger;
            _mRepo = mRepo;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var list = await _mRepo.GetAllAsync().ToListAsync();
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
            var man = await _mRepo.GetAsync(id);
            man = man ?? new Manufacturer();
            return View(man);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Manufacturer inMan)
        {
            if (ModelState.IsValid)
            {
                using (var trans = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
                {
                    var cat = await _mRepo.GetAsync(inMan.ManufacturerId);
                    if (cat != null)
                    {
                        cat.ManufacturerName = inMan.ManufacturerName;
                        TempData["Message"] = $"{cat.ManufacturerName} has been modified.";
                    }
                    else
                    {
                        _mRepo.Add(inMan);
                        TempData["Message"] = $"{inMan.ManufacturerName} has been added.";
                    }
                    _mRepo.Save();
                    trans.Complete();
                }
                return RedirectToAction("Index");
            }
            else
                return View(inMan);
        }

        [HttpPost]
        public async Task<IActionResult> Remove(int id)
        {
            var man = await _mRepo.GetAsync(id);
            if (man == null)
                return RedirectToAction("Error");

            using (var trans = new TransactionScope(TransactionScopeOption.RequiresNew, TransactionScopeAsyncFlowOption.Enabled))
            {
                _mRepo.Delete(man);
                _mRepo.Save();
                trans.Complete();
                TempData["Message"] = $"{man.ManufacturerName} has been deleted.";
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