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
    public class LoadMenuClientController : Controller
    {
        // GET: LoadMenuClient
        public ActionResult Index()
        {
            var context = new BS_LoadMenuClient();
            var Result = context.MenuClient();

            @ViewBag.TypeWebsite = Result.Version;
            return PartialView(Result);
        }

        public ActionResult vi()
        {
            //if (Version != null)
            //{
            //    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(Version);
            //    Thread.CurrentThread.CurrentUICulture = new CultureInfo(Version);
            //}
            //if (Request.UrlReferrer == null)
            //{
            //    return RedirectToAction("Language", "Index");
            //}
            //var currentlink = Request.Url.Authority;

            HttpCookie cookie = new HttpCookie("Version");
            cookie.Value = "vi";
            cookie.Expires = DateTime.Now.AddDays(1d);
            Response.Cookies.Add(cookie);

            //string path = "";
            //if (Version == "vi")
            //{
            //    path = "trangchu";
            //}
            //else
            //{
            //    path = "HomeEn";
            //}
            //currentlink = currentlink + "trangchu";
            return RedirectToRoute("trangchu");
        }
        public ActionResult en()
        {
            //if (Version != null)
            //{
            //    Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(Version);
            //    Thread.CurrentThread.CurrentUICulture = new CultureInfo(Version);
            //}
            //if (Request.UrlReferrer == null)
            //{
            //    return RedirectToAction("Language", "Index");
            //}
            //var currentlink = Request.Url.Authority;

            HttpCookie cookie = new HttpCookie("Version");
            cookie.Value = "en";
            cookie.Expires = DateTime.Now.AddDays(1d);
            Response.Cookies.Add(cookie);

            //string path = "";
            //if (Version == "vi")
            //{
            //    path = "trangchu";
            //}
            //else
            //{
            //    path = "HomeEn";
            //}
            //currentlink = currentlink + "trangchu";
            return RedirectToRoute("HomeEn");
        }


    }
}