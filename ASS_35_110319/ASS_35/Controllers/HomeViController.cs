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
    public class HomeViController : Controller
    {
        // GET: HomeVi
        public ActionResult Index()
        {
            return PartialView();
        }

        public ActionResult ContentVideo()
        {
            var context = new BS_TrangChu();
            var Result = context.ShowVideoHotTrangChu();

            return PartialView(Result);
        }
        public ActionResult ContentPost()
        {
            return PartialView();
        }
        public ActionResult TinTuc()
        {
            var context = new BS_TrangChu();
            var Result = context.ShowTinTuc();

            return PartialView(Result);
        }
        public ActionResult SuKien()
        {
            var context = new BS_TrangChu();
            var Result = context.ShowSuKien();

            return PartialView(Result);
        }

        public ActionResult SliderVideo()
        {
            var context = new BS_TrangChu();
            var Result = context.ShowSliderVideo();

            return PartialView(Result);
        }

        public ActionResult SliderImage()
        {
            var context = new BS_TrangChu();
            var Result = context.ShowSliderImage();

            return PartialView(Result);
        }
        public ActionResult SliderTop()
        {
            var context = new BS_TrangChu();
            var Result = context.ShowSlidePost();

            return PartialView(Result);
        }
        public ActionResult SliderImageTop()
        {
            var context = new BS_TrangChu();
            var Result = context.ShowSlideImage();

            return PartialView(Result);
        }

    }
}