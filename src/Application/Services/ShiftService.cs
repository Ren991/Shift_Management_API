using Application.Interfaces;
using Application.Models.ShiftDtos;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ShiftService: IShiftService
    
    {
        private readonly IShiftRepository _shiftRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBarberShopRepository _barberShopRepository;
        public ShiftService(IShiftRepository shiftRepository, IUserRepository userRepository, IBarberShopRepository barberShopRepository)
        {
            _shiftRepository = shiftRepository;
            _userRepository = userRepository;
            _barberShopRepository = barberShopRepository;
        }

        public List<Shift> GetAllShift()
        {
            var shifts = _shiftRepository.Get();
            return shifts;
        }

        public ShiftDto AddNewShift(ShiftCreateRequest shiftDto)
        {
            var shift = ShiftCreateRequest.ToEntity(shiftDto);

            var barber = _userRepository.Get(shiftDto.Barber.Id);
            if (barber == null)
            {
                throw new Exception("Barber not found");
            }

            var barberShop = _barberShopRepository.Get(shiftDto.BarberShop.Id);
            if (barberShop == null)
            {
                throw new Exception("Barber not found");
            }

            shift.BarberShop = barberShop;
            shift.Barber = barber;
            

            var createdShift = _shiftRepository.Create(shift);

            return ShiftDto.ToDto(createdShift);
        }

        public async void ConfirmShift(int shiftId, int clientId, List<int> serviceIds)
        {
            await _shiftRepository.ConfirmShiftAsync(shiftId, clientId, serviceIds);
        }
    }
}
