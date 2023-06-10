using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Three_Far_Away.Infrastructure;
using Three_Far_Away.Models;
using Three_Far_Away.ViewModels;

namespace Three_Far_Away.Commands
{
    internal class NavigateToCreateAttractionCommand:CommandBase
    {
        string eventName;
        public Attraction Attraction { get; set; }
        ViewModelBase ViewModelBase { get; set; }

        private string from;
        private string to;

        public NavigateToCreateAttractionCommand(object vm, string from, string to)
        {
            this.from = from;
            this.to = to;
            if (from == "Map")
            {
                CreateAttractionMapViewModel avm = (CreateAttractionMapViewModel)vm;
                ViewModelBase = avm;
                Attraction = avm.Attraction;
            }
            else if (from == "Home")
            {
                CreateAttractionViewModel avm = (CreateAttractionViewModel)vm;
                ViewModelBase = avm;
                Attraction = avm.Attraction;
            }

            if (to == "Map")
            {
                eventName = "CreateAttractionMap";
            }
            else
            {
                eventName = "CreateAttraction";
            }
            if (ViewModelBase != null)
                ViewModelBase.PropertyChanged += OnViewModelPropertyChanged;
        }

        public NavigateToCreateAttractionCommand(Attraction attraction)
        {

            CreateAttractionViewModel jvm = new CreateAttractionViewModel(attraction);
            Attraction = attraction;
            ViewModelBase = jvm;
            eventName = "CreateAttraction";
            from = to = "edit";

            if (ViewModelBase != null)
                ViewModelBase.PropertyChanged += OnViewModelPropertyChanged;
        }
        public override bool CanExecute(object parameter)
        {
            if (from == null)
                return true;
            if (from == "Home")
            {
                return true;
            }
            /*else if (from == "None")
            {
                return true;
            }
            else if (from == "Map")
            {
                return Journey.StartLocation != null && Journey.EndLocation != null;
            }
            else if (from == "Attractions")
            {
                return Journey.Attractions.Count > 0;
            }
            else if (from == "edit")
            {
                return !(Journey.StartDate <= DateTime.Now);
            }*/
            return true;

        }

        public override void Execute(object parameter)
        {
            EventBus.FireEvent(eventName, Attraction);
        }
        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            /*if (Journey == null)
                return;
            if (from == "Home")
            {
                if (e.PropertyName == nameof(CreateJourneyViewModel.Journey.Name) ||
                e.PropertyName == nameof(CreateJourneyViewModel.Journey.Price) ||
                e.PropertyName == nameof(CreateJourneyViewModel.Journey.StartDate) ||
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
                if (e.PropertyName == "StartLocationModel" ||
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
            }*/
        }
    }
}
