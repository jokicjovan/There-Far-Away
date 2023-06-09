using System;
using System.Collections.Generic;
using Three_Far_Away.Models;
using Three_Far_Away.Repositories.Interfaces;
using Three_Far_Away.Services.Interfaces;

namespace Three_Far_Away.Services
{
    public class ArrangementService: IArrangementService
    {
        private readonly IArrangementRepository _arrangementRepository;
        public ArrangementService(IArrangementRepository arrangementRepository)
        {
            _arrangementRepository = arrangementRepository;
        }

        #region CRUD
        public Arrangement Create(Arrangement entity)
        {
            return _arrangementRepository.Create(entity);
        }

        public Arrangement Read(Guid id)
        {
            return _arrangementRepository.Read(id);
        }

        public List<Arrangement> ReadAll()
        {
            return (List<Arrangement>)_arrangementRepository.ReadAll();
        }

        public Arrangement Update(Arrangement entity)
        {
            return _arrangementRepository.Update(entity);
        }

        public Arrangement Delete(Guid id)
        {
            return _arrangementRepository.Delete(id);
        }
        #endregion

        public IEnumerable<Arrangement> GetJourneyArrangements(Guid journeyId)
        {
            return _arrangementRepository.FindJourneyArrangements(journeyId);
        }


        public Arrangement GetJourneyArrangementForUser(Guid journeyId, Guid userId)
        {
            return _arrangementRepository.FindJourneyArrangementForUser(journeyId, userId);
        }

        public List<Arrangement> ReadPage(Guid userId, int page, int size, ArrangementStatus status)
        {
            List<Arrangement> arrangements =(List<Arrangement>) _arrangementRepository.FindArrangementsForUser(userId, status);
            List<Arrangement> newArrangements = new List<Arrangement>();
            for (int i = page * size; i < (page + 1) * size; i++)
            {
                if (i > arrangements.Count - 1 || i < 0) break;
                newArrangements.Add(arrangements[i]);
            }
            return newArrangements;
        }
    }
}
