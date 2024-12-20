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

        [HttpPost("confirm")]
        public async Task<IActionResult> ConfirmShift(int shiftId, int clientId, [FromBody] IEnumerable<int> serviceIds, bool payShift)
        {
           var shiftConfirmed = _shiftService.ConfirmShiftAsync(shiftId, clientId, serviceIds, payShift);
            return Ok(shiftConfirmed);
        }
    }
}
