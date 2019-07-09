using DataContext.ClientBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASS_35.Controllers
{
    public class LienHeController : Controller
    {
        // GET: LienHe
        public ActionResult Index()
        {
            var context = new BS_TrangChu();
            var Result = context.ShowLienHe();

            return PartialView(Result);
        }
    }
}