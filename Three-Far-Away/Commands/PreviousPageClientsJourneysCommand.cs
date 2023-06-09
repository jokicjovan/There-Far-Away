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
                // List<Arrangement> arrangements = _clientsJourneysViewModel.arrangementService.ReadPage(_clientsJourneysViewModel.userId, _clientsJourneysViewModel.reservatedPage > 0 ? --_clientsJourneysViewModel.reservatedPage : 0, 4, ArrangementStatus.RESERVED);
                // List<JourneyForCard> journeysForCard = new List<JourneyForCard>();
                // if (_clientsJourneysViewModel.reservatedPage == 0) 
                //     _clientsJourneysViewModel.PreviousPageReservedVisibility= Visibility.Hidden;
                // _clientsJourneysViewModel.NextPageReservedVisibility= Visibility.Visible;
                // foreach (var arrangement in arrangements)
                //     journeysForCard.Add(new JourneyForCard(arrangement.Journey));
                // _clientsJourneysViewModel.ReservedJourneys = new ObservableCollection<JourneyForCard>(journeysForCard);
                // _clientsJourneysViewModel.ReservedJourneyCardViewModels =
                //     new ObservableCollection<JourneyCardViewModel>(_clientsJourneysViewModel.CreateJourneyCardViews(_clientsJourneysViewModel.ReservedJourneys));
                _clientsJourneysViewModel.reservatedPage--;
                _clientsJourneysViewModel.LoadReservedJourneys();
            }
            else
            {
                // List<Arrangement> arrangements = _clientsJourneysViewModel.arrangementService.ReadPage(_clientsJourneysViewModel.userId, _clientsJourneysViewModel.boughtPage > 0 ? --_clientsJourneysViewModel.boughtPage : 0, 4, ArrangementStatus.BOUGHT);
                // List<JourneyForCard> journeysForCard = new List<JourneyForCard>();
                // if (_clientsJourneysViewModel.boughtPage == 0) _clientsJourneysViewModel.PreviousPageVisibility = Visibility.Hidden;
                // _clientsJourneysViewModel.NextPageVisibility = Visibility.Visible;
                // foreach (var arrangement in arrangements)
                //     journeysForCard.Add(new JourneyForCard(arrangement.Journey));
                // _clientsJourneysViewModel.BoughtJourneys = new ObservableCollection<JourneyForCard>(journeysForCard);
                // _clientsJourneysViewModel.BoughtJourneyCardViewModels =
                //     new ObservableCollection<JourneyCardViewModel>(_clientsJourneysViewModel.CreateJourneyCardViews(_clientsJourneysViewModel.BoughtJourneys));
                _clientsJourneysViewModel.boughtPage--;
                _clientsJourneysViewModel.LoadBoughtJourneys();
            }
        }
    }
}
