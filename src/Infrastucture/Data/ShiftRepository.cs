using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastucture.Data
{
    public class ShiftRepository : RepositoryBase<Shift>, IShiftRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IServicesAndHaircutsRepository _serviceRepository;

        public ShiftRepository(ApplicationDbContext context, IServicesAndHaircutsRepository serviceRepository) : base(context)
        {
            _serviceRepository = serviceRepository;
            _context = context;
        }

        public async Task ConfirmShiftAsync(int shiftId, int clientId, IEnumerable<int>? serviceIds, bool payShift)
        {
            //var shift = await _context.Shift.FindAsync(shiftId);
            var shift = await _context.Shift
                             .Include(s => s.Services) // Incluye servicios existentes
                             .FirstOrDefaultAsync(s => s.Id == shiftId);
            if (shift == null)
            {
                throw new Exception("Turno no encontrado"); // Or any other appropriate exception
            }

            var user = await _context.Users.FindAsync(clientId);
            if (user == null)
            {
                throw new Exception("Usuario no encontrado");
            }

            shift.Confirmed = true;
            shift.ClientID = clientId;

            if (payShift)
            {
                shift.IsPayabled = true;
            }

            // Get valid services using GetById from repository
            //var validServiceIds = serviceIds?.ToList() ?? new List<int>(); // Handle null or empty serviceIds
            //var validServices = new List<ServicesAndHaircuts>();

            // Validar y asociar servicios
            //var validServiceIds = serviceIds?.ToList() ?? new List<int>();
            //var validServices = await _context.ServicesAndHaircuts
            //.Where(s => validServiceIds.Contains(s.Id))
            //.ToListAsync();

            //if (validServices.Count != validServiceIds.Count)
            //{
            //    throw new Exception("Uno o más servicios no son válidos");
            //}

            //foreach (var serviceId in validServiceIds)
            //{
            //    var service =  _serviceRepository.GetById(serviceId); // Assuming GetByIdAsync is async
            //    if (service != null)
            //    {
            //        //validServices.Add(service);
            //        service.ShiftId = shiftId; // Asociar servicio al turno
            //    }
            //}
            var validServices = await _context.ServicesAndHaircuts
                                       .Where(s => serviceIds.Contains(s.Id))
                                       .ToListAsync();

            shift.Services = validServices;
            shift.Price = validServices.Sum(s => s.Price);
            shift.ClientID = clientId;
            shift.Confirmed = true;
            shift.IsPayabled = payShift;

            var totalPrice = validServices.Where(s => s != null).Sum(s => s.Price);
            shift.Price = totalPrice;

            // **Set the retrieved services to the shift**
            //shift.Services = validServices;

            await _context.SaveChangesAsync();
        }

        public List<Shift> Get()
        {
            return _context.Shift
                           .Include(s => s.Services) // Incluir la relación con servicios
                           .ToList();
        }

    }
}
