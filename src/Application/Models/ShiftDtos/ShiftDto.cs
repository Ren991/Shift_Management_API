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

        public int id { get; set; }

        public int price { get; set; }

        public bool pending { get; set; }

        public bool confirmed { get; set; }

        public bool isPayabled { get; set; }

        public User client { get; set; }

        public User barber { get; set; }


        public BarberShop barberShop { get; set; }


        public List<ServicesAndHaircuts> services { get; set; }


        public DateTime day { get; set; }

        public static ShiftDto ToDto(Shift shift) 
        {
            ShiftDto shifDto = new();
            shifDto.id = shift.id;
            shifDto.price = shift.price;
            shifDto.pending = shift.pending;
            shifDto.confirmed = shift.confirmed;
            shifDto.services = shift.services;
            shifDto.day = shift.day;
            shifDto.client = shift.client;
            shifDto.barberShop = shift.barberShop;

            return shifDto;
        }
    }
}
