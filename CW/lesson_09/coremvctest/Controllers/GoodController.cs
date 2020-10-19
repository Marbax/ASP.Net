using coremvctest.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace coremvctest.Controllers
{
    public class GoodController : Controller
    {
        IRepository goodRepository;
        public GoodController(IRepository goodRepository)
        {
            this.goodRepository = goodRepository;
        }
        public IActionResult Index()
        {
            return View(goodRepository.GetAll());
        }
    }
}