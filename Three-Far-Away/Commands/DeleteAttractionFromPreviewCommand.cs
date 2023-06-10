using System;
using System.ComponentModel;
using System.Windows;
using Three_Far_Away.Infrastructure;
using Three_Far_Away.ViewModels;

namespace Three_Far_Away.Commands
{
    public class DeleteAttractionFromPreviewCommand : CommandBase
    {
        private readonly AttractionPreviewViewModel _attractionPreviewViewModel;
        public DeleteAttractionFromPreviewCommand(AttractionPreviewViewModel attractionPreviewViewModel)
        {
            _attractionPreviewViewModel = attractionPreviewViewModel;
        }

        public override bool CanExecute(object parameter)
        {
            return base.CanExecute(parameter);
        }

        public override void Execute(object parameter)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure you want to delete this attraction?", "Delete Confirmation", MessageBoxButton.YesNo);

            if (messageBoxResult == MessageBoxResult.Yes)
            {
                _attractionPreviewViewModel.attractionService.Delete(_attractionPreviewViewModel.AttractionId);
                EventBus.FireEvent("Attractions");
            }
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnCanExecuteChanged();
        }
    }
}
