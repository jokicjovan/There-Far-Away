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
    internal class CreateAttractionCommand:CommandBase
    {
        private readonly CreateAttractionMapViewModel _createAttractionMapViewModel;
        public CreateAttractionCommand(CreateAttractionMapViewModel createAttractionMapViewModel)
        {
            _createAttractionMapViewModel = createAttractionMapViewModel;

            if (_createAttractionMapViewModel != null)
                _createAttractionMapViewModel.PropertyChanged += OnViewModelPropertyChanged;

        }

        public override bool CanExecute(object parameter)
        {
            return _createAttractionMapViewModel.Attraction.Location!=null;
        }

        public override void Execute(object parameter)
        {
            if (_createAttractionMapViewModel._attractionService.Read(_createAttractionMapViewModel.Attraction.Id) != null)
            {
                _createAttractionMapViewModel._attractionService.Update(_createAttractionMapViewModel.Attraction);
            }
            else
            {
                Attraction created = _createAttractionMapViewModel._attractionService.Create(_createAttractionMapViewModel.Attraction);
            }

            EventBus.FireEvent("AgentAttractions");
        }
        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "StartLocationModel")
            {
                OnCanExecuteChanged();
            }
        }
    }
}
