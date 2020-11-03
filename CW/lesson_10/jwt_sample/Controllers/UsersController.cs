using System.Collections.Generic;
using jwt_sample.Models;
using jwt_sample.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace jwt_sample.Controllers
{

    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class UsersController : Controller
    {

        private IUserService userService;
        public UsersController(IUserService userService)
        {
            this.userService = userService;
        }


        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody]User userData)
        {
            var user = userService.Authenticate(userData.Login, userData.Password);

            if (user == null)
                return BadRequest("Incorrect login or password");
            
            return Ok(user);
        } 



        public IActionResult GetUsers()
        {
           var users = userService.GetAll();
           return Ok(users);
        }
    }
}