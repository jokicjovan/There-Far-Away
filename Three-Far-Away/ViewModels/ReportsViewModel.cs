using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Three_Far_Away.Models;
using Three_Far_Away.Services.Interfaces;

namespace Three_Far_Away.ViewModels
{
    public class ReportsViewModel : ViewModelBase
    {
        public int page;
        public readonly IJourneyService journeyService;

        private ObservableCollection<Journey> journeys;
        public ObservableCollection<Journey> Journeys
        {
            get { return journeys; }
            set
            {
                journeys = value;
                OnPropertyChanged(nameof(Journeys));
            }
        }

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

        public ReportsViewModel(IJourneyService journeyService)
        {
            page = 0;
            this.journeyService = journeyService;
            Journeys = new ObservableCollection<Journey>(journeyService.ReadPageWithDate(page, 4, DateTime.MinValue, DateTime.Now));
            ReportCardViewModels = new ObservableCollection<ReportCardViewModel>(CreateReportCardViewModels());
        }


        public List<ReportCardViewModel> CreateReportCardViewModels()
        {
            List<ReportCardViewModel> journeyCardViewModels = new List<ReportCardViewModel>();

            foreach (Journey journey in Journeys)
            {
                journeyCardViewModels.Add(new ReportCardViewModel(journey));

            }

            return journeyCardViewModels;
        }
    }
}
