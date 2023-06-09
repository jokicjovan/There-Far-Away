using Three_Far_Away.ViewModels;

namespace Three_Far_Away.Commands
{
    public class NextPageReportsCommand : CommandBase
    {
        private readonly ReportsViewModel _reportsViewModel;
        public NextPageReportsCommand(ReportsViewModel reportsViewModel)
        {
            _reportsViewModel = reportsViewModel;
        }

        public override void Execute(object parameter)
        {
            _reportsViewModel.page++;
            _reportsViewModel.LoadReports();
        }
    }
}
