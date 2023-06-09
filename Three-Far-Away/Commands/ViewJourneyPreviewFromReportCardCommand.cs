using Three_Far_Away.Infrastructure;
using Three_Far_Away.ViewModels;

namespace Three_Far_Away.Commands
{
    public class ViewJourneyPreviewFromReportCardCommand : CommandBase
    {
        private readonly ReportCardViewModel _reportCardViewModel;
        public ViewJourneyPreviewFromReportCardCommand(ReportCardViewModel reportCardViewModel)
        {
            _reportCardViewModel = reportCardViewModel;

        }
        public override void Execute(object parameter)
        {
            EventBus.FireEvent("AgentJourneyPreview", _reportCardViewModel.JourneyId);
        }
    }
}
