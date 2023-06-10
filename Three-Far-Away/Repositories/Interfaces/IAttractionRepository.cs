using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Three_Far_Away.Infrastructure;
using Three_Far_Away.Models;

namespace Three_Far_Away.Repositories.Interfaces
{
    public interface IAttractionRepository : ICrudRepository<Attraction>
    {
        List<Attraction> GetAttractionsWithLocations();
        List<Attraction> FindByType(AttractionType type);
        Attraction FindById(Guid id);
    }
}
