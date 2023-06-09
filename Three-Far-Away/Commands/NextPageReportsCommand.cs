using System.ComponentModel;
using Three_Far_Away.ViewModels;

namespace Three_Far_Away.Commands
{
    public class NextPageReportsCommand : CommandBase
    {
        private readonly ReportsViewModel _reportsViewModel;
        public NextPageReportsCommand(ReportsViewModel reportsViewModel)
        {
            _reportsViewModel = reportsViewModel;
            _reportsViewModel.PropertyChanged += OnViewModelPropertyChanged;
        }

        public override void Execute(object parameter)
        {
            _reportsViewModel.page++;
            _reportsViewModel.LoadReports();
        }

        public override bool CanExecute(object parameter)
        {
            return _reportsViewModel.NextPageVisibility == System.Windows.Visibility.Visible && base.CanExecute(parameter);
        }

        private void OnViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(ReportsViewModel.NextPageVisibility))
            {
                OnCanExecuteChanged();
            }
        }
    }
}
