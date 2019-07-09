using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataContext.DAL
{
    public class AccountInfo
    {
        public int id { get; set; }

        public string email { get; set; }
        public string fullname { get; set; }

        public int type { get; set; }
    }
}