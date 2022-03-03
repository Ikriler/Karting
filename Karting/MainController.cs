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
    }
}
