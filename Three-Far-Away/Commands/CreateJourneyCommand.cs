﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Three_Far_Away.Infrastructure;
using Three_Far_Away.Models;
using Three_Far_Away.Services.Interfaces;
using Three_Far_Away.ViewModels;

namespace Three_Far_Away.Commands
{
    public class CreateJourneyCommand : CommandBase
    {
        private readonly CreateJourneyAttractionsViewModel _createJourneyAttractionsViewModel;
        public CreateJourneyCommand(CreateJourneyAttractionsViewModel createJourneyAttractionsViewModel)
        {
            _createJourneyAttractionsViewModel = createJourneyAttractionsViewModel;

            if (_createJourneyAttractionsViewModel != null)
                _createJourneyAttractionsViewModel.PropertyChanged += OnViewModelPropertyChanged;

        }

        public override bool CanExecute(object parameter)
        {
            return _createJourneyAttractionsViewModel.Journey.Attractions.Count > 0;
        }

        public override void Execute(object parameter)
        {
            if (_createJourneyAttractionsViewModel._journeyService.Read(_createJourneyAttractionsViewModel.Journey.Id) != null)
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to save changes?", "Edit Confirmation", MessageBoxButton.YesNo);


                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    _createJourneyAttractionsViewModel._journeyService.Update(_createJourneyAttractionsViewModel.Journey);
                    EventBus.FireEvent("AgentJourneys");
                }
            }
            else
            {
                MessageBoxResult messageBoxResult = MessageBox.Show("Are you certain you wish to submit the new journey?", "Add Confirmation", MessageBoxButton.YesNo);


                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    Journey created = _createJourneyAttractionsViewModel._journeyService.Create(_createJourneyAttractionsViewModel.Journey);
                    EventBus.FireEvent("AgentJourneys");
                }
            }

            
        }
        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Attractions")
            {
                OnCanExecuteChanged();
            }
        }
    }
}
