using Application.Interfaces;
using Application.Models.ShiftDtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftController : ControllerBase
    {
        private readonly IShiftService _shiftService;
        public ShiftController(IShiftService shiftService)
        {
            _shiftService = shiftService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult GetAll()
        {
            var shifts = _shiftService.GetAllShift();
            return Ok(shifts);
        }

        [Authorize(Roles = "Admin,Barber")]
        [HttpPost]
        public IActionResult AddShift([FromBody] ShiftCreateRequest shift)
        {
            var newShift = _shiftService.AddNewShift(shift);
            return Ok(newShift);
        }

        [Authorize(Roles = "Client")]
        [HttpPost("confirm")]
        public async Task<IActionResult> ConfirmShift(int shiftId, int clientId, [FromBody] IEnumerable<int> serviceIds, bool payShift)
        {
            await _shiftService.ConfirmShift(shiftId, clientId, serviceIds, payShift);
            return Ok();
        }

        [Authorize]
        [HttpGet("filter")]
        public IActionResult FindByDayAndBarberShop([FromQuery] int barberShopId, [FromQuery] DateTime day) 
        {
            var filteredShift = _shiftService.GetByBarberShopAndDay(barberShopId, day);

            return Ok(filteredShift);
        }

        [Authorize(Roles = "Client")]
        [HttpPut("cancel-shift")]
        public async Task<IActionResult> CancelShift([FromQuery] int shiftId)
        { 
           await  _shiftService.CancelShift(shiftId);

            return Ok();
           
        }

    }
}
