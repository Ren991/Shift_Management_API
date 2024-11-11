using Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;


namespace Application.Services
{
    public class PasswordHasherService: IPasswordHasherService
    {
        public string HashPassword(string password)
        {
            // Generar salt aleatorio
            byte[] salt = RandomNumberGenerator.GetBytes(16);

            // Hashear la contraseña con bcrypt
            using (var hasher = new Rfc2898DeriveBytes(password, salt, 10000))
            {
                byte[] hashedPassword = hasher.GetBytes(32);
                return Convert.ToBase64String(hashedPassword);
            }
        }
    }
}
