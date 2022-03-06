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
using Karting.DataSetTableAdapters;
using static Karting.DataSet;

namespace Karting
{
    public partial class InventoryReceipt : Window
    {
        public InventoryReceipt()
        {
            InitializeComponent();
            DataController.StartTimerOnCurrentWindow(this.textBlock_DayXInfo, this.textBlock_DayXChanger);
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            Inventory inventory = new Inventory();
            inventory.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            inventory.Show();
            this.Close();
        }

        private void t_KeyDown(object sender, KeyEventArgs e)
        {
            string number = e.Key.ToString();
            string pattern = "D1D2D3D4D5D6D7D8D9D0";
            if (!pattern.Contains(number) || e.Key == Key.D || (sender as TextBox).Text.Length > 8)
            {
                e.Handled = true;
            }
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            complectsTableAdapter complects = new complectsTableAdapter();
            complectsDataTable complectsRows = new complectsDataTable();
            complects.Fill(complectsRows);

            int oldAmount = 0;
            if (this.t_number.Text.Length != 0)
            {
                oldAmount = complectsRows.FindBycomplectsId("HaveNumber").complectsNumber;
                int newAmountPlus = Convert.ToInt32(this.t_number.Text);
                complects.ComplectUpdateQuery("HaveNumber", oldAmount + newAmountPlus, "HaveNumber");
            }
            if (this.t_braslet.Text.Length != 0)
            {
                oldAmount = complectsRows.FindBycomplectsId("HaveBraslet").complectsNumber;
                int newAmountPlus = Convert.ToInt32(this.t_braslet.Text);
                complects.ComplectUpdateQuery("HaveBraslet", oldAmount + newAmountPlus, "HaveBraslet");
            }
            if (this.t_helm.Text.Length != 0)
            {
                oldAmount = complectsRows.FindBycomplectsId("HaveHelm").complectsNumber;
                int newAmountPlus = Convert.ToInt32(this.t_helm.Text);
                complects.ComplectUpdateQuery("HaveHelm", oldAmount + newAmountPlus, "HaveHelm");
            }
            if (this.t_equip.Text.Length != 0)
            {
                oldAmount = complectsRows.FindBycomplectsId("HaveEquip").complectsNumber;
                int newAmountPlus = Convert.ToInt32(this.t_equip.Text);
                complects.ComplectUpdateQuery("HaveEquip", oldAmount + newAmountPlus, "HaveEquip");
            }

            Inventory inventory = new Inventory();
            inventory.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            inventory.Show();
            this.Close();
        }
    }
}
