﻿using System;
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

            this.textbox_photo_path.Text = MainController.IconPath + racersAdditionRow.PhotoPath;

            var uriImageSource = new Uri(MainController.IconPath + racersAdditionRow.PhotoPath, UriKind.RelativeOrAbsolute);
            ImageSource image = new BitmapImage(uriImageSource);
            this.photo_img.Source = image;

            this.dp_bd.SelectedDate = currentRacer.DateOfBirth;
        }

        private void EditConfirm_Click(object sender, RoutedEventArgs e)
        {

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
