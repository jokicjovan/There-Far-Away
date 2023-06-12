using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using Three_Far_Away.Models;
using Three_Far_Away.Infrastructure;

namespace Three_Far_Away.Views
{
    /// <summary>
    /// Interaction logic for SplashScreenView.xaml
    /// </summary>
    public partial class SplashScreenView : UserControl
    {
        private DispatcherTimer timer;
        public SplashScreenView()
        {
            InitializeComponent();
            timer = new DispatcherTimer(DispatcherPriority.Normal);
            
            timer.Interval = TimeSpan.FromSeconds(0.1);
            timer.Tick += Timer_Tick;
            mediaElement.MediaOpened += MediaElement_MediaOpened;
        }

        private void MediaElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            
            Dispatcher.Invoke(() =>
            {
                pbStatus.Value = 0; 
                timer.Start();
            });
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (pbStatus.Value < pbStatus.Maximum)
            {
                pbStatus.Value += 0.7;
            }
            else
            {
                timer.Stop();
                EventBus.FireEvent("GoToLogin");
            }
        }
    }







}
