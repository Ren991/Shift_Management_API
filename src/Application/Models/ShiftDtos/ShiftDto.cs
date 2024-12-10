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

        public int Price { get; set; }


        public bool Confirmed { get; set; }

        public bool IsPayabled { get; set; }

        public User Client { get; set; }

        public User Barber { get; set; }

        
        public BarberShop BarberShop { get; set; }


        public List<ServicesAndHaircuts> Services { get; set; }


        public DateTime Day { get; set; }

        public TimeOnly ShiftTime { get; set; }

        public static ShiftDto ToDto(Shift shift) 
        {
            ShiftDto shiftDto = new();
            shiftDto.Id = shift.Id;
            shiftDto.Price = shift.Price;
            shiftDto.Confirmed = shift.Confirmed;
            shiftDto.Services = shift.Services;
            shiftDto.Day = shift.Day;
            shiftDto.Client = shift.Client;
            shiftDto.ShiftTime = shift.ShiftTime;
            shiftDto.BarberShop = shift.BarberShop;

            return shiftDto;
        }
    }
}
