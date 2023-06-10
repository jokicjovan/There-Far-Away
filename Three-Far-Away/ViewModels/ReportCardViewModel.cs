using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using Three_Far_Away.Commands;
using Three_Far_Away.Models;
using Three_Far_Away.Services.Interfaces;

namespace Three_Far_Away.ViewModels
{
    public class ReportCardViewModel : ViewModelBase
    {
        #region services
        public readonly IJourneyService journeyService;
        public readonly IArrangementService arrangementService;
        #endregion

        #region properties
        private Guid _journeyId;
        public Guid JourneyId
        {
            get
            {
                return _journeyId;
            }
            set
            {
                _journeyId = value;
                OnPropertyChanged(nameof(JourneyId));
            }
        }

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string _image;
        public string Image
        {
            get
            {
                return _image;
            }
            set
            {
                _image = value;
                OnPropertyChanged(nameof(Image));
            }
        }

        private int _numberSold;
        public int NumberSold
        {
            get
            {
                return _numberSold;
            }
            set
            {
                _numberSold = value;
                OnPropertyChanged(nameof(NumberSold));
            }
        }

        private string _date;
        public string Date
        {
            get
            {
                return _date;
            }
            set
            {
                _date = value;
                OnPropertyChanged(nameof(Date));
            }
        }
        #endregion

        #region commands
        public ICommand ViewJourneyPreviewCommand { get; }
        #endregion

        public ReportCardViewModel(Journey journey)
        {
            journeyService = App.host.Services.GetService<IJourneyService>();
            arrangementService = App.host.Services.GetService<IArrangementService>();

            JourneyId = journey.Id;
            Name = journey.Name;
            Date = journey.StartDate.ToString().Split(" ")[0] + " - " + journey.EndDate.ToString().Split(" ")[0];
            Image = journey.Image;

            IEnumerable<Arrangement> arrangements = arrangementService.GetJourneyArrangements(journey.Id);
            NumberSold = arrangements.Count();
            ViewJourneyPreviewCommand = new ViewJourneyPreviewFromReportCardCommand(this);
        }
    }
}
