using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Three_Far_Away.Commands;
using Three_Far_Away.Models;
using Three_Far_Away.Models.DTOs;
using Three_Far_Away.Services;
using Three_Far_Away.Services.Interfaces;
using Three_Far_Away.Stores;

namespace Three_Far_Away.ViewModels
{
    public class ClientsJourneysViewModel : ViewModelBase
    {
        #region services
        public readonly IArrangementService arrangementService;
        #endregion

        #region commands
        public ICommand NextPageCommand { get; }
        public ICommand PreviousPageCommand { get; }
        #endregion

        #region stores
        public readonly AccountStore accountStore;
        #endregion

        #region properties
        public int reservatedPage;
        public int boughtPage;
        public Guid userId;

        private ObservableCollection<JourneyCardViewModel> _boughtJourneyCardViewModels;
        public ObservableCollection<JourneyCardViewModel> BoughtJourneyCardViewModels
        {
            get
            {
                return _boughtJourneyCardViewModels;
            }
            set
            {
                _boughtJourneyCardViewModels = value;
                OnPropertyChanged(nameof(BoughtJourneyCardViewModels));
            }
        }

        private ObservableCollection<JourneyForCard> _boughtJourneys;
        public ObservableCollection<JourneyForCard> BoughtJourneys
        {
            get { return _boughtJourneys; }
            set
            {
                _boughtJourneys = value;
                OnPropertyChanged(nameof(BoughtJourneys));
            }
        }


        private ObservableCollection<JourneyCardViewModel> _reservedJourneyCardViewModels;
        public ObservableCollection<JourneyCardViewModel> ReservedJourneyCardViewModels
        {
            get
            {
                return _reservedJourneyCardViewModels;
            }
            set
            {
                _reservedJourneyCardViewModels = value;
                OnPropertyChanged(nameof(ReservedJourneyCardViewModels));
            }
        }

        private ObservableCollection<JourneyForCard> _reservedJourneys;
        public ObservableCollection<JourneyForCard> ReservedJourneys
        {
            get { return _reservedJourneys; }
            set
            {
                _reservedJourneys = value;
                OnPropertyChanged(nameof(ReservedJourneys));
            }
        }

        private int _selectedTabIndex;
        public int SelectedTabIndex
        {
            get { return _selectedTabIndex; }
            set
            {
                _selectedTabIndex = value;
                OnPropertyChanged(nameof(SelectedTabIndex));
            }
        }

        private Visibility _nextPageVisibility;
        public Visibility NextPageVisibility
        {
            get
            {
                return _nextPageVisibility;
            }
            set
            {
                _nextPageVisibility = value;
                OnPropertyChanged(nameof(NextPageVisibility));
            }
        }

        private Visibility _previousPageVisibility;
        public Visibility PreviousPageVisibility
        {
            get
            {
                return _previousPageVisibility;
            }
            set
            {
                _previousPageVisibility = value;
                OnPropertyChanged(nameof(PreviousPageVisibility));
            }
        }

        private Visibility _nextPageReservedVisibility;
        public Visibility NextPageReservedVisibility
        {
            get
            {
                return _nextPageReservedVisibility;
            }
            set
            {
                _nextPageReservedVisibility = value;
                OnPropertyChanged(nameof(NextPageReservedVisibility));
            }
        }

        private Visibility _previousPageReservedVisibility;
        public Visibility PreviousPageReservedVisibility
        {
            get
            {
                return _previousPageReservedVisibility;
            }
            set
            {
                _previousPageReservedVisibility = value;
                OnPropertyChanged(nameof(PreviousPageReservedVisibility));
            }
        }
        #endregion



        public ClientsJourneysViewModel(IArrangementService arrangementService)
        {
            reservatedPage = 0;
            boughtPage = 0;
            accountStore = App.host.Services.GetService<AccountStore>();
            NextPageCommand = new NextPageClientsJourneysCommand(this);
            PreviousPageCommand = new PreviousPageClientsJourneysCommand(this);
            this.arrangementService = arrangementService;
            userId = accountStore.Id;
            ReservedJourneys =
                new ObservableCollection<JourneyForCard>(readCards(userId, reservatedPage, 4, ArrangementStatus.RESERVED));
            BoughtJourneys =
                new ObservableCollection<JourneyForCard>(readCards(userId, boughtPage, 4, ArrangementStatus.BOUGHT));
            ReservedJourneyCardViewModels =
                new ObservableCollection<JourneyCardViewModel>(CreateJourneyCardViews(ReservedJourneys));
            BoughtJourneyCardViewModels =
                new ObservableCollection<JourneyCardViewModel>(CreateJourneyCardViews(BoughtJourneys));
            PreviousPageVisibility = Visibility.Hidden;
            PreviousPageReservedVisibility = Visibility.Hidden;
            NextPageVisibility = Visibility.Visible;
            NextPageReservedVisibility = Visibility.Visible;
        }

        private List<JourneyForCard> readCards(Guid userId, int page, int pageSize, ArrangementStatus status)
        {
            List<Arrangement> arrangements = arrangementService.ReadPage(userId, page, 4, status);
            List<JourneyForCard> journeysForCard = new List<JourneyForCard>();
            foreach (var arrangement in arrangements)
                journeysForCard.Add(new JourneyForCard(arrangement.Journey));
            return journeysForCard;
        }

        public List<JourneyCardViewModel> CreateJourneyCardViews(ObservableCollection<JourneyForCard> journeys)
        {
            List<JourneyCardViewModel> journeyCardViews = new List<JourneyCardViewModel>();

            foreach (JourneyForCard journey in journeys)
            {
                JourneyCardViewModel journeyCardViewModel = new JourneyCardViewModel(journey);
                journeyCardViews.Add(journeyCardViewModel);

            }

            return journeyCardViews;
        }
    }
    
}
