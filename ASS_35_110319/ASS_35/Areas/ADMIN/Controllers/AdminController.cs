using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataContext.Until;
using DataContext.Socket;
using DataContext.DAL;
using Newtonsoft.Json;
using DataContext.Constants;


namespace ASS_35.Areas.ADMIN.Controllers
{
    [CheckSessionUser]
    public class AdminController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LogoutAdmin()
        {
            SessionUser session = (SessionUser)HttpContext.Session["uSession"];
            List<kenhlamviec> klv = SocketHandler.klv;

            SocketHandler.users.Where(u => u.session == session).All(m =>
            {
                m.socket.Send(JsonConvert.SerializeObject(new { logout = true }));
                var ka1 = klv.Where(x => x.emaildangnhap == session.email && x.sessionid == session.sessionid).ToList();
                foreach (var item in ka1)
                {
                    klv.Remove(item);
                }
                return true;
            });

            var ka = klv.Where(x => x.emaildangnhap == session.email && x.sessionid == session.sessionid).ToList();
            foreach (var item in ka)
            {
                klv.Remove(item);
            }
            Session.Abandon();
            HttpContext.Session.Remove("uSession");
            return RedirectToAction("Index", "Admin");
        }

        public ActionResult LogoutAllTab()
        {
            SessionUser session = (SessionUser)HttpContext.Session["uSession"];
            List<kenhlamviec> klv = SocketHandler.klv;

            SocketHandler.users.Where(u => u.session == session).All(m =>
            {
                m.socket.Send(JsonConvert.SerializeObject(new { logout = true }));
                var ka1 = klv.Where(x => x.emaildangnhap == session.email && x.sessionid == session.sessionid).ToList();
                foreach (var item in ka1)
                {
                    klv.Remove(item);
                }
                return true;
            });

            var ka = klv.Where(x => x.emaildangnhap == session.email && x.sessionid == session.sessionid).ToList();
            foreach (var item in ka)
            {
                klv.Remove(item);
            }
            Session.Abandon();
            HttpContext.Session.Remove("uSession");

            return Json(new { success = true, msg = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.IsLogoutSuccess, SystemMessageConst.systemmessage.IsLogoutSuccessEn) }, JsonRequestBehavior.AllowGet);
        }

    }
}