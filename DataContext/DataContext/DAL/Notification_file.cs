using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataContext.DAL
{
    public class Notification_file
    {
        public m_thongbao notification { get; set; }

        public List<m_filethongbao> lstFile { get; set; }
    }
}