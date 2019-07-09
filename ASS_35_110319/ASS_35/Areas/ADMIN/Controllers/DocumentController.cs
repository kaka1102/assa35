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
    public class DocumentController : Controller
    {
        public ActionResult LoadAllDocument()
        {
            SessionUser session = (SessionUser)HttpContext.Session["uSession"];
            @ViewBag.roll = session.idquyentaikhoan;

            @ViewBag.title = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.DocumentList, SystemMessageConst.systemmessage.DocumentListEn);
            return PartialView();
        }
        [HttpPost]
        public JsonResult _ViewListDocument()
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

            int trangthai = 3;
            int.TryParse(HttpContext.Request["trangthai"], out trangthai);
            int CurentPage = (minrow / 10) + 1;

            var context = new DocumentBusiness();
            var Result = context.LoadAllDocument(bsearch, search, trangthai, CurentPage);


            return Json(new { data = Result.data, draw = draw, recordsFiltered = Result.totalCount, recordsTotal = Result.totalCount }, JsonRequestBehavior.AllowGet);
          
        }

        [HttpPost]
        public JsonResult _DeleteDocument()
        {
            JObject JObj = JObject.Parse(System.Web.HttpContext.Current.Request["DataSend"]);
            int id = 0;
            DisplayUntils.GetJson<int>("id", JObj, out id);

            var Result = new DocumentBusiness();
            var context = Result.BN_DeleteDocument(id);

            return Json(new { Result = context }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddNewDocument()
        {
            SessionUser session = (SessionUser)HttpContext.Session["uSession"];
            @ViewBag.roll = session.idquyentaikhoan;
            return PartialView();
        }

        [HttpPost]
        public JsonResult _AddNewDocument()
        {
            List<int> lstFile = (List<int>)Newtonsoft.Json.JsonConvert.DeserializeObject(System.Web.HttpContext.Current.Request["lstFile"], typeof(List<int>));
            JObject JObj = JObject.Parse(System.Web.HttpContext.Current.Request["DataSend"]);
            string tenchude = "", gioithieu;
            int phienban = 1, trangthai = 4, loaitl =0;

            DisplayUntils.GetJson<string>("tenchude", JObj, out tenchude);
            DisplayUntils.GetJson<string>("gioithieu", JObj, out gioithieu);
            DisplayUntils.GetJson<int>("phienban", JObj, out phienban);
            DisplayUntils.GetJson<int>("loaitl", JObj, out loaitl);
            DisplayUntils.GetJson<int>("trangthai", JObj, out trangthai);

            var Result = new DocumentBusiness();
            var Context = Result.BN_AddNewDocument(tenchude, gioithieu, phienban,loaitl, trangthai, lstFile);

            return Json(new { Resulf = Context }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditDocument()
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
        public JsonResult _EditDocument()
        {
            List<int> lstDel = (List<int>)Newtonsoft.Json.JsonConvert.DeserializeObject(System.Web.HttpContext.Current.Request["lstDel"], typeof(List<int>));
            List<int> lstFile = (List<int>)Newtonsoft.Json.JsonConvert.DeserializeObject(System.Web.HttpContext.Current.Request["lstFile"], typeof(List<int>));

            JObject JObj = JObject.Parse(System.Web.HttpContext.Current.Request["DataSend"]);
            string tenchude = "", gioithieu, lydo = "";
            int phienban = 1, id = 0, trangthai = 0;

            DisplayUntils.GetJson<string>("tenchude", JObj, out tenchude);
            DisplayUntils.GetJson<string>("gioithieu", JObj, out gioithieu);
            DisplayUntils.GetJson<int>("phienban", JObj, out phienban);
            DisplayUntils.GetJson<int>("id", JObj, out id);
            DisplayUntils.GetJson<int>("trangthai", JObj, out trangthai);
            DisplayUntils.GetJson<string>("lydo", JObj, out lydo);

            var Result = new DocumentBusiness();
            var context = Result.BN_UpdateLibraryDocument(tenchude, gioithieu, phienban, id, trangthai, lydo, lstFile, lstDel);

            return Json(new { Result = context }, JsonRequestBehavior.AllowGet);
        }
    }
}