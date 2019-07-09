using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataContext.DAL
{
    public class ThongKe
    {
        public int IDTAIKHOAN { get; set; }
        public int IDCOQUAN { get; set; }
        public string EMAILTAIKHOAN { get; set; }
        public int IDTHONGTIN { get; set; }

        public string TENCOQUAN { get; set; }
        public string LOAITK { get; set; }
        public int IDLOAITK { get; set; }
        public int IDQUYEN { get; set; }
        public string TENQUYEN { get; set; }
        public string HOVATEN { get; set; }
        public string EMAILCANHAN { get; set; }
        public string DIACHI { get; set; }
        public string DIACHICOQUAN { get; set; }
        public string EMAILCOQUAN { get; set; }
        public string THOIGIANDANGKY { get; set; }
        public string DANGKYPHONG { get; set; }
        public string DANGKYXE { get; set; }

    }


   
}