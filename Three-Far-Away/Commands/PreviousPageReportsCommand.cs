using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using Three_Far_Away.Models;
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

            if (_reportsViewModel.page == 0) 
                _reportsViewModel.PreviousPageVisibility = Visibility.Collapsed;
            else
                _reportsViewModel.PreviousPageVisibility = Visibility.Visible;

            if (_reportsViewModel.ReportCardViewModels.Count < 4)
                _reportsViewModel.NextPageVisibility = Visibility.Collapsed;
            else if (_reportsViewModel.ReportCardViewModels.Count == 4 && _reportsViewModel.journeyService.ReadPageWithDateByMonthAndYear(_reportsViewModel.page + 1, 4, DateTime.MinValue, DateTime.Now, int.Parse(_reportsViewModel.SelectedMonth),
                int.Parse(_reportsViewModel.SelectedYear)).Count == 0)
                _reportsViewModel.NextPageVisibility = Visibility.Collapsed;
            else
                _reportsViewModel.NextPageVisibility = Visibility.Visible;
        }
    }
}
