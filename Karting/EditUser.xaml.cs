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
    public partial class EditUser : Window
    {
        public string editingUserEmail = "";
        public EditUser(string userEmail = "")
        {
            InitializeComponent();
            DataController.StartTimerOnCurrentWindow(this.textBlock_DayXInfo, this.textBlock_DayXChanger);
            InitComboRoles();
            editingUserEmail = userEmail;
            InitFields();
        }

        private void InitComboRoles()
        {
            RoleTableAdapter roleTableAdapter = new RoleTableAdapter();
            RoleDataTable roleRows = new RoleDataTable();
            roleTableAdapter.Fill(roleRows);

            foreach(RoleRow row in roleRows)
            {
                this.c_roles.Items.Add(row.Role_Name);
            }
        }

        private void InitFields()
        {
            if (editingUserEmail == "") return;

            RoleTableAdapter roleTableAdapter = new RoleTableAdapter();
            RoleDataTable roleRows = new RoleDataTable();
            roleTableAdapter.Fill(roleRows);

            UserTableAdapter userTableAdapter = new UserTableAdapter();
            UserDataTable userRows = new UserDataTable();
            userTableAdapter.Fill(userRows);

            UserRow editingUser = userRows.Where(eu => eu.Email.Equals(editingUserEmail)).FirstOrDefault();

            this.l_email.Content = editingUser.Email;
            this.t_name.Text = editingUser.First_Name;
            this.t_lastName.Text = editingUser.Last_Name;
            this.t_pass.Text = editingUser.Password;
            this.t_rePass.Text = editingUser.Password;

            string roleName = roleRows.Where(r => r.ID_Role.Equals(editingUser.ID_Role)).FirstOrDefault().Role_Name;

            this.c_roles.SelectedItem = roleName;

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
            string first_name = this.t_name.Text;
            string last_name = this.t_lastName.Text;
            string password = this.t_pass.Text;
            string rePassword = this.t_rePass.Text;

            #region VALIDATION

            if (!(Validations.StringIsNotEmpty(password) &&
                Validations.StringIsNotEmpty(rePassword) &&
                Validations.StringIsNotEmpty(first_name) &&
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
            #endregion

            UserTableAdapter userTableAdapter = new UserTableAdapter();
            UserDataTable userRows = new UserDataTable();
            userTableAdapter.Fill(userRows);

            userTableAdapter.UserRacerUpdateQuery(password, first_name, last_name, editingUserEmail);

            MessageBox.Show("Пользователь успешно изменен.");

            ControlUsers controlUsers = new ControlUsers();
            controlUsers.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            controlUsers.Show();
            this.Close();

        }
    }
}
