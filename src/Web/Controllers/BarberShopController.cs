using Application.Interfaces;
using Application.Models.BarberShopDtos;
using Application.Models.ServicesAndHaircutsDtos;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BarberShopController : ControllerBase
    {
        private readonly IBarberShopService _barberShopService; 
        public BarberShopController(IBarberShopService barberShopService)
        {
            _barberShopService = barberShopService;
        }

        [HttpGet]
        public IActionResult GetAll()
        {

            var barberShops = _barberShopService.GetAllBarberShop();

            return Ok(barberShops);
        }

        [HttpPost]

        public IActionResult AddBarberShop([FromBody] BarberShopRequest barberShop) // Este endpoint es para crear usuario comunes.

        {
            var newBarberShop = _barberShopService.AddNewBarberShop(barberShop);
            return Ok(newBarberShop);
        }
    }
}
