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
    public partial class ControlCharity : Window
    {
        public ControlCharity()
        {
            InitializeComponent();
            DataController.StartTimerOnCurrentWindow(this.textBlock_DayXInfo, this.textBlock_DayXChanger);
            InitCharityList();
        }

        class CharityData
        {
            public string ImagePath { get; set; }
            public string CharityName { get; set; }
            public string CharityDescription { get; set; }
        }

        private void InitCharityList()
        {
            CharityTableAdapter charityTableAdapter = new CharityTableAdapter();
            CharityDataTable charityRows = new CharityDataTable();
            charityTableAdapter.Fill(charityRows);

            foreach(CharityRow row in charityRows)
            {
                CharityData charityData = new CharityData();
                charityData.ImagePath = MainController.IconPath + row.Charity_Logo;
                charityData.CharityName = row.Charity_Name;
                charityData.CharityDescription = row.Charity_Description;
                this.charityList.Items.Add(charityData);
            }
        }

        private void go_back_Click(object sender, RoutedEventArgs e)
        {
            AdminPanel adminPanel = new AdminPanel();
            adminPanel.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            adminPanel.Show();
            this.Close();
        }

        private void logout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            mainWindow.Show();
            this.Close();
        }

        private void addNewCharity_Click(object sender, RoutedEventArgs e)
        {
            AddOrEditCharity addOrEditCharity = new AddOrEditCharity();
            addOrEditCharity.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            addOrEditCharity.Show();
            this.Close();
        }

        private void b_edit_Click(object sender, RoutedEventArgs e)
        {
            string charityName = (sender as Button).Tag.ToString();
            AddOrEditCharity addOrEditCharity = new AddOrEditCharity(charityName);
            addOrEditCharity.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            addOrEditCharity.Show();
            this.Close();
        }
    }
}
