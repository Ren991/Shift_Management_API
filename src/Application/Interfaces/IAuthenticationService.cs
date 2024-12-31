using Application.Models.AuthDtos;
using Application.Models.UserDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IAuthenticationService
    {
        string AuthenticateAsync(UserLoginRequest request);
    }
}
