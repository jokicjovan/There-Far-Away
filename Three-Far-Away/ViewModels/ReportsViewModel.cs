using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using Three_Far_Away.Commands;
using Three_Far_Away.Models;
using Three_Far_Away.Services.Interfaces;

namespace Three_Far_Away.ViewModels
{
    public class ReportsViewModel : ViewModelBase
    {
        public int page;
        #region services
        public readonly IJourneyService journeyService;
        #endregion

        #region properties
        private ObservableCollection<ReportCardViewModel> _reportCardViewModels;
        public ObservableCollection<ReportCardViewModel> ReportCardViewModels
        {
            get { return _reportCardViewModels; }
            set
            {
                _reportCardViewModels = value;
                OnPropertyChanged(nameof(ReportCardViewModels));
            }
        }

        private ObservableCollection<string> _months;
        public ObservableCollection<string> Months
        {
            get { return _months; }
            set
            {
                _months = value;
                OnPropertyChanged(nameof(Months));
            }
        }

        private ObservableCollection<string> _years;
        public ObservableCollection<string> Years
        {
            get { return _years; }
            set
            {
                _years = value;
                OnPropertyChanged(nameof(Years));
            }
        }

        private Visibility _nextPageVisibility;
        public Visibility NextPageVisibility
        {
            get
            {
                return _nextPageVisibility;
            }
            set
            {
                _nextPageVisibility = value;
                OnPropertyChanged(nameof(NextPageVisibility));
            }
        }

        private Visibility _previousPageVisibility;
        public Visibility PreviousPageVisibility
        {
            get
            {
                return _previousPageVisibility;
            }
            set
            {
                _previousPageVisibility = value;
                OnPropertyChanged(nameof(PreviousPageVisibility));
            }
        }

        #endregion

        #region commands
        public ICommand NextPageCommand { get; }
        public ICommand PreviousPageCommand { get; }
        #endregion

        public ReportsViewModel(IJourneyService journeyService)
        {
            page = 0;
            this.journeyService = journeyService;
            Years = new ObservableCollection<string>();
            Months = new ObservableCollection<string>();
            Years.Add("All");
            Months.Add("All");

            var yearsEnumrator = journeyService.GetJourneysInsideDate(DateTime.MinValue, DateTime.Now).Select(j => j.StartDate.Year).Distinct().GetEnumerator();
            while (yearsEnumrator.MoveNext()) { 
                Years.Add(yearsEnumrator.Current.ToString());
            }

            var monthsEnumerator = journeyService.GetJourneysInsideDate(DateTime.MinValue, DateTime.Now).Select(j => j.StartDate.Month).Distinct().GetEnumerator();
            while (monthsEnumerator.MoveNext())
            {
                Months.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(monthsEnumerator.Current));
            }

            var journeys = new List<Journey>(journeyService.ReadPageWithDate(page, 4, DateTime.MinValue, DateTime.Now));
            ReportCardViewModels = new ObservableCollection<ReportCardViewModel>(CreateReportCardViewModels(journeys));

            NextPageCommand = new NextPageReportsCommand(this);
            PreviousPageCommand = new PreviousPageReportsCommand(this); 
            PreviousPageVisibility = Visibility.Collapsed;
            NextPageVisibility = Visibility.Visible;
        }


        public List<ReportCardViewModel> CreateReportCardViewModels(List<Journey> journeys)
        {
            List<ReportCardViewModel> journeyCardViewModels = new List<ReportCardViewModel>();

            foreach (Journey journey in journeys)
            {
                journeyCardViewModels.Add(new ReportCardViewModel(journey));
            }

            return journeyCardViewModels;
        }
    }
}
