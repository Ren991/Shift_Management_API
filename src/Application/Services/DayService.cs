using Application.Interfaces;
using Application.Models.DayDtos;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class DayService : IDayService
    {
        private readonly IDayRepository _dayRepository;
        public DayService(IDayRepository dayRepository)
        {
            _dayRepository = dayRepository;
        }

        public List<Day> GetAllDay()
        {
            var days = _dayRepository.Get();
            return days;
        }

        public DayDto AddNewDay(DayRequest dayDto)
        {



            var day = DayRequest.ToEntity(dayDto);


            var createdDay = _dayRepository.Create(day);

            return DayDto.ToDto(createdDay);
        }
    }
}
