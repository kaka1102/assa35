using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataContext.DAL
{
    public class ListProgramme
    {
        public List<Programmer> lstNgay1 { get; set; }
        public List<Programmer> lstNgay2 { get; set; }
        public List<Programmer> lstNgay3 { get; set; }
        public string DateToDate { get; set; }
        public string Date1 { get; set; }
        public string Date2 { get; set; }
        public string Date3 { get; set; }
    }

    public class Programmer
    {
        public int id { get; set; }
        public string ghichu { get; set; }
        public string tencuochop { get; set; }
        public string noidung { get; set; }
        public string ngaydienra { get; set; }
        public string thoigianbatdau { get; set; }
        public string thoigianketthuc { get; set; }
        public string avatar { get; set; }

    }
}