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
    public class VideoHinhAnhController : Controller
    {
        // GET: VideoHinhAnh
        public ActionResult Index()
        {
            //int moidang = 0;
            //int xemnhieu = 0;
            //int page = 1;
            //int.TryParse(HttpContext.Request["moidang"], out moidang);
            //int.TryParse(HttpContext.Request["xemnhieu"], out xemnhieu);
            //int.TryParse(HttpContext.Request["page"], out page);

            //if (page == 0)
            //{
            //    page = 1;
            //}
            //var context = new BS_TrangChu();
            //var Result = context.GetListImage(moidang, xemnhieu, page);
            //return PartialView(Result);
            return PartialView();
        }

        public ActionResult ContentImage()
        {
            int moidang = 0;
            int xemnhieu = 0;
            int page = 1;
            string curPage="";
            int.TryParse(HttpContext.Request["moidang"], out moidang);
            int.TryParse(HttpContext.Request["xemnhieu"], out xemnhieu);
            int.TryParse(HttpContext.Request["page"], out page);
            curPage = HttpContext.Request["curpage"];
            if (page == 0)
            {
                page = 1;
            }
            var context = new BS_TrangChu();
            var Result = context.GetListImage(moidang, xemnhieu, page, curPage);
            //if(Result.lstMDA == null)
            //{
            //    return RedirectToAction;
            //}else
            //{
                return View(Result);
            //}
            
        }
        public ActionResult ContentDetailImage(string page , string moidang, string xemnhieu)
        {
            int _moidang = 0;
            int _xemnhieu = 0;
            int _page = 1;
            string _curpage = "";
            int.TryParse(moidang, out _moidang);
            int.TryParse(xemnhieu, out _xemnhieu);
            int.TryParse(page, out _page);
           string curPage = HttpContext.Request["curpage"];
            
            if (_page == 0)
            {
                _page = 1;
            }
            var context = new BS_TrangChu();
            var Result = context.GetListImage(_moidang, _xemnhieu, _page, curPage);

            return View(Result);
        }

        public ActionResult TopLibraryImage()
        {
            var context = new BS_TrangChu();
            var Result = context.ShowImageHot1();

            return PartialView(Result);
        }


        public ActionResult Video()
        {
            return PartialView();
        }

        public ActionResult ContentVideo()
        {
            int moidang = 0;
            int xemnhieu = 0;
            int page = 1;
            int.TryParse(HttpContext.Request["moidang"], out moidang);
            int.TryParse(HttpContext.Request["xemnhieu"], out xemnhieu);
            int.TryParse(HttpContext.Request["page"], out page);
          string  curPage = HttpContext.Request["curpage"];
            if (page == 0)
            {
                page = 1;
            }
            var context = new BS_TrangChu();
            var Result = context.GetListVideo(moidang, xemnhieu, page,curPage);

            return PartialView(Result);
        }

        public ActionResult ContentDetailVideo(string page, string moidang, string xemnhieu)
        {
            int _moidang = 0;
            int _xemnhieu = 0;
            int _page = 1;
            int.TryParse(moidang, out _moidang);
            int.TryParse(xemnhieu, out _xemnhieu);
            int.TryParse(page, out _page);
           string curPage = HttpContext.Request["curpage"];
            if (_page == 0)
            {
                _page = 1;
            }
            var context = new BS_TrangChu();
            var Result = context.GetListVideo(_moidang, _xemnhieu, _page,curPage);

            return PartialView(Result);
        }
        public ActionResult TopLibVideo()
        {
            var context = new BS_TrangChu();
            var Result = context.ShowVideoHot1();

            return PartialView(Result);
        }

        public ActionResult ThuVienAnhChiTiet()
        {
            int id = 0;

            string key = (string)this.RouteData.Values["id"];
            int.TryParse(key, out id);

            var context = new BS_TrangChu();
            var Result = context.ChiTietMedia(id);
            return PartialView(Result);
        }

        public ActionResult ThuVienVideoChiTiet()
        {
            int id = 0;

            string key = (string)this.RouteData.Values["id"];
            int.TryParse(key, out id);

            var context = new BS_TrangChu();
            var Result = context.ChiTietMedia(id);
            return PartialView(Result);
        }
    }
}