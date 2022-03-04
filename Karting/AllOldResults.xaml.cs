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
            InitTInfo();
            InitComboEvent();
            InitComboCategory();
            youRacer = racer;
        }

        private void InitTInfo()
        {
            RacerTableAdapter racerTableAdapter = new RacerTableAdapter();
            RacerDataTable racerRows = new RacerDataTable();
            racerTableAdapter.Fill(racerRows);

            int racerCount = racerRows.Count();

            ResultTableAdapter resultTableAdapter = new ResultTableAdapter();
            ResultDataTable resultRows = new ResultDataTable();
            resultTableAdapter.Fill(resultRows);

            TimeSpan commonTime = new TimeSpan();
            List<int> registrationIdList = new List<int>();

            foreach (ResultRow result in resultRows)
            {
                commonTime += result.RaceTime;
                if(!registrationIdList.Contains(result.ID_Registration))
                {
                    registrationIdList.Add(result.ID_Registration);
                }
            }

            int allFinished = registrationIdList.Count();

            TimeSpan middleTime = new TimeSpan(commonTime.Ticks / resultRows.Count());

            this.t_info.Text = String.Format("Всего пилотов: {0} Всего пилотов финишировало: {1} Среднее время: {2}m {3}s", racerCount, allFinished, middleTime.Minutes, middleTime.Seconds);

        }

        private void InitComboCategory()
        {
            this.c_category.ItemsSource = MainController.AgeCategoryList;
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

            foreach (GenderRow gender in genderRows)
            {
                this.c_gender.Items.Add(gender.Gender_Name);
            }
            this.c_gender.Items.Add("Any");
        }

        class EventData
        {
            public string YearEvent { get; set; }
            public string EventName { get; set; }
            public int EventId { get; set; }
        }
        private void InitComboEvent()
        {
            EventTableAdapter eventTableAdapter = new EventTableAdapter();
            EventDataTable eventRows = new EventDataTable();
            eventTableAdapter.Fill(eventRows);

            foreach (EventRow eventRow in eventRows)
            {
                EventData eventData = new EventData();
                DateTime date = eventRow.StartDateTime;
                
                eventData.YearEvent = date.Year.ToString();
                eventData.EventName = eventRow.Event_Name;
                eventData.EventId = eventRow.ID_Event;

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

        class RaceData
        {
            public string racerName { get; set; }
            public int Place { get; set; }
            public string timeRace { get; set; }
            public string CountryName { get; set; }

        }


        private void search_Click(object sender, RoutedEventArgs e)
        {
            this.raceList.Items.Clear();

            string eventName = "";
            string eventType = "";

            EventTableAdapter eventTableAdapter = new EventTableAdapter();
            EventDataTable eventRows = new EventDataTable();
            eventTableAdapter.Fill(eventRows);

            if (this.с_event.SelectedItem != null)
            {
                eventName = eventRows.Where(evn => evn.ID_Event.Equals((this.с_event.SelectedItem as EventData).EventId)).FirstOrDefault().Event_Name;
                eventType = this.t_event_type.Content.ToString();
            }
            else
            {
                MessageBox.Show("Необходимо выбрать событие.");
                return;
            }

            string gender = "";
            if (this.c_gender.SelectedItem != null)
            {
                gender = this.c_gender.SelectedItem.ToString();
            }
            else
            {
                MessageBox.Show("Необходимо выбрать пол.");
                return;
            }

            int category = -1;
            if (this.c_category.SelectedItem != null)
            {
                category = this.c_category.SelectedIndex;
            }

            GenderTableAdapter genderTableAdapter = new GenderTableAdapter();
            GenderDataTable genderRows = new GenderDataTable();
            genderTableAdapter.Fill(genderRows);

            OldResultsTableAdapter oldResultsTableAdapter = new OldResultsTableAdapter();
            OldResultsDataTable oldResultsRows = new OldResultsDataTable();


            if (gender.Equals("Any") || gender == "")
            {
                oldResultsTableAdapter.FillByEventName(oldResultsRows, eventName);
            }
            else
            {
                string genderId = genderRows.Where(g => g.Gender_Name.Equals(gender)).FirstOrDefault().ID_Gender;
                oldResultsTableAdapter.FillByEventNameAndGender(oldResultsRows, eventName, genderId);
            }

            if (category == -1)
            {
                foreach (OldResultsRow data in oldResultsRows)
                {
                    RaceData raceData = new RaceData();
                    raceData.Place = data.BidNumber;
                    raceData.racerName = data.First_Name + " " + data.Last_Name;
                    raceData.timeRace = data.RaceTime.ToString();
                    raceData.CountryName = data.Country_Name;
                    this.raceList.Items.Add(raceData);
                }
            }
            else if (category != -1)
            {
                foreach (OldResultsRow data in oldResultsRows)
                {
                    int age = Convert.ToInt32((DateTime.Today.Subtract(data.DateOfBirth)).Days / 365);
                    if (MainController.RacerInCategory(age, category)) 
                    {
                        RaceData raceData = new RaceData();
                        raceData.Place = data.BidNumber;
                        raceData.racerName = data.First_Name + " " + data.Last_Name;
                        raceData.timeRace = data.RaceTime.ToString();
                        raceData.CountryName = data.Country_Name;
                        this.raceList.Items.Add(raceData);
                    }
                }
            }

            Application.Current.Dispatcher.InvokeAsync(async () =>
            {
                AlarmWindow alarm = new AlarmWindow("Поиск");
                alarm.Show();
                await Task.Delay(1000);
                alarm.Close();
            });

        }
    }
}
