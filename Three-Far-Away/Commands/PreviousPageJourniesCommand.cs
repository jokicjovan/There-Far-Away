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
using System.ComponentModel;

namespace Three_Far_Away.Commands
{
    public class PreviousPageJourniesCommand : CommandBase
    {
        private readonly JourneysViewModel _createJourneyViewModel;
        public PreviousPageJourniesCommand(JourneysViewModel createJourneyViewModel)
        {
            _createJourneyViewModel = createJourneyViewModel;
            _createJourneyViewModel.PropertyChanged += ViewModelPropertyChanged;

        }
        public override void Execute(object parameter)
        {
            _createJourneyViewModel.page--;
            _createJourneyViewModel.LoadJourneys();
        }

        public override bool CanExecute(object parameter)
        {
            return _createJourneyViewModel.PreviousPageVisibility == Visibility.Visible;
        }

        private void ViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_createJourneyViewModel.PreviousPageVisibility))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
