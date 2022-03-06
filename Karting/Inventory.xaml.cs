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
    public partial class Inventory : Window
    {
        public Inventory()
        {
            InitializeComponent();
            DataController.StartTimerOnCurrentWindow(this.textBlock_DayXInfo, this.textBlock_DayXChanger);
            InitListView();
        }

        class ForInitList
        {
            public int TypeACount { get; set; }
            public int TypeBCount { get; set; }
            public int TypeCCount { get; set; }
            public int AllNeedNumber { get; set; }
            public int RestNumber { get; set; }
            public int NeedNumber { get; set; }
            public int AllNeedBraslet { get; set; }
            public int RestBraslet { get; set; }
            public int NeedBraslet { get; set; }
            public int AllNeedHelm { get; set; }
            public int RestHelm { get; set; }
            public int NeedHelm { get; set; }
            public int AllNeedEquip { get; set; }
            public int RestEquip { get; set; }
            public int NeedEquip { get; set; }

        }

        private void InitListView()
        {
            RacerTableAdapter racerTableAdapter = new RacerTableAdapter();
            RacerDataTable racerRows = new RacerDataTable();
            racerTableAdapter.Fill(racerRows);
            this.t_pilots.Text = "Всего пилотов: " + racerRows.Count();

            ForInitList forInitList = new ForInitList();

            complectsTableAdapter complects = new complectsTableAdapter();
            complectsDataTable complectsRows = new complectsDataTable();
            complects.Fill(complectsRows);

            forInitList.TypeACount = complectsRows.FindBycomplectsId("TypeACount").complectsNumber;
            forInitList.TypeBCount = complectsRows.FindBycomplectsId("TypeBCount").complectsNumber;
            forInitList.TypeCCount = complectsRows.FindBycomplectsId("TypeCCount").complectsNumber;


            //number
            forInitList.AllNeedNumber = forInitList.TypeACount + forInitList.TypeBCount + forInitList.TypeCCount;

            forInitList.RestNumber = complectsRows.FindBycomplectsId("HaveNumber").complectsNumber;

            if(forInitList.AllNeedNumber - forInitList.RestNumber < 0)
            {
                forInitList.NeedNumber = 0;
            }
            else
            {
                forInitList.NeedNumber = forInitList.AllNeedNumber - forInitList.RestNumber;
            }

            //braslet
            forInitList.AllNeedBraslet = forInitList.TypeACount + forInitList.TypeBCount + forInitList.TypeCCount;

            forInitList.RestBraslet = complectsRows.FindBycomplectsId("HaveBraslet").complectsNumber;

            if (forInitList.AllNeedBraslet - forInitList.RestBraslet < 0)
            {
                forInitList.NeedBraslet = 0;
            }
            else
            {
                forInitList.NeedBraslet = forInitList.AllNeedBraslet - forInitList.RestBraslet;
            }

            //helm
            forInitList.AllNeedHelm = forInitList.TypeBCount + forInitList.TypeCCount;

            forInitList.RestHelm = complectsRows.FindBycomplectsId("HaveHelm").complectsNumber;

            if (forInitList.AllNeedHelm - forInitList.RestHelm < 0)
            {
                forInitList.NeedHelm = 0;
            }
            else
            {
                forInitList.NeedHelm = forInitList.AllNeedHelm - forInitList.RestHelm;
            }

            //equip
            forInitList.AllNeedEquip = forInitList.TypeCCount;

            forInitList.RestEquip = complectsRows.FindBycomplectsId("HaveEquip").complectsNumber;

            if (forInitList.AllNeedEquip - forInitList.RestEquip < 0)
            {
                forInitList.NeedEquip = 0;
            }
            else
            {
                forInitList.NeedEquip = forInitList.AllNeedEquip - forInitList.RestEquip;
            }

            this.listView.Items.Add(forInitList);
        }

        private void go_back_Click(object sender, RoutedEventArgs e)
        {
            AdminPanel adminPanel = new AdminPanel();
            adminPanel.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            adminPanel.Show();
            this.Close();
        }

        private void printReport_Click(object sender, RoutedEventArgs e)
        {
            ReportPrinting reportPrinting = new ReportPrinting();
            reportPrinting.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            reportPrinting.Show();
            this.Close();
        }

        private void receipt_Click(object sender, RoutedEventArgs e)
        {
            InventoryReceipt inventoryReceipt = new InventoryReceipt();
            inventoryReceipt.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            inventoryReceipt.Show();
            this.Close();
        }
    }
}
