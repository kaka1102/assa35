using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataContext.DAL
{
    public class AllPost
    {
        public int id { get; set; }
        public string tieude { get; set; }
        public string gioithieu { get; set; }
        public string noidung { get; set; }
        public string tacgia { get; set; }
        public string avatar { get; set; }
        public string thoigianpheduyet { get; set; }
        public string tennguoitao { get; set; }
        public string ngaytao { get; set; }
        public DateTime thoigiantao { get; set; }
        public int phienban { get; set; }
        public int trangthai { get; set; }
        public string lydo { get; set; }
        public string trangthaibaiviet { get; set; }
    }
    public class ReturnDataPost
    {
        public List<AllPost> data { get; set; }
        public int totalCount { get; set; }
    }
}