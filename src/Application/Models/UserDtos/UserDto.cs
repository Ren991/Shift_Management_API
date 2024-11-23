using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.UserDtos
{
    public class UserDto
    {
            public int Id { get; set; }
        
            public string FirstName { get; set; }

            public string LastName { get; set; }

            public string Email { get; set; }
            public string Password { get; set; }

            public UserType UserType { get; set; }

            

            public bool IsActive { get; set; }

            public static UserDto ToDto(User user)
            {
                UserDto userDto = new();
                userDto.Id = user.Id;
                userDto.FirstName = user.FirstName;
                userDto.LastName = user.LastName;
                userDto.Email = user.Email;
                userDto.Password = user.Password;
                userDto.UserType = user.UserType;
                userDto.IsActive = user.IsActive;

                return userDto;

            }

        
    }
}
