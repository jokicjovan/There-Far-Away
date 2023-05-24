using Three_Far_Away.DbContexts;
using Three_Far_Away.Infrastructure;
using Three_Far_Away.Models;

namespace Three_Far_Away.Repositories
{
    public class ArrangementRepository : CrudRepository<Arrangement>, IArrangementRepository
    {
        public ArrangementRepository(ThereFarAwayDbContext context) : base(context)
        {
        }
    }
}
