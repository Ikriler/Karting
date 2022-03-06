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
    public partial class ControlVolunteer : Window
    {
        public ControlVolunteer()
        {
            InitializeComponent();
            DataController.StartTimerOnCurrentWindow(this.textBlock_DayXInfo, this.textBlock_DayXChanger);
            InitVolunteerList();
            InitComboSort();
        }

        class VolunteerData
        {
            public string lastName { get; set; }
            public string firstName { get; set; }
            public string countryName { get; set; }
            public string genderName { get; set; }
        }

        private void InitComboSort()
        {
            this.c_sort.Items.Add("Фамилия");
            this.c_sort.Items.Add("Имя");
            this.c_sort.Items.Add("Страна");
            this.c_sort.Items.Add("Пол");
            this.c_sort.Items.Add("Без сортировки");
        }

        private void InitVolunteerList(string sort = "")
        {
            this.volunteerList.Items.Clear();

            VolunteerTableAdapter volunteerTableAdapter = new VolunteerTableAdapter();
            VolunteerDataTable volunteerRows = new VolunteerDataTable();
            volunteerTableAdapter.Fill(volunteerRows);

            GenderTableAdapter genderTableAdapter = new GenderTableAdapter();
            GenderDataTable genderRows = new GenderDataTable();
            genderTableAdapter.Fill(genderRows);

            CountryTableAdapter countryTableAdapter = new CountryTableAdapter();
            CountryDataTable countryRows = new CountryDataTable();
            countryTableAdapter.Fill(countryRows);


            List<VolunteerRow> volunteerRowsList = volunteerRows.ToList();
            if (sort != "" && sort != "Без сортировки")
            {
                switch (sort)
                {
                    case "Фамилия":
                        volunteerRowsList = volunteerRowsList.OrderBy(vF => vF.Last_Name).ToList();
                        break;
                    case "Имя":
                        volunteerRowsList = volunteerRowsList.OrderBy(vN => vN.First_Name).ToList();
                        break;
                    case "Страна":
                        volunteerRowsList = volunteerRowsList.OrderBy(vC => vC.ID_Country).ToList();
                        break;
                    case "Пол":
                        volunteerRowsList = volunteerRowsList.OrderBy(vG => vG.Gender_ID).ToList();
                        break;
                }
            }

            foreach (VolunteerRow row in volunteerRowsList)
            {
                VolunteerData volunteerData = new VolunteerData();
                volunteerData.firstName = row.First_Name;
                volunteerData.lastName = row.Last_Name;

                string countryName = countryRows.Where(c => c.ID_Country.Equals(row.ID_Country)).FirstOrDefault().Country_Name;
                volunteerData.countryName = countryName;

                string genderName = genderRows.Where(g => g.ID_Gender.Equals(row.Gender_ID)).FirstOrDefault().Gender_Name;
                volunteerData.genderName = genderName;

                this.volunteerList.Items.Add(volunteerData);
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

        private void loadVolunteers_Click(object sender, RoutedEventArgs e)
        {
            VolunteerLoading volunteerLoading = new VolunteerLoading();
            volunteerLoading.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            volunteerLoading.Show();
            this.Close();
        }

        private void b_sort_Click(object sender, RoutedEventArgs e)
        {
            string sort = "";
            if(this.c_sort.SelectedItem != null)
            {
                sort = this.c_sort.SelectedItem.ToString();
            }
            InitVolunteerList(sort);
        }
    }
}
