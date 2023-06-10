using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.DependencyInjection;
using System.ComponentModel;
using Three_Far_Away.ViewModels;
using Three_Far_Away.Views;

namespace Three_Far_Away.Commands
{
    public class OpenAttractionImagePreviewCommand : CommandBase
    {
        private CreateAttractionViewModel _createAttractionViewModel;
        public OpenAttractionImagePreviewCommand(CreateAttractionViewModel createAttractionViewModel) 
        {
            _createAttractionViewModel = createAttractionViewModel;
            _createAttractionViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override bool CanExecute(object parameter)
        {
            return base.CanExecute(parameter) && _createAttractionViewModel.AttractionImage != "";
        }

        public override void Execute(object parameter)
        {
            ImagePreviewViewModel imagePreviewViewModel = new ImagePreviewViewModel(_createAttractionViewModel.AttractionImage);
            ImagePreviewView dialogView = new ImagePreviewView();
            dialogView.DataContext = imagePreviewViewModel;

            App.host.Services.GetRequiredService<MainWindow>().ShowDialog(dialogView);
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(CreateAttractionViewModel.AttractionImage))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
