using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataContext.DAL
{
    public class SuKien
    {
        public int id { get; set; }
        public string ghichu { get; set; }
        public string thoigianduyet { get; set; }
        public string tencuochop { get; set; }
        public string noidung { get; set; }
        public string diadiem { get; set; }
        public string ngaydienra { get; set; }
        public string thoigianbatdau { get; set; }
        public string thoigianketthuc { get; set; }
        public string avatar { get; set; }
    }
    public class ReturnSuKien
    {
        public List<SuKien> lstPost { get; set; }
        public string dataPage { get; set; }
    }
}