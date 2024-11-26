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
    public class ServicesAndHaircutsRepository: RepositoryBase<ServicesAndHaircuts>, IServicesAndHaircutsRepository
    {
        private readonly ApplicationDbContext _context;

        public ServicesAndHaircutsRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public ServicesAndHaircuts? GetByName(string name)
        {
            return _context.ServicesAndHaircuts.FirstOrDefault(u => u.Name == name);
        }

        public ServicesAndHaircuts? GetById(int id)
        {
            return _context.ServicesAndHaircuts.FirstOrDefault(u => u.Id == id);
        }
    }
}
