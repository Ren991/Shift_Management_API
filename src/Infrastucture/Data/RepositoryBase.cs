using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastucture.Data
{
    public abstract class RepositoryBase<T> where T : class
    {
        private readonly DbContext _dbContext;

        public RepositoryBase(DbContext dbContext)
        {
            _dbContext = dbContext;

        }

        public List<T> Get()
        {
            return _dbContext.Set<T>().ToList();
        }

        public T? Get<Tid>(Tid id)
        {
            return _dbContext.Set<T>().Find(new object[] { id });
        }

        /*public IEnumerable<T> GetAllActive()
        {
            return _dbContext.Set<T>().Where(ex => ex.GetType().GetProperty("IsActive")!.Equals(true));
        }*/

        public T Create(T entity)
        {
            _dbContext.Set<T>().Add(entity);
            _dbContext.SaveChanges();
            return entity;
        }

        public T Update(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            _dbContext.SaveChanges();
            return entity;
        }


        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            _dbContext.SaveChanges();
        }
    }
}
