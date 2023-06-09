using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Three_Far_Away.Models.DTOs;
using Three_Far_Away.Models;
using Three_Far_Away.ViewModels;
using System.Windows;

namespace Three_Far_Away.Commands
{
    public class NextPageClientsJourneysCommand : CommandBase
    {
        private readonly ClientsJourneysViewModel _clientsJourneysViewModel;

        public NextPageClientsJourneysCommand(ClientsJourneysViewModel clientsJourneysViewModel)
        {
            _clientsJourneysViewModel = clientsJourneysViewModel;
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
    }
}
