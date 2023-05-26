using Three_Far_Away.ViewModels;

namespace Three_Far_Away.Services.Interfaces
{
    public interface INavigationService<TViewModel> where TViewModel : ViewModelBase
    {
        void Navigate();
    }
}