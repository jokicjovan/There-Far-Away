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
    class PreviousPageAttractionsCommand : CommandBase
    {
        private readonly AttractionViewModel _attractionViewModel;
        public PreviousPageAttractionsCommand(AttractionViewModel attractionViewModel)
        {
            _attractionViewModel = attractionViewModel;
            _attractionViewModel.PropertyChanged += ViewModelPropertyChanged;

        }
        public override void Execute(object parameter)
        {
            _attractionViewModel.page--;
            _attractionViewModel.LoadAttractions();
        }

        public override bool CanExecute(object parameter)
        {
            return _attractionViewModel.PreviousPageVisibility == Visibility.Visible;
        }

        private void ViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(_attractionViewModel.PreviousPageVisibility))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
