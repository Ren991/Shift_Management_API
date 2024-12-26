using Application.Interfaces;
using Application.Models.BarberShopDtos;
using Application.Models.ServicesAndHaircutsDtos;
using Application.Services;
using Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceStack;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

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

        [Authorize]
        [HttpGet]
        public IActionResult GetAll()
        {

            var barberShops = _barberShopService.GetAllBarberShop();

            return Ok(barberShops);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]

        public IActionResult AddBarberShop([FromBody] BarberShopRequest barberShop) // Este endpoint es para crear usuario comunes.

        {
            var newBarberShop = _barberShopService.AddNewBarberShop(barberShop);
            return Ok(newBarberShop);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{barberShopId}")]

        public IActionResult DeleteBarberShop([FromRoute] int barberShopId)
        {

            _barberShopService.DeleteBarberShop(barberShopId);
            return Ok(new { message = "BarberShop deleted successfully." });
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("edit")]

        public IActionResult UpdateName( int id, string premiseName) 
        {

            _barberShopService.UpdateBarberShop(id, premiseName);

            return Ok();



        }


    }
}
