using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataContext.Until;
using System.Threading;
using System.Globalization;
using DataContext.ClientBusiness;


namespace ASS_35.Controllers
{
    public class DocumentEnController : Controller
    {
        public ActionResult Change()
        {
            string Version = DisplayUntils.GetDocumentTab();
            ViewBag.Ver = Version;
            return PartialView();
        }

        public ActionResult ChangeVersionDocument(string type)
        {
            HttpCookie cookie = new HttpCookie("type");
            cookie.Value = type;
            cookie.Expires = DateTime.Now.AddDays(1d);
            Response.Cookies.Add(cookie);

            return RedirectToRoute("document");
        }
        public ActionResult Agenda()
        {
            var Context = new BS_HomeEn();
            var Result = Context.ShowIntroduceAgenda();
            return PartialView(Result);
        }
      

        public ActionResult PressRelease()
        {
            int page = 0;
            int.TryParse(HttpContext.Request["page"], out page);

            if (page == 0)
            {
                page = 1;
            }
            var Context = new BS_HomeEn();
            var Result = Context.ShowIntroducePressRealease(page);
            return PartialView(Result);
        }
        public ActionResult NewsAndEvents()
        {
            int page = 0;
            int.TryParse(HttpContext.Request["page"], out page);

            if (page == 0)
            {
                page = 1;
            }
            var Context = new BS_HomeEn();
            var Result = Context.ShowIntroducePressRealease(page);
            return View(Result);
        }
        public ActionResult Report()
        {
            var Context = new BS_HomeEn();
            var Result = Context.ShowIntroduceReportAndDocument();
            return PartialView(Result);
        }

        public ActionResult DetailsNew()
        {
            int id = 0;

            string key = (string)this.RouteData.Values["id"];
            int.TryParse(key, out id);

            var context = new BS_HomeEn();
            var Result = context.DetailsNew(id);
            return PartialView(Result);
        }

        public ActionResult NewsInvolve()
        {
            int id = 0;

            string key = (string)this.RouteData.Values["id"];
            int.TryParse(key, out id);

            var context = new BS_HomeEn();
            var Result = context.ShowListNewsInvolve(id);
            return PartialView(Result);
        }

        public ActionResult Portfolio()
        {
            string Version = DisplayUntils.GetPortfolioTab();
            ViewBag.Type = Version;
            return PartialView();
        }
        public ActionResult ChangeTypePortfolio(string type)
        {
            HttpCookie cookie = new HttpCookie("Type_Speaker");
            cookie.Value = type;
            cookie.Expires = DateTime.Now.AddDays(1d);
            Response.Cookies.Add(cookie);

            return RedirectToRoute("document");
        }
        public ActionResult GUESTSPEAKER()
        {
            var Context = new BS_HomeEn();
            var Result = Context.ShowListPortfolio("GUESTSPEAKER");
            return PartialView(Result);
        }
        public ActionResult KEYNOTESPEAKER()
        {
            var Context = new BS_HomeEn();
            var Result = Context.ShowListPortfolio("KEYNOTESPEAKER");
            return PartialView(Result);
        }
        public ActionResult PANELIST()
        {
            var Context = new BS_HomeEn();
            var Result = Context.ShowListPortfolio("PANELIST");
            return PartialView(Result);
        }
    }
}