using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Migrations.Operations;
using Three_Far_Away.Models;
using Three_Far_Away.Repositories.Interfaces;
using Three_Far_Away.Services.Interfaces;

namespace Three_Far_Away.Services
{
    public class JourneyService : IJourneyService
    {
        private readonly IJourneyRepository _journeyRepository;

        public JourneyService(IJourneyRepository journeyRepository)
        {
            _journeyRepository = journeyRepository;
        }

        public Journey Create(Journey entity)
        {
            return _journeyRepository.Create(entity);
        }

        public Journey Read(Guid id)
        {
            return _journeyRepository.Read(id);
        }

        public Journey Update(Journey entity)
        {
            return _journeyRepository.Update(entity);
        }

        public Journey Delete(Guid id)
        {
            return _journeyRepository.Delete(id);
        }
    }
}
