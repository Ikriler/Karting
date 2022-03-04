using System;
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
using System.Windows.Shapes;

namespace Karting
{
    public partial class VolunteerLoading : Window
    {
        public VolunteerLoading()
        {
            InitializeComponent();
            DataController.StartTimerOnCurrentWindow(this.textBlock_DayXInfo, this.textBlock_DayXChanger);
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            ControlVolunteer controlVolunteer = new ControlVolunteer();
            controlVolunteer.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            controlVolunteer.Show();
            this.Close();
        }
    }
}
