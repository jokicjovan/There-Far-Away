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
        private AgentHamburgerNavigationBarViewModel _agentHamburgerNavigationBarViewModel;
        public HamburgerMenuCommand(AgentHamburgerNavigationBarViewModel agentHamburgerNavigationBarViewModel)
        {
            _agentHamburgerNavigationBarViewModel = agentHamburgerNavigationBarViewModel;
        }

        public override void Execute(object parameter)
        {
            _agentHamburgerNavigationBarViewModel.IsMenuOpen = !_agentHamburgerNavigationBarViewModel.IsMenuOpen;

            const double targetWidth = 150;
            const int animationDurationMilliseconds = 100;
            const int animationSteps = 20;

            double stepWidth = targetWidth / animationSteps;
            int delay = animationDurationMilliseconds / animationSteps;

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(delay);

            double currentWidth = _agentHamburgerNavigationBarViewModel.MenuWidth;
            double targetMenuWidth = _agentHamburgerNavigationBarViewModel.MenuWidth == 0 ? targetWidth : 0;

            timer.Tick += (sender, e) =>
            {
                if (currentWidth < targetMenuWidth)
                {
                    currentWidth += stepWidth;
                    if (currentWidth >= targetMenuWidth)
                    {
                        currentWidth = targetMenuWidth;
                        timer.Stop();

                    }
                }
                else
                {
                    currentWidth -= stepWidth;
                    if (currentWidth <= targetMenuWidth)
                    {
                        currentWidth = targetMenuWidth;
                        timer.Stop();
                    }
                }

                _agentHamburgerNavigationBarViewModel.MenuWidth = currentWidth;
            };

            timer.Start();
        }
    }
}
