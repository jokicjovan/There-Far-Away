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
        public readonly IJourneyService journeyService;
        public int page;
        public ICommand NextPageCommand { get; }
        public ICommand PreviousPageCommand { get; }
        public readonly AccountStore accountStore;



        private ObservableCollection<JourneyForCard> journeys;
        public ObservableCollection<JourneyForCard> Journeys
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

        public JourneysViewModel(IJourneyService journeyService)
        {
            page = 0;
            this.journeyService = journeyService;
            Journeys = new ObservableCollection<JourneyForCard>(readCards(page, 4));
            JourneyCardViewModels = new ObservableCollection<JourneyCardViewModel>(CreateJourneyCardViews());
            NextPageCommand = new NextPageJourniesCommand(this);
            PreviousPageCommand = new PreviousPageJourniesCommand(this);
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

        }

        private List<JourneyForCard> readCards(int page, int pageSize)
        {
            List<Journey> journeys = journeyService.ReadPage(page, 4);
            List<JourneyForCard> journeysForCard = new List<JourneyForCard>();
            foreach(var journey in journeys)
                journeysForCard.Add(new JourneyForCard(journey));
            return journeysForCard;
        }

        public List<JourneyCardViewModel> CreateJourneyCardViews()
        {
            List<JourneyCardViewModel> journeyCardViews = new List<JourneyCardViewModel>();

            foreach (JourneyForCard journey in Journeys)
            {
                JourneyCardViewModel journeyCardViewModel = new JourneyCardViewModel(journey);
                journeyCardViewModel.JourneyDeletedEvent += HandleJourneyDeleted;
                journeyCardViews.Add(journeyCardViewModel);

            }

            return journeyCardViews;
        }

        private void HandleJourneyDeleted(object sender, EventArgs e)
        {
            Journeys = new ObservableCollection<JourneyForCard>(readCards(page, 4));
            JourneyCardViewModels = new ObservableCollection<JourneyCardViewModel>(CreateJourneyCardViews());
        }
    }
}
