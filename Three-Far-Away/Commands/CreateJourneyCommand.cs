using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Three_Far_Away.Models;
using Three_Far_Away.Services.Interfaces;
using Three_Far_Away.ViewModels;

namespace Three_Far_Away.Commands
{
    public class CreateJourneyCommand : CommandBase
    {
        private readonly CreateJourneyViewModel _createJourneyViewModel;
        public CreateJourneyCommand(CreateJourneyViewModel createJourneyViewModel)
        {
            _createJourneyViewModel = createJourneyViewModel;

        }

        public override void Execute(object parameter)
        {
            Journey journey = new Journey
            {

            };
            Journey created = _createJourneyViewModel._journeyService.Create(journey);
        }
    }
}
