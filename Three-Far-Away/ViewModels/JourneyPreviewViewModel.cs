using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Three_Far_Away.Commands;
using Three_Far_Away.Components;
using Three_Far_Away.Models;
using Three_Far_Away.Services.Interfaces;
using Three_Far_Away.Stores;

namespace Three_Far_Away.ViewModels
{
    public class JourneyPreviewViewModel : ViewModelBase
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

        private Guid _id;
        public Guid Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
                OnPropertyChanged(nameof(Id));
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

        private bool _isAgent;
        public bool IsAgent
        {
            get
            {
                return _isAgent;
            }
            set
            {
                _isAgent = value;
                OnPropertyChanged(nameof(IsAgent));
            }
        }

        #endregion

        public ICommand DeleteJourney { get; }

        public JourneyPreviewViewModel(Guid id)
        {
            AccountStore accountStore = App.host.Services.GetService<AccountStore>();
            journeyService = App.host.Services.GetService<IJourneyService>();
            Journey journey = journeyService.GetJourneyWithAttractions(id);
            Id = journey.Id;
            Name = journey.Name;
            StartDate = journey.StartDate.ToString().Split(" ")[0];
            EndDate = journey.EndDate.ToString().Split(" ")[0];
            Price = journey.Price.ToString();
            LocationListItemViewModels = new ObservableCollection<LocationListItemViewModel>(CreateLocationItemViews(journey));

            if (accountStore.Role == Role.AGENT)
            {
                _isAgent = true;
                DeleteJourney = new DeleteJourneyFromPreviewCommand(this);
            }
            else {
                _isAgent = false;
            }
        }

        private List<LocationListItemViewModel> CreateLocationItemViews (Journey journey)
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
