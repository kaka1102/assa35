using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataContext.DAL
{
    public class ListMenu
    {
        public int id { get; set; }
        public string tendanhmuc { get; set; }
        public string url { get; set; }
        public string physical { get; set; }
        public int ismenu { get; set; }
        public int stt { get; set; }
        public string icon { get; set; }
        public int idcha { get; set; }
        public int trangthai { get; set; }
        public List<ListMenu> lstMn { get; set; }
    }


}