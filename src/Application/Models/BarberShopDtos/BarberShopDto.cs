using Application.Models.ServicesAndHaircutsDtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.BarberShopDtos
{
    public class BarberShopDto
    {
        public int Id { get; set; }

        public string PremiseName { get; set; }

        public string Address { get; set; }

        public string City { get; set; }

        public bool IsActive { get; set; }

        public static BarberShopDto ToDto(BarberShop barberShopDto)
        {
            BarberShopDto barberShop = new();
            barberShopDto.Id = barberShopDto.Id;
            barberShopDto.PremiseName = barberShopDto.PremiseName;
            barberShopDto.Address = barberShopDto.Address;
            barberShopDto.City = barberShopDto.City;
            barberShopDto.IsActive = barberShopDto.IsActive;

            return barberShop;

        }


    }
}
