using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public string GetAllUsers()
        {
            string respuesta = "Acá devuelve todos los usuarios";

            return respuesta;
        }

        [HttpGet("get-by-email")]
        public string GetUserByEmail(string email)
        {
            string respuesta = "El email es " + email;
            return respuesta;
        }

        [HttpPost]
        public string AddNewUser()
        {
            string respuesta = "Se creo correctamente el usuario";
            return respuesta;
        }

        [HttpDelete("delete-user")]
        public string DeleteUser()
        {
            string respuesta = "Usuario eliminado con exito";
            return respuesta;
        }
    }
}
