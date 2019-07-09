using DataContext.ClientBusiness;
using DataContext.Until;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASS_35.Controllers
{
    public class PracticalController : Controller
    {
        // GET: Practical
        public ActionResult ChangePractical(string tab)
        {
            //string Version = DisplayUntils.GetPracticalTab();
            //ViewBag.Ver = Version;
            
            return PartialView();
        }

        public ActionResult ChangeTabPractical(string tab)
        {
            HttpCookie cookie = new HttpCookie("tab");
            cookie.Value = tab;
            cookie.Expires = DateTime.Now.AddDays(1d);
            Response.Cookies.Add(cookie);

            return RedirectToRoute("practical");
        }

        public ActionResult Venue()
        {
            var Context = new BS_HomeEn();
            var Result = Context.ShowIntroduceVenue();
            return PartialView(Result);
        }
        public ActionResult Custom()
        {
            var Context = new BS_HomeEn();
            var Result = Context.ShowIntroduceCustom();
            return PartialView(Result);
        }
        public ActionResult Flight()
        {
            var Context = new BS_HomeEn();
            var Result = Context.ShowIntroduceFlightInformation();
            return PartialView(Result);
        }
        public ActionResult General()
        {
            var Context = new BS_HomeEn();
            var Result = Context.ShowIntroduceGeneral();
            return PartialView(Result);
        }

    }
}