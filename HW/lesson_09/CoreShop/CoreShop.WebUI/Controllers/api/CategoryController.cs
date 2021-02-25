using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;
using CoreShop.Domain.Abstract;
using CoreShop.Domain.Entities;
using CoreShop.WebUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CoreShop.WebUI.Controllers.api
{
    [Authorize(Roles = "admin,manager")]
    [ApiController]
    [Route("api/[controller]")]
    public class CategoryController : Controller
    {
        private readonly ILogger<CategoryController> _logger;
        private readonly IRepository<Category> _cRepo;
        public CategoryController(ILogger<CategoryController> logger, IRepository<Category> cRepo)
        {
            _logger = logger;
            _cRepo = cRepo;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var list = _cRepo.GetAllAsync().ToList().Select(c => new { id = c.CategoryId, ThreadPriorityLevel = c.CategoryName });
            return Ok(list);
        }
    }
}