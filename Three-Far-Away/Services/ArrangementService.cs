using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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


        public Arrangement Create(Arrangement entity)
        {
            return _arrangementRepository.Create(entity);
        }

        public Arrangement Read(Guid id)
        {
            return _arrangementRepository.Read(id);
        }

        public Arrangement Update(Arrangement entity)
        {
            return _arrangementRepository.Update(entity);
        }

        public Arrangement Delete(Guid id)
        {
            return _arrangementRepository.Delete(id);
        }
    }
}
