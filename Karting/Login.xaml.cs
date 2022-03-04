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
    /// <summary>
    /// Логика взаимодействия для Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            DataController.StartTimerOnCurrentWindow(this.textBlock_DayXInfo, this.textBlock_DayXChanger);
        }

        private void Canel_Click(object sender, RoutedEventArgs e)
        {
            new MainWindow().Show();
            this.Close();
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            string email = this.t_email.Text;
            string password = this.t_password.Text;

            UserTableAdapter userTableAdapter = new UserTableAdapter();
            UserDataTable userRows = new UserDataTable();
            userTableAdapter.Fill(userRows);

            UserRow user = userRows.Where(u => u.Email.Equals(email) && u.Password.Equals(password)).FirstOrDefault();
            if(user != null)
            {
                MainController.AuthorizeCurrentUser(user);
                string role = user.ID_Role;
                switch (role)
                {
                    case "R":
                        RacerPanel racerPanel = new RacerPanel();
                        racerPanel.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                        racerPanel.Show();
                        this.Close();
                        break;
                    case "A":
                        AdminPanel adminPanel = new AdminPanel();
                        adminPanel.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                        adminPanel.Show();
                        this.Close();
                        break;
                    case "C":
                        CoordinatorPanel coordinatorPanel = new CoordinatorPanel();
                        coordinatorPanel.WindowStartupLocation = WindowStartupLocation.CenterScreen;
                        coordinatorPanel.Show();
                        this.Close();
                        break;
                }
            }
            else
            {
                MessageBox.Show("Такого пользователя не существует.");
            }
        }
    }
}
