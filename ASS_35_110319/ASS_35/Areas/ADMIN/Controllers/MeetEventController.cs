using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASS_35.Areas.ADMIN.Controllers
{
    public class MeetEventController : Controller
    {
        // GET: ADMIN/MeetEvent
        public ActionResult ListMeetEvent()
        {
            return PartialView();
        }
        public ActionResult _ListMeetEvent()
        {
            return PartialView();
        }
    }
}