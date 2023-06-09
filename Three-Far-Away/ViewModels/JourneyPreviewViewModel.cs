using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Three_Far_Away.Commands;
using Three_Far_Away.Components;
using Three_Far_Away.Models;
using Three_Far_Away.Services;
using Three_Far_Away.Services.Interfaces;
using Three_Far_Away.Stores;

namespace Three_Far_Away.ViewModels
{
    public class JourneyPreviewViewModel : ViewModelBase
    {
        #region services
        public readonly IJourneyService journeyService;
        public readonly IArrangementService arrangementService;
        public readonly IUserService userService;
        #endregion

        #region stores
        public readonly AccountStore accountStore;
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


        #region commands
        public ICommand ShowJourneyPassengersCommand { get; }
        public ICommand EditJourneyCommand { get; }
        public ICommand DeleteJourneyCommand { get; }
        public ICommand ReserveCommand { get; }
        public ICommand BuyCommand { get; }
        public ICommand NavigateToEditJourneyCommand { get; }
        #endregion

        #region command properties

        private bool _isBuyEnabled;
        public bool IsBuyEnabled
        {
            get
            {
                return _isBuyEnabled;
            }
            set
            {
                _isBuyEnabled = value;
                OnPropertyChanged(nameof(IsBuyEnabled));
            }
        }

        private bool _isReserveEnabled;
        public bool IsReserveEnabled
        {
            get
            {
                return _isReserveEnabled;
            }
            set
            {
                _isReserveEnabled = value;
                OnPropertyChanged(nameof(IsReserveEnabled));
            }
        }

        private string _reserveButtonText;
        public string ReserveButtonText
        {
            get
            {
                return _reserveButtonText;
            }
            set
            {
                _reserveButtonText = value;
                OnPropertyChanged(nameof(ReserveButtonText));
            }
        }

        private string _infoMessage;
        public string InfoMessage
        {
            get
            {
                return _infoMessage;
            }
            set
            {
                _infoMessage = value;
                OnPropertyChanged(nameof(InfoMessage));
            }
        }
        #endregion

        public JourneyPreviewViewModel(Guid id)
        {
            accountStore = App.host.Services.GetService<AccountStore>();
            journeyService = App.host.Services.GetService<IJourneyService>();
            arrangementService = App.host.Services.GetRequiredService<IArrangementService>();
            userService = App.host.Services.GetRequiredService<IUserService>();
            Journey journey = journeyService.GetJourneyWithAttractions(id);
            Id = journey.Id;
            Name = journey.Name;
            StartDate = journey.StartDate.ToString().Split(" ")[0];
            EndDate = journey.EndDate.ToString().Split(" ")[0];
            Price = journey.Price.ToString();
            NavigateToEditJourneyCommand = new NavigateToCreateJourneyCommand(journey);
            LocationListItemViewModels = new ObservableCollection<LocationListItemViewModel>(CreateLocationItemViews(journey));

            if (accountStore.Role == Role.AGENT)
            {
                _isAgent = true;
                ShowJourneyPassengersCommand = new ShowJourneyPassengersCommand(this);
                //EditJourney = new 
                DeleteJourneyCommand = new DeleteJourneyFromPreviewCommand(this);
            }
            else {
                _isAgent = false;
                ReserveCommand = new ToggleJourneyReservationCommand(this);
                BuyCommand = new BuyJourneyCommand(this);

                _isReserveEnabled = true;
                _isBuyEnabled = true;
                _reserveButtonText = "Reserve";
                _infoMessage = "Journey is not reserved or bought.";

                Arrangement userArrangement = arrangementService.GetJourneyArrangementForUser(Id, accountStore.Id);
                if (userArrangement != null)
                {
                    if (userArrangement.Status == ArrangementStatus.BOUGHT)
                    {
                        _isReserveEnabled = false;
                        _isBuyEnabled = false;
                        _infoMessage = "Journey is bought.";
                    }
                    else if (userArrangement.Status == ArrangementStatus.RESERVED)
                    {
                        _isReserveEnabled = true;
                        _isBuyEnabled = true;
                        _reserveButtonText = "Cancel reservation";
                        _infoMessage = "Journey is reserved, but not bought.";
                    }
                }
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
