using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataContext.DAL
{
    public class LstMenuClient
    {
        public string Version { get; set; }
        public List<MenuCL> lstMN { get; set; }
    }

    public class MenuCL
    {
        public int id { get; set; }
        public string tenmenu { get; set; }
        public string url { get; set; }
        public bool isActive { get; set; }
        public List<MenuCL> mnChil { get; set; }
    }

}