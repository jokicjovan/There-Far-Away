using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Three_Far_Away.DbContexts;
using Three_Far_Away.Infrastructure;
using Three_Far_Away.Models;
using Three_Far_Away.Repositories.Interfaces;

namespace Three_Far_Away.Repositories
{
    public class AttractionRepository : CrudRepository<Attraction>, IAttractionRepository
    {
        public AttractionRepository(ThereFarAwayDbContext context) : base(context)
        {

        }
    }
}
