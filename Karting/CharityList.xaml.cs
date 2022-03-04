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
    public partial class CharityList : Window
    {

        struct CharityData 
        {
            public string ImagePath { get; set; }
            public string CharityName { get; set; }
            public string CharityDescription { get; set; }
        }
        public CharityList()
        {
            InitializeComponent();
            DataController.StartTimerOnCurrentWindow(this.textBlock_DayXInfo, this.textBlock_DayXChanger);
            InitCharityList();
        }

        private void go_back_Click(object sender, RoutedEventArgs e)
        {
            InfoMainWindow infoMainWindow = new InfoMainWindow();
            infoMainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            infoMainWindow.Show();
            this.Close();
        }

        private void InitCharityList() 
        {
            CharityTableAdapter charityTableAdapter = new CharityTableAdapter();
            CharityDataTable charityRows = new CharityDataTable();
            charityTableAdapter.Fill(charityRows);

            foreach(CharityRow charity in charityRows)
            {
                CharityData charityData = new CharityData();
                charityData.ImagePath = MainController.IconPath + charity.Charity_Logo;
                charityData.CharityName = charity.Charity_Name;
                charityData.CharityDescription = charity.Charity_Description;

                this.listView.Items.Add(charityData);
            }
        }
    }
}
