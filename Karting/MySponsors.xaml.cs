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
    public partial class MySponsors : Window
    {
        decimal commonAmount = 0;
        public MySponsors()
        {
            InitializeComponent();
            DataController.StartTimerOnCurrentWindow(this.textBlock_DayXInfo, this.textBlock_DayXChanger);
            InitSponsorList();
            InitCharityInfo();
        }

        private void InitSponsorList()
        {
            RacerSponsorConnectorTableAdapter racerSponsorConnectorTableAdapter = new RacerSponsorConnectorTableAdapter();
            RacerSponsorConnectorDataTable racerSponsorConnectorRows = new RacerSponsorConnectorDataTable();
            racerSponsorConnectorTableAdapter.Fill(racerSponsorConnectorRows);

            RacersAdditionTableAdapter racersAdditionTableAdapter = new RacersAdditionTableAdapter();
            RacersAdditionDataTable racersAdditionRows = new RacersAdditionDataTable();
            racersAdditionTableAdapter.Fill(racersAdditionRows);

            int racerId = racersAdditionRows.Where(rA => rA.UserEmail.Equals(MainController.currentUser.Email)).FirstOrDefault().RacerId;

            List<RacerSponsorConnectorRow> racerSponsorConnectorRowsList = racerSponsorConnectorRows.Where(rsc => rsc.RacerId.Equals(racerId)).ToList();

            SponsorshipTableAdapter sponsorshipTableAdapter = new SponsorshipTableAdapter();
            SponsorshipDataTable sponsorshipRows = new SponsorshipDataTable();
            sponsorshipTableAdapter.Fill(sponsorshipRows);

            List<SponsorshipRow> sponsorshipRowsList = new List<SponsorshipRow>();

            foreach(RacerSponsorConnectorRow row in racerSponsorConnectorRowsList)
            {
                sponsorshipRowsList.Add(sponsorshipRows.Where(sr => sr.ID_Sponsorship.Equals(row.SponsorId)).FirstOrDefault());
            }

            foreach(SponsorshipRow row in sponsorshipRowsList)
            {
                Sponsor sponsor = new Sponsor();
                sponsor.name = row.SponsorName;
                sponsor.amount = row.Amount;
                commonAmount += sponsor.amount;
                this.sponsorList.Items.Add(sponsor);
            }

            this.t_commot_amount.Text = "$ " + commonAmount;
        }


        private void InitCharityInfo()
        {
            RacersAdditionTableAdapter racersAdditionTableAdapter = new RacersAdditionTableAdapter();
            RacersAdditionDataTable racersAdditionRows = new RacersAdditionDataTable();
            racersAdditionTableAdapter.Fill(racersAdditionRows);

            int racerId = racersAdditionRows.Where(rA => rA.UserEmail.Equals(MainController.currentUser.Email)).FirstOrDefault().RacerId;

            RegistrationTableAdapter registrationTableAdapter = new RegistrationTableAdapter();
            RegistrationDataTable registrationRows = new RegistrationDataTable();
            registrationTableAdapter.Fill(registrationRows);

            RegistrationRow registrationRow = registrationRows.Where(rr => rr.ID_Racer.Equals(racerId)).LastOrDefault();

            if (registrationRow == null)
            {
                MessageBox.Show("Вы еще не регистрировались в гонке.");
                this.t_charity_name.Text = "";
                this.t_charity_description.Text = "";
                return;
            }

            int id_charity = registrationRow.ID_Charity;

            CharityTableAdapter charityTableAdapter = new CharityTableAdapter();
            CharityDataTable charityRows = new CharityDataTable();
            charityTableAdapter.Fill(charityRows);

            CharityRow charityRow = charityRows.Where(cr => cr.ID_Сharity.Equals(id_charity)).FirstOrDefault();

            var uriImageSource = new Uri(MainController.IconPath + charityRow.Charity_Logo, UriKind.RelativeOrAbsolute);
            ImageSource image = new BitmapImage(uriImageSource);
            this.charity_icon.Source = image;

            this.t_charity_name.Text = charityRow.Charity_Name;


            this.t_charity_description.Text = charityRow.Charity_Description;
        }

        class Sponsor 
        {
            public string name { get; set; }
            public decimal amount { get; set; }
        }


        private void go_back_Click(object sender, RoutedEventArgs e)
        {
            RacerPanel racerPanel = new RacerPanel();
            racerPanel.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            racerPanel.Show();
            this.Close();
        }

        private void logout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            mainWindow.Show();
            this.Close();
        }
    }
}
