using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using DataContext.DAL;
using DataContext.Until;
using DataContext.Socket;
using Newtonsoft.Json;
using System.Web.UI;
using System.Web;
using System.Web.Routing;


namespace DataContext.Until
{
    public class CheckSessionUser : ActionFilterAttribute
    {
        SessionUser session = new SessionUser();

        List<kenhlamviec> klv = SocketHandler.klv;
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            string ip = HttpContext.Current.Request.UserHostAddress;
            string Agent = HttpContext.Current.Request.UserAgent;
            string ComputerName = System.Environment.MachineName;

            string sessionDefaulf = HttpContext.Current.Session.SessionID;
            session = (SessionUser)filterContext.HttpContext.Session["uSession"];

            var checkSessionID = klv.Where(m => m.sessionid == sessionDefaulf).FirstOrDefault();

            if (session == null)
            {
                // filterContext.Result = new RedirectResult("/ADMIN/Login/Index");
                filterContext.Result = new RedirectResult("/Login/CheckSession");
            }
            else if (session != null && string.IsNullOrEmpty(session.idquyentaikhoan.ToString()) && string.IsNullOrEmpty(session.Tooken))
            {
                filterContext.HttpContext.Session.Abandon();
                filterContext.HttpContext.Session.Remove("uSession");
                filterContext.Result = new RedirectResult("/Login/Index");
            }
            else if (checkSessionID != null && (checkSessionID.Agent != Agent || checkSessionID.ip != ip || checkSessionID.ComputerName != ComputerName))
            {
                SocketHandler.users.Where(m => m.session.email == session.email && (m.socket.Agent != Agent || m.socket.ip != ip || m.socket.ComputerName != ComputerName)).All(m =>
                {
                    m.socket.Send(JsonConvert.SerializeObject(new { coppySessionId = true }));
                    return true;
                });
                //SocketHandler.users.Where(m => m.session.email == session.email && m.socket.Agent == Agent && m.socket.ip == ip && m.socket.ComputerName == ComputerName).All(m =>
                //{
                //    m.socket.Send(JsonConvert.SerializeObject(new { coppySessionIdFail = true }));
                //    SocketHandler.users.Remove(m);
                //    return true;
                //});
                filterContext.Result = new RedirectResult("/Login/Index");
            }
        }

    }
}
