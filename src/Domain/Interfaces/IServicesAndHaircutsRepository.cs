﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IServicesAndHaircutsRepository : IRepositoryBase<ServicesAndHaircuts>
    {
        public List<ServicesAndHaircuts> GetAllActive();

        public ServicesAndHaircuts? GetByName(string name);

        public ServicesAndHaircuts? GetById(int id);

        public Task SaveChangesAsync();
    }
}
