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

        public async Task<List<Shift>> GetAllShifts()
        {
            return await _context.Shift
                .Include(c => c.Services) // Incluir las líneas de venta de cada carrito
                .Include(s => s.BarberShop)
                .ToListAsync();
        }

        public async Task<Shift> UpdateAsync(Shift shift)
        {
            
            _context.Shift.Update(shift);
            await _context.SaveChangesAsync();
            return shift;
        }
    }
}
