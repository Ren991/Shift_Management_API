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
                "d/M/yyyy HH:mm", // Cambiado el formato al esperado
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

        public async Task CreatePredefinedShifts(int month, int year, int barberShopID)
        {
            // Validar entrada
            if (month < 1 || month > 12)
                throw new Exception("El mes es inválido.");

            if (year < DateTime.UtcNow.Year)
                throw new Exception("El año no puede ser menor al actual.");

            if (barberShopID <= 0)
                throw new Exception("BarberShopID inválido.");

            // Configuración fija
            var startTime = new TimeSpan(9, 0, 0); // 09:00
            var endTime = new TimeSpan(18, 0, 0);  // 18:00
            var workingDays = new[] { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday };

            // Generar los turnos
            //var shiftsToCreate = new List<Shift>();
            var daysInMonth = DateTime.DaysInMonth(year, month);

            for (int day = 1; day <= daysInMonth; day++)
            {
                var currentDate = new DateTime(year, month, day);

                if (workingDays.Contains(currentDate.DayOfWeek))
                {
                    for (var time = startTime; time < endTime; time = time.Add(TimeSpan.FromHours(1)))
                    {
                        var shift = new Shift
                        {
                            Day = DateOnly.FromDateTime(currentDate),
                            ShiftTime = time.ToString(@"hh\:mm"),
                            Confirmed = false,
                            IsPayabled = false,
                            BarberShopID = barberShopID,
                            BarberID = 1,
                            Price = 0
                        };
                        //shiftsToCreate.Add(shift);
                        _shiftRepository.Create(shift);
                    }
                }
            }

            // Guardar los turnos
            //await _shiftRepository.AddRangeAsync(shiftsToCreate);
            await _shiftRepository.SaveChangesAsync();
        }

        public Shift GetById(int id)
        {
            return _shiftRepository.Get(id); // Método que busca por ID
        }

        public void Delete(Shift shift)
        {
            _shiftRepository.Delete(shift); // Llama al método Delete del repositorio base
        }


        public async Task DeleteShift(int shiftId)
        {
            // Busca el turno en la base de datos
            var shift = _shiftRepository.GetById(shiftId);

            if (shift == null)
            {
                throw new Exception("Shift not found.");
            }

            // Llama al método Delete del repositorio para eliminar el turno
            _shiftRepository.Delete(shift);

            // Guarda los cambios en la base de datos
            await _shiftRepository.SaveChangesAsync();
        }

    }

}