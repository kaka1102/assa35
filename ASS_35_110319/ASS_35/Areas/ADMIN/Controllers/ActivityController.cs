using DataContext.AdminBusiness;
using DataContext.Until;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASS_35.Areas.ADMIN.Controllers
{
    [CheckSessionUser]
    public class ActivityController : Controller
    {

        // GET: ADMIN/Activity
        public ActionResult ListActivity()
        {
            return PartialView();
        }
        public ActionResult _ListActivity(int status = -1)
        {
            var db = new ActivityBusiness();
            var result = db.GetListActivity(status);
            return PartialView(result);
        }
        public ActionResult AddActivity()
        {
            return PartialView();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult _AddActivity()
        {
            return PartialView();
        }
    }
}