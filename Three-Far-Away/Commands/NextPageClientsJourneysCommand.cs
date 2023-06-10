using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Three_Far_Away.Models;
using Three_Far_Away.ViewModels;
using System.Windows;
using System.ComponentModel;

namespace Three_Far_Away.Commands
{
    public class NextPageClientsJourneysCommand : CommandBase
    {
        private readonly ClientsJourneysViewModel _clientsJourneysViewModel;

        public NextPageClientsJourneysCommand(ClientsJourneysViewModel clientsJourneysViewModel)
        {
            _clientsJourneysViewModel = clientsJourneysViewModel;
            _clientsJourneysViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override void Execute(object parameter)
        {
            if (_clientsJourneysViewModel.SelectedTabIndex == 0)
            {
                _clientsJourneysViewModel.reservatedPage++;
                _clientsJourneysViewModel.LoadReservedJourneys();
            }
            else
            {
                _clientsJourneysViewModel.boughtPage++;
                _clientsJourneysViewModel.LoadBoughtJourneys();
            }
        }

        public override bool CanExecute(object parameter)
        {
            if(_clientsJourneysViewModel.SelectedTabIndex == 0)
                return _clientsJourneysViewModel.NextPageReservedVisibility == Visibility.Visible && base.CanExecute(parameter);

            return _clientsJourneysViewModel.NextPageVisibility == Visibility.Visible && base.CanExecute(parameter);
                
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ClientsJourneysViewModel.NextPageVisibility) || e.PropertyName == nameof(ClientsJourneysViewModel.NextPageReservedVisibility) || e.PropertyName == nameof(ClientsJourneysViewModel.SelectedTabIndex))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
