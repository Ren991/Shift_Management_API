using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastucture.Data
{
    public class BarberShopRepository : RepositoryBase<BarberShop>, IBarberShopRepository
    {
        private readonly ApplicationDbContext _context;

        public BarberShopRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public BarberShop? GetByCity(string city)
        {
            return _context.BarberShop.FirstOrDefault(u => u.City == city);
        }

    }
}
