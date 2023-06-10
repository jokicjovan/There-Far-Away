using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;
using Three_Far_Away.ViewModels;
using Three_Far_Away.Views;

namespace Three_Far_Away.Commands
{
    public class OpenJourneyImagePreviewCommand : CommandBase
    {
        private CreateJourneyViewModel _createJourneyViewModel;
        public OpenJourneyImagePreviewCommand(CreateJourneyViewModel createJourneyViewModel)
        {
            _createJourneyViewModel = createJourneyViewModel;
            _createJourneyViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object parameter)
        {
            return base.CanExecute(parameter) && _createJourneyViewModel.JourneyImage != "";
        }

        public override void Execute(object parameter)
        {
            ImagePreviewViewModel imagePreviewViewModel = new ImagePreviewViewModel(_createJourneyViewModel.JourneyImage);
            ImagePreviewView dialogView = new ImagePreviewView();
            dialogView.DataContext = imagePreviewViewModel;

            App.host.Services.GetRequiredService<MainWindow>().ShowDialog(dialogView);
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CreateJourneyViewModel.JourneyImage))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
