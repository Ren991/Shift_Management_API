﻿using Application.Interfaces;
using Application.Models.ShiftDtos;
using Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
//using ServiceStack;

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

        [Authorize(Roles = "Admin,Client")]
        [HttpGet]
        public IActionResult GetAll()
        {
            var shifts = _shiftService.GetAllShift();
            return Ok(shifts);
        }

        [Authorize(Roles = "Admin,Barber")]//YYYY-MM-DD
        [HttpPost]
        public IActionResult AddShift([FromBody] ShiftCreateRequest shift)
        {
            var newShift = _shiftService.AddNewShift(shift);
            return Ok(newShift);
        }

        [Authorize(Roles = "Client")]
        [HttpPost("confirm")]
        public async Task<IActionResult> ConfirmShift(int shiftId, int clientId, IEnumerable<int> serviceIds, bool payShift)
        {
            await _shiftService.ConfirmShift(shiftId, clientId, serviceIds, payShift);
            return Ok();
        }

        [Authorize]
        [HttpGet("filter")]
        public IActionResult FindByDayAndBarberShop(int barberShopId, DateOnly day) 
        {
            var filteredShift = _shiftService.GetByBarberShopAndDay(barberShopId, day);

            return Ok(filteredShift);
        }

        [Authorize(Roles = "Client")]
        [HttpPut("cancel-shift")]
        public async Task<IActionResult> CancelShift( int shiftId)
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                throw new Exception("User ID is not valid.");
            }

            await _shiftService.CancelShift(shiftId,userId);

           return Ok();
           
        }

        [Authorize(Roles="Client")]
        [HttpGet("get-shifts-by-user")]
        public async Task<IActionResult> GetShiftsById()
        {
            var userIdClaim = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdClaim) || !int.TryParse(userIdClaim, out int userId))
            {
                throw new Exception("User ID is not valid.");
            }

            var shifts = await _shiftService.GetShiftByUser(userId);
            return Ok(shifts);
        }

        [Authorize(Roles = "Admin,Barber")]
        [HttpPost("create-predefined")]
        public async Task<IActionResult> CreatePredefinedShifts(int month , int year, int barberShopId)
        {
            try
            {
                // Llama al servicio para crear los turnos
                await _shiftService.CreatePredefinedShifts(month, year, barberShopId);

                return Ok(new { Message = "Turnos predefinidos creados exitosamente." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { Error = ex.Message });
            }
        }


        [Authorize(Roles = "Admin")]
        [HttpDelete("{shiftId}")]
        public async Task<IActionResult> DeleteShift([FromRoute] int shiftId)
        {
            try
            {
                // Llama al método DeleteShift del servicio
                await _shiftService.DeleteShift(shiftId);
                return Ok(new { message = "Shift deleted successfully." });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


    }
}
