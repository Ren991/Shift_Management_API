using Application.Models.BarberShopDtos;
using Application.Models.DayDtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
   public interface IDayService
    {
        List<Day> GetAllDay();

        DayDto AddNewDay(DayRequest dayDto);
    }
}
