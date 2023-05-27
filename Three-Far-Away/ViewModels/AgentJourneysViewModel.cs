using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Three_Far_Away.Commands;
using Three_Far_Away.Components;
using Three_Far_Away.Models;
using Three_Far_Away.Models.DTOs;
using Three_Far_Away.Services.Interfaces;
using Three_Far_Away.Stores;

namespace Three_Far_Away.ViewModels
{
    public class AgentJourneysViewModel : ViewModelBase, INotifyPropertyChanged
    {
        public readonly IJourneyService journeyService;
        private ObservableCollection<JourneyForCard> journeys;
        public int page = 0;
        public ICommand NextPageCommand { get; }
        public ICommand PreviousPageCommand { get; }
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

        private AgentNavigationBarViewModel _agentNavigationBarViewModel;
        public AgentNavigationBarViewModel AgentNavigationBarViewModel {
            get { return _agentNavigationBarViewModel; }
            set
            {
                _agentNavigationBarViewModel = value;
                OnPropertyChanged(nameof(AgentNavigationBarViewModel));
            }
        }
        public AgentJourneysViewModel(IJourneyService journeyService, AgentNavigationBarViewModel agentNavigationBarViewModel)
        {
            _agentNavigationBarViewModel = agentNavigationBarViewModel;
            this.journeyService = journeyService;
            Journeys = new ObservableCollection<JourneyForCard>(readCards(0, 4));
            JourneyCardViewModels = new ObservableCollection<JourneyCardViewModel>(CreateJourneyCardViews());
            NextPageCommand = new NextPageJourniesCommand(this);
            PreviousPageCommand = new PreviousPageJourniesCommand(this);
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
                journeyCardViews.Add(new JourneyCardViewModel(journey));
            }

            return journeyCardViews;
        }


    }
}
