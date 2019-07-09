using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataContext.DAL
{
    public class Statistical
    {
        public class Reporter
        {
            public int id { get; set; }
            public string email { get; set; }
            public string hovaten { get; set; }
            public string ngaytao { get; set; }
            public string tenloaitaikhoan { get; set; }
            public string tencoquan { get; set; }
            public string diachicoquan { get; set; }
            public string emailcoquan { get; set; }
            public string sodienthoai { get; set; }
           
            public List<thongtincanhan> thongtincanhan { get; set; }
        }

        public class thongtincanhan
        {
            public int id { get; set; }
            public string hovaten { get; set; }
            public string chucvu { get; set; }
            public string email { get; set; }
            public string diachi { get; set; }
            public string sodienthoai { get; set; }
            public string dangkyxe { get; set; }
            public string dangkyphong { get; set; }
        }
    }
}