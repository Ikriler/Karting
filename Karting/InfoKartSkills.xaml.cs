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
    public partial class InfoKartSkills : Window
    {
        public InfoKartSkills()
        {
            InitializeComponent();
            DataController.StartTimerOnCurrentWindow(this.textBlock_DayXInfo, this.textBlock_DayXChanger);
        }

        private void go_back_Click(object sender, RoutedEventArgs e)
        {
            InfoMainWindow infoMainWindow = new InfoMainWindow();
            infoMainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            infoMainWindow.Show();
            this.Close();
        }

        private void map_Click(object sender, RoutedEventArgs e)
        {
            Map map = new Map();
            map.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            map.Show();
            this.Close();
        }

    }
}
