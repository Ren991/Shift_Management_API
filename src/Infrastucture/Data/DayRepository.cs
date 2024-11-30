using Domain.Entities;
using Domain.Interfaces;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastucture.Data
{
     public class DayRepository : RepositoryBase<Day>, IDayRepository
    {
        private readonly ApplicationDbContext _context;

        public DayRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }
    }
}
