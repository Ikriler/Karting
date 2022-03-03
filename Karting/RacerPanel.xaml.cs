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
    /// Логика взаимодействия для RacerPanel.xaml
    /// </summary>
    public partial class RacerPanel : Window
    {
        public RacerPanel()
        {
            InitializeComponent();
            DataController.StartTimerOnCurrentWindow(this.textBlock_DayXInfo, this.textBlock_DayXChanger);
        }

        private void logout_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

        private void showContact_Click(object sender, RoutedEventArgs e)
        {
            string message = String.Format("Email: {0}", MainController.currentUser.Email);
            MessageBox.Show(message, "Контакты");
        }

        private void registrationOnRace_Click(object sender, RoutedEventArgs e)
        {
            new RegistrationOnRace().Show();
            this.Close();
        }
    }
}
