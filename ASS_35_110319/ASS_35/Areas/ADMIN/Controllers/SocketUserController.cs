using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Web.WebSockets;
using DataContext.DAL;
using DataContext.Socket;
using System.Net.Sockets;
using DataContext.Until;

namespace ASS_35.Areas.ADMIN.Controllers
{
    public class SocketUserController : Controller
    {
        // GET: ADMIN/SocketUser
        public ActionResult SocketProcessRequest()
        {
            if (HttpContext.IsWebSocketRequest)
            {
                SocketTypeMessages socketMess = new SocketTypeMessages();
                socketMess.Type = 1;
                socketMess.Message = "Check login";
                string message = DisplayUntils.ConvertObjectToStringJson(socketMess);


                var session = (SessionUser)HttpContext.Session["uSession"];
                HttpContext.AcceptWebSocketRequest(new SocketHandler(session, message));
            }
            return null;
        }
    }
}