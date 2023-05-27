using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Three_Far_Away.Infrastructure;
using Three_Far_Away.Models;

namespace Three_Far_Away.Services.Interfaces
{
    public interface IJourneyService : ICrudService<Journey>
    {
        List<Journey> ReadPage(int page, int size);
        public Journey GetJourneyWithAttractions(Guid id);
    }
}
