using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataContext.DAL
{
    public class AllDocument
    {
        public int id { get; set; }
        public string tenchude { get; set; }
        public string thoigian { get; set; }
        public DateTime thoigiantao { get; set; }
        public string tennguoitao { get; set; }
        public int trangthai { get; set; }
        public string trangthaihienthi { get; set; }
        public string tennguoipheduyet { get; set; }
        public string ghichu { get; set; }
        public string gioithieu { get; set; }
        public string thoigianpheduyet { get; set; }
        public int phienban { get; set; }
        public string lydo { get; set; }
        public List<ChiTietDocument> chitiet { get; set; }
    }
    public class ChiTietDocument
    {
        public int id { get; set; }
        public string duongdanfile { get; set; }
        public string ngaytao { get; set; }
        public string tenfile { get; set; }
        public string caption { get; set; }
        public string kieufile { get; set; }
        public int size { get; set; }
    }

    public class ReturnDataDocument
    {
        public List<AllDocument> data { get; set; }
        public int totalCount { get; set; }
    }
}