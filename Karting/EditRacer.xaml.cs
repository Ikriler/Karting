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
    public partial class EditRacer : Window
    {
        public string currentEmail = "";
        public EditRacer(string email = "")
        {
            InitializeComponent();
            DataController.StartTimerOnCurrentWindow(this.textBlock_DayXInfo, this.textBlock_DayXChanger);
            InitComboGender();
            InitComboCountry();
            currentEmail = email;
            if(currentEmail.Equals(""))
            {
                currentEmail = MainController.currentUser.Email;
            }
            InitFields();
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            RacerPanel racerPanel = new RacerPanel();
            racerPanel.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            racerPanel.Show();
            this.Close();
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
        }

        private void InitComboCountry()
        {
            CountryTableAdapter countryTableAdapter = new CountryTableAdapter();
            CountryDataTable countryRows = new CountryDataTable();
            countryTableAdapter.Fill(countryRows);

            foreach(CountryRow country in countryRows)
            {
                this.c_country.Items.Add(country.Country_Name);
            }
        }

        private void InitFields()
        {
            UserTableAdapter userTableAdapter = new UserTableAdapter();
            UserDataTable userRows = new UserDataTable();
            userTableAdapter.Fill(userRows);

            RacerTableAdapter racerTableAdapter = new RacerTableAdapter();
            RacerDataTable racerRows = new RacerDataTable();
            racerTableAdapter.Fill(racerRows);

            RacersAdditionTableAdapter racersAdditionTableAdapter = new RacersAdditionTableAdapter();
            RacersAdditionDataTable racersAdditionRows = new RacersAdditionDataTable();
            racersAdditionTableAdapter.Fill(racersAdditionRows);

            GenderTableAdapter genderTableAdapter = new GenderTableAdapter();
            GenderDataTable genderRows = new GenderDataTable();
            genderTableAdapter.Fill(genderRows);

            CountryTableAdapter countryTableAdapter = new CountryTableAdapter();
            CountryDataTable countryRows = new CountryDataTable();
            countryTableAdapter.Fill(countryRows);

            UserRow currentUser = userRows.Where(u => u.Email.Equals(currentEmail)).FirstOrDefault();
            RacersAdditionRow racersAdditionRow = racersAdditionRows.Where(rA => rA.UserEmail.Equals(currentEmail)).FirstOrDefault();
            RacerRow currentRacer = racerRows.Where(r => r.ID_Racer.Equals(racersAdditionRow.RacerId)).FirstOrDefault();

            this.l_email.Content = currentEmail;
            this.t_pass.Text = currentUser.Password;
            this.t_rePass.Text = currentUser.Password;
            this.t_name.Text = currentUser.First_Name;
            this.t_lastName.Text = currentUser.Last_Name;

            string gender = genderRows.Where(g => g.ID_Gender.Equals(currentRacer.Gender)).FirstOrDefault().Gender_Name;

            this.c_gender.SelectedItem = gender;

            string country = countryRows.Where(c => c.ID_Country.Equals(currentRacer.ID_Country)).FirstOrDefault().Country_Name;

            this.c_country.SelectedItem = country;

            try 
            {
                this.textbox_photo_path.Text = MainController.IconPath + racersAdditionRow.PhotoPath;
                var uriImageSource = new Uri(MainController.IconPath + racersAdditionRow.PhotoPath, UriKind.RelativeOrAbsolute);
                ImageSource image = new BitmapImage(uriImageSource);
                this.photo_img.Source = image;
            }
            catch
            {

            }

            this.dp_bd.SelectedDate = currentRacer.DateOfBirth;
        }

        private void EditConfirm_Click(object sender, RoutedEventArgs e)
        {
            string password = this.t_pass.Text;
            string rePassword = this.t_rePass.Text;
            string name = this.t_name.Text;
            string last_name = this.t_lastName.Text;

            //Gender
            string selectedGender = "";
            if (this.c_gender.SelectedItem != null)
            {
                selectedGender = this.c_gender.SelectedItem.ToString();
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
                selectedCountry = this.c_country.SelectedItem.ToString();
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

            int age = Convert.ToInt32((DateTime.Today.Subtract(bd_date)).Days / 365);

            if (age < 10)
            {
                MessageBox.Show("Вам должно быть не менее 10 лет");
                return;
            }

            if (!(Validations.StringIsNotEmpty(password) &&
                Validations.StringIsNotEmpty(rePassword) &&
                Validations.StringIsNotEmpty(name) &&
                Validations.StringIsNotEmpty(last_name)))
            {
                MessageBox.Show("Поля не должны быть пустыми.");
                return;
            }

            if (password != rePassword)
            {
                MessageBox.Show("Пароли не совпадают.");
                return;
            }

            if (!Validations.CheckPassword(password))
            {
                MessageBox.Show("Пароль слишком простой.");
                return;
            }

            if (!Validations.StringIsNotEmpty(photo_path))
            {
                MessageBox.Show("Выберите фотографию.");
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

            GenderTableAdapter genderTableAdapter = new GenderTableAdapter();
            GenderDataTable genderRows = new GenderDataTable();
            genderTableAdapter.Fill(genderRows);

            userTableAdapter.UserRacerUpdateQuery(password, name, last_name , currentEmail);

            string genderId = genderRows.Where(g => g.Gender_Name.Equals(selectedGender)).FirstOrDefault().ID_Gender;

            CountryTableAdapter countryTableAdapter = new CountryTableAdapter();
            CountryDataTable countryRows = new CountryDataTable();
            countryTableAdapter.Fill(countryRows);

            string countryId = countryRows.Where(c => c.Country_Name.Equals(selectedCountry)).FirstOrDefault().ID_Country;

            int racerId = racersAdditionRows.Where(rA => rA.UserEmail.Equals(currentEmail)).FirstOrDefault().RacerId;

            racerTableAdapter.RacerUpdateQuery(name, last_name, genderId, bd_date, countryId, racerId);

            userTableAdapter.Fill(userRows);
            racerTableAdapter.Fill(racerRows);

            RacerRow racer = racerRows.Where(r => r.ID_Racer.Equals(racerId)).LastOrDefault();
            UserRow user = userRows.Where(u => u.Email.Equals(currentEmail)).LastOrDefault();

            string[] photo_path_params = photo_path.Split('.');
            string newPhotoName = Guid.NewGuid() + "." + photo_path_params[photo_path_params.Length - 1];
            string realPathPhoto = MainController.IconPath + newPhotoName;
            File.Copy(photo_path, realPathPhoto);

            if (racer != null && user != null)
            {
                int originalRacerAddition = racersAdditionRows.Where(rAI => rAI.UserEmail.Equals(currentEmail)).FirstOrDefault().RacerAddiotionId;
                racersAdditionTableAdapter.RacersAdditionUpdateQuery(user.Email, racer.ID_Racer, newPhotoName, originalRacerAddition);
                MessageBox.Show("Пользователь успешно обновлен.");
                MainController.AuthorizeCurrentUser(user);
                RacerPanel racerPanel = new RacerPanel();
                racerPanel.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                racerPanel.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Не удалось обновить пользователя.");
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
