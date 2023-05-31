using Three_Far_Away.Infrastructure;

namespace Three_Far_Away.ViewModels
{
    public class NavigableViewModel : ObservableEntity
    {

        private object _currentViewModel;
        public object CurrentViewModel { get => _currentViewModel; set => OnPropertyChanged(ref _currentViewModel, value); }

        public NavigableViewModel()
        {
        }

        public void SwitchCurrentViewModel(object nextViewModel)
        {
            CurrentViewModel = nextViewModel;
        }
    }
}
