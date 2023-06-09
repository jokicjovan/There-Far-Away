using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Three_Far_Away.Models;

namespace Three_Far_Away.Infrastructure
{
    public interface ICrudRepository<T> where T : IBaseEntity
    {
        IEnumerable<T> ReadAll();
        T Read(Guid id);
        T Create(T entity);
        T Update(T entity);
        T Delete(Guid id);
    }
}
