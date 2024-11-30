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
        public bool pending { get; set; }

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

        public DateTime day { get; set; }

        public static Shift ToEntity(ShiftCreateRequest shiftCreateRequest)
        {
            Shift shift = new Shift();
            shift.price = shiftCreateRequest.price;
            shift.pending = shiftCreateRequest.pending;
            shift.confirmed = shiftCreateRequest.confirmed;
            shift.barber = shiftCreateRequest.barber;
            shift.services = shiftCreateRequest.services;
            shift.day = shiftCreateRequest.day;
            shift.barberShop = shiftCreateRequest.barberShop;
            shift.client = shiftCreateRequest.client;
            shift.isPayabled = shiftCreateRequest.isPayabled;

            return shift;
        }

    }
}
