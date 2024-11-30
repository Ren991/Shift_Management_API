using Application.Models.ShiftDtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.DayDtos
{
    public class DayRequest
    {

        [Required]
        public DateTime Date { get; set; }

        public static Day ToEntity(DayRequest dayDto)
        {
            Day day = new Day();
            day.Date = dayDto.Date;
            day.Shifts = new List<Shift>();


            return day;
        }
    }
}
