using System;
using System.Collections.Generic;
using Three_Far_Away.Infrastructure;
using Three_Far_Away.Models;

namespace Three_Far_Away.Services.Interfaces
{
    public interface IJourneyService : ICrudService<Journey>
    {
        public List<Journey> ReadPage(int page, int size);
        public List<Journey> ReadPageWithDateByMonthAndYear(int page, int size, DateTime fromTime, DateTime toTime, int month=0, int year=0);
        public List<Journey> GetJourneysInsideDate(DateTime fromTime, DateTime toTime);
        public Journey GetJourneyWithAttractions(Guid id);
    }
}
