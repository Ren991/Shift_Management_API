using Application.Interfaces;
using Application.Models.ServicesAndHaircutsDtos;
using Application.Models.UserDtos;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ServicesAndHaircutsService: IServicesAndHaircutsService
    
    {
        private readonly IServicesAndHaircutsRepository _servicesAndHaircutsRepository;
        public ServicesAndHaircutsService(IServicesAndHaircutsRepository servicesAndHaircutsRepository)
        {
            _servicesAndHaircutsRepository = servicesAndHaircutsRepository;
        }

        public IEnumerable<ServicesAndHaircuts> GetAllServices()
        {
            var services = _servicesAndHaircutsRepository.GetAllActive();
            return services;
        }

        public ServicesAndHaircutsDto AddNewService(ServicesAndHaircutsRequest serviceDto)
        {

            var existingService = _servicesAndHaircutsRepository.GetByName(serviceDto.Name);
            if (existingService != null)
            {
                throw new Exception("Service already exists. Please try again.");
            }
            
            var service = ServicesAndHaircutsRequest.ToEntity(serviceDto);
            var createdService = _servicesAndHaircutsRepository.Create(service);

            return ServicesAndHaircutsDto.ToDto(createdService);
        }

        /*ServicesAndHaircutsDto GetServiceByName(string name);

        ServicesAndHaircutsDto GetServiceById(int id);*/

        public ServicesAndHaircutsDto GetServiceByName(string name)
        {
            return ServicesAndHaircutsDto.ToDto(_servicesAndHaircutsRepository.GetByName(name)!);
        }

        public ServicesAndHaircutsDto GetServiceById(int id)
        {
            return ServicesAndHaircutsDto.ToDto(_servicesAndHaircutsRepository.GetById(id)!);
        }

        public void UpdateService(int id, double price)
        {
            ServicesAndHaircuts? service = _servicesAndHaircutsRepository.Get(id);
            if (service == null)
            {
                throw new Exception("Service not found.");
            }
            service.Price = price;
            _servicesAndHaircutsRepository.Update(service);
        }

        public void DeleteService(int serviceId) //BAJA LOGICA
        {
            var service = _servicesAndHaircutsRepository.Get(serviceId);
            if (service != null)
            {
                service.IsActive = false;
                _servicesAndHaircutsRepository.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Service not found.");
            }
        }

    }
}
