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
    /// <summary>
    /// Логика взаимодействия для CoordinatorPanel.xaml
    /// </summary>
    public partial class CoordinatorPanel : Window
    {
        public CoordinatorPanel()
        {
            InitializeComponent();
            DataController.StartTimerOnCurrentWindow(this.textBlock_DayXInfo, this.textBlock_DayXChanger);
        }

        private void logout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            mainWindow.Show();
            this.Close();
        }

        private void sponsors_Click(object sender, RoutedEventArgs e)
        {
            SponsorsReview sponsorsReview = new SponsorsReview();
            sponsorsReview.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            sponsorsReview.Show();
            this.Close();
        }

        private void racers_Click(object sender, RoutedEventArgs e)
        {
            ControlRacers controlRacers = new ControlRacers();
            controlRacers.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            controlRacers.Show();
            this.Close();
        }
    }
}
