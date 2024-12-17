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
        Task ConfirmShiftAsync(int shiftId, int clientId, List<int> servicesId);

    }
}
