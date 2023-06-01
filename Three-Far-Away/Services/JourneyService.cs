﻿using System;
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

        #region CRUD
        public Journey Create(Journey entity)
        {
            return _journeyRepository.Create(entity);
        }

        public Journey Read(Guid id)
        {
            return _journeyRepository.Read(id);
        }

        public List<Journey> ReadAll()
        {
            return (List<Journey>)_journeyRepository.ReadAll();
        }

        public Journey Update(Journey entity)
        {
            return _journeyRepository.Update(entity);
        }

        public Journey Delete(Guid id)
        {
            return _journeyRepository.Delete(id);
        }
        #endregion

        public List<Journey> ReadPage(int page, int size)
        {
            List<Journey> journeys = ReadAll();
            List<Journey> newJourneys = new List<Journey>();
            for (int i = page * size; i < (page + 1) * size; i++)
            {
                if(i > journeys.Count - 1 || i < 0) break;
                newJourneys.Add(journeys[i]);
            }
            return newJourneys;
        }

        public Journey GetJourneyWithAttractions(Guid id)
        {
            return _journeyRepository.GetJourneyWithAttractions(id);
        }

        public List<Journey> ReadPageWithDate(int page, int size, DateTime fromTime, DateTime toTime)
        {
            List<Journey> journeys = _journeyRepository.FindJourneysInsideDate(fromTime, toTime);
            List<Journey> newJourneys = new List<Journey>();
            for (int i = page * size; i < (page + 1) * size; i++)
            {
                if (i > journeys.Count - 1 || i < 0) break;
                newJourneys.Add(journeys[i]);
            }
            return newJourneys;
        }
    }
}
