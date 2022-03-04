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
    public partial class AdminPanel : Window
    {
        public AdminPanel()
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

        private void users_Click(object sender, RoutedEventArgs e)
        {
            ControlUsers controlUsers = new ControlUsers();
            controlUsers.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            controlUsers.Show();
            this.Close();
        }

        private void charities_Click(object sender, RoutedEventArgs e)
        {
            ControlCharity controlCharity = new ControlCharity();
            controlCharity.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            controlCharity.Show();
            this.Close();
        }

        private void volunteers_Click(object sender, RoutedEventArgs e)
        {
            ControlVolunteer controlVolunteer = new ControlVolunteer();
            controlVolunteer.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            controlVolunteer.Show();
            this.Close();
        }

        private void inventory_Click(object sender, RoutedEventArgs e)
        {
            Inventory inventory = new Inventory();
            inventory.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            inventory.Show();
            this.Close();
        }
    }
}
