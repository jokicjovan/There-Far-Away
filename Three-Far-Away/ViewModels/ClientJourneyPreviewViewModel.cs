using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Three_Far_Away.Components;
using Three_Far_Away.Models;
using Three_Far_Away.Services.Interfaces;

namespace Three_Far_Away.ViewModels
{
    public class ClientJourneyPreviewViewModel : ViewModelBase
    {
        #region services
        public readonly IJourneyService journeyService;

        #endregion


        #region properties

        private ObservableCollection<LocationListItemViewModel> _locationListItemViewModels;
        public ObservableCollection<LocationListItemViewModel> LocationListItemViewModels
        {
            get
            {
                return _locationListItemViewModels;
            }
            set
            {
                _locationListItemViewModels = value;
                OnPropertyChanged(nameof(LocationListItemViewModels));
            }
        }

        private AgentNavigationBarViewModel _agentNavigationBarViewModel;
        public AgentNavigationBarViewModel AgentNavigationBarViewModel
        {
            get { return _agentNavigationBarViewModel; }
            set
            {
                _agentNavigationBarViewModel = value;
                OnPropertyChanged(nameof(AgentNavigationBarViewModel));
            }
        }

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

        private string _startDate;
        public string StartDate
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

        private string _endDate;
        public string EndDate
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

        #endregion

        public ClientJourneyPreviewViewModel(Guid id)
        {
            AgentNavigationBarViewModel = App.host.Services.GetService<AgentNavigationBarViewModel>();
            this.journeyService = App.host.Services.GetService<IJourneyService>();
            Journey journey = journeyService.GetJourneyWithAttractions(id);
            Name = journey.Name;
            StartDate = journey.StartDate.ToString().Split(" ")[0];
            EndDate = journey.EndDate.ToString().Split(" ")[0];
            Price = journey.Price.ToString();
            LocationListItemViewModels = new ObservableCollection<LocationListItemViewModel>(CreateLocationItemViews(journey));

        }

        private List<LocationListItemViewModel> CreateLocationItemViews(Journey journey)
        {
            List<LocationListItemViewModel> locations = new List<LocationListItemViewModel>();
            locations.Add(new LocationListItemViewModel(journey.StartLocation.Address));
            foreach (var journeyAttraction in journey.Attractions)
            {
                locations.Add(new LocationListItemViewModel(journeyAttraction.Name));
            }
            locations.Add(new LocationListItemViewModel(journey.EndLocation.Address));
            return locations;
        }
    }
}
