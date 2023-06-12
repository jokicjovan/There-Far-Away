using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Animation;
using System.Windows;
using Three_Far_Away.Components;
using System.Threading;
using System.Windows.Threading;

namespace Three_Far_Away.Commands
{
    public class HamburgerMenuCommand : CommandBase
    {
        private IHamburgerMenu _hamburgerNavigationBarViewModel;
        private bool _canExecute = true;
        public HamburgerMenuCommand(IHamburgerMenu hamburgerNavigationBarViewModel)
        {
            _hamburgerNavigationBarViewModel = hamburgerNavigationBarViewModel;
        }

        public override bool CanExecute(object parameter)
        {
            return _canExecute;
        }

        public override void Execute(object parameter)
        {
            _hamburgerNavigationBarViewModel.IsMenuOpen = !_hamburgerNavigationBarViewModel.IsMenuOpen;
            _canExecute = false;
            const double targetWidth = 150;
            const int animationDurationMilliseconds = 0;
            const int animationSteps = 20;

            double stepWidth = targetWidth / animationSteps;
            int delay = animationDurationMilliseconds / animationSteps;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(delay);

            double currentWidth = _hamburgerNavigationBarViewModel.MenuWidth;
            double targetMenuWidth = _hamburgerNavigationBarViewModel.MenuWidth == 0 ? targetWidth : 0;

            timer.Tick += (sender, e) =>
            {
                if (currentWidth < targetMenuWidth)
                {
                    currentWidth += stepWidth;
                    if (currentWidth >= targetMenuWidth)
                    {
                        currentWidth = targetMenuWidth;
                        timer.Stop();
                        _canExecute = true;
                    }
                }
                else
                {
                    currentWidth -= stepWidth;
                    if (currentWidth <= targetMenuWidth)
                    {
                        currentWidth = targetMenuWidth;
                        timer.Stop();
                        _canExecute = true;
                    }
                }

                _hamburgerNavigationBarViewModel.MenuWidth = currentWidth;
            };

            timer.Start();
        }
    }
}
