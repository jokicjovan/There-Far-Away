using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Three_Far_Away.Infrastructure;
using Three_Far_Away.Models;

namespace Three_Far_Away.Services.Interfaces
{
    public interface IAttractionService : ICrudService<Attraction>
    {
        public List<Attraction> ReadPage(int page, int size, AttractionType type);
        public List<Attraction> ReadPage(int page, int size);
    }
}
