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

namespace ASS_35.Areas.ADMIN.Controllers
{
    [CheckSessionUser]
    public class SpeakerController : Controller
    {
        // GET: ADMIN/Speaker
        public ActionResult Index()
        {
            @ViewBag.title = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.ManageSpeaker, SystemMessageConst.systemmessage.ManageSpeakerEn);
            return PartialView();
        }

        [HttpPost]
        public JsonResult _Index()
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

            var context = new SpeakerBusiness();
            var Result = context.LoadAllSpeaker(bsearch, search);

            var ListShow = Result.LstData.Skip(minrow).Take(length);
            return Json(new { data = ListShow, draw = draw, recordsFiltered = Result.LstData.Count(), recordsTotal = Result.LstData.Count() }, JsonRequestBehavior.AllowGet);

        }

        public ActionResult AddNew()
        {
            SessionUser session = (SessionUser)HttpContext.Session["uSession"];
            @ViewBag.roll = session.idquyentaikhoan;
            return PartialView();
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult _AddNew()
        {

            JObject JObj = JObject.Parse(System.Web.HttpContext.Current.Request["DataSend"]);
            List<int> lstFile1 = (List<int>)Newtonsoft.Json.JsonConvert.DeserializeObject(System.Web.HttpContext.Current.Request["lstFile1"], typeof(List<int>));
            List<int> lstFile2 = (List<int>)Newtonsoft.Json.JsonConvert.DeserializeObject(System.Web.HttpContext.Current.Request["lstFile2"], typeof(List<int>));
            string hoten = "", noidung = "", chucvu = "", avatar = "", type = "";
            int trangthai = 4;

            var file = Request.Files;
            for (int i = 0; i < file.Count; i++)
            {
                var fileImages = Request.Files[i];
                string url = MultiFileUntils.SaveMultiFile(fileImages, UrlConstant.UrlConst.PostImages, ListExtentionFile.ExtentionFile.exImg);

                if (!string.IsNullOrEmpty(url))
                {
                    avatar = url;
                }
            }

            DisplayUntils.GetJson<string>("hoten", JObj, out hoten);
            DisplayUntils.GetJson<string>("chucvu", JObj, out chucvu);
            DisplayUntils.GetJson<string>("noidung", JObj, out noidung);
            DisplayUntils.GetJson<string>("phienban", JObj, out type);
            DisplayUntils.GetJson<int>("trangthai", JObj, out trangthai);

            var Result = new SpeakerBusiness();
            var Context = Result.BN_AddNewSpeaker(hoten, noidung, chucvu, type, avatar, trangthai, lstFile1, lstFile2);

            return Json(new { Resulf = Context }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteSpeaker()
        {
            JObject JObj = JObject.Parse(System.Web.HttpContext.Current.Request["DataSend"]);
            int id = 0;
            DisplayUntils.GetJson<int>("id", JObj, out id);

            var Result = new SpeakerBusiness();
            var context = Result.BN_DeleteSpeaker(id);

            return Json(new { Result = context }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditSpeaker()
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
        public JsonResult _UpdateSpeaker()
        {
            List<int> lstFile1 = (List<int>)Newtonsoft.Json.JsonConvert.DeserializeObject(System.Web.HttpContext.Current.Request["lstFile1"], typeof(List<int>));
            List<int> lstFile2 = (List<int>)Newtonsoft.Json.JsonConvert.DeserializeObject(System.Web.HttpContext.Current.Request["lstFile2"], typeof(List<int>));

            JObject JObj = JObject.Parse(System.Web.HttpContext.Current.Request["DataSend"]);
            string hoten = "", chucvu, noidung = "", phienban = "", avatar = "";
            int id = 0, trangthai = 0;

            DisplayUntils.GetJson<string>("hoten", JObj, out hoten);
            DisplayUntils.GetJson<string>("chucvu", JObj, out chucvu);
            DisplayUntils.GetJson<string>("phienban", JObj, out phienban);
            DisplayUntils.GetJson<int>("id", JObj, out id);
            DisplayUntils.GetJson<int>("trangthai", JObj, out trangthai);
            DisplayUntils.GetJson<string>("noidung", JObj, out noidung);

            var file = Request.Files;
            for (int i = 0; i < file.Count; i++)
            {
                var fileImages = Request.Files[i];
                string url = MultiFileUntils.SaveMultiFile(fileImages, UrlConstant.UrlConst.PostImages, ListExtentionFile.ExtentionFile.exImg);

                if (!string.IsNullOrEmpty(url))
                {
                    avatar = url;
                }
            }


            var Result = new SpeakerBusiness();
            var context = Result.BN_UpdateSpeaker(hoten, chucvu, phienban, id, trangthai, noidung,avatar, lstFile1, lstFile2);

            return Json(new { Result = context }, JsonRequestBehavior.AllowGet);
        }
    }
}