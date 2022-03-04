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
    public partial class MyResults : Window
    {
        public MyResults()
        {
            InitializeComponent();
            DataController.StartTimerOnCurrentWindow(this.textBlock_DayXInfo, this.textBlock_DayXChanger);
            InitFields();
            InitListMyResult();
        }

        private void InitListMyResult()
        {
            RacersAdditionTableAdapter racersAdditionTableAdapter = new RacersAdditionTableAdapter();
            RacersAdditionDataTable racersAdditionRows = new RacersAdditionDataTable();
            racersAdditionTableAdapter.Fill(racersAdditionRows);

            RacerTableAdapter racerTableAdapter = new RacerTableAdapter();
            RacerDataTable racerRows = new RacerDataTable();
            racerTableAdapter.Fill(racerRows);

            int racerId = racersAdditionRows.Where(rA => rA.UserEmail.Equals(MainController.currentUser.Email)).FirstOrDefault().RacerId;
            RacerRow racer = racerRows.Where(r => r.ID_Racer.Equals(racerId)).FirstOrDefault();


            MyResultTableAdapter myResultTableAdapter = new MyResultTableAdapter();
            MyResultDataTable myResultRows = new MyResultDataTable();
            try 
            {
                myResultTableAdapter.Fill(myResultRows, racerId);
            }
            catch
            {

            }

            if(myResultRows.Count == 0)
            {
                Application.Current.Dispatcher.InvokeAsync(async () =>
                {
                    AlarmWindow alarm = new AlarmWindow("Поиск");
                    alarm.Show();
                    await Task.Delay(1000);
                    alarm.Close();
                });
            }

            this.myResultList.ItemsSource = myResultRows;
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

        private void showAllRsult_Click(object sender, RoutedEventArgs e)
        {
            AllOldResults allOldResults = new AllOldResults(true);
            allOldResults.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            allOldResults.Show();
            this.Close();
        }

        private void InitFields()
        {
            RacersAdditionTableAdapter racersAdditionTableAdapter = new RacersAdditionTableAdapter();
            RacersAdditionDataTable racersAdditionRows = new RacersAdditionDataTable();
            racersAdditionTableAdapter.Fill(racersAdditionRows);

            RacerTableAdapter racerTableAdapter = new RacerTableAdapter();
            RacerDataTable racerRows = new RacerDataTable();
            racerTableAdapter.Fill(racerRows);

            GenderTableAdapter genderTableAdapter = new GenderTableAdapter();
            GenderDataTable genderRows = new GenderDataTable();
            genderTableAdapter.Fill(genderRows);

            int racerId = racersAdditionRows.Where(rA => rA.UserEmail.Equals(MainController.currentUser.Email)).FirstOrDefault().RacerId;
            RacerRow racer = racerRows.Where(r => r.ID_Racer.Equals(racerId)).FirstOrDefault();

            string gender = genderRows.Where(g => g.ID_Gender.Equals(racer.Gender)).FirstOrDefault().Gender_Name;
            this.l_gender.Content = gender;

            int age = Convert.ToInt32((DateTime.Today.Subtract(racer.DateOfBirth)).Days / 365);

            string age_category = "";
            if (age < 18)
            {
                age_category = MainController.AgeCategoryList[0];
            }
            else if (age >= 18 && age <= 29)
            {
                age_category = MainController.AgeCategoryList[1];
            }
            else if (age >= 30 && age <= 39)
            {
                age_category = MainController.AgeCategoryList[2];
            }
            else if (age >= 40 && age <= 55)
            {
                age_category = MainController.AgeCategoryList[3];
            }
            else if (age >= 56 && age <= 70)
            {
                age_category = MainController.AgeCategoryList[4];
            }
            else if (age > 70)
            {
                age_category = MainController.AgeCategoryList[5];
            }
            this.l_age_category.Content = age_category;
        }
    }
}
