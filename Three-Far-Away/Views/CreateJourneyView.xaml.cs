using System;
using System.Globalization;
using System.Threading;
using System.Windows.Controls;
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

            CultureInfo ci = CultureInfo.CreateSpecificCulture(CultureInfo.CurrentCulture.Name);
            ci.DateTimeFormat.ShortDatePattern = "MM/dd/yyyy";
            Thread.CurrentThread.CurrentCulture = ci;
        }
    }
}
