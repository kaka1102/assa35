using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataContext.DAL
{

   

    public class ListMedia
    {
        public int id { get; set; }
        public string tenchude { get; set; }
        public string tennguoitao { get; set; }
        public string gioithieu { get; set; }
        public string loaitl { get; set; }
        public string thoigianpheduyet { get; set; }
        public List<media> lstMedia { get; set; }
    }
    public class media
    {
        public int id { get; set; }
        public string duongdanfile { get; set; }
        public string tenfile { get; set; }
        public string trichdan { get; set; }
      
    }

    public class ImageCaption
    {
        public int id { get; set; }
        public string caption { get; set; }
    }
}