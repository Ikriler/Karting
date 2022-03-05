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
    public partial class ControlUsers : Window
    {
        public ControlUsers()
        {
            InitializeComponent();
            DataController.StartTimerOnCurrentWindow(this.textBlock_DayXInfo, this.textBlock_DayXChanger);
            InitComboRole();
            InitUsersList();
            InitComboSort();
        }

        class UserData
        {
            public string email { get; set; }
            public string first_name { get; set; }
            public string last_name { get; set; }
            public string role { get; set; }
        }

        private void InitUsersList()
        {
            this.usersList.Items.Clear();

            UserTableAdapter userTableAdapter = new UserTableAdapter();
            UserDataTable userRows = new UserDataTable();
            userTableAdapter.Fill(userRows);

            RoleTableAdapter roleTableAdapter = new RoleTableAdapter();
            RoleDataTable roleRows = new RoleDataTable();
            roleTableAdapter.Fill(roleRows);

            foreach (UserRow row in userRows)
            {
                UserData userData = new UserData();
                userData.email = row.Email;
                userData.first_name = row.First_Name;
                userData.last_name = row.Last_Name;
                userData.role = roleRows.Where(r => r.ID_Role.Equals(row.ID_Role)).FirstOrDefault().Role_Name;
                this.usersList.Items.Add(userData);
            }
        }

        private void InitUsersListWithParameters(string filter = "", string sort = "", string search = "") 
        {
            this.usersList.Items.Clear();

            UserTableAdapter userTableAdapter = new UserTableAdapter();
            UserDataTable userRows = new UserDataTable();
            userTableAdapter.Fill(userRows);

            RoleTableAdapter roleTableAdapter = new RoleTableAdapter();
            RoleDataTable roleRows = new RoleDataTable();
            roleTableAdapter.Fill(roleRows);

            List<UserRow> users = new List<UserRow>();

            if(filter == "" || filter == "Все роли")
            {
                users = userRows.ToList();
            }
            else
            {
                string roleId = roleRows.Where(r => r.Role_Name.Equals(filter)).FirstOrDefault().ID_Role;

                users = userRows.Where(u => u.ID_Role.Equals(roleId)).ToList();
            }

            if (sort != "" && sort != "Без сортировки")
            {
                switch (sort)
                {
                    case "Имя":
                        users = users.OrderBy(uN => uN.First_Name).ToList();
                        break;
                    case "Фамилия":
                        users = users.OrderBy(uF => uF.Last_Name).ToList();
                        break;
                    case "Email":
                        users = users.OrderBy(uE => uE.Email).ToList();
                        break;
                    case "Роль":
                        users = users.OrderBy(uR => uR.ID_Role).ToList();
                        break;
                }
            }

            List<UserRow> userWithSearch = new List<UserRow>();
            if (search != "")
            {
                foreach(UserRow row in users)
                {
                    if(row.First_Name.Contains(search) || row.Last_Name.Contains(search) || row.Email.Contains(search))
                    {
                        userWithSearch.Add(row);
                    }
                }
            }
            else
            {
                userWithSearch = users;
            }


            foreach (UserRow row in userWithSearch)
            {
                UserData userData = new UserData();
                userData.email = row.Email;
                userData.first_name = row.First_Name;
                userData.last_name = row.Last_Name;
                userData.role = roleRows.Where(r => r.ID_Role.Equals(row.ID_Role)).FirstOrDefault().Role_Name;
                this.usersList.Items.Add(userData);
            }
        }

        private void InitComboRole()
        {
            RoleTableAdapter roleTableAdapter = new RoleTableAdapter();
            RoleDataTable roleRows = new RoleDataTable();
            roleTableAdapter.Fill(roleRows);

            foreach(RoleRow row in roleRows)
            {
                this.c_roles.Items.Add(row.Role_Name);
            }
            this.c_roles.Items.Add("Все роли");
        }

        private void InitComboSort()
        {
            this.c_sort.Items.Add("Имя");
            this.c_sort.Items.Add("Фамилия");
            this.c_sort.Items.Add("Email");
            this.c_sort.Items.Add("Роль");
            this.c_sort.Items.Add("Без сортировки");
        }

        private void go_back_Click(object sender, RoutedEventArgs e)
        {
            AdminPanel adminPanel = new AdminPanel();
            adminPanel.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            adminPanel.Show();
            this.Close();
        }

        private void logout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            mainWindow.Show();
            this.Close();
        }

        private void addNewUser_Click(object sender, RoutedEventArgs e)
        {
            AddNewUser addNewUser = new AddNewUser();
            addNewUser.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            addNewUser.Show();
            this.Close();
        }

        private void search_Click(object sender, RoutedEventArgs e)
        {
            string search = this.t_search.Text;
            string filter = "";
            string sort = "";

            if(this.c_roles.SelectedItem != null)
            {
                filter = this.c_roles.SelectedItem.ToString();
            }

            if(this.c_sort.SelectedItem != null)
            {
                sort = this.c_sort.SelectedItem.ToString();
            }

            InitUsersListWithParameters(filter, sort, search);
        }

        private void b_edit_Click(object sender, RoutedEventArgs e)
        {
            string userEmail = (sender as Button).Tag.ToString();
            EditUser editUser = new EditUser(userEmail);
            editUser.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            editUser.Show();
            this.Close();
        }
    }
}
