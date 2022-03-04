using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Karting.DataSetTableAdapters;
using Karting.Models;
using static Karting.DataSet;

namespace Karting
{
    class MainController
    {
        public static string IconPath = AppDomain.CurrentDomain.BaseDirectory + "AvatarIcon\\";

        #region CurrentUser

        public static User currentUser;

        public static void AuthorizeCurrentUser(UserRow user)
        {
            currentUser.Email = user.Email;
            currentUser.Password = user.Password;
            currentUser.RoleId = user.ID_Role;
        }

        #endregion

        public static string RACER_ROLE = "R";
        public static void FillTableRacerAddition() 
        {
            RacersAdditionTableAdapter racersAdditionTableAdapter = new RacersAdditionTableAdapter();
            RacersAdditionDataTable racersAdditionRows = new RacersAdditionDataTable();
            racersAdditionTableAdapter.Fill(racersAdditionRows);

            UserTableAdapter userTableAdapter = new UserTableAdapter();
            UserDataTable userRows = new UserDataTable();
            userTableAdapter.Fill(userRows);

            RacerTableAdapter racerTableAdapter = new RacerTableAdapter();
            RacerDataTable racerRows = new RacerDataTable();
            racerTableAdapter.Fill(racerRows);

            foreach(UserRow userRow in userRows)
            {
                if (userRow.ID_Role.Equals(RACER_ROLE))
                {
                    RacersAdditionRow racerAddition = racersAdditionRows.Where(rA => rA.UserEmail.Equals(userRow.Email)).FirstOrDefault();
                    RacerRow racer = racerRows.Where(r => r.First_Name.Equals(userRow.First_Name) && r.Last_Name.Equals(userRow.Last_Name)).FirstOrDefault();
                    if (racerAddition == null && racer != null)
                    {
                        racersAdditionTableAdapter.RacerAdditionInsertQuery(userRow.Email, racer.ID_Racer, "");
                    }
                }
            }
        }

        public static List<string> AgeCategoryList = new List<string>() { "до 18", "от 18 до 29", "от 30 до 39", "от 40 до 55", "от 56 до 70", "более 70" };

        public static bool RacerInCategory(int age, int CategoryNumber)
        {
            bool inCategory = false;
            if (age < 18 && CategoryNumber == 0)
            {
                inCategory = true;
            }
            else if (age >= 18 && age <= 29 && CategoryNumber == 1)
            {
                inCategory = true;
            }
            else if (age >= 30 && age <= 39 && CategoryNumber == 2)
            {
                inCategory = true;
            }
            else if (age >= 40 && age <= 55 && CategoryNumber == 3)
            {
                inCategory = true;
            }
            else if (age >= 56 && age <= 70 && CategoryNumber == 4)
            {
                inCategory = true;
            }
            else if (age > 70 && CategoryNumber == 5)
            {
                inCategory = true;
            }
            return inCategory;
        }
    }
}
