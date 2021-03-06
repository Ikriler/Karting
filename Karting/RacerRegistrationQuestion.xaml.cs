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
    public partial class RacerRegistrationQuestion : Window
    {
        public RacerRegistrationQuestion()
        {
            InitializeComponent();
            DataController.StartTimerOnCurrentWindow(this.textBlock_DayXInfo, this.textBlock_DayXChanger);
        }

        private void go_back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            mainWindow.Show();
            this.Close();
        }

        private void answer_yes_Click(object sender, RoutedEventArgs e)
        {
            Login login = new Login();
            login.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            login.Show();
            this.Close();
        }

        private void answer_no_Click(object sender, RoutedEventArgs e)
        {
            RacerRegistration racerRegistration = new RacerRegistration();
            racerRegistration.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            racerRegistration.Show();
            this.Close();
        }
    }
}
