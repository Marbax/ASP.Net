using CoreShop.Domain.Entities;
using CoreShop.WebUi.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoreShop.WebUI.Controllers.api
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [AllowAnonymous]
        [HttpPost("Authenticate")]
        public IActionResult Authenticate([FromBody] User userData)
        {
            var user = _userService.Authenticate(userData.Login, userData.Password);

            if (user == null)
                return BadRequest("Incorrect login or password");

            return Ok(user);
        }

        [Authorize(Roles = "admin")]
        [HttpGet("GetUsers")]
        public IActionResult GetUsers()
        {
            var users = _userService.GetAll();
            return Ok(users);
        }
    }
}