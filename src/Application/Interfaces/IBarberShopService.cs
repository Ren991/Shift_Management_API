using Application.Models.BarberShopDtos;
using Application.Models.UserDtos;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IBarberShopService
    {
        List<BarberShop> GetAllBarberShop();

        BarberShopDto AddNewBarberShop(BarberShopRequest barberShopDto);

        //UserDto AddNewAdminUser(UserAdminCreateRequest userDto);

        BarberShopDto GetByCity(string city);



        void UpdateBarberShop(int id, string premiseName);

        void DeleteBarberShop(int id);
    }
}
