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
        private readonly IServicesAndHaircutsRepository _servicesRepository;
        public ShiftService(IShiftRepository shiftRepository, IUserRepository userRepository, IBarberShopRepository barberShopRepository, IServicesAndHaircutsRepository servicesRepository)
        {
            _shiftRepository = shiftRepository;
            _userRepository = userRepository;
            _barberShopRepository = barberShopRepository;
            _servicesRepository = servicesRepository;
        }

        public async Task<List<Shift>> GetAllShift()
        {
            var shifts = _shiftRepository.Get();
            //var shiftDtos = shifts.Select(shift => ShiftDto.ToDto(shift)).ToList();
            return shifts;
        }

        public ShiftDto AddNewShift(ShiftCreateRequest shiftDto)
        {
            var shift = ShiftCreateRequest.ToEntity(shiftDto);

            var barber = _userRepository.Get(shiftDto.BarberID);
            if (barber == null)
            {
                throw new Exception("Barber not found");
            }

            var barberShop = _barberShopRepository.Get(shiftDto.BarberShopID);
            if (barberShop == null)
            {
                throw new Exception("Barber not found");
            }

            shift.BarberShop = barberShop;
            shift.BarberID = barber.Id;
            

            var createdShift = _shiftRepository.Create(shift);

            return ShiftDto.ToDto(createdShift);
        }

        public async Task<Shift> ConfirmShiftAsync(int shiftId, int clientId, IEnumerable<int> serviceIds, bool payShift)
        {
            var shift =  _shiftRepository.Get(shiftId);
            if (shift == null)
            {
                throw new Exception("Turno no encontrado");
            }

            var validServiceIds = serviceIds?.ToList() ?? new List<int>(); // Handle null or empty serviceIds
            var validServices = new List<ServicesAndHaircuts>();

            foreach (var serviceId in validServiceIds)
            {
                var service = _servicesRepository.Get(serviceId); // Assuming GetByIdAsync is async
                if (service != null)
                {
                    validServices.Add(service);
                }
            }

            // Validar servicios y actualizar el turno

            shift.Confirmed = true;
            shift.ClientID = clientId;
            shift.IsPayabled = payShift;            
            

            return await _shiftRepository.UpdateAsync(shift);
        }

        
    }
}
