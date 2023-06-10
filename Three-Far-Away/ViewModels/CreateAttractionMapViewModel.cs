using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Three_Far_Away.Models;

namespace Three_Far_Away.ViewModels
{
    internal class CreateAttractionMapViewModel:ViewModelBase
    {
        public Attraction Attraction { get; set; }
        public CreateAttractionMapViewModel(Attraction attraction)
        {

        }
    }
}
