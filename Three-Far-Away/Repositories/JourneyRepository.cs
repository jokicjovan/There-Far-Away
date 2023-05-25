using System;
using Three_Far_Away.DbContexts;
using Three_Far_Away.Infrastructure;
using Three_Far_Away.Models;
using Three_Far_Away.Repositories.Interfaces;

namespace Three_Far_Away.Repositories
{
    public class JourneyRepository : CrudRepository<Journey>, IJourneyRepository
    {
        public JourneyRepository(ThereFarAwayDbContext context) : base(context)
        {
            Console.WriteLine("POZ");
        }
    }
}
