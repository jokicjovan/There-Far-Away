using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Three_Far_Away.Models.DTOs;
using Three_Far_Away.Models;
using Three_Far_Away.Services;
using Three_Far_Away.ViewModels;
using System.Collections.ObjectModel;

namespace Three_Far_Away.Commands
{
    public class NextPageJourniesCommand : CommandBase
    {
        private readonly AgentJourneysViewModel _createJourneyViewModel;
        public NextPageJourniesCommand(AgentJourneysViewModel createJourneyViewModel)
        {
            _createJourneyViewModel = createJourneyViewModel;

        }
        public override void Execute(object parameter)
        {
            List<Journey> journeys = _createJourneyViewModel.journeyService.ReadPage(++_createJourneyViewModel.page, 4);
            List<JourneyForCard> journeysForCard = new List<JourneyForCard>();
            foreach (var journey in journeys)
                journeysForCard.Add(new JourneyForCard(journey));
            this._createJourneyViewModel.Journeys = new ObservableCollection<JourneyForCard>(journeysForCard);
            this._createJourneyViewModel.JourneyCardViewModels = new ObservableCollection<JourneyCardViewModel>(this._createJourneyViewModel.CreateJourneyCardViews());

        }
    }
}
