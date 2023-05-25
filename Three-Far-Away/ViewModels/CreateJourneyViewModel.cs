using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Three_Far_Away.Commands;
using Three_Far_Away.Models;
using Three_Far_Away.Services.Interfaces;

namespace Three_Far_Away.ViewModels
{
    public class CreateJourneyViewModel : ViewModelBase
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
		private DateTime _startDate;
		public DateTime StartDate
        {
			get
			{
				return _startDate;
			}
			set
			{
				_startDate = value;
				OnPropertyChanged(nameof(StartDate));
			}
		}

		private DateTime _endDate;
		public DateTime EndDate
		{
			get
			{
				return _endDate;
			}
			set
			{
				_endDate = value;
				OnPropertyChanged(nameof(EndDate));
			}
		}

		private Location _startLocation;
		public Location StartLocation
		{
			get
			{
				return _startLocation;
			}
			set
			{
				_startLocation = value;
				OnPropertyChanged(nameof(StartLocation));
			}
		}

		private Location _endLocation;
		public Location EndLocation
		{
			get
			{
				return _endLocation;
			}
			set
			{
				_endLocation = value;
				OnPropertyChanged(nameof(EndLocation));
			}
		}
		private Double _price;
		public Double Price
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

		private List<Attraction> _attractions;
		public List<Attraction> Attractions
		{
			get
			{
				return _attractions;
			}
			set
			{
				_attractions = value;
				OnPropertyChanged(nameof(Attractions));
			}
		}

		private TransportationType _transportation;
		public TransportationType Transportation
		{
			get
			{
				return _transportation;
			}
			set
			{
				_transportation = value;
				OnPropertyChanged(nameof(Transportation));
			}
		}

		public ICommand CreateJourneyCommand { get; }

        public CreateJourneyViewModel(IJourneyService journeyService)
        {
            _journeyService = journeyService;
            CreateJourneyCommand = new CreateJourneyCommand(this);
        }

		
	}
}
