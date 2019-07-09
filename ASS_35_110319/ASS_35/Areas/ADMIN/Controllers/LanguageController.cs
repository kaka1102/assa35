using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace ASS_35.Areas.ADMIN.Controllers
{
    public class LanguageController : Controller
    {
        //public ActionResult Change(string LanguageAbbrevation)
        //{
        //    if (LanguageAbbrevation != null)
        //    {
        //        Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(LanguageAbbrevation);
        //        Thread.CurrentThread.CurrentUICulture = new CultureInfo(LanguageAbbrevation);
        //    }
        //    if (Request.UrlReferrer == null)
        //    {
        //        return RedirectToAction("Language", "Index");
        //    }
        //    var currentlink = Request.UrlReferrer.AbsoluteUri;

        //    HttpCookie cookie = new HttpCookie("Language");
        //    cookie.Value = LanguageAbbrevation;
        //    cookie.Expires = DateTime.Now.AddDays(1d);
        //    Response.Cookies.Add(cookie);
        //    return Redirect(currentlink);
        //}

        public JsonResult Change(string LanguageAbbrevation)
        {
            if (LanguageAbbrevation != null)
            {
                Thread.CurrentThread.CurrentCulture = CultureInfo.CreateSpecificCulture(LanguageAbbrevation);
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(LanguageAbbrevation);
            }
          
            HttpCookie cookie = new HttpCookie("Language");
            cookie.Value = LanguageAbbrevation;
            cookie.Expires = DateTime.Now.AddDays(1d);
            Response.Cookies.Add(cookie);
            return Json(new { data = true }, JsonRequestBehavior.AllowGet);
        }
    }
}