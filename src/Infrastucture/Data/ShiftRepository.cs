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


            await _context.SaveChangesAsync();
        }

        public List<Shift> Get()
        {
            return _context.Shift
                           .Include(s => s.Services) // Incluir la relación con servicios
                           .ToList();
        }

        public async Task<Shift> GetShiftWithServicesAsync(int shiftId)
        {
            return await _context.Shift
                                 .Include(s => s.Services)
                                 .FirstOrDefaultAsync(s => s.Id == shiftId);
        }

        public async Task<List<ServicesAndHaircuts>> GetServicesByIdsAsync(IEnumerable<int> serviceIds)
        {
            return await _context.ServicesAndHaircuts
                                 .Where(s => serviceIds.Contains(s.Id))
                                 .ToListAsync();
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }

    }
}
