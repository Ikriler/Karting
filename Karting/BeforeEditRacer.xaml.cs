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
    public partial class BeforeEditRacer : Window
    {
        public string currnetBeforeEditRacerEmail;
        public BeforeEditRacer(string email = "")
        {
            InitializeComponent();
            DataController.StartTimerOnCurrentWindow(this.textBlock_DayXInfo, this.textBlock_DayXChanger);
            currnetBeforeEditRacerEmail = email;
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            ControlRacers controlRacers = new ControlRacers();
            controlRacers.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            controlRacers.Show();
            this.Close();
        }

        private void logout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            mainWindow.Show();
            this.Close();
        }

        private void coordEditRacer_Click(object sender, RoutedEventArgs e)
        {
            CoordinatorEditRacer coordinatorEditRacer = new CoordinatorEditRacer(this.t_email.Text);
            coordinatorEditRacer.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            coordinatorEditRacer.Show();
            this.Close();
        }
    }
}
