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
    public class PreviousPageClientsJourneysCommand : CommandBase
    {
        private readonly ClientsJourneysViewModel _clientsJourneysViewModel;

        public PreviousPageClientsJourneysCommand(ClientsJourneysViewModel clientsJourneysViewModel)
        {
            _clientsJourneysViewModel = clientsJourneysViewModel;
            _clientsJourneysViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override void Execute(object parameter)
        {
            if (_clientsJourneysViewModel.SelectedTabIndex == 0)
            {
                _clientsJourneysViewModel.reservatedPage--;
                _clientsJourneysViewModel.LoadReservedJourneys();
            }
            else
            {
                _clientsJourneysViewModel.boughtPage--;
                _clientsJourneysViewModel.LoadBoughtJourneys();
            }
        }

        public override bool CanExecute(object parameter)
        {
            if (_clientsJourneysViewModel.SelectedTabIndex == 0)
                return _clientsJourneysViewModel.PreviousPageReservedVisibility == Visibility.Visible && base.CanExecute(parameter);

            return _clientsJourneysViewModel.PreviousPageVisibility == Visibility.Visible && base.CanExecute(parameter);

        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ClientsJourneysViewModel.PreviousPageReservedVisibility) || e.PropertyName == nameof(ClientsJourneysViewModel.PreviousPageVisibility) || e.PropertyName == nameof(ClientsJourneysViewModel.SelectedTabIndex))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
