using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataContext.Socket
{
    public class SocketMessage
    {
        public int? Type { get; set; }
        public string Message { get; set; }
        public int? Id { get; set; }
        public string FullName { get; set; }
        public DateTime? SendDate { get; set; }
        public string avarta { get; set; }
    }
}