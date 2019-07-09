using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataContext.AdminBusiness;
using DataContext.Constants;
using DataContext.DAL;
using DataContext.Until;
using DocumentFormat.OpenXml.Spreadsheet;
using Newtonsoft.Json.Linq;

namespace ASS_35.Areas.ADMIN.Controllers
{
    [CheckSessionUser]
    public class SlideController : Controller
    {
        // GET: ADMIN/Slide
        public ActionResult ListImage()
        {
            return PartialView();
        }

        public ActionResult _ListImage(int site =1)
        {
            var db = new SlideBusiness();
            var result = db.ListImage(site);
            return PartialView(result);
        }

        public ActionResult AddImage()
        {
            SessionUser session = (SessionUser)HttpContext.Session["uSession"];
            ViewBag.roll = session.idquyentaikhoan;
            return PartialView();
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult _AddImage()
        {
            JObject JObj = JObject.Parse(System.Web.HttpContext.Current.Request["DataSend"]);
            string tieude = "", gioithieu = "",  avatar = "", duongdan="";
            int phienban = 1, trangthai = 4, thutu = 1;
            var file = Request.Files;
            for(int i = 0; i <file.Count; i++)
            {
                var fileImages = Request.Files[i];
                string url = MultiFileUntils.SaveMultiFile(fileImages, UrlConstant.UrlConst.PostImages,
                    ListExtentionFile.ExtentionFile.exImg);
                if (!string.IsNullOrEmpty(url))
                {
                    avatar = url;
                }
            }
            DisplayUntils.GetJson<string>("tieude", JObj, out tieude);
            DisplayUntils.GetJson<string>("gioithieu", JObj, out gioithieu);
            DisplayUntils.GetJson<int>("phienban", JObj, out phienban);
            DisplayUntils.GetJson<int>("trangthai", JObj, out trangthai);
            DisplayUntils.GetJson<int>("thutu", JObj, out thutu);
            DisplayUntils.GetJson<string>("duongdan", JObj, out duongdan);
            var result = new SlideBusiness().BN_AddImage(tieude,gioithieu,avatar,thutu,phienban,trangthai,duongdan);
            return Json(new {Resulf = result}, JsonRequestBehavior.AllowGet);

        }

        public ActionResult EditImage(int id)
        {
            SessionUser session = (SessionUser) HttpContext.Session["uSession"];
            var result = new SlideBusiness().GetImageById(id);
            ViewBag.id = id;
            ViewBag.quyen = session.idquyentaikhoan;
            return PartialView(result);
        }
        [HttpPost]
        [ValidateInput(false)]
        public JsonResult _EditImage()
        {
            JObject JObj = JObject.Parse(System.Web.HttpContext.Current.Request["DataSend"]);
            string tenanh = "", gioithieu = "", avatar = "", duongdan = "";
            int phienban = 1, trangthai = 3, thutu = 1;
            int id;
            var file = Request.Files;
            if (file != null)
            {
                for (int i = 0; i < file.Count; i++)
                {
                    var fileImages = Request.Files[i];
                    string url = MultiFileUntils.SaveMultiFile(fileImages, UrlConstant.UrlConst.PostImages,
                        ListExtentionFile.ExtentionFile.exImg);
                    if (!string.IsNullOrEmpty(url))
                    {
                        avatar = url;
                    }
                }
            }
            DisplayUntils.GetJson<int>("id", JObj, out id);
            DisplayUntils.GetJson<string>("tenanh", JObj, out tenanh);
            DisplayUntils.GetJson<string>("gioithieu", JObj, out gioithieu);
            DisplayUntils.GetJson<int>("phienban", JObj, out phienban);
            DisplayUntils.GetJson<int>("trangthai", JObj, out trangthai);
            DisplayUntils.GetJson<int>("thutu", JObj, out thutu);
            DisplayUntils.GetJson<string>("duongdan", JObj, out duongdan);
            var result = new SlideBusiness().BN_EditImage(id,tenanh, gioithieu, avatar, thutu, phienban, trangthai, duongdan);
            return Json(new { Resulf = result }, JsonRequestBehavior.AllowGet);

        }

        public JsonResult DeleteImage(int id)
        {
            SystemMess mess = new SystemMess();
            mess = new SlideBusiness().DeleteImage(id);
            return Json(new {result = mess}, JsonRequestBehavior.AllowGet);
        }

        public JsonResult CheckThuTu(int stt)
        {
            SystemMess mess = new SystemMess();
            mess = new SlideBusiness().CheckThutu(stt);
            return Json(mess);
        }

        public JsonResult RemoveImage(int id)
        {
            SystemMess mess = new SystemMess();
            mess = new SlideBusiness().RemoveImage(id);
            return Json(new {Result = mess}, JsonRequestBehavior.AllowGet);
        }
    }
}