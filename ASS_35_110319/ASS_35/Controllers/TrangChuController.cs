using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataContext.Until;

namespace ASS_35.Controllers
{
    public class TrangChuController : Controller
    {
        // GET: TrangChu
        public ActionResult View()
        {
            string Version = DisplayUntils.GetVersionValues();
            ViewBag.Ver = Version;
            return PartialView();
        }
    }
}