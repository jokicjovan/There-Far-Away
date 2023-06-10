using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Three_Far_Away.ViewModels;

namespace Three_Far_Away.Commands
{
    internal class TurnOnAttractionLocationPinCommand:CommandBase
    {
        private readonly CreateAttractionMapViewModel _createAttractionMapViewModel;
        public TurnOnAttractionLocationPinCommand(CreateAttractionMapViewModel createAttractionMapViewModel)
        {
            _createAttractionMapViewModel = createAttractionMapViewModel;
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override async void Execute(object parameter)
        {
           _createAttractionMapViewModel.CanDropStart = true;
        }
    }
}
