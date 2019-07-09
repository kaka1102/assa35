using DataContext.ClientBusiness;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using DataContext.Until;

namespace ASS_35.Controllers
{
    public class HomeEnController : Controller
    {
        // GET: HomeEn
        public ActionResult Index()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture("en");
            Thread.CurrentThread.CurrentUICulture = new CultureInfo("en");
            HttpCookie cookie = new HttpCookie("Language");
            cookie.Value = "en";
            cookie.Expires = DateTime.Now.AddDays(1d);
            Response.Cookies.Add(cookie);
            if (Request.Cookies["Version"] != null)
            {
                var c = new HttpCookie("Version");
                c.Expires = DateTime.Now.AddDays(-1);
                Response.Cookies.Add(c);
            }else
            {
                HttpCookie cookieVer = new HttpCookie("Version");
                cookieVer.Value = "en";
                cookieVer.Expires = DateTime.Now.AddDays(1d);
                Response.Cookies.Add(cookieVer);
            }

            return View();
        }
        public ActionResult SlideTopEn()
        {
            var context = new BS_HomeEn();
            var Result = context.ShowSlideVenueEn();

            return PartialView(Result);
        }
        public ActionResult IntroduceHome()
        {
            var context = new BS_HomeEn();
            var Result = context.ShowIntroduceHome();

            return PartialView(Result);
        }
    }
}