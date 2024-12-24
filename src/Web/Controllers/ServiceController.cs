using Application.Interfaces;
using Application.Models.ServicesAndHaircutsDtos;
using Application.Models.UserDtos;
using Application.Services;
using Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private readonly IServicesAndHaircutsService _servicesAndHaircutService;
        public ServiceController(IServicesAndHaircutsService servicesAndHaircutService)
        {
            _servicesAndHaircutService = servicesAndHaircutService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult GetAll()
        {
            var userTypeString = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Role)?.Value;
            var services = _servicesAndHaircutService.GetAllServices();

            return Ok(services);
        }

        [HttpPost]

        public IActionResult AddService([FromBody] ServicesAndHaircutsRequest service) // Este endpoint es para crear usuario comunes.

        {
            var newService = _servicesAndHaircutService.AddNewService(service);
            return Ok(newService);
        }

        [HttpPut("edit")]

        public IActionResult UpdateService(int id, double price)
        {

            _servicesAndHaircutService.UpdateService(id, price);

            return Ok();



        }


        [HttpDelete("{serviceId}")]

        public IActionResult DeleteService([FromRoute] int serviceId)
        {

            _servicesAndHaircutService.DeleteService(serviceId);
            return Ok(new { message = "Service deleted successfully." });
        }
    }
}
