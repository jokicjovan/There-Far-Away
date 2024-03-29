﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
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
        }

        public Journey GetJourneyWithAttractions(Guid id)
        {
            return _entities
                .Include(e => e.Attractions).ThenInclude(a => a.Location)
                .Include(e => e.StartLocation)
                .Include(e => e.EndLocation)
                .FirstOrDefault(e => e.Id == id);
        }

        public List<Journey> FindJourneysInsideDate(DateTime fromTime, DateTime toTime) 
        {
            return _entities.Where(j => j.StartDate >= fromTime && j.EndDate <= toTime).ToList();
        }

        public List<Journey> FindFutureJourneys()
        {
            return _entities.Where(j => j.StartDate >= DateTime.Now).OrderBy(e => e.StartDate).ToList();
        }
    }
}
