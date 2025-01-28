using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IShiftRepository : IRepositoryBase<Shift>
    {
        Task ConfirmShiftAsync(int shiftId, int clientId, IEnumerable<int>? serviceIds, bool payShift);
        Task<Shift> GetShiftWithServicesAsync(int shiftId);

        Task<List<Shift>> GetShiftByUserId(int userId);

        Task<List<ServicesAndHaircuts>> GetServicesByIdsAsync(IEnumerable<int> serviceIds);

        Task SaveChangesAsync();

        Task<List<Shift>> GetByBarberShopAndDay(int barberShopId, DateOnly day);

        Shift GetById(int id);

        void Delete(Shift shift);
    }
}
