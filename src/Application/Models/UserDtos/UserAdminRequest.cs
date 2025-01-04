using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.UserDtos
{
    public class UserAdminRequest
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"((?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[\W_]).{6,50})",
         ErrorMessage = "Password requires at least 1 lower case character, 1 upper case character, 1 number, 1 special character and must be at least 6 characters and at most 50")]
        public string Password { get; set; }

        public static User ToEntity(UserAdminRequest userDto)
        {
            User user = new User();
            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.Email = userDto.Email;
            user.Password = userDto.Password;
            user.Role = Role.Admin;
            user.IsActive = true;

            return user;

        }
    }
}
