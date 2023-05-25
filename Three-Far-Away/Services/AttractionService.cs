using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Three_Far_Away.Models;
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

        public Attraction Update(Attraction entity)
        {
            return _attractionRepository.Update(entity);
        }

        public Attraction Delete(Guid id)
        {
            return _attractionRepository.Delete(id);
        }
        #endregion
    }
}
