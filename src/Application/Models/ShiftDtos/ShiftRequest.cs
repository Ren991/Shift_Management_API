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
        public double Price { get; set; }

        [Required]
        public int BarberID { get; set; }

        [Required]
        public int BarberShopID { get; set; }

        [Required]
        public DateOnly Day { get; set; } 

        [Required]
        public string ShiftTime { get; set; }


        public static Shift ToEntity(ShiftCreateRequest shiftCreateRequest)
        {
            Shift shift = new Shift();
            shift.Price = shiftCreateRequest.Price;
            shift.Confirmed = false;
            shift.BarberID = shiftCreateRequest.BarberID;
            shift.Day = shiftCreateRequest.Day;
            shift.BarberShopID = shiftCreateRequest.BarberShopID;            
            shift.IsPayabled = false;
            shift.ShiftTime = shiftCreateRequest.ShiftTime;

            return shift;
        }

    }
}
