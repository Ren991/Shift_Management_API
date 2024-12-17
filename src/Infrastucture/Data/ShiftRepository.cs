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

        public ShiftRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task ConfirmShiftAsync(int shiftId, int clientId, List<int> serviceIds)
        {
            var shift = await _context.Shift.FindAsync(shiftId);
            if (shift == null)
            {
                throw new Exception("Turno no encontrado"); // O cualquier otra excepción adecuada
            }

            var user = await _context.Users.FindAsync(clientId);
            if (user == null)
            {
                throw new Exception("Usuario no encontrado");
            }

            shift.Confirmed = true;
            shift.Client = user;

            // Obtener los servicios válidos
            var validServices = await _context.ServicesAndHaircuts
                .Where(s => serviceIds.Contains(s.Id))
                .ToListAsync();

            // Asignar los servicios válidos al turno
            shift.Services = validServices;

            await _context.SaveChangesAsync();
        }
    }
}
