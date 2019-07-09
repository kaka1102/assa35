using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataContext.DAL
{


    public class DetailsPost
    {
        public int id { get; set; }
        public string tieude { get; set; }
        public string gioithieu { get; set; }
        public string noidung { get; set; }
        public string tacgia { get; set; }
        public string avatar { get; set; }
        public string thoigianpheduyet { get; set; }
        public string tennguoitao { get; set; }

    }
    public class ReturnPost
    {
        public List<DetailsPost> lstPost { get; set; }
        public string dataPage { get; set; }
    }

   
}