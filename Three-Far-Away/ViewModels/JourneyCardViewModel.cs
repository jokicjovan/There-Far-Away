using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Three_Far_Away.Models;
using Three_Far_Away.Models.DTOs;
using Three_Far_Away.Services.Interfaces;

namespace Three_Far_Away.ViewModels
{
    public class JourneyCardViewModel : ViewModelBase
    {
        public readonly IJourneyService _journeyService;
		private string _name;
		public string Name
		{
			get
			{
				return _name;
			}
			set
			{
				_name = value;
				OnPropertyChanged(nameof(Name));
			}
		}

		private string _date;
		public string Date
		{
			get
			{
				return _date;
			}
			set
			{
				_date = value;
				OnPropertyChanged(nameof(Date));
			}
		}
        private string _price;
		public string Price
		{
			get
			{
				return _price;
			}
			set
			{
				_price = value;
				OnPropertyChanged(nameof(Price));
			}
		}

        public JourneyCardViewModel(JourneyForCard journey)
        {
            Name = journey.Name;
			Date = journey.Date;
			Price = journey.Price;
        }
	}
}

