using DataContext.ClientBusiness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASS_35.Controllers
{
    public class HoiNghiController : Controller
    {
        // GET: HoiNghi
        public ActionResult Index()
        {
            return PartialView();
        }
        public ActionResult AssaVN()
        {
            return PartialView();
        }
        public ActionResult SlideTop()
        {
            var context = new BS_TrangChu();
            var Result = context.ShowSliderImage();

            return PartialView(Result);
        }
        public ActionResult GioiThieuHoiNghi()
        {
            var context = new BS_TrangChu();
            var Result = context.ShowGioiThieuHoiNghi();

            return PartialView(Result);
        }
        public ActionResult GioiThieuAssaVN()
        {
            var context = new BS_TrangChu();
            var Result = context.ShowGioiThieuAssaVN();

            return PartialView(Result);
        }
    }
}