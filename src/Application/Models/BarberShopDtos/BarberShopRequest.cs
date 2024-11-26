using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.BarberShopDtos
{
    public class BarberShopRequest
    {
        [Required]
        public string PremiseName { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        public string City { get; set; }

        public static BarberShop ToEntity(BarberShopRequest barberShopRequest)
        {
            BarberShop barberShop = new();
            barberShop.PremiseName = barberShopRequest.PremiseName;
            barberShop.Address = barberShopRequest.Address;
            barberShop.City = barberShopRequest.City;
            barberShop.IsActive = true;


            return barberShop;

        }
    }
}
