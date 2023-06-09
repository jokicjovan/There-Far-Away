using System.Collections.Generic;
using Three_Far_Away.Models.DTOs;
using Three_Far_Away.Models;
using Three_Far_Away.ViewModels;
using System.Collections.ObjectModel;
using System.Windows;

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
            _journeysViewModel.page++;
            _journeysViewModel.LoadJourneys();
        }

        public override bool CanExecute(object parameter)
        {
            return _journeysViewModel.NextPageVisibility == Visibility.Visible;
        }
    }
}
