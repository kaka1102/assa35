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
    public class OverviewController : Controller
    {
        // GET: Overview
        public ActionResult Overview()
        {
            return PartialView();
        }
        public ActionResult SliderOverview()
        {
            return PartialView();
        }

        public ActionResult IntroduceOverview()
        {
            var context = new BS_HomeEn();
            var Result = context.ShowIntroduceOverview();

            return PartialView(Result);
        }

        public ActionResult AssaVietNam()
        {
            return PartialView();
        }

        public ActionResult IntroductionAssaVN()
        {
            var context = new BS_HomeEn();
            var Result = context.ShowIntroductionAssaVN();

            return PartialView(Result);
        }

    }
}