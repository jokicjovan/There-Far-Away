using System;
using System.Collections.Generic;
using Three_Far_Away.Infrastructure;
using Three_Far_Away.Models;

namespace Three_Far_Away.Services.Interfaces
{
    public interface IArrangementService : ICrudService<Arrangement>
    {
        public IEnumerable<Arrangement> GetJourneyArrangements(Guid journeyId);
        public Arrangement GetJourneyArrangementForUser(Guid journeyId, Guid userId);
    }
}
