using Application.Models.ShiftDtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.DayDtos
{
    public class DayDto
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public ICollection<Shift> Shifts { get; set; }



        public static DayDto ToDto(Day day)
        {
            DayDto dayDto = new();
            dayDto.Id = day.Id;
            dayDto.Date = day.Date;
            dayDto.Shifts = day.Shifts;


            return dayDto;
        }
    }
}
