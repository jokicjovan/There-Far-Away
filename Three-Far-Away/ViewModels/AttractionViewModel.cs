using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Three_Far_Away.Commands;
using Three_Far_Away.Models;
using Three_Far_Away.Services.Interfaces;
using Three_Far_Away.Stores;
using Three_Far_Away.Views;

namespace Three_Far_Away.ViewModels
{
    public class AttractionViewModel : ViewModelBase
    {
        public int page;

        public readonly AccountStore accountStore;

        #region services
        public readonly IAttractionService attractionService;
        #endregion

        #region commands
        public ICommand NextPageCommand { get; }
        public ICommand PreviousPageCommand { get; }
        public ICommand NavigateToCreateAttractionCommand { get; }
        #endregion

        #region properties
        private ObservableCollection<Attraction> attractions;
        public ObservableCollection<Attraction> Attractions
        {
            get { return attractions; }
            set
            {
                attractions = value;
                OnPropertyChanged(nameof(Attractions));
            }
        }

        private ObservableCollection<AttractionCardViewModel> attractionCardViewModels;
        public ObservableCollection<AttractionCardViewModel> AttractionCardViewModels
        {
            get { return attractionCardViewModels; }
            set
            {
                attractionCardViewModels = value;
                OnPropertyChanged(nameof(AttractionCardViewModels));
            }
        }

        private Visibility _addAttractionVisibility;
        public Visibility AddAttractionVisibility
        {
            get
            {
                return _addAttractionVisibility;
            }
            set
            {
                _addAttractionVisibility = value;
                OnPropertyChanged(nameof(AddAttractionVisibility));
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

        private ObservableCollection<string> _types;
        public ObservableCollection<string> Types
        {
            get
            {
                return _types;
            }
            set
            {
                _types = value;
                OnPropertyChanged(nameof(Types));
                InitPager();
                LoadAttractions();
            }
        }

        private string _selectedType;
        public string SelectedType
        {
            get
            {
                return _selectedType;
            }
            set
            {
                _selectedType = value;
                OnPropertyChanged(nameof(SelectedType));
                InitPager();
                LoadAttractions();
            }
        }
        #endregion

        public AttractionViewModel(IAttractionService attractionService)
        {
            page = 0;
            this.attractionService = attractionService;
            SelectedType = "ALL";
            String option1 = char.ToUpper(AttractionType.ACCOMODATION.ToString()[0]) + AttractionType.ACCOMODATION.ToString().ToLower().Substring(1);
            String option2 = char.ToUpper(AttractionType.ATTRACTION.ToString()[0]) + AttractionType.ATTRACTION.ToString().ToLower().Substring(1);
            String option3 = char.ToUpper(AttractionType.RESTAURANT.ToString()[0]) + AttractionType.RESTAURANT.ToString().ToLower().Substring(1);
            Types = new ObservableCollection<string>
            {
                "All",
                option1,
                option2, 
                option3
            };
            NextPageCommand = new NextPageAttractionsCommand(this);
            PreviousPageCommand = new PreviousPageAttractionsCommand(this);
            // NavigateToCreateAttractionCommand= new NavigateToCreateJourneyCommand(new Journey());
            accountStore = App.host.Services.GetService<AccountStore>();

            if (accountStore.Role.Equals(Role.AGENT))
            {
                AddAttractionVisibility = Visibility.Visible;
            }
            else
            {
                AddAttractionVisibility = Visibility.Hidden;
            }

            PreviousPageVisibility = Visibility.Hidden;
            NextPageVisibility = Visibility.Visible;
            InitPager();
            LoadAttractions();
        }

        public void InitPager()
        {
            page = 0;
            PreviousPageVisibility = Visibility.Collapsed;
            NextPageVisibility = Visibility.Collapsed;
        }

        public void LoadAttractions()
        {
            Attractions = new ObservableCollection<Attraction>(ReadPage(page, 4));
            AttractionCardViewModels = new ObservableCollection<AttractionCardViewModel>(CreateAttractionCardViews());
            if (page == 0)
                PreviousPageVisibility = Visibility.Collapsed;
            else
                PreviousPageVisibility = Visibility.Visible;

            if (AttractionCardViewModels.Count < 4)
                NextPageVisibility = Visibility.Collapsed;
            else if (AttractionCardViewModels.Count == 4 && ReadPage(page + 1, 4).Count == 0)
                NextPageVisibility = Visibility.Collapsed;
            else
                NextPageVisibility = Visibility.Visible;
        }

        public List<Attraction> ReadPage(int page, int size)
        {
            if (SelectedType.ToLower() == AttractionType.ACCOMODATION.ToString().ToLower())
                return attractionService.ReadPage(page, size, AttractionType.ACCOMODATION);
            if (SelectedType.ToLower() == AttractionType.ATTRACTION.ToString().ToLower())
                return attractionService.ReadPage(page, size, AttractionType.ATTRACTION);
            if (SelectedType.ToLower() == AttractionType.RESTAURANT.ToString().ToLower())
                return attractionService.ReadPage(page, size, AttractionType.RESTAURANT);
            return attractionService.ReadPage(page, size);

        }


        public List<AttractionCardViewModel> CreateAttractionCardViews()
        {
            List<AttractionCardViewModel> attractionCardViews = new List<AttractionCardViewModel>();

            foreach (Attraction attraction in Attractions)
            {
                AttractionCardViewModel attractionCardViewModel = new AttractionCardViewModel(attraction);
                attractionCardViewModel.AttractionDeletedEvent += HandleAttractionDeleted;
                attractionCardViews.Add(attractionCardViewModel);

            }

            return attractionCardViews;
        }

        private void HandleAttractionDeleted(object sender, EventArgs e)
        {
            if (Attractions.Count == 1)
            {
                PreviousPageCommand.Execute(null);
                return;
            }
            Attractions = new ObservableCollection<Attraction>(attractionService.ReadPage(page, 4));
            AttractionCardViewModels = new ObservableCollection<AttractionCardViewModel>(CreateAttractionCardViews());
        }
    }
}
