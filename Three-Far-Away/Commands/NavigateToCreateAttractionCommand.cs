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
            if (from == "Map" && to == "Home")
                return true;
            if (from == null)
                return true;
            if (from == "Home")
            {
                return Attraction.Name.Length > 2 && Attraction.Name.Length < 30 && Attraction.Description.Length > 10 && Attraction.Description.Length < 250 && Attraction.Image != "" && base.CanExecute(parameter);
            }
            else if (from == "Map")
            {
                return Attraction.Location!= null;
            }
            return true;

        }

        public override void Execute(object parameter)
        {
            EventBus.FireEvent(eventName, Attraction);
        }
        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (Attraction == null)
                return;
            if (from == "Home")
            {
                if (e.PropertyName == nameof(CreateAttractionViewModel.Attraction.Name) ||
                e.PropertyName == nameof(CreateAttractionViewModel.Attraction.Description) ||
                e.PropertyName == nameof(CreateAttractionViewModel.Attraction.Type) ||
                e.PropertyName == nameof(CreateAttractionViewModel.Attraction.Image))
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
                if (e.PropertyName == "StartLocationModel")
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
