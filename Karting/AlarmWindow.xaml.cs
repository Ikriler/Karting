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
    public partial class AlarmWindow : Window
    {
        public AlarmWindow(string message)
        {
            InitializeComponent();
            this.inforamtion.Content = message;

            Application.Current.Dispatcher.InvokeAsync(async () =>
            {
                for(int i = 0; i < 3; i++)
                {
                    this.inforamtion.Content += ".";
                    await Task.Delay(300);
                }
            });
        }


    }
}
