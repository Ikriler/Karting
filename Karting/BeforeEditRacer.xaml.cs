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
    public partial class BeforeEditRacer : Window
    {
        public string currnetBeforeEditRacerEmail;
        public BeforeEditRacer(string email = "")
        {
            InitializeComponent();
            DataController.StartTimerOnCurrentWindow(this.textBlock_DayXInfo, this.textBlock_DayXChanger);
            currnetBeforeEditRacerEmail = email;
            InitFields();
        }

        private void InitFields()
        {
            if (currnetBeforeEditRacerEmail == "") return;

            this.t_email.Text = currnetBeforeEditRacerEmail;

            RacersAdditionTableAdapter racersAdditionTableAdapter = new RacersAdditionTableAdapter();
            RacersAdditionDataTable racersAdditionRows = new RacersAdditionDataTable();
            racersAdditionTableAdapter.Fill(racersAdditionRows);

            RacerTableAdapter racerTableAdapter = new RacerTableAdapter();
            RacerDataTable racerRows = new RacerDataTable();
            racerTableAdapter.Fill(racerRows);

            int racerId = racersAdditionRows.Where(rA => rA.UserEmail.Equals(currnetBeforeEditRacerEmail)).FirstOrDefault().RacerId;
            RacerRow racerRow = racerRows.FindByID_Racer(racerId);

            this.t_first_name.Text = racerRow.First_Name;
            this.t_last_name.Text = racerRow.Last_Name;

            GenderTableAdapter genderTableAdapter = new GenderTableAdapter();
            GenderDataTable genderRows = new GenderDataTable();
            genderTableAdapter.Fill(genderRows);

            string gender = genderRows.FindByID_Gender(racerRow.Gender).Gender_Name;

            this.t_gender.Text = gender;

            this.t_datebd.Text = racerRow.DateOfBirth.ToString().Split(' ')[0];

            CharityTableAdapter charityTableAdapter = new CharityTableAdapter();
            CharityDataTable charityRows = new CharityDataTable();
            charityTableAdapter.Fill(charityRows);

            RegistrationTableAdapter registrationTableAdapter = new RegistrationTableAdapter();
            RegistrationDataTable registrationRows = new RegistrationDataTable();
            registrationTableAdapter.Fill(registrationRows);

            RegistrationRow registrationRow = registrationRows.Where(rr => rr.ID_Racer.Equals(racerRow.ID_Racer)).LastOrDefault();

            int charityId = registrationRow.ID_Charity;
            CharityRow charity = charityRows.FindByID_Сharity(charityId);

            this.t_cahrity.Text = charity.Charity_Name;

            RacerSponsorConnectorTableAdapter racerSponsorConnectorTableAdapter = new RacerSponsorConnectorTableAdapter();
            RacerSponsorConnectorDataTable racerSponsorConnectorRows = new RacerSponsorConnectorDataTable();
            racerSponsorConnectorTableAdapter.Fill(racerSponsorConnectorRows);

            List<RacerSponsorConnectorRow> racerSponsorConnectorRowsList = racerSponsorConnectorRows.Where(rsc => rsc.CharityId.Equals(charityId) && rsc.RacerId.Equals(racerId)).ToList();

            SponsorshipTableAdapter sponsorshipTableAdapter = new SponsorshipTableAdapter();
            SponsorshipDataTable sponsorshipRows = new SponsorshipDataTable();
            sponsorshipTableAdapter.Fill(sponsorshipRows);

            decimal amount = 0;

            foreach(RacerSponsorConnectorRow row in racerSponsorConnectorRowsList)
            {
                SponsorshipRow sponsorship = sponsorshipRows.FindByID_Sponsorship(row.SponsorId);
                amount += sponsorship.Amount;
            }

            this.t_charity_amount.Text = "$ " + amount;


            Registration_StatusTableAdapter registration_StatusTableAdapter = new Registration_StatusTableAdapter();
            Registration_StatusDataTable registration_StatusRows = new Registration_StatusDataTable();
            registration_StatusTableAdapter.Fill(registration_StatusRows);

            string registrationStatusName = registration_StatusRows.FindByID_Registration_Status(registrationRow.ID_Registration_Status).Statu_Name;

            this.t_registration_status.Text = registrationStatusName;

            string photoPath = racersAdditionRows.Where(rA => rA.UserEmail.Equals(currnetBeforeEditRacerEmail)).FirstOrDefault().PhotoPath;

            try
            {
                var uriImageSource = new Uri(MainController.IconPath + photoPath, UriKind.RelativeOrAbsolute);
                ImageSource image = new BitmapImage(uriImageSource);
                this.photo_img.Source = image;
            }
            catch
            {

            }

            CountryTableAdapter countryTableAdapter = new CountryTableAdapter();
            CountryDataTable countryRows = new CountryDataTable();
            countryTableAdapter.Fill(countryRows);

            string countryName = countryRows.FindByID_Country(racerRow.ID_Country).Country_Name;

            this.t_country.Text = countryName;

            EventTableAdapter eventTableAdapter = new EventTableAdapter();
            EventDataTable eventRows = new EventDataTable();
            eventTableAdapter.Fill(eventRows);

            Event_TypeTableAdapter event_TypeTableAdapter = new Event_TypeTableAdapter();
            Event_TypeDataTable event_TypeRows = new Event_TypeDataTable();
            event_TypeTableAdapter.Fill(event_TypeRows);

            ResultTableAdapter resultTableAdapter = new ResultTableAdapter();
            ResultDataTable resultRows = new ResultDataTable();
            resultTableAdapter.Fill(resultRows);

            try
            {
                ResultRow resultRow = resultRows.Where(res => res.ID_Registration.Equals(registrationRow.ID_Registration)).LastOrDefault();
                if (resultRow != null)
                {
                    EventRow eventRow = eventRows.FindByID_Event(resultRow.ID_Event);
                    Event_TypeRow event_TypeRow = event_TypeRows.FindByID_Event_type(eventRow.ID_EventType);
                    this.t_raceType.Text = event_TypeRow.Event_Type_Name;
                }
                else
                {
                    this.t_raceType.Text = "Пользователь еще не участвовал в гонке.";
                }
            }
            catch
            {
                this.t_raceType.Text = "Пользователь еще не участвовал в гонке.";
            }

        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            ControlRacers controlRacers = new ControlRacers();
            controlRacers.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            controlRacers.Show();
            this.Close();
        }

        private void logout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            mainWindow.Show();
            this.Close();
        }

        private void coordEditRacer_Click(object sender, RoutedEventArgs e)
        {
            CoordinatorEditRacer coordinatorEditRacer = new CoordinatorEditRacer(this.t_email.Text);
            coordinatorEditRacer.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            coordinatorEditRacer.Show();
            this.Close();
        }
    }
}
