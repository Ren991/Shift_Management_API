using Application.Interfaces;
using Application.Models.UserDtos;
using Microsoft.AspNetCore.Mvc;

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

        [HttpPost]

        public IActionResult AddUser([FromBody] UserCreateRequest user) // Este endpoint es para crear usuario comunes.

        {
            var newUser = _userService.AddNewUser(user);
            return Ok(newUser);
        }

        [HttpDelete("{userId}")]

        public IActionResult DeleteUser([FromRoute] int userId)
        {

            _userService.DeleteUser(userId);
            return Ok(new { message = "User deleted successfully." });
        }
    }
}
