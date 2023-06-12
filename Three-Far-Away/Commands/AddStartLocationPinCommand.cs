using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Three_Far_Away.Models;
using Three_Far_Away.ViewModels;
using Microsoft.Maps.MapControl.WPF;
using System.Windows.Input;
using System.Windows;

namespace Three_Far_Away.Commands
{
    internal class AddStartLocationPinCommand:CommandBase
    {
        private readonly CreateJourneyMapViewModel _createJourneyMapViewModel;
        public AddStartLocationPinCommand(CreateJourneyMapViewModel createJourneyMapViewModel)
        {
            _createJourneyMapViewModel = createJourneyMapViewModel;
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override async void Execute(object parameter)
        {
            _createJourneyMapViewModel.Locations.Add(new MapLocation(new Microsoft.Maps.MapControl.WPF.Location(10, 10), "AAA",true,""));
            
        }
    }
}
