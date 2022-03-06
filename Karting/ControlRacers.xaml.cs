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
    public partial class ControlRacers : Window
    {
        public ControlRacers()
        {
            InitializeComponent();
            DataController.StartTimerOnCurrentWindow(this.textBlock_DayXInfo, this.textBlock_DayXChanger);
            InitComboStatus();
            InitComboRaceType();
            InitComboSort();
            InitRacersList();
        }

        private void go_back_Click(object sender, RoutedEventArgs e)
        {
            CoordinatorPanel coordinatorPanel = new CoordinatorPanel();
            coordinatorPanel.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            coordinatorPanel.Show();
            this.Close();
        }

        private void logout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            mainWindow.Show();
            this.Close();
        }

        private void InitComboStatus()
        {
            Registration_StatusTableAdapter registration_StatusTableAdapter = new Registration_StatusTableAdapter();
            Registration_StatusDataTable registration_StatusRows = new Registration_StatusDataTable();
            registration_StatusTableAdapter.Fill(registration_StatusRows);

            foreach(Registration_StatusRow row in registration_StatusRows)
            {
                this.c_status.Items.Add(row.Statu_Name);
            }
            this.c_status.Items.Add("Без фильтра");
        }

        private void InitComboRaceType()
        {
            Event_TypeTableAdapter event_TypeTableAdapter = new Event_TypeTableAdapter();
            Event_TypeDataTable event_TypeRows = new Event_TypeDataTable();
            event_TypeTableAdapter.Fill(event_TypeRows);

            foreach(Event_TypeRow row in event_TypeRows)
            {
                this.c_event_type.Items.Add(row.Event_Type_Name);
            }
            this.c_event_type.Items.Add("Без фильтра");
        }

        private void InitComboSort()
        {
            this.c_sort.Items.Add("Имя");
            this.c_sort.Items.Add("Фамилия");
            this.c_sort.Items.Add("Email");
            this.c_sort.Items.Add("Статус");
            this.c_sort.Items.Add("Без сортировки");
        }

        class RacerData
        {
            public string firstName { get; set; }
            public string lastName { get; set; }
            public string email { get; set; }
            public string status { get; set; }
        }

        private void InitRacersList(string sort = "", string eventTypeFilter = "", string statusFilter = "")
        {
            this.racersList.Items.Clear();

            Event_TypeTableAdapter event_TypeTableAdapter = new Event_TypeTableAdapter();
            Event_TypeDataTable event_TypeRows = new Event_TypeDataTable();
            event_TypeTableAdapter.Fill(event_TypeRows);

            Registration_StatusTableAdapter registration_StatusTableAdapter = new Registration_StatusTableAdapter();
            Registration_StatusDataTable registration_StatusRows = new Registration_StatusDataTable();
            registration_StatusTableAdapter.Fill(registration_StatusRows);

            RegistrationTableAdapter registrationTableAdapter = new RegistrationTableAdapter();
            RegistrationDataTable registrationRows = new RegistrationDataTable();
            registrationTableAdapter.Fill(registrationRows);

            EventTableAdapter eventTableAdapter = new EventTableAdapter();
            EventDataTable eventRows = new EventDataTable();
            eventTableAdapter.Fill(eventRows);

            RacerTableAdapter racerTableAdapter = new RacerTableAdapter();
            RacerDataTable racerRows = new RacerDataTable();
            racerTableAdapter.Fill(racerRows);

            RacersAdditionTableAdapter racersAdditionTableAdapter = new RacersAdditionTableAdapter();
            RacersAdditionDataTable racersAdditionRows = new RacersAdditionDataTable();
            racersAdditionTableAdapter.Fill(racersAdditionRows);

            List<RegistrationRow> registrationRowsList = registrationRows.ToList();

            if(eventTypeFilter != "" && eventTypeFilter != "Без фильтра")
            {
                string eventTypeId = event_TypeRows.Where(evT => evT.Event_Type_Name.Equals(eventTypeFilter)).FirstOrDefault().ID_Event_type;
                int eventId = eventRows.Where(ev => ev.ID_EventType.Equals(eventTypeId)).FirstOrDefault().ID_Event;
                registrationRowsList = registrationRowsList.Where(rR => rR.ID_Charity.Equals(eventId)).ToList();
            }

            if(statusFilter != "" && statusFilter != "Без фильтра")
            {
                int registrationStatusId = registration_StatusRows.Where(rS => rS.Statu_Name.Equals(statusFilter)).FirstOrDefault().ID_Registration_Status;
                registrationRowsList = registrationRowsList.Where(rR => rR.ID_Registration_Status.Equals(registrationStatusId)).ToList();
            }

            List<RacerData> racerDataList = new List<RacerData>();

            foreach(RegistrationRow row in registrationRowsList)
            {
                RacerData racerData = new RacerData();

                RacerRow racer = racerRows.Where(racerR => racerR.ID_Racer.Equals(row.ID_Racer)).FirstOrDefault();

                racerData.firstName = racer.First_Name;
                racerData.lastName = racer.Last_Name;

                string email = racersAdditionRows.Where(rAR => rAR.RacerId.Equals(racer.ID_Racer)).FirstOrDefault().UserEmail;

                racerData.email = email;

                string status = registration_StatusRows.FindByID_Registration_Status(row.ID_Registration_Status).Statu_Name;
                racerData.status = status;

                racerDataList.Add(racerData);
            }

            if(sort != "" && sort != "Без сортировки")
            {
                switch (sort)
                {
                    case "Имя":
                        racerDataList = racerDataList.OrderBy(rDL => rDL.firstName).ToList();
                        break;
                    case "Фамилия":
                        racerDataList = racerDataList.OrderBy(rDL => rDL.lastName).ToList();
                        break;
                    case "Email":
                        racerDataList = racerDataList.OrderBy(rDL => rDL.email).ToList();
                        break;
                    case "Статус":
                        racerDataList = racerDataList.OrderBy(rDL => rDL.status).ToList();
                        break;
                }
            }

            foreach(RacerData data in racerDataList)
            {
                this.racersList.Items.Add(data);
            }

        }

        private void reload_Click(object sender, RoutedEventArgs e)
        {
            string sort = "";
            string eventTypeFilter = "";
            string statusFilter = "";
            
            if(this.c_sort.SelectedItem != null)
            {
                sort = this.c_sort.SelectedItem.ToString();
            }
            if(this.c_event_type.SelectedItem != null)
            {
                eventTypeFilter = this.c_event_type.SelectedItem.ToString();
            }
            if (this.c_status.SelectedItem != null)
            {
                statusFilter = this.c_status.SelectedItem.ToString();
            }
            InitRacersList(sort, eventTypeFilter, statusFilter);
        }
    }
}
