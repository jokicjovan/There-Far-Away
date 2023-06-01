﻿using System;
using System.Windows;
using Three_Far_Away.Events;
using Three_Far_Away.ViewModels;

namespace Three_Far_Away.Commands
{
    public class DeleteJourneyFromCardCommand : CommandBase
    {
        private readonly JourneyCardViewModel _journeyCardViewModel;
        public DeleteJourneyFromCardCommand(JourneyCardViewModel journeyCardViewModel)
        {
            _journeyCardViewModel = journeyCardViewModel;

        }

        public override void Execute(object parameter)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to delete this journey?", "Delete Confirmation", MessageBoxButton.YesNo);

            if (_journeyCardViewModel.journeyService.Read(_journeyCardViewModel.JourneyId).StartDate <= DateTime.Now)
            {
                MessageBox.Show("Journey has already passed", "Error");
                return;
            }

            if (messageBoxResult == MessageBoxResult.Yes)
            {
                _journeyCardViewModel.journeyService.Delete(_journeyCardViewModel.JourneyId);
                _journeyCardViewModel.OnJourneyDeletedEvent();
            }
        }
    }
}
