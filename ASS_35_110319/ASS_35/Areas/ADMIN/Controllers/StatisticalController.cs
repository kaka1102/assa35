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
using DataContext.DAL;
using DataContext.Constants;
using System.IO;
using System.Net;
using System.Data.SqlClient;
using System.Globalization;

namespace ASS_35.Areas.ADMIN.Controllers
{
    [CheckSessionUser]
    public class StatisticalController : Controller
    {
        ASS_35Entities entity = new ASS_35Entities();


        //// lam lại tu day

        public ActionResult LoadAlLReporter()
        {
            @ViewBag.title = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Statisticsreporters, SystemMessageConst.systemmessage.StatisticsreportersEn);
            return PartialView();
        }

        public JsonResult _LoadAlLReporter()
        {
            int minrow = 0;
            int maxrow = 0;
            int.TryParse(HttpContext.Request["start"], out minrow);
            int length = 10;
            int.TryParse(HttpContext.Request["length"], out length);
            maxrow = (minrow + length);
            int draw = 0;
            int.TryParse(HttpContext.Request["draw"], out draw);



            int loaitaikhoan = 0;
            string tendoan, tendaibieu, tungay, denngay;
            bool dkphong = false, dkxe = false;
            int ttdkphong = 0, ttdkxe = 0;

            int.TryParse(HttpContext.Request["loaitaikhoan"], out loaitaikhoan);

            tendoan = HttpContext.Request["tendoan"];
            tendaibieu = HttpContext.Request["tendaibieu"];
            tungay = HttpContext.Request["tungay"];
            denngay = HttpContext.Request["denngay"];

            bool.TryParse(HttpContext.Request["dkphong"], out dkphong);
            bool.TryParse(HttpContext.Request["dkxe"], out dkxe);
            if (dkphong == true)
            {
                ttdkphong = 1;
            }
            if (dkxe == true)
            {
                ttdkxe = 1;
            }
            bool ckDStart = false, ckDEnd = false;

            DateTime dDKStart = DateTime.Now, dDKEnd = DateTime.Now;

            if (!string.IsNullOrEmpty(tungay))
            {
                dDKStart = DateTime.ParseExact(tungay, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                ckDStart = true;
            }
            if (!string.IsNullOrEmpty(denngay))
            {
                dDKEnd = DateTime.ParseExact(denngay, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                ckDEnd = true;
            }

            var context = new StatisticalBusiness();
            var Result = context.BN_LoadReporter(loaitaikhoan, tendoan, tendaibieu, ttdkphong, ttdkxe, dDKStart, dDKEnd, ckDStart, ckDEnd);


            return Json(new { data = Result.LstData, draw = draw, recordsFiltered = Result.LstData.Count(), recordsTotal = Result.LstData.Count() }, JsonRequestBehavior.AllowGet);
        }


        public JsonResult CreateStatisticalReporter()
        {
            List<Statistical.Reporter> danhsachadmin = (List<Statistical.Reporter>)Newtonsoft.Json.JsonConvert.DeserializeObject(System.Web.HttpContext.Current.Request["DataSend"], typeof(List<Statistical.Reporter>));
          
            var context = new StatisticalBusiness();
            var Result = context.BN_CreateStatisticalReporter(danhsachadmin);

            return Json(new { Result = Result }, JsonRequestBehavior.AllowGet);
        }


        public ActionResult LoadAllDelegate()
        {
            @ViewBag.title = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Statisticsdelegates, SystemMessageConst.systemmessage.StatisticsdelegatesEn);
            return PartialView();
        }

        public JsonResult _LoadAllDelegate()
        {
            int minrow = 0;
            int maxrow = 0;
            int.TryParse(HttpContext.Request["start"], out minrow);
            int length = 10;
            int.TryParse(HttpContext.Request["length"], out length);
            maxrow = (minrow + length);
            int draw = 0;
            int.TryParse(HttpContext.Request["draw"], out draw);



            int loaitaikhoan = 0;
            string tendoan, tendaibieu, tungay, denngay;
            bool dkphong = false, dkxe = false;
            int ttdkphong = 0, ttdkxe = 0;

            int.TryParse(HttpContext.Request["loaitaikhoan"], out loaitaikhoan);

            tendoan = HttpContext.Request["tendoan"];
            tendaibieu = HttpContext.Request["tendaibieu"];
            tungay = HttpContext.Request["tungay"];
            denngay = HttpContext.Request["denngay"];

            bool.TryParse(HttpContext.Request["dkphong"], out dkphong);
            bool.TryParse(HttpContext.Request["dkxe"], out dkxe);
            if (dkphong == true)
            {
                ttdkphong = 1;
            }
            if (dkxe == true)
            {
                ttdkxe = 1;
            }
            bool ckDStart = false, ckDEnd = false;

            DateTime dDKStart = DateTime.Now, dDKEnd = DateTime.Now;

            if (!string.IsNullOrEmpty(tungay))
            {
                dDKStart = DateTime.ParseExact(tungay, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                ckDStart = true;
            }
            if (!string.IsNullOrEmpty(denngay))
            {
                dDKEnd = DateTime.ParseExact(denngay, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                ckDEnd = true;
            }

            var context = new StatisticalBusiness();
            var Result = context.BN_LoadDelegate(loaitaikhoan, tendoan, tendaibieu, ttdkphong, ttdkxe, dDKStart, dDKEnd, ckDStart, ckDEnd);


            return Json(new { data = Result.LstData, draw = draw, recordsFiltered = Result.LstData.Count(), recordsTotal = Result.LstData.Count() }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CreateStatisticalDelegate()
        {
            List<Statistical.Reporter> danhsachadmin = (List<Statistical.Reporter>)Newtonsoft.Json.JsonConvert.DeserializeObject(System.Web.HttpContext.Current.Request["DataSend"], typeof(List<Statistical.Reporter>));

            var context = new StatisticalBusiness();
            var Result = context.BN_CreateStatisticalDelegate(danhsachadmin);

            return Json(new { Result = Result }, JsonRequestBehavior.AllowGet);
        }
    }
}