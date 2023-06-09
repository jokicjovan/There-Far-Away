using Three_Far_Away.ViewModels;

namespace Three_Far_Away.Commands
{
    public class PreviousPageReportsCommand : CommandBase
    {
        private readonly ReportsViewModel _reportsViewModel;
        public PreviousPageReportsCommand(ReportsViewModel reportsViewModel)
        {
            _reportsViewModel = reportsViewModel;

        }
        public override void Execute(object parameter)
        {
            _reportsViewModel.page--;
            _reportsViewModel.LoadReports();
        }
    }
}
