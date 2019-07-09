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

namespace ASS_35.Areas.ADMIN.Controllers
{
    [CheckSessionUser]
    public class MeetingAndEventController : Controller
    {
        ASS_35Entities entity = new ASS_35Entities();
        public ActionResult Index()
        {
            @ViewBag.title = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.ManageMeeting, SystemMessageConst.systemmessage.ManageMeetingEn);
            return PartialView();
        }
        public JsonResult ViewMeeting()
        {
            int minrow = 0;
            int maxrow = 0;
            int.TryParse(HttpContext.Request["start"], out minrow);
            int length = 10;
            int.TryParse(HttpContext.Request["length"], out length);
            maxrow = (minrow + length);
            int draw = 0;
            int.TryParse(HttpContext.Request["draw"], out draw);

            string search = HttpContext.Request["search[value]"].Trim();
            bool bsearch = !string.IsNullOrEmpty(string.Format("{0}", search));

            int trangthai = 0;
            int.TryParse(HttpContext.Request["trangthai"], out trangthai);
            int CurentPage = (minrow / 10) + 1;

            var context = new MeetingAndEventBusiness();
            var Result = context.LoadAllMeetingAndEvent(bsearch, search, trangthai, CurentPage);

            return Json(new { data = Result.data, draw = draw, recordsFiltered = Result.totalCount, recordsTotal = Result.totalCount }, JsonRequestBehavior.AllowGet);

        }

        //client
        //public JsonResult ViewMeeting()
        //{
        //    int minrow = 0;
        //    int maxrow = 0;
        //    int.TryParse(HttpContext.Request["start"], out minrow);
        //    int length = 10;
        //    int.TryParse(HttpContext.Request["length"], out length);
        //    maxrow = (minrow + length);
        //    int draw = 0;
        //    int.TryParse(HttpContext.Request["draw"], out draw);

        //    string search = HttpContext.Request["search[value]"].Trim();
        //    bool bsearch = !string.IsNullOrEmpty(string.Format("{0}", search));

        //    int trangthai = 0;
        //    int.TryParse(HttpContext.Request["trangthai"], out trangthai);
        //    int CurentPage = (minrow / 10) + 1;

        //    var context = new MeetingAndEventBusiness();
        //    var Result = context.LoadAllMeetingAndEvent(bsearch, search, trangthai, CurentPage);

        //    return Json(new { data = Result.data, draw = draw, recordsFiltered = Result.totalCount, recordsTotal = Result.totalCount }, JsonRequestBehavior.AllowGet);

        //}


        [HttpPost]
        public JsonResult _DeleteMeetingAndEvent()
        {
            JObject JObj = JObject.Parse(System.Web.HttpContext.Current.Request["DataSend"]);
            int id = 0;
            DisplayUntils.GetJson<int>("id", JObj, out id);


            var Result = new MeetingAndEventBusiness();
            var context = Result.BN_DeleteMeetingAndEvent(id);

            return Json(new { Result = context }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditMeetingAndEvent()
        {
            SessionUser session = (SessionUser)HttpContext.Session["uSession"];
            var data = System.Web.HttpContext.Current.Request["DataSend"];
            var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            var userInfoJson = jss.Serialize(data);

            @ViewBag.data = userInfoJson;
            @ViewBag.quyen = session.idquyentaikhoan;
            return PartialView();
        }

        [HttpPost]
        public JsonResult _EditMeetingAndEvent()
        {

            JObject JObj = JObject.Parse(System.Web.HttpContext.Current.Request["DataSend"]);
            string tencuochop = "", diadiem, lydo = "", startDate = "";
            int phienban = 1, id = 0, trangthai = 0;

            DisplayUntils.GetJson<string>("tencuochop", JObj, out tencuochop);
            DisplayUntils.GetJson<string>("diadiem", JObj, out diadiem);
            DisplayUntils.GetJson<int>("phienban", JObj, out phienban);
            DisplayUntils.GetJson<int>("id", JObj, out id);
            DisplayUntils.GetJson<int>("trangthai", JObj, out trangthai);
            DisplayUntils.GetJson<string>("lydo", JObj, out lydo);
            DisplayUntils.GetJson<string>("startDate", JObj, out startDate);

            var Result = new MeetingAndEventBusiness();
            var context = Result.BN_UpdateMeetingAndEvent(tencuochop, diadiem, phienban, id, trangthai, lydo, startDate);

            return Json(new { Result = context }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult _UpdateAllEvent()
        {

            JObject JObj = JObject.Parse(System.Web.HttpContext.Current.Request["DataSend"]);
            int id = 0, trangthai = 0;

            DisplayUntils.GetJson<int>("id", JObj, out id);
            DisplayUntils.GetJson<int>("trangthai", JObj, out trangthai);

            var Result = new MeetingAndEventBusiness();
            var context = Result.BN_UpdateAllEvent(id, trangthai);

            return Json(new { Result = context }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddMeeting()
        {
            SessionUser session = (SessionUser)HttpContext.Session["uSession"];
            @ViewBag.roll = session.idquyentaikhoan;
            return PartialView();
        }

        [HttpPost]
        public JsonResult _AddMeeting()
        {

            JObject JObj = JObject.Parse(System.Web.HttpContext.Current.Request["DataSend"]);
            string tencuochop = "", diadiem = "", startDate = "";
            int phienban = 1, trangthai = 4;

            DisplayUntils.GetJson<string>("tencuochop", JObj, out tencuochop);
            DisplayUntils.GetJson<string>("diadiem", JObj, out diadiem);
            DisplayUntils.GetJson<int>("phienban", JObj, out phienban);
            DisplayUntils.GetJson<string>("startDate", JObj, out startDate);
            DisplayUntils.GetJson<int>("trangthai", JObj, out trangthai);

            var Result = new MeetingAndEventBusiness();
            var Context = Result.BN_AddNewMeetingEvent(tencuochop, diadiem, phienban, startDate, trangthai);

            return Json(new { Resulf = Context }, JsonRequestBehavior.AllowGet);
        }



        public ActionResult DetailMeeting()
        {

            SessionUser session = (SessionUser)HttpContext.Session["uSession"];
            JObject JObj = JObject.Parse(System.Web.HttpContext.Current.Request["DataSend"]);
            int id = 0;

            DisplayUntils.GetJson<int>("id", JObj, out id);
            var data = entity.m_sukienhop.Where(m => m.id == id).FirstOrDefault();
            @ViewBag.quyen = session.idquyentaikhoan;
            @ViewBag.id = id;
            @ViewBag.title = data.tencuochop;
            @ViewBag.place = data.diadiem;
            @ViewBag.ngaydienra = data.ngayhop.Value.ToString("dd/MM/yyyy");
            @ViewBag.trangthai = data.trangthai;
            return PartialView();
        }
        public ActionResult AllDetail(int id)
        {
            @ViewBag.idMeeting = id;
            var data = entity.m_sukienhop.Where(m => m.id == id).FirstOrDefault();
            @ViewBag.trangthai = data != null ? data.trangthai : 3;
            @ViewBag.title = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.InforMeeting, SystemMessageConst.systemmessage.InforMeetingEn);
            return PartialView();
        }

        public JsonResult _AllDetail()
        {
            SessionUser session = (SessionUser)HttpContext.Session["uSession"];
            int id = 0;
            int.TryParse(System.Web.HttpContext.Current.Request["id"], out id);

            //@ViewBag.quyen = session.idquyentaikhoan;
            //@ViewBag.title = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.InforMeeting, SystemMessageConst.systemmessage.InforMeetingEn);


            int minrow = 0;
            int maxrow = 0;
            int.TryParse(HttpContext.Request["start"], out minrow);
            int length = 10;
            int.TryParse(HttpContext.Request["length"], out length);
            maxrow = (minrow + length);
            int draw = 0;
            int.TryParse(HttpContext.Request["draw"], out draw);

            string search = HttpContext.Request["search[value]"];
            bool bsearch = !string.IsNullOrEmpty(string.Format("{0}", search));

            int trangthai = 0;
            int.TryParse(HttpContext.Request["trangthai"], out trangthai);
            int CurentPage = (minrow / 10) + 1;

            var context = new MeetingAndEventBusiness();
            var Result = context.LoadDetailMeetingByID(bsearch, search, trangthai, CurentPage, id);

            return Json(new { data = Result.data, draw = draw, recordsFiltered = Result.totalCount, recordsTotal = Result.totalCount }, JsonRequestBehavior.AllowGet);
        }


        [ValidateInput(false)]
        [HttpPost]
        public ActionResult _AddInforMeeting()
        {
            JObject JObj = JObject.Parse(System.Web.HttpContext.Current.Request["DataSend"]);
            string ten = "", diadiem = "", noidung = "", startDate = "", startTime = "", endTime = "";
            int id = 0, trangthai = 4;

            DisplayUntils.GetJson<string>("ten", JObj, out ten);
            DisplayUntils.GetJson<string>("diadiem", JObj, out diadiem);
            DisplayUntils.GetJson<string>("noidung", JObj, out noidung);
            DisplayUntils.GetJson<string>("startDate", JObj, out startDate);
            DisplayUntils.GetJson<string>("startTime", JObj, out startTime);
            DisplayUntils.GetJson<string>("endTime", JObj, out endTime);
            DisplayUntils.GetJson<int>("id", JObj, out id);
            DisplayUntils.GetJson<int>("trangthai", JObj, out trangthai);

            var Result = new MeetingAndEventBusiness();
            var Context = Result.BN_AddNewInforMeeting(ten, diadiem, noidung, startDate, startTime, endTime, id, trangthai);

            return Json(new { Resulf = Context }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult _DeleteInforMeeting()
        {
            JObject JObj = JObject.Parse(System.Web.HttpContext.Current.Request["DataSend"]);
            int id = 0;
            DisplayUntils.GetJson<int>("id", JObj, out id);


            var Result = new MeetingAndEventBusiness();
            var context = Result.BN_DeleteInforMeeting(id);

            return Json(new { Result = context }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditInforMeeting()
        {
            SessionUser session = (SessionUser)HttpContext.Session["uSession"];
            var data = System.Web.HttpContext.Current.Request["DataSend"];
            var jss = new System.Web.Script.Serialization.JavaScriptSerializer();
            var userInfoJson = jss.Serialize(data);

            @ViewBag.data = userInfoJson;
            @ViewBag.quyen = session.idquyentaikhoan;
            return PartialView();
        }

        [HttpPost]
        public JsonResult _EditInforMeeting()
        {
            JObject JObj = JObject.Parse(System.Web.HttpContext.Current.Request["DataSend"]);
            string ten = "", diadiem, lydo = "", noidung = "", startDate = "", startTime = "", endTime = "";
            int id = 0, trangthai = 0;

            DisplayUntils.GetJson<string>("ten", JObj, out ten);
            DisplayUntils.GetJson<string>("diadiem", JObj, out diadiem);
            DisplayUntils.GetJson<string>("lydo", JObj, out lydo);

            DisplayUntils.GetJson<string>("noidung", JObj, out noidung);
            DisplayUntils.GetJson<string>("startDate", JObj, out startDate);
            DisplayUntils.GetJson<string>("startTime", JObj, out startTime);
            DisplayUntils.GetJson<string>("endTime", JObj, out endTime);

            DisplayUntils.GetJson<int>("id", JObj, out id);
            DisplayUntils.GetJson<int>("trangthai", JObj, out trangthai);


            var Result = new MeetingAndEventBusiness();
            var context = Result.BN_UpdateInforMeeting(ten, diadiem, lydo, noidung, startDate, startTime, endTime, id, trangthai);

            return Json(new { Result = context }, JsonRequestBehavior.AllowGet);
        }
    }
}