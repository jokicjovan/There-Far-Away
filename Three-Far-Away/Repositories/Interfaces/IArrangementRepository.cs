using System.Collections.Generic;
using System;
using System.DirectoryServices.ActiveDirectory;
using Three_Far_Away.Infrastructure;
using Three_Far_Away.Models;

namespace Three_Far_Away.Repositories.Interfaces
{
    public interface IArrangementRepository : ICrudRepository<Arrangement>
    {
        public IEnumerable<Arrangement> FindJourneyArrangements(Guid journeyId);
        public Arrangement FindJourneyArrangementForUser(Guid journeyId, Guid userId);
        public IEnumerable<Arrangement> FindArrangementsForUser(Guid userId, ArrangementStatus status);
    }
}