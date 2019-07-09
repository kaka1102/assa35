using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataContext;
using DataContext.AdminBusiness;
using System.Data;
using Newtonsoft.Json.Linq;
using DataContext.Until;


namespace ASS_35.Areas.ADMIN.Controllers
{
    [CheckSessionUser]
    public class MenuAdminController : Controller
    {
        public ActionResult Index()
        {
            var context = new MenuAdminBusiness();
            var Result = context.LoadMenuAdminByUser();

            return PartialView(Result);
        }
    }
}