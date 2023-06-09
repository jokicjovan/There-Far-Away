﻿using System;
using System.Collections.Generic;
using Three_Far_Away.Infrastructure;
using Three_Far_Away.Models;

namespace Three_Far_Away.Repositories.Interfaces
{
    public interface IJourneyRepository : ICrudRepository<Journey>
    {
        public Journey GetJourneyWithAttractions(Guid id);
        public List<Journey> FindJourneysInsideDate(DateTime fromTime, DateTime toTime);

    }
}
