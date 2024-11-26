using Application.Interfaces;
using Application.Models.BarberShopDtos;
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
    public class BarberShopService: IBarberShopService
    { 
        private readonly IBarberShopRepository _barberShopRepository;
        public BarberShopService(IBarberShopRepository barberShopRepository) 
        {
            _barberShopRepository = barberShopRepository;
        }

        public List<BarberShop> GetAllBarberShop()
        {
            var barberShops = _barberShopRepository.Get();
            return barberShops;
        }

        public BarberShopDto AddNewBarberShop(BarberShopRequest barberShopDto)
        {

           

            var barberShop = BarberShopRequest.ToEntity(barberShopDto);


            var createdBarber = _barberShopRepository.Create(barberShop);

            return BarberShopDto.ToDto(createdBarber);
        }


        public BarberShopDto GetByCity(string city)
        {
            return BarberShopDto.ToDto(_barberShopRepository.GetByCity(city)!);
        }

        public void UpdateBarberShop(int id, string premiseName)
        {
            BarberShop? barberShop = _barberShopRepository.Get(id);
            if (barberShop == null)
            {
                throw new Exception("BarberSHop not found.");
            }
            barberShop.PremiseName = premiseName;
            _barberShopRepository.Update(barberShop);
        }

        public void DeleteBarberShop(int id) //BAJA LOGICA
        {
            var barberShop = _barberShopRepository.Get(id);
            if (barberShop != null)
            {
                barberShop.IsActive = false;
                _barberShopRepository.Update(barberShop);
            }
            else
            {
                throw new Exception("BarberShop not found.");
            }
        }
    }
}
