using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Three_Far_Away.Commands;
using Three_Far_Away.Stores;
using Three_Far_Away.ViewModels;

namespace Three_Far_Away.Components
{
    public class AgentHamburgerNavigationBarViewModel : ViewModelBase
    {
        public ICommand NavigateJourneys { get; }
        public ICommand ToggleMenuCommand { get; }
        public ICommand NavigateLogin { get; }


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

        private double _menuWidth;
        public double MenuWidth
        {
            get { return _menuWidth; }
            set
            {
                if (_menuWidth != value)
                {
                    _menuWidth = value;
                    OnPropertyChanged(nameof(MenuWidth));
                }
            }
        }
        private bool isMenuOpen;
        public bool IsMenuOpen
        {
            get { return isMenuOpen; }
            set { isMenuOpen = value; OnPropertyChanged(nameof(IsMenuOpen)); }
        }

        public AgentHamburgerNavigationBarViewModel(AccountStore accountStore)
        {
            _name = accountStore.Name;
            NavigateJourneys = new FireEventCommand("AgentJourneys");
            NavigateLogin = new FireEventCommand("BackToLogin");
            ToggleMenuCommand = new HamburgerMenuCommand(this);
            isMenuOpen = false;
        }

    }
}
