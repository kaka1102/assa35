using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataContext.Until
{
    public class ParseDayToText
    {
        public static string Convert(string Date)
        {
            string dateReturn = Date.Substring(3, 2);

            if (dateReturn == "01")
            {
                dateReturn = Date.Remove(2, 4).Insert(2, " January ");
            }
            else if (dateReturn == "02")
            {
                dateReturn = Date.Remove(2, 4).Insert(2, " February ");
            }
            else if (dateReturn == "03")
            {
                dateReturn = Date.Remove(2, 4).Insert(2, " March ");
            }
            else if (dateReturn == "04")
            {
                dateReturn = Date.Remove(2, 4).Insert(2, " April ");
            }
            else if (dateReturn == "05")
            {
                dateReturn = Date.Remove(2, 4).Insert(2, " May ");
            }
            else if (dateReturn == "06")
            {
                dateReturn = Date.Remove(2, 4).Insert(2, " June ");
            }
            else if (dateReturn == "07")
            {
                dateReturn = Date.Remove(2, 4).Insert(2, " July ");
            }
            else if (dateReturn == "08")
            {
                dateReturn = Date.Remove(2, 4).Insert(2, " August ");
            }
            else if (dateReturn == "09")
            {
                dateReturn = Date.Remove(2, 4).Insert(2, " September ");
            }
            else if (dateReturn == "10")
            {
                dateReturn = Date.Remove(2, 4).Insert(2, " October ");
            }
            else if (dateReturn == "11")
            {
                dateReturn = Date.Remove(2, 4).Insert(2, " November ");
            }
            else if (dateReturn == "12")
            {
                dateReturn = Date.Remove(2, 4).Insert(2, " December ");
            }
            else
            {
                dateReturn = Date;
            }
            return dateReturn;
        }

        internal static object Convert(DateTime i)
        {
            throw new NotImplementedException();
        }
    }
}