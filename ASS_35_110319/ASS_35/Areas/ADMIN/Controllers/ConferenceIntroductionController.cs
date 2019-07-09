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
    public class ConferenceIntroductionController : Controller
    {
        public ActionResult Index()
        {
            @ViewBag.title = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.ManageIntroductionConference, SystemMessageConst.systemmessage.ManageIntroductionConferenceEn);
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


            var context = new ConferenceIntroductionBusiness();
            var Result = context.LoadAllConferenceIntroduction(bsearch, search);

            var ListShow = Result.LstData.Skip(minrow).Take(length);
            return Json(new { data = ListShow, draw = draw, recordsFiltered = Result.LstData.Count(), recordsTotal = Result.LstData.Count() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditConferenceIntroduction()
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
        [ValidateInput(false)]
        public JsonResult _EditConferenceIntroduction()
        {

            JObject JObj = JObject.Parse(System.Web.HttpContext.Current.Request["DataSend"]);
            string noidung = "";
            int id = 0;

            DisplayUntils.GetJson<string>("noidung", JObj, out noidung);
            DisplayUntils.GetJson<int>("id", JObj, out id);


            var Result = new ConferenceIntroductionBusiness();
            var context = Result.BN_ConferenceIntroduction(noidung, id);

            return Json(new { Result = context }, JsonRequestBehavior.AllowGet);
        }
    }
}