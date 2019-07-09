using DataContext.ClientBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace ASS_35.Controllers
{
    public class ContactController : Controller
    {
        // GET: Contact
        public ActionResult Index()
        {
            var context = new BS_HomeEn();
            var Result = context.ShowContact();

            return PartialView(Result);
        }
    }
}