using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Application.Models.ShiftDtos
{
    public class ShiftCreateRequest
    {
        [Required]
        public int Price { get; set; }

        

        [Required]
        public User Barber { get; set; }

        [Required]

        public BarberShop BarberShop { get; set; }

        [Required]

        public List<ServicesAndHaircuts> Services { get; set; }

        [Required]

        public DateTime Day { get; set; }

        [Required]
        public string ShiftTime { get; set; }
        

        public static Shift ToEntity(ShiftCreateRequest shiftCreateRequest)
        {
            Shift shift = new Shift();
            shift.Price = shiftCreateRequest.Price;
            shift.Confirmed = false;
            shift.Barber = shiftCreateRequest.Barber;
            shift.Services = shiftCreateRequest.Services;
            shift.Day = shiftCreateRequest.Day;
            shift.BarberShop = shiftCreateRequest.BarberShop;
            shift.Client = null;
            shift.IsPayabled = false;
            shift.ShiftTime = shiftCreateRequest.ShiftTime;

            return shift;
        }

    }
}
