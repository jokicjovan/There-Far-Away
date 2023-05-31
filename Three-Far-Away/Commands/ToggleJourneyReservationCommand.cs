using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
            if (userArrangement == null)
            {
                Arrangement arrangement = new Arrangement();
                arrangement.User = _journeyPreviewViewModel.userService.Read(_journeyPreviewViewModel.accountStore.Id);
                arrangement.Journey = _journeyPreviewViewModel.journeyService.Read(_journeyPreviewViewModel.Id);
                arrangement.Status = ArrangementStatus.RESERVED;
                arrangement.DateArranged = DateTime.Now;
                _journeyPreviewViewModel.arrangementService.Create(arrangement);

                _journeyPreviewViewModel.ReserveButtonText = "Unreserve";
            }
            else {
                _journeyPreviewViewModel.arrangementService.Delete(userArrangement.Id);

                _journeyPreviewViewModel.ReserveButtonText = "Reserve";
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
