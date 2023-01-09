using Microsoft.AspNetCore.Mvc;
using web.Entities;
using web.Models;
using web.Services;

namespace web.Controllers 
{
    [ApiController]
    [Route("users")]
    public class UserController : ControllerBase
    {
        private IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterModel model) 
        {
            userService.Register(model);
            return Ok(new { message = "Registration was successful!" });
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthRequest model) 
        {
            return Ok(userService.Authenticate(model));
        }

        [HttpGet]
        public IActionResult Get()
        {
            
            return Ok(userService.GetUsers());
        }

        [HttpDelete("remove")]
        public IActionResult Delete(int id) 
        {
            userService.RemoveById(id);
            return Ok(new { message = "User was successfully deleted!" });
        }
    }
}
