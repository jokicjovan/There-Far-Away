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
                IsEmpty = (value.Count > 0) ? false : true;
                OnPropertyChanged(nameof(BoughtJourneyCardViewModels));
            }
        }

        private ObservableCollection<Journey> _boughtJourneys;
        public ObservableCollection<Journey> BoughtJourneys
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
                IsEmpty = (value.Count > 0) ? false : true;
                OnPropertyChanged(nameof(ReservedJourneyCardViewModels));
            }
        }

        private ObservableCollection<Journey> _reservedJourneys;
        public ObservableCollection<Journey> ReservedJourneys
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

        private bool _isEmpty;
        public bool IsEmpty
        {
            get
            {
                return _isEmpty;
            }
            set
            {
                _isEmpty = value;
                OnPropertyChanged(nameof(IsEmpty));
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
            PreviousPageVisibility = Visibility.Hidden;
            NextPageVisibility = Visibility.Visible;
            PreviousPageReservedVisibility = Visibility.Hidden;
            NextPageReservedVisibility = Visibility.Visible;
            InitBoughtPager();
            InitReservedPager();
            LoadBoughtJourneys();
            LoadReservedJourneys();
        }

        public void InitReservedPager()
        {
            reservatedPage = 0;
            PreviousPageReservedVisibility = Visibility.Collapsed;
            NextPageReservedVisibility = Visibility.Collapsed;
        }

        public void InitBoughtPager()
        {
            boughtPage = 0;
            PreviousPageVisibility = Visibility.Collapsed;
            NextPageVisibility = Visibility.Collapsed;
        }

        public void LoadReservedJourneys()
        {
            ReservedJourneys =
                new ObservableCollection<Journey>(readCards(userId, reservatedPage, 4, ArrangementStatus.RESERVED));
            ReservedJourneyCardViewModels =
                new ObservableCollection<JourneyCardViewModel>(CreateJourneyCardViews(ReservedJourneys));
            if (reservatedPage == 0)
                PreviousPageReservedVisibility = Visibility.Collapsed;
            else
                PreviousPageReservedVisibility = Visibility.Visible;

            if (ReservedJourneyCardViewModels.Count < 4)
                NextPageReservedVisibility = Visibility.Collapsed;
            else if (ReservedJourneyCardViewModels.Count == 4 && readCards(userId, reservatedPage + 1, 4, ArrangementStatus.RESERVED).Count == 0)
                NextPageReservedVisibility = Visibility.Collapsed;
            else
                NextPageReservedVisibility = Visibility.Visible;
        }

        public void LoadBoughtJourneys()
        {
            BoughtJourneys =
                new ObservableCollection<Journey>(readCards(userId, boughtPage, 4, ArrangementStatus.BOUGHT));
            BoughtJourneyCardViewModels =
                new ObservableCollection<JourneyCardViewModel>(CreateJourneyCardViews(BoughtJourneys));
            if (boughtPage == 0)
                PreviousPageVisibility = Visibility.Collapsed;
            else
                PreviousPageVisibility = Visibility.Visible;

            if (BoughtJourneyCardViewModels.Count < 4)
                NextPageVisibility = Visibility.Collapsed;
            else if (BoughtJourneyCardViewModels.Count == 4 && readCards(userId, boughtPage + 1, 4, ArrangementStatus.BOUGHT).Count == 0)
                NextPageVisibility = Visibility.Collapsed;
            else
                NextPageVisibility = Visibility.Visible;
        }

        private List<Journey> readCards(Guid userId, int page, int pageSize, ArrangementStatus status)
        {
            List<Arrangement> arrangements = arrangementService.ReadPage(userId, page, 4, status);
            List<Journey> journeysForCard = new List<Journey>();
            foreach (var arrangement in arrangements)
                journeysForCard.Add(arrangement.Journey);
            return journeysForCard;
        }

        public List<JourneyCardViewModel> CreateJourneyCardViews(ObservableCollection<Journey> journeys)
        {
            List<JourneyCardViewModel> journeyCardViews = new List<JourneyCardViewModel>();

            foreach (Journey journey in journeys)
            {
                JourneyCardViewModel journeyCardViewModel = new JourneyCardViewModel(journey);
                journeyCardViews.Add(journeyCardViewModel);

            }

            return journeyCardViews;
        }
    }
    
}
