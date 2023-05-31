using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Three_Far_Away.Infrastructure;
using Three_Far_Away.Models;
using Three_Far_Away.ViewModels;

namespace Three_Far_Away.Commands
{
    public class ViewJourneyPreviewCommand : CommandBase
    {
        private readonly JourneyCardViewModel _journeyCardViewModel;
        public ViewJourneyPreviewCommand(JourneyCardViewModel journeyCardViewModel)
        {
            _journeyCardViewModel = journeyCardViewModel;

        }
        public override void Execute(object parameter)
        {
            if (_journeyCardViewModel.role.Equals(Role.AGENT))
                EventBus.FireEvent("AgentJourneyPreview", _journeyCardViewModel.JourneyId);
            else
                EventBus.FireEvent("ClientJourneyPreview", _journeyCardViewModel.JourneyId);
        }
    }
}
