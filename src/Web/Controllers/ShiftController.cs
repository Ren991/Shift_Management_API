using Application.Interfaces;
using Application.Models.ShiftDtos;
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

        [HttpGet]
        public IActionResult GetAll()
        {
            var shifts = _shiftService.GetAllShift();
            return Ok(shifts);
        }

        [HttpPost]
        public IActionResult AddShift([FromBody] ShiftCreateRequest shift)
        {
            var newShift = _shiftService.AddNewShift(shift);
            return Ok(newShift);
        }

        /*[HttpPut("confirm")]
        public async Task<IActionResult> ConfirmShift([FromBody] int shiftId, int clientId, List<int> serviceIds)
        {
            _shiftService.ConfirmShift(shiftId, clientId, serviceIds);
            return Ok();
        }*/
    }
}
