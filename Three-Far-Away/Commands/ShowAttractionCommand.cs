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
    public class ShowAttractionCommand : CommandBase
    {
        private readonly LocationListItemViewModel _locationListItemViewModel;
        public ShowAttractionCommand(LocationListItemViewModel locationListItemViewModel)
        {
            _locationListItemViewModel = locationListItemViewModel;

        }
        public override void Execute(object parameter)
        {
            if (_locationListItemViewModel.accountStore.Role.Equals(Role.AGENT))
                EventBus.FireEvent("AgentAttractionPreview", _locationListItemViewModel.AttractionId);
            else
                EventBus.FireEvent("ClientAttractionPreview", _locationListItemViewModel.AttractionId);
        }

    }
}
