using Application.Models.BarberShopDtos;
using Application.Models.ShiftDtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IShiftService
    {
        List<Shift> GetAllShift();

        ShiftDto AddNewShift(ShiftCreateRequest shiftDto);

        Task ConfirmShift(int shiftId, int clientId, IEnumerable<int>? serviceIds, bool payShift);

        Task<List<Shift>> GetByBarberShopAndDay(int barberShopId, DateOnly day);

        Task<List<Shift>> GetShiftByUser(int userId);

        Task CancelShift(int shiftId);
    }
}