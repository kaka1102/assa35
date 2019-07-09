using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataContext.DAL
{
    public class SystemMess
    {
        public bool? IsSuccess { get; set; }
        public string Message { get; set; }
        public string ReturnValue { get; set; }
        public int? Id { get; set; }
        public List<object> LstData { get; set; }

        [System.ComponentModel.DefaultValue(0)]
        public object ItemData { get; set; }
    }
}
