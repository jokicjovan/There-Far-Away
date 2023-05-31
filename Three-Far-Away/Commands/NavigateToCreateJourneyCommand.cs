using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Three_Far_Away.Infrastructure;
using Three_Far_Away.Models;
using Three_Far_Away.ViewModels;

namespace Three_Far_Away.Commands
{
    internal class NavigateToCreateJourneyCommand : CommandBase
    {
        string eventName;
        public Journey Journey{ get; set; }

        public NavigateToCreateJourneyCommand(object vm, string from, string to)
        {
            if (from == "Map")
            {
                CreateJourneyMapViewModel mvm= (CreateJourneyMapViewModel)vm;
                Journey = mvm.Journey;
            }else if(from == "Attractions"){
                CreateJourneyAttractionsViewModel avm = (CreateJourneyAttractionsViewModel)vm;
                Journey = avm.Journey;
            }
            else if (from == "None")
            {
                Journey = new Journey();
                Journey.StartDate= DateTime.Now;
                Journey.EndDate = DateTime.Now.AddDays(5);
            }
            else
            {
                CreateJourneyViewModel jvm = (CreateJourneyViewModel)vm;
                Journey = jvm.Journey;
            }

            if (to == "Map")
            {
                eventName = "CreateJourneyMap";
            }
            else if (to == "Attractions")
            {
                eventName = "CreateJourneyAttractions";
            }
            else
            {
                eventName = "CreateJourney";
            }
        }

        public override void Execute(object parameter)
        {
            EventBus.FireEvent(eventName, Journey);
        }

    }
}
