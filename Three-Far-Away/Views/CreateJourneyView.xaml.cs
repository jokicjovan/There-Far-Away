﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Three_Far_Away.Models;

namespace Three_Far_Away.Views
{
    /// <summary>
    /// Interaction logic for CreateJourneyView.xaml
    /// </summary>
    public partial class CreateJourneyView : UserControl
    {
        public CreateJourneyView()
        {
            InitializeComponent();
            trasportationsCB.ItemsSource = Enum.GetValues(typeof (TransportationType));

        }
    }
}
