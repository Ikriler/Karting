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
    public partial class RegistrationOnRace : Window
    {
        public RegistrationOnRace()
        {
            InitializeComponent();
            DataController.StartTimerOnCurrentWindow(this.textBlock_DayXInfo, this.textBlock_DayXChanger);
            InitComboSponsor();
        }

        private void InitComboSponsor() 
        {
            CharityTableAdapter charityTableAdapter = new CharityTableAdapter();
            CharityDataTable charityRows = new CharityDataTable();
            charityTableAdapter.Fill(charityRows);

            this.c_charity.ItemsSource = charityRows;
            this.c_charity.DisplayMemberPath = "Charity_Name";
        }

        private void back_Click(object sender, RoutedEventArgs e)
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

        private void check_sum_Click(object sender, RoutedEventArgs e)
        {
            int amount = 0;
            if (r_2.IsChecked.Value) amount += 30;
            if (r_3.IsChecked.Value) amount += 50;
            if (check_one.IsChecked.Value) amount += 25;
            if (check_two.IsChecked.Value) amount += 40;
            if (check_three.IsChecked.Value) amount += 65;

            this.t_amount.Text = "$ " + amount;
        }
        private void t_sponsor_amount_KeyDown(object sender, KeyEventArgs e)
        {
            string number = e.Key.ToString();
            string pattern = "D1D2D3D4D5D6D7D8D9D0";
            if (!pattern.Contains(number) || e.Key == Key.D || this.t_charity_amount.Text.Length >= 9)
            {
                e.Handled = true;
            }
        }

        private void cancel_Click(object sender, RoutedEventArgs e)
        {
            RacerPanel racerPanel = new RacerPanel();
            racerPanel.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            racerPanel.Show();
            this.Close();
        }

        private void registration_Click(object sender, RoutedEventArgs e)
        {
            int amount = Convert.ToInt32(this.t_amount.Text.Split(' ')[1]);
            int organisationCost = 0;

            if(this.t_charity_amount.Text.Length != 0)
            {
                organisationCost = Convert.ToInt32(this.t_charity_amount.Text);
            }

            DataRowView organisation = null;
            if(this.c_charity.SelectedItem != null)
            {
                organisation = this.c_charity.SelectedItem as DataRowView;
            }
            else 
            {
                MessageBox.Show("Организация не выбрана");
                return;
            }

            #region VALIDATION

            if(organisationCost == 0)
            {
                MessageBox.Show("Взонс не должен быть равен 0.");
                return;
            }

            if(this.check_one.IsChecked.Value == false &&
                this.check_two.IsChecked.Value == false &&
                this.check_three.IsChecked.Value == false)
            {
                MessageBox.Show("Должна быть выбрана хотя бы одна гонка");
                return;
            }

            #endregion

            complectsTableAdapter complects = new complectsTableAdapter();
            complectsDataTable complectsRows = new complectsDataTable();
            complects.Fill(complectsRows);

            int oldAmount = 0;
            if (r_1.IsChecked.Value)
            {
                oldAmount = complectsRows.FindBycomplectsId("TypeACount").complectsNumber;
                complects.ComplectUpdateQuery("TypeACount", oldAmount + 1, "TypeACount");
            }
            if (r_2.IsChecked.Value)
            {
                oldAmount = complectsRows.FindBycomplectsId("TypeBCount").complectsNumber;
                complects.ComplectUpdateQuery("TypeBCount", oldAmount + 1, "TypeBCount");
            }
            if (r_3.IsChecked.Value)
            {
                oldAmount = complectsRows.FindBycomplectsId("TypeCCount").complectsNumber;
                complects.ComplectUpdateQuery("TypeCCount", oldAmount + 1, "TypeCCount");
            }


            RegistrationTableAdapter registrationTableAdapter = new RegistrationTableAdapter();
            RegistrationDataTable registrationRows = new RegistrationDataTable();
            registrationTableAdapter.Fill(registrationRows);

            UserTableAdapter userTableAdapter = new UserTableAdapter();
            UserDataTable userRows = new UserDataTable();
            userTableAdapter.Fill(userRows);

            RacersAdditionTableAdapter racersAdditionTableAdapter = new RacersAdditionTableAdapter();
            RacersAdditionDataTable racersAdditionRows = new RacersAdditionDataTable();
            racersAdditionTableAdapter.Fill(racersAdditionRows);

            int racerId = racersAdditionRows.Where(rA => rA.UserEmail.Equals(MainController.currentUser.Email)).FirstOrDefault().RacerId;

            CharityTableAdapter charityTableAdapter = new CharityTableAdapter();
            CharityDataTable charityRows = new CharityDataTable();
            charityTableAdapter.Fill(charityRows);

            int organisationId = charityRows.Where(c => c.Charity_Name.Equals(organisation["Charity_Name"].ToString())).FirstOrDefault().ID_Сharity;

            registrationTableAdapter.Insert(racerId, DateTime.Today, 1, amount, organisationId, organisationCost);

            RacerThanks racerThanks = new RacerThanks();
            racerThanks.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            racerThanks.Show();
            this.Close();
        }
    }
}
