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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Threading;
using System.Windows.Threading;

namespace Karting
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataController.StartTimerOnCurrentWindow(this.textBlock_DayXInfo, this.textBlock_DayXChanger);
        }

        private void go_registration_racer_Click(object sender, RoutedEventArgs e)
        {
            new RacerRegistrationQuestion().Show();
            this.Close();
        }

        private void enter_system_Click(object sender, RoutedEventArgs e)
        {
            new Login().Show();
            this.Close();
        }

        private void go_info_Click(object sender, RoutedEventArgs e)
        {
            new InfoMainWindow().Show();
            this.Close();
        }

        private void go_registration_sponsor_Click(object sender, RoutedEventArgs e)
        {
            new SponsorRegistration().Show();
            this.Close();
        }
    }
}
