using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Three_Far_Away.Models;
using Three_Far_Away.ViewModels;

namespace Three_Far_Away.Commands
{
    public class BuyJourneyCommand : CommandBase
    {
        private readonly JourneyPreviewViewModel _journeyPreviewViewModel;
        public BuyJourneyCommand(JourneyPreviewViewModel journeyPreviewViewModel)
        {
            _journeyPreviewViewModel = journeyPreviewViewModel;
            _journeyPreviewViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object parameter)
        {
            Arrangement userArrangement = _journeyPreviewViewModel.arrangementService.GetJourneyArrangementForUser(_journeyPreviewViewModel.Id, _journeyPreviewViewModel.accountStore.Id);
            return base.CanExecute(parameter) && !(userArrangement != null && userArrangement.Status == ArrangementStatus.BOUGHT) &&
                !(_journeyPreviewViewModel.journeyService.Read(_journeyPreviewViewModel.Id).StartDate < DateTime.UtcNow);
        }

        public override async void Execute(object parameter)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to buy this journey?", "Buying Confirmation", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.No)
            {
                return;
            }

            Arrangement userArrangement = _journeyPreviewViewModel.arrangementService.GetJourneyArrangementForUser(_journeyPreviewViewModel.Id, _journeyPreviewViewModel.accountStore.Id);
            if (userArrangement == null)
            {
                Arrangement arrangement = new Arrangement();
                arrangement.User = _journeyPreviewViewModel.userService.Read(_journeyPreviewViewModel.accountStore.Id);
                arrangement.Journey = _journeyPreviewViewModel.journeyService.Read(_journeyPreviewViewModel.Id);
                arrangement.Status = ArrangementStatus.BOUGHT;
                arrangement.DateArranged = DateTime.Now;
                _journeyPreviewViewModel.arrangementService.Create(arrangement);
            }
            else
            {
                userArrangement.Status = ArrangementStatus.BOUGHT;
                _journeyPreviewViewModel.arrangementService.Update(userArrangement);
            }

            _journeyPreviewViewModel.InfoMessage = "Journey is bought.";
            _journeyPreviewViewModel.ReserveButtonText = "Reserve";
            _journeyPreviewViewModel.IsReserveEnabled = false;
            _journeyPreviewViewModel.IsBuyEnabled = false;
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnCanExecuteChanged();
        }
    }
}
