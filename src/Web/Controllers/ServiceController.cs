using Application.Interfaces;
using Application.Models.ServicesAndHaircutsDtos;
using Application.Models.UserDtos;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

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


        [HttpGet]
        public IActionResult GetAll()
        {

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
