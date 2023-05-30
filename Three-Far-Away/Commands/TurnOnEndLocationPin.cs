﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Three_Far_Away.ViewModels;

namespace Three_Far_Away.Commands
{
    internal class TurnOnEndLocationPin:CommandBase
    {
        private readonly CreateJourneyMapViewModel _createJourneyMapViewModel;
        public TurnOnEndLocationPin(CreateJourneyMapViewModel createJourneyMapViewModel)
        {
            _createJourneyMapViewModel = createJourneyMapViewModel;
        }

        public override bool CanExecute(object parameter)
        {
            return true;
        }

        public override async void Execute(object parameter)
        {
            _createJourneyMapViewModel.CanDropEnd = true;
            _createJourneyMapViewModel.CanDropStart = false;
        }
    }
}
