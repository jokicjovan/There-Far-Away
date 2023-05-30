using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using Three_Far_Away.ViewModels;
using Three_Far_Away.Views;

namespace Three_Far_Away.Commands
{
    public class ShowJourneyPassengersCommand : CommandBase
    {
        private readonly JourneyPreviewViewModel _journeyPreviewViewModel;
        public ShowJourneyPassengersCommand(JourneyPreviewViewModel journeyPreviewViewModel)
        {
            _journeyPreviewViewModel = journeyPreviewViewModel;

        }

        public override void Execute(object parameter)
        {
            JourneyPassengersViewModel journeyPassengersViewModel = new JourneyPassengersViewModel(_journeyPreviewViewModel.Id);
            JourneyPassengersView dialogView = new JourneyPassengersView();
            dialogView.DataContext = journeyPassengersViewModel;


/*            Window window = new Window
            {
                Title = "Passengers",
                Content = dialogView,
            };
            window.ShowDialog();*/

            App.host.Services.GetRequiredService<MainWindow>().ShowDialog(dialogView);
        }
    }
}
