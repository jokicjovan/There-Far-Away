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
                IsEmpty = (value.Count > 0) ? false : true;
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

        private string _selectedMonth;
        public string SelectedMonth
        {
            get
            {
                return _selectedMonth;
            }
            set
            {
                if (value == "All") _selectedMonth = "0";
                else _selectedMonth = DateTime.ParseExact(value, "MMMM", CultureInfo.CurrentCulture).Month.ToString();
                OnPropertyChanged(nameof(SelectedMonth));
                InitPager();
                LoadReports();
            }
        }

        private string _selectedYear;
        public string SelectedYear
        {
            get
            {
                return _selectedYear;
            }
            set
            {
                if (value == "All") _selectedYear = "0";
                else _selectedYear = value;
                OnPropertyChanged(nameof(SelectedYear));
                InitPager();
                LoadReports();
            }
        }

        private bool _isEmpty;
        public bool IsEmpty
        {
            get
            {
                return _isEmpty;
            }
            set
            {
                _isEmpty = value;
                OnPropertyChanged(nameof(IsEmpty));
            }
        }
        #endregion

        #region commands
        public ICommand NextPageCommand { get; }
        public ICommand PreviousPageCommand { get; }
        #endregion

        public ReportsViewModel(IJourneyService journeyService)
        {
            this.journeyService = journeyService;
            Years = new ObservableCollection<string>();
            Months = new ObservableCollection<string>();
            Years.Add("All");
            Months.Add("All");
            _selectedMonth = "0";
            _selectedYear = "0";

            var journeysUntilNow = journeyService.GetJourneysInsideDate(DateTime.MinValue, DateTime.Now);

            var yearsEnumrator = journeysUntilNow.Select(j => j.StartDate.Year).Distinct().GetEnumerator();
            while (yearsEnumrator.MoveNext()) { 
                Years.Add(yearsEnumrator.Current.ToString());
            }

            var monthsEnumerator = journeysUntilNow.Select(j => j.StartDate.Month).Distinct().GetEnumerator();
            while (monthsEnumerator.MoveNext())
            {
                Months.Add(CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(monthsEnumerator.Current));
            }

            NextPageCommand = new NextPageReportsCommand(this);
            PreviousPageCommand = new PreviousPageReportsCommand(this);
            InitPager();
            LoadReports();
        }

        public void InitPager() 
        {
            page = 0;
            PreviousPageVisibility = Visibility.Collapsed;
            NextPageVisibility = Visibility.Collapsed;
        }

        public void LoadReports() {
            var journeys = new List<Journey>(journeyService.ReadPageWithDateByMonthAndYear(page, 4, DateTime.MinValue, DateTime.Now, int.Parse(SelectedMonth), int.Parse(SelectedYear)));
            ReportCardViewModels = new ObservableCollection<ReportCardViewModel>(CreateReportCardViewModels(journeys));

            if (page == 0)
                PreviousPageVisibility = Visibility.Collapsed;
            else
                PreviousPageVisibility = Visibility.Visible;

            if (ReportCardViewModels.Count < 4)
                NextPageVisibility = Visibility.Collapsed;
            else if (ReportCardViewModels.Count == 4 && journeyService.ReadPageWithDateByMonthAndYear(page + 1, 4, DateTime.MinValue, DateTime.Now,
                int.Parse(SelectedMonth), int.Parse(SelectedYear)).Count == 0)
                NextPageVisibility = Visibility.Collapsed;
            else
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
