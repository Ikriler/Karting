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
    public partial class CoordinatorEditRacer : Window
    {
        string currentEditRacerEmail = "";
        public CoordinatorEditRacer(string email = "")
        {
            InitializeComponent();
            DataController.StartTimerOnCurrentWindow(this.textBlock_DayXInfo, this.textBlock_DayXChanger);
            currentEditRacerEmail = email;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            BeforeEditRacer beforeEditRacer = new BeforeEditRacer(currentEditRacerEmail);
            beforeEditRacer.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            beforeEditRacer.Show();
            this.Close();
        }
    }
}
