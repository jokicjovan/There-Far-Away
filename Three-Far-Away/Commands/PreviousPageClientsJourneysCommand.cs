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
    public class PreviousPageClientsJourneysCommand : CommandBase
    {
        private readonly ClientsJourneysViewModel _clientsJourneysViewModel;

        public PreviousPageClientsJourneysCommand(ClientsJourneysViewModel clientsJourneysViewModel)
        {
            _clientsJourneysViewModel = clientsJourneysViewModel;
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
    }
}
