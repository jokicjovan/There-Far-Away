using System.Windows;
using Three_Far_Away.Infrastructure;
using Three_Far_Away.Services.Interfaces;
using Three_Far_Away.ViewModels;

namespace Three_Far_Away.Commands
{
    public class DeleteJourneyFromPreviewCommand : CommandBase
    {
        private readonly JourneyPreviewViewModel _agentJourneyPreviewViewModel;
        public DeleteJourneyFromPreviewCommand(JourneyPreviewViewModel agentJourneyPreviewViewModel)
        {
            _agentJourneyPreviewViewModel = agentJourneyPreviewViewModel;
        }

        public override void Execute(object parameter)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to delete this journey?", "Delete Confirmation", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                _agentJourneyPreviewViewModel.journeyService.Delete(_agentJourneyPreviewViewModel.Id);
                EventBus.FireEvent("AgentJourneys");
            }
        }
    }
}
