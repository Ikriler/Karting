using Karting.DataSetTableAdapters;
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
using static Karting.DataSet;

namespace Karting
{
    public partial class AddNewUser : Window
    {
        public AddNewUser()
        {
            InitializeComponent();
            DataController.StartTimerOnCurrentWindow(this.textBlock_DayXInfo, this.textBlock_DayXChanger);
            InitComboRoles();
        }

        private void InitComboRoles()
        {
            RoleTableAdapter roleTableAdapter = new RoleTableAdapter();
            RoleDataTable roleRows = new RoleDataTable();
            roleTableAdapter.Fill(roleRows);

            foreach (RoleRow row in roleRows)
            {
                if(row.Role_Name != "Racer")
                this.c_roles.Items.Add(row.Role_Name);
            }
        }

        private void back_Click(object sender, RoutedEventArgs e)
        {
            ControlUsers controlUsers = new ControlUsers();
            controlUsers.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            controlUsers.Show();
            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            ControlUsers controlUsers = new ControlUsers();
            controlUsers.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            controlUsers.Show();
            this.Close();
        }

        private void EditConfirm_Click(object sender, RoutedEventArgs e)
        {
            string email = this.t_email.Text;
            string first_name = this.t_name.Text;
            string last_name = this.t_lastName.Text;
            string password = this.t_pass.Text;
            string rePassword = this.t_rePass.Text;


            string role = "";
            if(this.c_roles.SelectedItem != null)
            {
                role = this.c_roles.SelectedItem.ToString();
            }
            else
            {
                MessageBox.Show("Выберите роль.");
                return;
            }

            #region VALIDATION

            if (!(Validations.StringIsNotEmpty(email) && 
                Validations.StringIsNotEmpty(password) &&
                Validations.StringIsNotEmpty(rePassword) &&
                Validations.StringIsNotEmpty(first_name) &&
                Validations.StringIsNotEmpty(last_name)))
            {
                MessageBox.Show("Поля не должны быть пустыми.");
                return;
            }

            if (!Validations.CheckMail(email))
            {
                MessageBox.Show("Неправильный email.");
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
            #endregion

            UserTableAdapter userTableAdapter = new UserTableAdapter();
            UserDataTable userRows = new UserDataTable();
            userTableAdapter.Fill(userRows);

            UserRow checkUser = userRows.Where(u => u.Email.Equals(email)).FirstOrDefault();
            if (checkUser != null)
            {
                MessageBox.Show("Пользователь с такой почтой уже существует.");
                return;
            }

            RoleTableAdapter roleTableAdapter = new RoleTableAdapter();
            RoleDataTable roleRows = new RoleDataTable();
            roleTableAdapter.Fill(roleRows);

            string roleId = roleRows.Where(r => r.Role_Name.Equals(role)).FirstOrDefault().ID_Role;

            userTableAdapter.Insert(email, password, first_name, last_name, roleId);

            MessageBox.Show("Пользователь успешно добавлен.");

            ControlUsers controlUsers = new ControlUsers();
            controlUsers.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            controlUsers.Show();
            this.Close();
        }
    }
}
