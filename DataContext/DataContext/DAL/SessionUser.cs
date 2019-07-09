using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataContext.DAL
{
    public class SessionUser
    {
        public int id { get; set; }
        public int idquyentaikhoan { get; set; }
        public int? idcoquan { get; set; }
        public string hovaten { get; set; }
        public string email { get; set; }
        public string sessionid { get; set; }
        public string ip { get; set; }
        public string Agent { get; set; }
        public string ComputerName { get; set; }
        public string Tooken { get; set; }
        public string avatar { get; set; }
        public int? typeAcc { get; set; }
        public int trangthaiduatin { get; set; }
    }
}