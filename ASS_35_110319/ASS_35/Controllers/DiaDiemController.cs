using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataContext.ClientBusiness;
namespace ASS_35.Controllers
{
    public class DiaDiemController : Controller
    {
        // GET: DiaDiem
        public ActionResult Index()
        {
            var Context = new BS_TrangChu();
            var Result = Context.ShowListDiadiem();
            return PartialView(Result);
        }

        public ActionResult ChiTietDiaDiem()
        {

            int id = 0;

            string key = (string)this.RouteData.Values["id"];
            int.TryParse(key, out id);

            var context = new BS_TrangChu();
            var Result = context.ChiTietDiaDiem(id);
            return PartialView(Result);
        }
    }
}