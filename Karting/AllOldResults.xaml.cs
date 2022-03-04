using System;
using System.Collections.Generic;
using System.Data;
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
    public partial class AllOldResults : Window
    {
        bool youRacer = false;
        public AllOldResults(bool racer = false)
        {
            InitializeComponent();
            DataController.StartTimerOnCurrentWindow(this.textBlock_DayXInfo, this.textBlock_DayXChanger);
            InitComboGender();
            InitComboEvent();
            youRacer = racer;
        }

        private void go_back_Click(object sender, RoutedEventArgs e)
        {
            if (!youRacer)
            {
                InfoMainWindow infoMainWindow = new InfoMainWindow();
                infoMainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                infoMainWindow.Show();
                this.Close();
            }
            else 
            {
                MyResults myResults = new MyResults();
                myResults.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                myResults.Show();
                this.Close();
            }
        }

        private void InitComboGender()
        {
            GenderTableAdapter genderTableAdapter = new GenderTableAdapter();
            GenderDataTable genderRows = new GenderDataTable();
            genderTableAdapter.Fill(genderRows);

            foreach(GenderRow gender in genderRows)
            {
                this.c_gender.Items.Add(gender.Gender_Name);
            }
            this.c_gender.Items.Add("Any");
        }

        class EventData
        {
            public string YearEvent { get; set; }
            public string EventName { get; set; }
        }
        private void InitComboEvent()
        {
            EventTableAdapter eventTableAdapter = new EventTableAdapter();
            EventDataTable eventRows = new EventDataTable();
            eventTableAdapter.Fill(eventRows);

            foreach(EventRow eventRow in eventRows)
            {
                EventData eventData = new EventData();
                DateTime date = eventRow.StartDateTime;
                eventData.YearEvent = date.Year.ToString();
                eventData.EventName = eventRow.Event_Name;

                this.с_event.Items.Add(eventData);
            }

        }

        private void с_event_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            EventTableAdapter eventTableAdapter = new EventTableAdapter();
            EventDataTable eventRows = new EventDataTable();
            eventTableAdapter.Fill(eventRows);

            EventData eventView = null;
            if (this.с_event.SelectedItem != null)
            {
                eventView = this.с_event.SelectedItem as EventData;
            }
            else
            {
                return;
            }

            if (eventView != null) 
            {
                string eventName = eventView.EventName;
                EventRow eventRow = eventRows.Where(ev => ev.Event_Name == eventName).FirstOrDefault();

                this.t_event_type.Content = "Race " + eventRow.ID_EventType;
            }
        }
    }
}
