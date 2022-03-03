using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    }
}
