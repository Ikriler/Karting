using CsvHelper;
using CsvHelper.Configuration;
using CsvHelper.Configuration.Attributes;
using Microsoft.Win32;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
    public partial class VolunteerLoading : Window
    {
        public VolunteerLoading()
        {
            InitializeComponent();
            DataController.StartTimerOnCurrentWindow(this.textBlock_DayXInfo, this.textBlock_DayXChanger);
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            ControlVolunteer controlVolunteer = new ControlVolunteer();
            controlVolunteer.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            controlVolunteer.Show();
            this.Close();
        }

        private void b_check_csv_file_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog csv = new OpenFileDialog();
            csv.Filter = "CSV|*.csv";
            if (csv.ShowDialog() == true)
            {
                this.t_csv_file_path.Text = csv.FileName;
            }
        }

        class VolunteerLoadData
        {
            [Name("VolunteerId")]
            public string VolunteerId { get; set; }
            [Name("FirstName")]
            public string FirstName { get; set; }
            [Name("LastName")]
            public string LastName { get; set; }
            [Name("CountryCode")]
            public string CountryCode { get; set; }
            [Name("Gender")]
            public string Gender { get; set; }
        }

        private void loadVolunteers_Click(object sender, RoutedEventArgs e)
        {
            if(this.t_csv_file_path.Text == "")
            {
                MessageBox.Show("Сначала выберите файл.");
                return;
            }
            VolunteerTableAdapter volunteerTableAdapter = new VolunteerTableAdapter();
            VolunteerDataTable volunteerRows = new VolunteerDataTable();
            volunteerTableAdapter.Fill(volunteerRows);
            try
            {
                string csvPath = this.t_csv_file_path.Text;
                using (StreamReader streamReader = new StreamReader(csvPath))
                {
                    using (CsvReader csvReader = new CsvReader(streamReader, System.Globalization.CultureInfo.InvariantCulture))
                    {
                        IEnumerable Volunteers = csvReader.GetRecords<VolunteerLoadData>();
                        foreach (VolunteerLoadData volunteer in Volunteers)
                        {
                            VolunteerRow checkExistsVolunteerById = volunteerRows.FindByID_Volunteer(volunteer.VolunteerId);
                            if(checkExistsVolunteerById == null)
                            {
                                volunteerTableAdapter.Insert(volunteer.VolunteerId, volunteer.FirstName, volunteer.LastName, volunteer.CountryCode, volunteer.Gender);
                            }
                        }
                        ControlVolunteer controlVolunteer = new ControlVolunteer();
                        controlVolunteer.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                        controlVolunteer.Show();
                        this.Close();
                    }
                }
            }
            catch
            {
                MessageBox.Show("Что-то пошло не так.");
                return;
            }
        }
    }
}
