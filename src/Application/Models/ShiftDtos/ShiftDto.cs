using Application.Models.ServicesAndHaircutsDtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.ShiftDtos
{
    public class ShiftDto

    {
        public int Id { get; set; }
        
        public User? User { get; set; }

        public double? Price { get; set; }


        public bool? Confirmed { get; set; }

        public bool? IsPayabled { get; set; }

        public int? ClientID { get; set; }

        public int? BarberID { get; set; }

        public BarberShop? BarberShop { get; set; }

        public int? BarberShopID { get; set; }

        public ICollection<ServicesAndHaircutsDto> Services { get; set; } = new List<ServicesAndHaircutsDto>();

        public DateTime? Day { get; set; }

        public string? ShiftTime { get; set; }

        public static ShiftDto ToDto(Shift shift) 
        {
            ShiftDto shiftDto = new();
            shiftDto.Id = shift.Id;
            shiftDto.Price = shift?.Price;
            shiftDto.Confirmed = shift?.Confirmed;
            shiftDto.Services = ServicesAndHaircutsDto.ToCollectionDto(shift.Services ?? new List<ServicesAndHaircuts>());
            shiftDto.Day = shift?.Day;
            shiftDto.ShiftTime = shift?.ShiftTime;
            shiftDto.IsPayabled = shift?.IsPayabled;
            shiftDto.ClientID = shift?.ClientID;
            shiftDto.BarberID = shift?.BarberID;
            shiftDto.ShiftTime = shift?.ShiftTime;
            shiftDto.BarberShopID = shift?.BarberShopID;
            shiftDto.BarberShop = shift?.BarberShop;

            return shiftDto;
        }
    }
}
