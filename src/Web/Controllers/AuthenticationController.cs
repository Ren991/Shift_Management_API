using Application.Interfaces;
using Application.Models.AuthDtos;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IAuthenticationService _customAuthenticationService;

        public AuthenticationController(IConfiguration config, IAuthenticationService autenticacionService)
        {
            _config = config;
            _customAuthenticationService = autenticacionService;
        }

        [HttpPost("authenticate")]
        public ActionResult<string> Autenticar(UserLoginRequest loginRequest)
        {
            string token = _customAuthenticationService.AuthenticateAsync(loginRequest);

            return Ok(token);
        }
    }
}