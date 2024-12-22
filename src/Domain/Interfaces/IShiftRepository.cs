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

        Task<List<ServicesAndHaircuts>> GetServicesByIdsAsync(IEnumerable<int> serviceIds);

        Task SaveChangesAsync();
    }
}
