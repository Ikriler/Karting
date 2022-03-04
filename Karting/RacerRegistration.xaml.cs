using System;
using System.Collections.Generic;
using System.Data;
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
using Microsoft.Win32;
using static Karting.DataSet;

namespace Karting
{
    public partial class RacerRegistration : Window
    {
        public RacerRegistration()
        {
            InitializeComponent();
            DataController.StartTimerOnCurrentWindow(this.textBlock_DayXInfo, this.textBlock_DayXChanger);
            InitComboGender();
            InitComboCountry();
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            mainWindow.Show();
            this.Close();
        }

        private void InitComboGender() 
        {
            GenderTableAdapter genderTableAdapter = new GenderTableAdapter();
            GenderDataTable genderRows = new GenderDataTable();
            genderTableAdapter.Fill(genderRows);

            this.c_gender.ItemsSource = genderRows;
            this.c_gender.DisplayMemberPath = "Gender_Name";
        }

        private void InitComboCountry() 
        {
            CountryTableAdapter countryTableAdapter = new CountryTableAdapter();
            CountryDataTable countryRows = new CountryDataTable();
            countryTableAdapter.Fill(countryRows);

            this.c_country.ItemsSource = countryRows;
            this.c_country.DisplayMemberPath = "Country_Name";
        }

        private void RegistrationConfirm_Click(object sender, RoutedEventArgs e)
        {
            string email = this.t_email.Text;
            string password = this.t_pass.Text;
            string rePassword = this.t_rePass.Text;
            string name = this.t_name.Text;
            string last_name = this.t_lastName.Text;

            //Gender
            string selectedGender = "";
            if(this.c_gender.SelectedItem != null)
            {
                selectedGender = (this.c_gender.SelectedItem as DataRowView)["Gender_Name"].ToString();
            }
            else
            {
                MessageBox.Show("Пол не выбран.");
                return;
            }
            //Country
            string selectedCountry = "";
            if (this.c_country.SelectedItem != null)
            {
                selectedCountry = (this.c_country.SelectedItem as DataRowView)["Country_Name"].ToString();
            }
            else
            {
                MessageBox.Show("Страна не выбрана.");
                return;
            }
            //Birth Day Date
            DateTime bd_date = new DateTime();
            if (this.dp_bd.SelectedDate != null)
            {
               bd_date = (DateTime)this.dp_bd.SelectedDate;
            }
            else 
            {
                MessageBox.Show("Дата не выбрана.");
                return;
            }

            string photo_path = (string)this.textbox_photo_path.Text;


            #region VALIDATION

            if(!(Validations.StringIsNotEmpty(email) &&
                Validations.StringIsNotEmpty(email) &&
                Validations.StringIsNotEmpty(email) &&
                Validations.StringIsNotEmpty(email) &&
                Validations.StringIsNotEmpty(email)))
            {
                MessageBox.Show("Поля не должны быть пустыми.");
                return;
            }

            if(password != rePassword)
            {
                MessageBox.Show("Пароли не совпадают.");
                return;
            }

            #endregion


            RacerTableAdapter racerTableAdapter = new RacerTableAdapter();
            RacerDataTable racerRows = new RacerDataTable();
            racerTableAdapter.Fill(racerRows);

            UserTableAdapter userTableAdapter = new UserTableAdapter();
            UserDataTable userRows = new UserDataTable();
            userTableAdapter.Fill(userRows);

            RacersAdditionTableAdapter racersAdditionTableAdapter = new RacersAdditionTableAdapter();
            RacersAdditionDataTable racersAdditionRows = new RacersAdditionDataTable();
            racersAdditionTableAdapter.Fill(racersAdditionRows);

            UserRow userCheckExists = userRows.Where(uCE => uCE.Email.Equals(email)).FirstOrDefault();
            if (userCheckExists != null) 
            {
                MessageBox.Show("Пользователь с такой почтой уже существует.");
                return;
            }

            int CheckNumber = userTableAdapter.UserInsertQuery(email, password, name, last_name, "R");

            GenderTableAdapter genderTableAdapter = new GenderTableAdapter();
            GenderDataTable genderRows = new GenderDataTable();
            genderTableAdapter.Fill(genderRows);

            string genderId = genderRows.Where(g => g.Gender_Name.Equals(selectedGender)).FirstOrDefault().ID_Gender;

            CountryTableAdapter countryTableAdapter = new CountryTableAdapter();
            CountryDataTable countryRows = new CountryDataTable();
            countryTableAdapter.Fill(countryRows);

            string countryId = countryRows.Where(c => c.Country_Name.Equals(selectedCountry)).FirstOrDefault().ID_Country;

            racerTableAdapter.RacerInsertQuery(name, last_name, genderId, bd_date.ToString(), countryId);

            userTableAdapter.Fill(userRows);
            racerTableAdapter.Fill(racerRows);

            RacerRow racer = racerRows.Where(r => r.First_Name.Equals(name) && r.Last_Name.Equals(last_name) && r.ID_Country.Equals(countryId)).LastOrDefault();
            UserRow user = userRows.Where(u => u.Email.Equals(email)).LastOrDefault();

            string[] photo_path_params = photo_path.Split('.');
            string newPhotoName = Guid.NewGuid() + "." + photo_path_params[photo_path_params.Length - 1];
            string realPathPhoto = MainController.IconPath + newPhotoName;
            File.Copy(photo_path, realPathPhoto);


            if (racer != null && user != null)
            {
                racersAdditionTableAdapter.RacerAdditionInsertQuery(user.Email, racer.ID_Racer, newPhotoName);
                MessageBox.Show("Пользователь успешно создан.");
                MainController.AuthorizeCurrentUser(user);
                RegistrationOnRace registrationOnRace = new RegistrationOnRace();
                registrationOnRace.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                registrationOnRace.Show();
                this.Close();
            }
            else 
            {
                MessageBox.Show("Не удалось создать пользователя.");
            }
        }

        private void CheckPhoto_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog photo = new OpenFileDialog();
            photo.Filter = "Images|*.jpg;*.gif;*.png;*.bmp";
            if (photo.ShowDialog() == true)
            {
                this.textbox_photo_path.Text = photo.FileName;
                var uriImageSource = new Uri(photo.FileName, UriKind.RelativeOrAbsolute);
                ImageSource image = new BitmapImage(uriImageSource);
                this.photo_img.Source = image;
            }
        }
    }
}
