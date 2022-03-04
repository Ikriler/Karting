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
    public partial class InfoMainWindow : Window
    {
        public InfoMainWindow()
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

        private void kartSkills2017_Click(object sender, RoutedEventArgs e)
        {
            InfoKartSkills infoKartSkills = new InfoKartSkills();
            infoKartSkills.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            infoKartSkills.Show();
            this.Close();
        }

        private void SpisokOrganiztion_Click(object sender, RoutedEventArgs e)
        {
            CharityList charityList = new CharityList();
            charityList.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            charityList.Show();
            this.Close();
        }

        private void oldResults_Click(object sender, RoutedEventArgs e)
        {
            AllOldResults allOldResults = new AllOldResults();
            allOldResults.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            allOldResults.Show();
            this.Close();
        }
    }
}
