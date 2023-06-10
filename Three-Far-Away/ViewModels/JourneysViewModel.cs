using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data;
using System.Windows;
using System.Windows.Input;
using Three_Far_Away.Commands;
using Three_Far_Away.Components;
using Three_Far_Away.Events;
using Three_Far_Away.Models;
using Three_Far_Away.Models.DTOs;
using Three_Far_Away.Services.Interfaces;
using Three_Far_Away.Stores;
using Three_Far_Away.Views;

namespace Three_Far_Away.ViewModels
{
    public class JourneysViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public int page;
        
        public readonly AccountStore accountStore;
        
        #region services
        public readonly IJourneyService journeyService;
        #endregion

        #region commands
        public ICommand NextPageCommand { get; }
        public ICommand PreviousPageCommand { get; }
        public ICommand NavigateToCreateJourneyCommand { get; }
        #endregion

        #region properties
        private ObservableCollection<Journey> journeys;
        public ObservableCollection<Journey> Journeys
        {
            get { return journeys; }
            set
            {
                journeys = value;
                OnPropertyChanged(nameof(Journeys));
            }
        }

        private ObservableCollection<JourneyCardViewModel> journeyCardViewModels;
        public ObservableCollection<JourneyCardViewModel> JourneyCardViewModels
        {
            get { return journeyCardViewModels; }
            set
            {
                journeyCardViewModels = value;
                OnPropertyChanged(nameof(JourneyCardViewModels));
            }
        }

        private Visibility _addJourneyVisibility;
        public Visibility AddJourneyVisibility
        {
            get
            {
                return _addJourneyVisibility;
            }
            set
            {
                _addJourneyVisibility = value;
                OnPropertyChanged(nameof(AddJourneyVisibility));
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
        #endregion

        public JourneysViewModel(IJourneyService journeyService)
        {
            page = 0;
            this.journeyService = journeyService;
            
            NextPageCommand = new NextPageJourniesCommand(this);
            PreviousPageCommand = new PreviousPageJourniesCommand(this);
            Journey j = journeyService.ReadAll()[0];
            NavigateToCreateJourneyCommand = new NavigateToCreateJourneyCommand(new Journey());
            accountStore = App.host.Services.GetService<AccountStore>();

            if (accountStore.Role.Equals(Role.AGENT))
            {
                AddJourneyVisibility = Visibility.Visible;
            }
            else
            {
                AddJourneyVisibility = Visibility.Hidden;
            }

            PreviousPageVisibility = Visibility.Hidden;
            NextPageVisibility = Visibility.Visible;
            InitPager();
            LoadJourneys();
        }

        public void InitPager()
        {
            page = 0;
            PreviousPageVisibility = Visibility.Collapsed;
            NextPageVisibility = Visibility.Collapsed;
        }

        public void LoadJourneys()
        {
            Journeys = new ObservableCollection<Journey>(readCards(page, 4));
            JourneyCardViewModels = new ObservableCollection<JourneyCardViewModel>(CreateJourneyCardViews());
            if (page == 0)
                PreviousPageVisibility = Visibility.Collapsed;
            else
                PreviousPageVisibility = Visibility.Visible;

            if(JourneyCardViewModels.Count < 4)
                NextPageVisibility = Visibility.Collapsed;
            else if (JourneyCardViewModels.Count == 4 && readCards(page + 1, 4).Count == 0)
                NextPageVisibility = Visibility.Collapsed;
            else
                NextPageVisibility = Visibility.Visible;
        }

        public List<Journey> readCards(int page, int pageSize)
        {
            List<Journey> journeys = journeyService.ReadPage(page, 4);
            return journeys;
        }

        public List<JourneyCardViewModel> CreateJourneyCardViews()
        {
            List<JourneyCardViewModel> journeyCardViews = new List<JourneyCardViewModel>();

            foreach (Journey journey in Journeys)
            {
                JourneyCardViewModel journeyCardViewModel = new JourneyCardViewModel(journey);
                journeyCardViewModel.JourneyDeletedEvent += HandleJourneyDeleted;
                journeyCardViews.Add(journeyCardViewModel);
            }

            return journeyCardViews;
        }

        private void HandleJourneyDeleted(object sender, EventArgs e)
        {
            if (journeys.Count == 1)
            {
                PreviousPageCommand.Execute(null);
                return;
            }
            Journeys = new ObservableCollection<Journey>(readCards(page, 4));
            JourneyCardViewModels = new ObservableCollection<JourneyCardViewModel>(CreateJourneyCardViews());
        }
    }
}
