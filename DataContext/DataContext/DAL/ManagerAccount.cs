using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataContext.DAL
{
    public class ManagerAccount
    {
        public List<ManagerAccount_Result> Data { get; set; }

        public int TotalRecort { get; set; }
    }
}