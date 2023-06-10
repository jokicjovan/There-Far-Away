using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Three_Far_Away.ViewModels;

namespace Three_Far_Away.Commands
{
    public class NextPageAttractionsCommand : CommandBase
    {
        private readonly AttractionViewModel _attractionViewModel;
        public NextPageAttractionsCommand(AttractionViewModel attractionViewModel)
        {
            _attractionViewModel = attractionViewModel;
            _attractionViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }
        public override void Execute(object parameter)
        {
            _attractionViewModel.page++;
            _attractionViewModel.LoadAttractions();
        }

        public override bool CanExecute(object parameter)
        {
            return _attractionViewModel.NextPageVisibility == Visibility.Visible;
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(AttractionViewModel.NextPageVisibility))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
