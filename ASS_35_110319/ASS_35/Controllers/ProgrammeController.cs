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
    public class ProgrammeController : Controller
    {
        // GET: Programme
        public ActionResult Index()
        {
            var context = new BS_HomeEn();
         //   var Result = context.ShowListProgramme();
            var Result = context.GetListDateMeeting();
            return PartialView(Result);
        }

        public JsonResult Test()
        {
            var context =new  BS_HomeEn();
            var res = context.Test();
            return Json(res);
        }

    }
}