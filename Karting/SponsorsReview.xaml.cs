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
    public partial class SponsorsReview : Window
    {
        public SponsorsReview()
        {
            InitializeComponent();
            DataController.StartTimerOnCurrentWindow(this.textBlock_DayXInfo, this.textBlock_DayXChanger);
            InitComboSort();
            InitCharityList();
        }

        private void InitComboSort()
        {
            this.c_sort.Items.Add("Наименование");
            this.c_sort.Items.Add("Итоговая сумма");
            this.c_sort.Items.Add("Без сортировки");
        }

        class CharityData
        {
            public string imagePath { get; set; }
            public string charityName { get; set; }
            public decimal commonAmount { get; set; }
        }

        private void InitCharityList(string sort = "")
        {
            this.charityList.Items.Clear();

            decimal commonAmounts = 0;

            CharityTableAdapter charityTableAdapter = new CharityTableAdapter();
            CharityDataTable charityRows = new CharityDataTable();
            charityTableAdapter.Fill(charityRows);

            SponsorshipTableAdapter sponsorshipTableAdapter = new SponsorshipTableAdapter();
            SponsorshipDataTable sponsorshipRows = new SponsorshipDataTable();
            sponsorshipTableAdapter.Fill(sponsorshipRows);

            RacerSponsorConnectorTableAdapter racerSponsorConnectorTableAdapter = new RacerSponsorConnectorTableAdapter();
            RacerSponsorConnectorDataTable racerSponsorConnectorRows = new RacerSponsorConnectorDataTable();
            racerSponsorConnectorTableAdapter.Fill(racerSponsorConnectorRows);

            this.t_charity_count.Text = "Благотворительные организации: " + charityRows.Count();

            List<CharityData> charityDataList = new List<CharityData>();
            foreach(CharityRow row in charityRows)
            {
                CharityData charityData = new CharityData();
                charityData.charityName = row.Charity_Name;
                charityData.imagePath = MainController.IconPath + row.Charity_Logo;
                decimal charityAmount = 0;
                List<RacerSponsorConnectorRow> racerSponsorConnectorRowList = racerSponsorConnectorRows.Where(rsc => rsc.CharityId.Equals(row.ID_Сharity)).ToList();
                foreach(RacerSponsorConnectorRow rscRow in racerSponsorConnectorRowList)
                {
                    SponsorshipRow sponsorship = sponsorshipRows.Where(s => s.ID_Sponsorship.Equals(rscRow.SponsorId)).FirstOrDefault();
                    charityAmount += sponsorship.Amount;
                }
                charityData.commonAmount = charityAmount;
                commonAmounts += charityData.commonAmount;
                charityDataList.Add(charityData);
            }

            if(sort != "" && sort != "Без сортировки")
            {
                switch(sort)
                {
                    case "Наименование":
                        charityDataList = charityDataList.OrderBy(cd => cd.charityName).ToList();
                        break;
                    case "Итоговая сумма":
                        charityDataList = charityDataList.OrderBy(cd => cd.commonAmount).Reverse().ToList();
                        break;
                }
            }

            this.t_common_amounts.Text = "Всего спонсорских взносов: $ " + commonAmounts;

            foreach(CharityData data in charityDataList)
            {
                this.charityList.Items.Add(data);
            }

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

        private void sort_Click(object sender, RoutedEventArgs e)
        {
            string sort = "";
            if(this.c_sort.SelectedItem != null)
            {
                sort = this.c_sort.SelectedItem.ToString();
            }
            InitCharityList(sort);
        }
    }
}
