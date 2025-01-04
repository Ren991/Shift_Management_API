using Application.Interfaces;
using Application.Models.UserDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceStack.Web;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult GetAll()
        {

            var users = _userService.GetAllUsers();

            return Ok(users);
        }



        [HttpGet("/email")]

        public IActionResult GetByEmail([FromQuery] string email)
        {
            return Ok(_userService.GetUserByEmail(email));
        }

 
        [HttpPost("/create-user")]

        public IActionResult AddUser([FromBody] UserCreateRequest user) // Este endpoint es para crear usuario comunes.

        {


            var newUser = _userService.AddNewUser(user);
            return Ok(newUser);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("/create-admin")]

        public IActionResult AddAdmin([FromBody] UserAdminRequest user) // Este endpoint es para crear usuario comunes.

        {
            var newUser = _userService.AddNewAdminUser(user);
            return Ok(newUser);
        }



        [Authorize(Roles = "Admin")]
        [HttpDelete("{userId}")]

        public IActionResult DeleteUser([FromRoute] int userId)
        {

            _userService.DeleteUser(userId);
            return Ok(new { message = "User deleted successfully." });
        }
    }
}
