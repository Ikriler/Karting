using System;
using System.Collections.Generic;
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
    public partial class CoordinatorEditRacer : Window
    {
        string currentEditRacerEmail = "";
        public CoordinatorEditRacer(string email = "")
        {
            InitializeComponent();
            DataController.StartTimerOnCurrentWindow(this.textBlock_DayXInfo, this.textBlock_DayXChanger);
            currentEditRacerEmail = email;
            InitComboGender();
            InitComboCountry();
            InitComboRegistrationStatus();
            InitFields();
        }

        
        private void InitComboGender()
        {
            GenderTableAdapter genderTableAdapter = new GenderTableAdapter();
            GenderDataTable genderRows = new GenderDataTable();
            genderTableAdapter.Fill(genderRows);

            foreach (GenderRow genderRow in genderRows)
            {
                this.c_gender.Items.Add(genderRow.Gender_Name);
            }
        }

        private void InitComboCountry()
        {
            CountryTableAdapter countryTableAdapter = new CountryTableAdapter();
            CountryDataTable countryRows = new CountryDataTable();
            countryTableAdapter.Fill(countryRows);

            foreach(CountryRow countryRow in countryRows)
            {
                this.c_country.Items.Add(countryRow.Country_Name);
            }
        }

        private void InitComboRegistrationStatus()
        {
            Registration_StatusTableAdapter registration_StatusTableAdapter = new Registration_StatusTableAdapter();
            Registration_StatusDataTable registration_StatusRows = new Registration_StatusDataTable();
            registration_StatusTableAdapter.Fill(registration_StatusRows);

            foreach(Registration_StatusRow registration_StatusRow in registration_StatusRows)
            {
                this.c_registration_status.Items.Add(registration_StatusRow.Statu_Name);
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

            UserRow currentUser = userRows.Where(u => u.Email.Equals(currentEditRacerEmail)).FirstOrDefault();
            RacersAdditionRow racersAdditionRow = racersAdditionRows.Where(rA => rA.UserEmail.Equals(currentEditRacerEmail)).FirstOrDefault();
            RacerRow currentRacer = racerRows.Where(r => r.ID_Racer.Equals(racersAdditionRow.RacerId)).FirstOrDefault();

            this.l_email.Content = currentEditRacerEmail;
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

            RegistrationTableAdapter registrationTableAdapter = new RegistrationTableAdapter();
            RegistrationDataTable registrationRows = new RegistrationDataTable();
            registrationTableAdapter.Fill(registrationRows);

            RegistrationRow registrationRow = registrationRows.Where(rr => rr.ID_Racer.Equals(racersAdditionRow.RacerId)).LastOrDefault();

            Registration_StatusTableAdapter registration_StatusTableAdapter = new Registration_StatusTableAdapter();
            Registration_StatusDataTable registration_StatusRows = new Registration_StatusDataTable();
            registration_StatusTableAdapter.Fill(registration_StatusRows);

            Registration_StatusRow registration_StatusRow = registration_StatusRows.FindByID_Registration_Status(registrationRow.ID_Registration_Status);

            this.c_registration_status.SelectedItem = registration_StatusRow.Statu_Name;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            BeforeEditRacer beforeEditRacer = new BeforeEditRacer(currentEditRacerEmail);
            beforeEditRacer.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            beforeEditRacer.Show();
            this.Close();
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

            string registration_status = "";
            if (this.c_registration_status.SelectedItem != null)
            {
                registration_status = this.c_registration_status.SelectedItem.ToString();
            }
            else
            {
                MessageBox.Show("Статус не выбран.");
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

            userTableAdapter.UserRacerUpdateQuery(password, name, last_name, currentEditRacerEmail);

            string genderId = genderRows.Where(g => g.Gender_Name.Equals(selectedGender)).FirstOrDefault().ID_Gender;

            CountryTableAdapter countryTableAdapter = new CountryTableAdapter();
            CountryDataTable countryRows = new CountryDataTable();
            countryTableAdapter.Fill(countryRows);

            string countryId = countryRows.Where(c => c.Country_Name.Equals(selectedCountry)).FirstOrDefault().ID_Country;

            int racerId = racersAdditionRows.Where(rA => rA.UserEmail.Equals(currentEditRacerEmail)).FirstOrDefault().RacerId;

            racerTableAdapter.RacerUpdateQuery(name, last_name, genderId, bd_date, countryId, racerId);

            userTableAdapter.Fill(userRows);
            racerTableAdapter.Fill(racerRows);

            RacerRow racer = racerRows.Where(r => r.ID_Racer.Equals(racerId)).LastOrDefault();
            UserRow user = userRows.Where(u => u.Email.Equals(currentEditRacerEmail)).LastOrDefault();


            RegistrationTableAdapter registrationTableAdapter = new RegistrationTableAdapter();
            RegistrationDataTable registrationRows = new RegistrationDataTable();
            registrationTableAdapter.Fill(registrationRows);

            RegistrationRow registrationRow = registrationRows.Where(rr => rr.ID_Racer.Equals(racer.ID_Racer)).LastOrDefault();

            Registration_StatusTableAdapter registration_StatusTableAdapter = new Registration_StatusTableAdapter();
            Registration_StatusDataTable registration_StatusRows = new Registration_StatusDataTable();
            registration_StatusTableAdapter.Fill(registration_StatusRows);

            Registration_StatusRow registration_StatusRow = registration_StatusRows.Where(rs => rs.Statu_Name.Equals(registration_status)).FirstOrDefault();

            registrationTableAdapter.RegistrationUpdateQuery(registrationRow.ID_Racer, registrationRow.Registration_Date, registration_StatusRow.ID_Registration_Status, registrationRow.Cost, registrationRow.ID_Charity, registrationRow.SponsorshipTarget, registrationRow.ID_Registration);

            string newPhotoName = "";
            try 
            {
                string[] photo_path_params = photo_path.Split('.');
                newPhotoName = Guid.NewGuid() + "." + photo_path_params[photo_path_params.Length - 1];
                string realPathPhoto = MainController.IconPath + newPhotoName;
                File.Copy(photo_path, realPathPhoto);
            }
            catch
            {

            }

            if (racer != null && user != null)
            {
                int originalRacerAddition = racersAdditionRows.Where(rAI => rAI.UserEmail.Equals(currentEditRacerEmail)).FirstOrDefault().RacerAddiotionId;
                racersAdditionTableAdapter.RacersAdditionUpdateQuery(user.Email, racer.ID_Racer, newPhotoName, originalRacerAddition);
                MessageBox.Show("Пользователь успешно обновлен.");
                MainController.AuthorizeCurrentUser(user);
                BeforeEditRacer beforeEditRacer = new BeforeEditRacer(currentEditRacerEmail);
                beforeEditRacer.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                beforeEditRacer.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Не удалось обновить пользователя.");
            }
        }
    }
}
