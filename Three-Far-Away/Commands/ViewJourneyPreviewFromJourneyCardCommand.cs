using Three_Far_Away.Infrastructure;
using Three_Far_Away.Models;
using Three_Far_Away.ViewModels;

namespace Three_Far_Away.Commands
{
    public class ViewJourneyPreviewFromJourneyCardCommand : CommandBase
    {
        private readonly JourneyCardViewModel _journeyCardViewModel;
        public ViewJourneyPreviewFromJourneyCardCommand(JourneyCardViewModel journeyCardViewModel)
        {
            _journeyCardViewModel = journeyCardViewModel;

        }
        public override void Execute(object parameter)
        {
            if (_journeyCardViewModel.accountStore.Role.Equals(Role.AGENT))
                EventBus.FireEvent("AgentJourneyPreview", _journeyCardViewModel.JourneyId);
            else
                EventBus.FireEvent("ClientJourneyPreview", _journeyCardViewModel.JourneyId);
        }
    }
}
