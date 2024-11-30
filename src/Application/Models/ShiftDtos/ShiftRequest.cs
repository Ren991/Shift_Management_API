using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.ShiftDtos
{
    public class ShiftCreateRequest
    {
        [Required]
        public int price { get; set; }


        [Required]
        public bool confirmed { get; set; }

        [Required]
        public bool isPayabled { get; set; }

        [Required]
        public User client { get; set; }

        [Required]
        public User barber { get; set; }

        [Required]

        public BarberShop barberShop { get; set; }

        [Required]

        public List<ServicesAndHaircuts> services { get; set; }

        [Required]

        public Day day { get; set; }

        [Required]

        public TimeOnly ShiftTime { get; set; }
        public static Shift ToEntity(ShiftCreateRequest shiftCreateRequest)
        {
            Shift shift = new Shift();
            shift.Price = shiftCreateRequest.price;
            shift.Confirmed = false;
            shift.Barber = shiftCreateRequest.barber;
            shift.Services = shiftCreateRequest.services;
            shift.Day = shiftCreateRequest.day;
            shift.BarberShop = shiftCreateRequest.barberShop;
            shift.Client = shiftCreateRequest.client;
            shift.IsPayabled = false;
            shift.ShiftTime = shiftCreateRequest.ShiftTime;

            return shift;
        }

    }
}
