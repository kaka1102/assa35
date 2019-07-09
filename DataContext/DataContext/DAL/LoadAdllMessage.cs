using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataContext.DAL
{
    public class LoadAdllMessage
    {
        public List<object> LstData { get; set; }
        public int? draw { get; set; }
        public int? maxrow { get; set; }
        public int? minrow { get; set; }
        public bool? IsSuccess { get; set; }
        public string Message { get; set; }
        public int? recordsFiltered { get; set; }
        public int? recordsTotal { get; set; }
    }
}