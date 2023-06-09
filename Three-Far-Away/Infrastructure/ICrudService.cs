using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Three_Far_Away.Models;

namespace Three_Far_Away.Infrastructure
{
    public interface ICrudService<T> where T : IBaseEntity
    {
        T Create(T entity);
        T Read(Guid id);
        List<T> ReadAll();
        T Update(T entity);
        T Delete(Guid id);
    }
}
