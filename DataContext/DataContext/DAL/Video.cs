using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataContext.DAL
{
    public class Video
    {
        public int id { get; set; }
        public string tenchude { get; set; }
        public string tennguoitao { get; set; }
        public string gioithieu { get; set; }
        public string thoigianpheduyet { get; set; }
        public string link { get; set; }
    }
    public class ReturnData
    {
        public List<Video> lstMDA { get; set; }
        public string dataPage { get; set; }
    }
}