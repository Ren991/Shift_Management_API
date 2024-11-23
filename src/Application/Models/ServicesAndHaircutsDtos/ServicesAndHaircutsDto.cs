using Application.Models.UserDtos;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.ServicesAndHaircutsDtos
{
    public class ServicesAndHaircutsDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double Price { get; set; }

        public string Category { get; set; }   

        public bool IsActive { get; set; }

        public static ServicesAndHaircutsDto ToDto(ServicesAndHaircuts servicesAndHaircutsDto)
        {
            ServicesAndHaircutsDto servicesAndHaircuts = new();
            servicesAndHaircutsDto.Id = servicesAndHaircutsDto.Id;
            servicesAndHaircutsDto.Name = servicesAndHaircutsDto.Name;
            servicesAndHaircutsDto.Category = servicesAndHaircutsDto.Category;
            servicesAndHaircutsDto.Price = servicesAndHaircutsDto.Price;
            servicesAndHaircutsDto.IsActive = servicesAndHaircutsDto.IsActive;

            return servicesAndHaircuts;

        }


    }
}
