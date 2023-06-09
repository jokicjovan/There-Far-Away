using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Three_Far_Away.Models;
using Three_Far_Away.Services.Interfaces;
using Three_Far_Away.Services;

namespace Three_Far_Away.ViewModels
{
    public class JourneyPassengersViewModel : ViewModelBase
    {
        #region services
        public readonly IArrangementService arrangementService;
        #endregion

        #region properties
        private ObservableCollection<Arrangement> _arragements;
        public ObservableCollection<Arrangement> Arragements
        {
            get
            {
                return _arragements;
            }
            set
            {
                _arragements = value;
                OnPropertyChanged(nameof(Arragements));
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

        public JourneyPassengersViewModel(Guid journeyId)
        {
            arrangementService = App.host.Services.GetService<IArrangementService>();
            _arragements = new ObservableCollection<Arrangement>(arrangementService.GetJourneyArrangements(journeyId));
            IsEmpty = _arragements.Count() <= 0 ? true : false;
        }
    }
}
