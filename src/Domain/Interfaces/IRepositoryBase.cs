using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IRepositoryBase<T> where T : class
    {
        public List<T> Get();

        public T? Get<Tid>(Tid id);

        public T Create(T entity);

        public T Update(T entity);

        public void Delete(T entity);
    }
}
