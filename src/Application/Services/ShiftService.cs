using Application.Interfaces;
using Application.Models.ShiftDtos;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ShiftService : IShiftService

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

        public async Task ConfirmShift(int shiftId, int clientId, IEnumerable<int>? serviceIds, bool payShift)
        {
            // Obtener el turno y validar que exista
            var shift = await _shiftRepository.GetShiftWithServicesAsync(shiftId);
            if (shift == null)
            {
                throw new Exception("Turno no encontrado");
            }


            // Validar que el cliente exista
            var user = _userRepository.Get(clientId);
            if (user == null)
            {
                throw new Exception("Usuario no encontrado");
            }

            var shiftsForClientOnSameDay = await _shiftRepository.GetByBarberShopAndDay(shift.BarberShopID, shift.Day);
                                                         // <-- Aquí se ejecuta la tarea y se obtiene la lista

            // Ahora puedes aplicar Where a la lista
            var filteredShifts = shiftsForClientOnSameDay.Where(s => s.ClientID == clientId).ToList();

            // Validar la cantidad de turnos
            if (filteredShifts.Count >= 1)
            {
                throw new Exception("El usuario ya tiene un turno reservado para este día.");
            }

            // Marcar el turno como confirmado y asociar el cliente
            shift.Confirmed = true;
            shift.ClientID = clientId;

            if (payShift)
            {
                shift.IsPayabled = true;
            }

            // Obtener y validar los servicios
            var validServices = await _shiftRepository.GetServicesByIdsAsync(serviceIds ?? Enumerable.Empty<int>());
            if (validServices.Count != (serviceIds?.Count() ?? 0))
            {
                throw new Exception("Uno o más servicios no son válidos");
            }

            // Asociar servicios al turno
            shift.Services = validServices;

            // Calcular el precio total del turno
            shift.Price = validServices.Sum(s => s.Price);

            // Guardar los cambios
            await _shiftRepository.SaveChangesAsync();
        }

        public async Task<List<Shift>> GetByBarberShopAndDay(int barberShopId, DateOnly day)
        {
            return await _shiftRepository.GetByBarberShopAndDay(barberShopId, day);
        }

        public async Task CancelShift(int shiftId, int userId)
        {
            var shift = await _shiftRepository.GetShiftWithServicesAsync(shiftId);
            if (shift == null)
            {
                throw new Exception("Turno no encontrado");
            }

            if(userId != shift.ClientID)
            {
                throw new Exception("Usuario no coincide");
            }

            var shiftDateTime = DateTime.ParseExact(
                $"{shift.Day} {shift.ShiftTime}",
                "dd/MM/yyyy HH:mm", // Cambiado el formato al esperado
                CultureInfo.InvariantCulture
            );

            // Validar si el turno puede ser cancelado con 24 horas de anticipación
            var currentTime = DateTime.UtcNow; 
            if (shiftDateTime < currentTime.AddHours(24))
            {
                throw new Exception("El turno no puede ser cancelado con menos de 24 horas de anticipación");
            }

            shift.Confirmed = false;
            shift.ClientID = null;
            shift.IsPayabled = false;
            shift.Services = [];

            // Guardar los cambios
            await _shiftRepository.SaveChangesAsync();
        }

        public async Task<List<Shift>> GetShiftByUser(int userId)
        {

            
            return await _shiftRepository.GetShiftByUserId(userId);
        }
    }
}