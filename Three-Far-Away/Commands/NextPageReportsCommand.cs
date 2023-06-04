using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using Three_Far_Away.Models;
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
            List<Journey> journeys = _reportsViewModel.journeyService.ReadPage(++_reportsViewModel.page, 4);

            if (_reportsViewModel.page == 0)
                _reportsViewModel.PreviousPageVisibility = Visibility.Collapsed;
            else
                _reportsViewModel.PreviousPageVisibility = Visibility.Visible;

            if (journeys.Count < 4)
                _reportsViewModel.NextPageVisibility = Visibility.Collapsed;
            else if (journeys.Count == 4 && _reportsViewModel.journeyService.ReadPageWithDate(_reportsViewModel.page + 1, 4, DateTime.MinValue, DateTime.Now).Count == 0)
                _reportsViewModel.NextPageVisibility = Visibility.Collapsed;
            else
                _reportsViewModel.NextPageVisibility = Visibility.Visible;

            _reportsViewModel.ReportCardViewModels = new ObservableCollection<ReportCardViewModel>(_reportsViewModel.CreateReportCardViewModels(journeys));
        }
    }
}
