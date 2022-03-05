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
    public partial class AddOrEditCharity : Window
    {
        public string currentCharityName = "";
        public AddOrEditCharity(string charityName = "")
        {
            InitializeComponent();
            DataController.StartTimerOnCurrentWindow(this.textBlock_DayXInfo, this.textBlock_DayXChanger);
            currentCharityName = charityName;
            if (currentCharityName != "")
            {
                InitFields();
            }
        }

        private void InitFields()
        {
            CharityTableAdapter charityTableAdapter = new CharityTableAdapter();
            CharityDataTable charityRows = new CharityDataTable();
            charityTableAdapter.Fill(charityRows);

            CharityRow charity = charityRows.Where(c => c.Charity_Name.Equals(currentCharityName)).FirstOrDefault();

            var uriImageSource = new Uri(MainController.IconPath + charity.Charity_Logo, UriKind.RelativeOrAbsolute);
            ImageSource image = new BitmapImage(uriImageSource);
            this.photo_img.Source = image;
            this.textbox_photo_path.Text = MainController.IconPath + charity.Charity_Logo;

            this.t_charity_name.Text = charity.Charity_Name;
            this.t_charity_description.Text = charity.Charity_Description;
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            ControlCharity controlCharity = new ControlCharity();
            controlCharity.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            controlCharity.Show();
            this.Close();
        }

        private void logout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            mainWindow.Show();
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

        private void b_save_Click(object sender, RoutedEventArgs e)
        {
            string charityName = this.t_charity_name.Text;
            string charityDescription = this.t_charity_description.Text;
            string imagePath = this.textbox_photo_path.Text;

            #region VALIDATIONS
            if(!(Validations.StringIsNotEmpty(charityName) && Validations.StringIsNotEmpty(charityDescription)))
            {
                MessageBox.Show("Поля не должны быть пустыми.");
                return;
            }
            if (!(Validations.StringIsNotEmpty(imagePath)))
            {
                MessageBox.Show("Выберите фотографию.");
                return;
            }
            #endregion

            string[] photo_path_params = imagePath.Split('.');
            string newPhotoName = Guid.NewGuid() + "." + photo_path_params[photo_path_params.Length - 1];
            string realPathPhoto = MainController.IconPath + newPhotoName;
            File.Copy(imagePath, realPathPhoto);

            CharityTableAdapter charityTableAdapter = new CharityTableAdapter();
            CharityDataTable charityRows = new CharityDataTable();
            charityTableAdapter.Fill(charityRows);

            if (currentCharityName != "")
            {
                CharityRow charity = charityRows.Where(c => c.Charity_Name.Equals(currentCharityName)).FirstOrDefault();

                try
                {
                    if(charityRows.Where(c => c.Charity_Name.Equals(charityName)).Count() >= 1 && charityName != currentCharityName)
                    {
                        MessageBox.Show("Организация с таким именем уже существует.");
                    }
                    else
                    {
                        charityTableAdapter.CharityUpdateQuery(charityName, charityDescription, newPhotoName, charity.ID_Сharity);
                        MessageBox.Show("Организация успешно обновлена.");
                        charityTableAdapter.Fill(charityRows);
                        ControlCharity controlCharity = new ControlCharity();
                        controlCharity.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                        controlCharity.Show();
                        this.Close();
                    }
                }
                catch
                {
                    MessageBox.Show("Организация с таким именем уже существует.");
                }
            }
            else
            {
                if (charityRows.Where(c => c.Charity_Name.Equals(charityName)).Count() >= 1 && charityName != currentCharityName)
                {
                    MessageBox.Show("Организация с таким именем уже существует.");
                }
                else
                {
                    charityTableAdapter.CharityInsertQuery(charityName, charityDescription, newPhotoName);
                    MessageBox.Show("Организация успешно создана.");
                    charityTableAdapter.Fill(charityRows);
                    ControlCharity controlCharity = new ControlCharity();
                    controlCharity.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                    controlCharity.Show();
                    this.Close();
                }
            }
        }
    }
}
