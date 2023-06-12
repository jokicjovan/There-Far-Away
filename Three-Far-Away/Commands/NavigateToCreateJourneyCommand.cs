using System;
using System.Collections.Generic;
using System.ComponentModel;
using Three_Far_Away.Infrastructure;
using Three_Far_Away.Models;
using Three_Far_Away.ViewModels;

namespace Three_Far_Away.Commands
{
    internal class NavigateToCreateJourneyCommand : CommandBase
    {
        string eventName;
        public Journey Journey{ get; set; }
        ViewModelBase ViewModelBase { get; set; }

        private string from;
        private string to;

        public NavigateToCreateJourneyCommand(object vm, string from, string to)
        {
            this.from = from;
            this.to = to;
            
            if (from == "Map")
            {
                CreateJourneyMapViewModel mvm= (CreateJourneyMapViewModel)vm;
                ViewModelBase = mvm;
                Journey = mvm.Journey;
            }else if(from == "Attractions"){
                CreateJourneyAttractionsViewModel avm = (CreateJourneyAttractionsViewModel)vm;
                ViewModelBase = avm;
                Journey = avm.Journey;
            }
            else if (from == "None")
            {
                CreateJourneyViewModel jvm = new CreateJourneyViewModel(new Journey());
                ViewModelBase = jvm;
                Journey= jvm.Journey;
                Journey.Name = "";
                Journey.StartDate= DateTime.Now;
                Journey.EndDate = DateTime.Now.AddDays(5);
                Journey.Transportation = TransportationType.Car;
                Journey.Attractions = new List<Attraction>();
            }

            else
            {
                CreateJourneyViewModel jvm = (CreateJourneyViewModel)vm;

                ViewModelBase = jvm;
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
            if (ViewModelBase!=null)
                ViewModelBase.PropertyChanged += OnViewModelPropertyChanged;
        }

        public NavigateToCreateJourneyCommand(Journey journey)
        {
            
            CreateJourneyViewModel jvm = new CreateJourneyViewModel(journey);
            Journey = journey;
            ViewModelBase = jvm;
            eventName = "CreateJourney";
            from = to = "edit";
            
            if (ViewModelBase != null)
                ViewModelBase.PropertyChanged += OnViewModelPropertyChanged;
        }
        public override bool CanExecute(object parameter)
        {
            if ((from == "Map" && to == "Home") || (from == "Attractions" && to == "Map"))
                return true;
            if (from == null)
                return true;
            if (from == "Home")
            {
                return Journey.Name.Length > 2 && Journey.Name.Length <= 30 && Journey.Price > 0 && Journey.StartDate >= DateTime.Now && Journey.StartDate <= Journey.EndDate &&  Journey.Image != "" && base.CanExecute(parameter);
            }
            else if (from == "None")
            {
                return true;
            }
            else if (from == "Map")
            {
                return Journey.StartLocation != null && Journey.EndLocation != null;
            }
            else if (from == "Attractions")
            {
                return Journey.Attractions.Count>0;
            }
            else if (from == "edit")
            {
                return !(Journey.StartDate <= DateTime.Now);
            }
            return false;

        }

        public override void Execute(object parameter)
        {
            EventBus.FireEvent(eventName, Journey);
        }
        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (Journey == null)
                return;
            if (from == "Home")
            {
                if (e.PropertyName == nameof(CreateJourneyViewModel.Journey.Name) ||
                e.PropertyName == nameof(CreateJourneyViewModel.Journey.Price) ||
                e.PropertyName == nameof(CreateJourneyViewModel.Journey.StartDate)||
                e.PropertyName == nameof(CreateJourneyViewModel.Journey.EndDate) ||
                e.PropertyName == nameof(CreateJourneyViewModel.Journey.Transportation) ||
                e.PropertyName == nameof(CreateJourneyViewModel.JourneyImage))
                {
                    OnCanExecuteChanged();
                }
            }
            else if (from == "None")
            {
                OnCanExecuteChanged();
            }
            else if (from == "Map")
            {
                if (e.PropertyName == "StartLocationModel"||
                e.PropertyName == "EndLocationModel")
                {
                    OnCanExecuteChanged();
                }
            }
            else if (from == "Attractions")
            {
                if (e.PropertyName == "Attractions")
                {
                    OnCanExecuteChanged();
                }
            }
        }

    }
}
