using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Karting
{
    class Validations
    {
        public static bool StringIsNotEmpty(string chcekString) 
        {
            if(chcekString.Trim().Equals("") || chcekString.Equals(String.Empty))
            {
                return false;
            }
            return true;
        }

        public static bool CheckMail(string email) 
        {
            Regex regex = new Regex(@"(.+)(@{1})(.+)(\.{1})(.+)");
            if(regex.IsMatch(email))
            {
                return true;
            }
            return false;
        }

        public static bool CheckPassword(string password)
        {
            Regex regexNumber = new Regex(@"[0-9]+");
            Regex regexLowChar = new Regex(@"[A-Z]+");
            Regex regexSpecChar = new Regex(@"[:!@#$%^]+");
            int Passlength = 6;
            bool check = true;

            if (!regexNumber.IsMatch(password))
            {
                check = false;
            }
            if (!regexLowChar.IsMatch(password))
            {
                check = false;
            }
            if (!regexSpecChar.IsMatch(password))
            {
                check = false;
            }
            if (password.Length < Passlength)
            {
                check = false;
            }

            return check;
        }
    }
}
