using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace DataContext.Until
{
    public class CheckDateTime
    {
        public static bool ValidateDate_Start(string date1, DateTime date2)
        {
            bool check = false;
            DateTime dd1 = DateTime.Now;
            dd1 = DateTime.ParseExact(date1, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (dd1 >= date2)
            {
                check = true;
            }
            return check;
        }

        public static bool ValidateDate_End(string date1, DateTime date2)
        {
            bool check = false;
            DateTime dd1 = DateTime.Now;
            dd1 = DateTime.ParseExact(date1, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            if (dd1 <= date2)
            {
                check = true;
            }
            return check;
        }
    }
}