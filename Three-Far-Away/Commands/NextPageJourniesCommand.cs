using System.Collections.Generic;
using Three_Far_Away.Models.DTOs;
using Three_Far_Away.Models;
using Three_Far_Away.ViewModels;
using System.Collections.ObjectModel;

namespace Three_Far_Away.Commands
{
    public class NextPageJourniesCommand : CommandBase
    {
        private readonly JourneysViewModel _journeysViewModel;
        public NextPageJourniesCommand(JourneysViewModel journeysViewModel)
        {
            _journeysViewModel = journeysViewModel;

        }
        public override void Execute(object parameter)
        {
            List<Journey> journeys = _journeysViewModel.journeyService.ReadPage(_journeysViewModel.page + 1, 4);
            if (journeys.Count == 4)
                _journeysViewModel.page++;
            List<JourneyForCard> journeysForCard = new List<JourneyForCard>();
            foreach (var journey in journeys)
                journeysForCard.Add(new JourneyForCard(journey));
            this._journeysViewModel.Journeys = new ObservableCollection<JourneyForCard>(journeysForCard);
            this._journeysViewModel.JourneyCardViewModels = new ObservableCollection<JourneyCardViewModel>(this._journeysViewModel.CreateJourneyCardViews());

        }
    }
}
