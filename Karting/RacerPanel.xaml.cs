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
    public partial class RacerPanel : Window
    {
        public RacerPanel()
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

        private void showContact_Click(object sender, RoutedEventArgs e)
        {
            ContactCard contactCard = new ContactCard();
            contactCard.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            contactCard.Show();
        }

        private void registrationOnRace_Click(object sender, RoutedEventArgs e)
        {
            RegistrationOnRace registrationOnRace = new RegistrationOnRace();
            registrationOnRace.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            registrationOnRace.Show();
            this.Close();
        }

        private void editUser_Click(object sender, RoutedEventArgs e)
        {
            EditRacer editRacer = new EditRacer();
            editRacer.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            editRacer.Show();
            this.Close();
        }

        private void myRezult_Click(object sender, RoutedEventArgs e)
        {
            MyResults myResults = new MyResults();
            myResults.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            myResults.Show();
            this.Close();
        }

        private void mySponsor_Click(object sender, RoutedEventArgs e)
        {
            MySponsors mySponsors = new MySponsors();
            mySponsors.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            mySponsors.Show();
            this.Close();
        }
    }
}
