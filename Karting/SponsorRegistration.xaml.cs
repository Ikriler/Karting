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
    public partial class SponsorRegistration : Window
    {
        public SponsorRegistration()
        {
            InitializeComponent();
            DataController.StartTimerOnCurrentWindow(this.textBlock_DayXInfo, this.textBlock_DayXChanger);
            InitComboRacer();
            InitComboCharity();
        }

        private void InitComboCharity()
        {
            CharityTableAdapter charityTableAdapter = new CharityTableAdapter();
            CharityDataTable charityRows = new CharityDataTable();
            charityTableAdapter.Fill(charityRows);

            this.l_charity.ItemsSource = charityRows;
            this.l_charity.DisplayMemberPath = "Charity_Name";
        }

        private void InitComboRacer() 
        {
            RacerTableAdapter racerTableAdapter = new RacerTableAdapter();
            RacerDataTable racerRows = new RacerDataTable();
            racerTableAdapter.Fill(racerRows);

            this.c_racer.ItemsSource = racerRows;
        }

        private void t_amount_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.l_amount_text.Content = "$" + this.t_amount.Text;
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            mainWindow.Show();
            this.Close();
        }

        private void t_amount_KeyDown(object sender, KeyEventArgs e)
        {
            string number = e.Key.ToString();
            string pattern = "D1D2D3D4D5D6D7D8D9D0";
            if (!pattern.Contains(number) || e.Key == Key.D || this.t_amount.Text.Length > 10)
            {
                e.Handled = true;
            }
        }

        private void add10_Click(object sender, RoutedEventArgs e)
        {
            this.t_amount.Text = (Convert.ToInt32(this.t_amount.Text) + 10).ToString();
        }

        private void del10_Click(object sender, RoutedEventArgs e)
        {
            long newAmount = Convert.ToInt64(this.t_amount.Text) - 10;
            if(newAmount > 0)
                this.t_amount.Text = newAmount.ToString();
        }

        private void t_card_number_KeyDown(object sender, KeyEventArgs e)
        {
            string number = e.Key.ToString();
            string pattern = "D1D2D3D4D5D6D7D8D9D0";
            if (!pattern.Contains(number) || e.Key == Key.D || this.t_card_number.Text.Length >= 16)
            {
                e.Handled = true;
            }
        }

        private void t_card_month_KeyDown(object sender, KeyEventArgs e)
        {
            string number = e.Key.ToString();
            string pattern = "D1D2D3D4D5D6D7D8D9D0";
            if (!pattern.Contains(number) || e.Key == Key.D || this.t_card_month.Text.Length >= 2)
            {
                e.Handled = true;
            }
        }

        private void t_card_year_KeyDown(object sender, KeyEventArgs e)
        {
            string number = e.Key.ToString();
            string pattern = "D1D2D3D4D5D6D7D8D9D0";
            if (!pattern.Contains(number) || e.Key == Key.D || this.t_card_year.Text.Length >= 4)
            {
                e.Handled = true;
            }
        }

        private void t_card_cvc_KeyDown(object sender, KeyEventArgs e)
        {
            string number = e.Key.ToString();
            string pattern = "D1D2D3D4D5D6D7D8D9D0";
            if (!pattern.Contains(number) || e.Key == Key.D || this.t_card_cvc.Text.Length >= 3)
            {
                e.Handled = true;
            }
        }

        private void Allow_Click(object sender, RoutedEventArgs e)
        {
            string yourName = this.t_your_name.Text;

            DataRowView racer = null;
            if(this.c_racer.SelectedItem != null)
            {
                racer = this.c_racer.SelectedItem as DataRowView;
            }
            if(racer == null)
            {
                MessageBox.Show("Гонщик не выбран.");
                return;
            }

            string cardHuman = this.t_card_human.Text;
            string cardNumber = this.t_card_number.Text;
            string cardMonth = this.t_card_month.Text;
            string cardYear = this.t_card_year.Text;
            string cardCVC = this.t_card_cvc.Text;

            int amount = Convert.ToInt32(this.t_amount.Text);


            #region VALIDATION
                
            if(!Validations.StringIsNotEmpty(yourName))
            {
                MessageBox.Show("Поле 'Ваше имя' не должно быть пустым.");
                return;
            }
            if(cardNumber.Count() < 16)
            {
                MessageBox.Show("Неправильный номер карты.");
                return;
            }
            if (cardMonth.Count() < 1)
            {
                MessageBox.Show("Неправильный месяц срока действия.");
                return;
            }
            if (Convert.ToInt32(cardMonth) <=0 || Convert.ToInt32(cardMonth) > 12)
            {
                MessageBox.Show("Неправильный месяц срока действия.");
                return;
            }
            if (cardYear.Count() < 4)
            {
                MessageBox.Show("Неправильный год срока действия.");
                return;
            }
            if (cardCVC.Count() < 3)
            {
                MessageBox.Show("Неправильный CVC.");
                return;
            }
            string charityName = "";
            if(this.l_charity.SelectedItem == null)
            {
                MessageBox.Show("Не выбрана благотворительная организация");
                return;
            }
            else
            {
                charityName = (this.l_charity.SelectedItem as DataRowView)["Charity_Name"].ToString();
            }

            #endregion


            string who = String.Format("{0} {1}({2}) из {3}", racer["First_Name"], racer["Last_Name"], racer["ID_Racer"], racer["ID_Country"]);

            SponsorshipTableAdapter sponsorshipTableAdapter = new SponsorshipTableAdapter();
            SponsorshipDataTable sponsorshipRows = new SponsorshipDataTable();
            sponsorshipTableAdapter.Fill(sponsorshipRows);

            RacerSponsorConnectorTableAdapter racerSponsorConnectorTableAdapter = new RacerSponsorConnectorTableAdapter();
            RacerSponsorConnectorDataTable racerSponsorConnectorRows = new RacerSponsorConnectorDataTable();
            racerSponsorConnectorTableAdapter.Fill(racerSponsorConnectorRows);

            CharityTableAdapter charityTableAdapter = new CharityTableAdapter();
            CharityDataTable charityRows = new CharityDataTable();
            charityTableAdapter.Fill(charityRows);

            sponsorshipTableAdapter.Insert(yourName, amount);
            sponsorshipTableAdapter.Fill(sponsorshipRows);

            int racerId = Convert.ToInt32(racer["ID_Racer"]);
            SponsorshipRow sponsor = sponsorshipRows.Where(s => s.SponsorName.Equals(yourName)).LastOrDefault();

            int charityId = charityRows.Where(c => c.Charity_Name.Equals(charityName)).FirstOrDefault().ID_Сharity;

            racerSponsorConnectorTableAdapter.Insert(racerId, sponsor.ID_Sponsorship, charityId);

            string CharityName = (this.l_charity.SelectedItem as DataRowView)["Charity_Name"].ToString();

            SponsorThanks sponsorThanks = new SponsorThanks(amount, who, CharityName);
            sponsorThanks.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            sponsorThanks.Show();
            this.Close();
        }
    }
}
