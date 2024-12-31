using Application.Models.ServicesAndHaircutsDtos;
using Application.Models.UserDtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IServicesAndHaircutsService
    {
        IEnumerable<ServicesAndHaircuts> GetAllServices();
        
        ServicesAndHaircutsDto AddNewService(ServicesAndHaircutsRequest serviceDto);

        //UserDto AddNewAdminUser(UserAdminCreateRequest userDto);

        ServicesAndHaircutsDto GetServiceByName(string name);

        ServicesAndHaircutsDto GetServiceById(int id);

        void UpdateService(int id, double price);

        void DeleteService(int id);
    }
}
