using Application.Models.UserDtos;
using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.ServicesAndHaircutsDtos
{
    public class ServicesAndHaircutsRequest
    {
        
        [Required]
        public string Name { get; set; }

        [Required]
        public string Category { get; set; }

        [Required]
        public double Price { get; set; }

        

        public static ServicesAndHaircuts ToEntity(ServicesAndHaircutsRequest serviceDto)
        {
            ServicesAndHaircuts servicesAndHaircuts = new ServicesAndHaircuts();
            servicesAndHaircuts.Name = serviceDto.Name;
            servicesAndHaircuts.Category = serviceDto.Category;
            servicesAndHaircuts.Price = serviceDto.Price;
            servicesAndHaircuts.IsActive = true;


            return servicesAndHaircuts;

        }
    
    }
}
