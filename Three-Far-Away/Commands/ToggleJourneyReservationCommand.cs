using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using Three_Far_Away.Models;
using Three_Far_Away.ViewModels;

namespace Three_Far_Away.Commands
{
    public class ToggleJourneyReservationCommand : CommandBase
    {
        private readonly JourneyPreviewViewModel _journeyPreviewViewModel;
        public ToggleJourneyReservationCommand(JourneyPreviewViewModel journeyPreviewViewModel)
        {
            _journeyPreviewViewModel = journeyPreviewViewModel;
            _journeyPreviewViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object parameter)
        {
            Arrangement userArrangement = _journeyPreviewViewModel.arrangementService.GetJourneyArrangementForUser(_journeyPreviewViewModel.Id, _journeyPreviewViewModel.accountStore.Id);
            return base.CanExecute(parameter) && !(userArrangement != null && userArrangement.Status == ArrangementStatus.BOUGHT);
        }

        public override async void Execute(object parameter)
        {
            Arrangement userArrangement = _journeyPreviewViewModel.arrangementService.GetJourneyArrangementForUser(_journeyPreviewViewModel.Id, _journeyPreviewViewModel.accountStore.Id);
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to " + 
                ((userArrangement == null) ? "reserve this journey?" : "cancel reservation for this journey?"), 
                "Reservation Confirmation", MessageBoxButton.YesNo);
            if (messageBoxResult == MessageBoxResult.No)
            {
                return;
            }

            if (userArrangement == null)
            {
                Arrangement arrangement = new Arrangement();
                arrangement.User = _journeyPreviewViewModel.userService.Read(_journeyPreviewViewModel.accountStore.Id);
                arrangement.Journey = _journeyPreviewViewModel.journeyService.Read(_journeyPreviewViewModel.Id);
                arrangement.Status = ArrangementStatus.RESERVED;
                arrangement.DateArranged = DateTime.Now;
                _journeyPreviewViewModel.arrangementService.Create(arrangement);

                _journeyPreviewViewModel.ReserveButtonText = "Cancel reservation";
                _journeyPreviewViewModel.InfoMessage = "Journey is reserved, but not bought.";
            }
            else {
                _journeyPreviewViewModel.arrangementService.Delete(userArrangement.Id);

                _journeyPreviewViewModel.ReserveButtonText = "Reserve";
                _journeyPreviewViewModel.InfoMessage = "Journey is not reserved or bought.";
            }

            _journeyPreviewViewModel.IsReserveEnabled = true;
            _journeyPreviewViewModel.IsBuyEnabled = true;

        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnCanExecuteChanged();
        }
    }
}
