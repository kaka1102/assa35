using DataContext.ClientBusiness;
using DataContext.Until;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ASS_35.Controllers
{
    public class TinTucSuKienController : Controller
    {
        // GET: TinTucSuKien
        public ActionResult TinTuc()
        {
            int page = 0;
            int.TryParse(HttpContext.Request["page"], out page);

            if (page == 0)
            {
                page = 1;
            }
            var context = new BS_TrangChu();
            var Result = context.GetListTinTuc(page);
            return PartialView(Result);
        }
      
        public ActionResult ChiTietTinTuc()
        {
            int id = 0;

            string key = (string)this.RouteData.Values["id"];
            int.TryParse(key, out id);

            var context = new BS_TrangChu();
            var Result = context.ChiTietTinTuc(id);
            return PartialView(Result);
        }

        public ActionResult TinTucLienQuan()
        {
            int id = 0;

            string key = (string)this.RouteData.Values["id"];
            int.TryParse(key, out id);

            var context = new BS_TrangChu();
            var Result = context.ShowListTinTucLienQuan(id);
            return PartialView(Result);
        }

        public ActionResult SuKien()
        {
            int page = 0;
            int.TryParse(HttpContext.Request["page"], out page);

            if (page == 0)
            {
                page = 1;
            }
            var context = new BS_TrangChu();
            var Result = context.GetListSuKien(page);
            return PartialView(Result);
        }

        public ActionResult ChiTietSuKien()
        {
            int id = 0;

            string key = (string)this.RouteData.Values["id"];
            int.TryParse(key, out id);

            var context = new BS_TrangChu();
            var Result = context.ChiTietsuKien(id);
            return PartialView(Result);
        }
        public ActionResult SuKienLienQuan()
        {
            int id = 0;

            string key = (string)this.RouteData.Values["id"];
            int.TryParse(key, out id);

            var context = new BS_TrangChu();
            var Result = context.ShowListSuKienLienQuan(id);
            return PartialView(Result);
        }
    }

}