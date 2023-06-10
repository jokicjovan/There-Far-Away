using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Three_Far_Away.Models;
using Three_Far_Away.Repositories;
using Three_Far_Away.Repositories.Interfaces;
using Three_Far_Away.Services.Interfaces;

namespace Three_Far_Away.Services
{
    public class AttractionService : IAttractionService
    {
        private readonly IAttractionRepository _attractionRepository;

        public AttractionService(IAttractionRepository attractionRepository)
        {
            _attractionRepository = attractionRepository;
        }

        #region CRUD
        public Attraction Create(Attraction entity)
        {
            return _attractionRepository.Create(entity);
        }

        public Attraction Read(Guid id)
        {
            return _attractionRepository.Read(id);
        }

        public List<Attraction> ReadAll()
        {
            return (List<Attraction>)_attractionRepository.GetAttractionsWithLocations();
        }

        public Attraction Update(Attraction entity)
        {
            return _attractionRepository.Update(entity);
        }

        public Attraction Delete(Guid id)
        {
            return _attractionRepository.Delete(id);
        }

        #endregion
        public List<Attraction> ReadPage(int page, int size, AttractionType type)
        {
            List<Attraction> attractions = _attractionRepository.FindByType(type);
            List<Attraction> newAttractions = new List<Attraction>();
            for (int i = page * size; i < (page + 1) * size; i++)
            {
                if (i > attractions.Count - 1 || i < 0) break;
                newAttractions.Add(attractions[i]);
            }
            return newAttractions;
        }
        public List<Attraction> ReadPage(int page, int size)
        {
            List<Attraction> attractions = (List<Attraction>)_attractionRepository.ReadAll();
            List<Attraction> newAttractions = new List<Attraction>();
            for (int i = page * size; i < (page + 1) * size; i++)
            {
                if (i > attractions.Count - 1 || i < 0) break;
                newAttractions.Add(attractions[i]);
            }
            return newAttractions;
        }
    }
}
