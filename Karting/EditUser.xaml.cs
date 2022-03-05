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
    public partial class EditUser : Window
    {
        public EditUser(string userEmail = "")
        {
            InitializeComponent();
            DataController.StartTimerOnCurrentWindow(this.textBlock_DayXInfo, this.textBlock_DayXChanger);
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            ControlUsers controlUsers = new ControlUsers();
            controlUsers.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            controlUsers.Show();
            this.Close();
        }
    }
}
