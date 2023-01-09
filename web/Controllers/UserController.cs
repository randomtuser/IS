using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using web.Models;
using web.Services;
using web.Entities;

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
        
        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {
            return Ok(userService.GetUser(id));
        }

        [Authorize(Roles = "Admin, Regular")]
        [HttpDelete("{id}")]
        public IActionResult Delete(int id) 
        {
            userService.DeleteUser(id);
            return Ok(new { message = "User was successfully deleted!" });
        }

        [Authorize(Roles = "Admin, Regular")]
        [HttpPut("{id}")]
        public IActionResult Update(UserModel model)
        {
            userService.UpdateUser(model);
            return Ok(new { message = "User was successfully updated!" });
        }
    }
}
