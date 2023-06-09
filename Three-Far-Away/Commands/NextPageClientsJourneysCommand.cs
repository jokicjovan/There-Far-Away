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
                // List<Arrangement> arrangements = _clientsJourneysViewModel.arrangementService.ReadPage(_clientsJourneysViewModel.userId, _clientsJourneysViewModel.reservatedPage + 1, 4, ArrangementStatus.RESERVED);
                // if (arrangements.Count == 4)
                //     _clientsJourneysViewModel.reservatedPage++;
                // else
                //     _clientsJourneysViewModel.NextPageReservedVisibility= Visibility.Hidden;
                // if (_clientsJourneysViewModel.reservatedPage >= 0) _clientsJourneysViewModel.PreviousPageReservedVisibility= Visibility.Visible;
                // List<JourneyForCard> journeysForCard = new List<JourneyForCard>();
                // foreach (var arrangement in arrangements)
                //     journeysForCard.Add(new JourneyForCard(arrangement.Journey));
                // _clientsJourneysViewModel.ReservedJourneys = new ObservableCollection<JourneyForCard>(journeysForCard);
                // _clientsJourneysViewModel.ReservedJourneyCardViewModels =
                //     new ObservableCollection<JourneyCardViewModel>(_clientsJourneysViewModel.CreateJourneyCardViews(_clientsJourneysViewModel.ReservedJourneys));
                _clientsJourneysViewModel.reservatedPage++;
                _clientsJourneysViewModel.LoadReservedJourneys();
            }
            else
            {
                // List<Arrangement> arrangements = _clientsJourneysViewModel.arrangementService.ReadPage(_clientsJourneysViewModel.userId, _clientsJourneysViewModel.boughtPage + 1, 4, ArrangementStatus.BOUGHT);
                // if (arrangements.Count == 4)
                //     _clientsJourneysViewModel.boughtPage++;
                // else
                //     _clientsJourneysViewModel.NextPageVisibility = Visibility.Hidden;
                // if (_clientsJourneysViewModel.boughtPage >= 0) _clientsJourneysViewModel.PreviousPageVisibility = Visibility.Visible;
                // List<JourneyForCard> journeysForCard = new List<JourneyForCard>();
                // foreach (var arrangement in arrangements)
                //     journeysForCard.Add(new JourneyForCard(arrangement.Journey));
                // _clientsJourneysViewModel.BoughtJourneys = new ObservableCollection<JourneyForCard>(journeysForCard);
                // _clientsJourneysViewModel.BoughtJourneyCardViewModels =
                //     new ObservableCollection<JourneyCardViewModel>(
                //         _clientsJourneysViewModel.CreateJourneyCardViews(_clientsJourneysViewModel.BoughtJourneys));
                _clientsJourneysViewModel.boughtPage++;
                _clientsJourneysViewModel.LoadBoughtJourneys();
            }
        }
    }
}
