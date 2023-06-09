using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Three_Far_Away.Models.DTOs;
using Three_Far_Away.Models;
using Three_Far_Away.ViewModels;

namespace Three_Far_Away.Commands
{
    public class PreviousPageJourniesCommand : CommandBase
    {
        private readonly JourneysViewModel _createJourneyViewModel;
        public PreviousPageJourniesCommand(JourneysViewModel createJourneyViewModel)
        {
            _createJourneyViewModel = createJourneyViewModel;

        }
        public override void Execute(object parameter)
        {
            _createJourneyViewModel.page--;
            _createJourneyViewModel.LoadJourneys();
        }
    }
}
