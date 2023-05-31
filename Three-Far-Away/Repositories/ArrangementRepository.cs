using System.Collections.Generic;
using System.Linq;
using System;
using Three_Far_Away.DbContexts;
using Three_Far_Away.Infrastructure;
using Three_Far_Away.Models;
using Three_Far_Away.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Three_Far_Away.Repositories
{
    public class ArrangementRepository : CrudRepository<Arrangement>, IArrangementRepository
    {
        public ArrangementRepository(ThereFarAwayDbContext context) : base(context)
        {
        }

        public IEnumerable<Arrangement> FindJourneyArrangements(Guid journeyId)
        {
            return _entities.Include(u => u.User).Where(a => a.Journey.Id == journeyId).ToList();
        }

        public Arrangement FindJourneyArrangementForUser(Guid journeyId, Guid userId)
        {
            return _entities.Include(u => u.User).FirstOrDefault(a => a.Journey.Id == journeyId && a.User.Id == userId);
        }

        public IEnumerable<Arrangement> FindArrangementsForUser(Guid userId, ArrangementStatus status)
        {
            return _entities.Include(e => e.Journey).Where(a => a.User.Id == userId)
                .Where(a => a.Status == status).ToList();
        }
    }
}
