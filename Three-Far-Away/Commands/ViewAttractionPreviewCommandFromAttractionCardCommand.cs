using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Three_Far_Away.Infrastructure;
using Three_Far_Away.Models;
using Three_Far_Away.ViewModels;
using Three_Far_Away.Views;

namespace Three_Far_Away.Commands
{
    class ViewAttractionPreviewCommandFromAttractionCardCommand : CommandBase
    {
        private readonly AttractionCardViewModel _attractionCardViewModel;
        public ViewAttractionPreviewCommandFromAttractionCardCommand(AttractionCardViewModel attractionCardViewModel)
        {
            _attractionCardViewModel = attractionCardViewModel;

        }
        public override void Execute(object parameter)
        {
            if (_attractionCardViewModel.accountStore.Role.Equals(Role.AGENT))
                EventBus.FireEvent("AgentAttractionPreview", _attractionCardViewModel.AttractionId);
            else
                EventBus.FireEvent("ClientAttractionPreview", _attractionCardViewModel.AttractionId);
        }
    
    }
}
