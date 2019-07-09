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
    public class ManagerPostController : Controller
    {
        public ActionResult LoadAllPost()
        {
            SessionUser session = (SessionUser)HttpContext.Session["uSession"];
            @ViewBag.quyen = session.idquyentaikhoan;

            @ViewBag.title = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Listofarticles, SystemMessageConst.systemmessage.ListofarticlesEn);
          
            return PartialView();
        }
        [HttpPost]
        public JsonResult _ViewListPost()
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

            var context = new ManagerPostBusiness();
            var Result = context.LoadAllPost(bsearch, search, trangthai, CurentPage);

            return Json(new { data = Result.data, draw = draw, recordsFiltered = Result.totalCount, recordsTotal = Result.totalCount }, JsonRequestBehavior.AllowGet);

            //var ListShow = Result.LstData.Skip(minrow).Take(length);
            //return Json(new { data = ListShow, draw = draw, recordsFiltered = Result.LstData.Count(), recordsTotal = Result.LstData.Count() }, JsonRequestBehavior.AllowGet);
       
        }
        [HttpPost]
        public JsonResult _DeletePost()
        {
            JObject JObj = JObject.Parse(System.Web.HttpContext.Current.Request["DataSend"]);
            int id = 0;
            DisplayUntils.GetJson<int>("id", JObj, out id);

            var Result = new ManagerPostBusiness();
            var context = Result.BN_DeletePost(id);

            return Json(new { Result = context }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddNewPost()
        {
            SessionUser session = (SessionUser)HttpContext.Session["uSession"];
            @ViewBag.roll = session.idquyentaikhoan;
            return PartialView();
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult _AddNewPost()
        {

            JObject JObj = JObject.Parse(System.Web.HttpContext.Current.Request["DataSend"]);
            string tieude = "", noidung = "", gioithieu = "", avatar = "",tacgia="";
            int phienban = 1, trangthai=4;
         
            avatar = "/Content/IMG/PostImages/logoassa.png";
           
            var file = Request.Files;
            if(file != null)
            {
                for (int i = 0; i < file.Count; i++)
                {
                    var fileImages = Request.Files[i];
                    string url = MultiFileUntils.SaveMultiFile(fileImages, UrlConstant.UrlConst.PostImages, ListExtentionFile.ExtentionFile.exImg);

                    if (!string.IsNullOrEmpty(url))
                    {
                        avatar = url;
                    }

                }
            }
            
            DisplayUntils.GetJson<string>("tieude", JObj, out tieude);
            DisplayUntils.GetJson<string>("gioithieu", JObj, out gioithieu);
            DisplayUntils.GetJson<string>("noidung", JObj, out noidung);
            DisplayUntils.GetJson<int>("phienban", JObj, out phienban);
            DisplayUntils.GetJson<int>("trangthai", JObj, out trangthai);
            DisplayUntils.GetJson<string>("tacgia", JObj, out tacgia);

            var Result = new ManagerPostBusiness();
            var Context = Result.BN_AddNewPost(tieude, noidung, tacgia,gioithieu, phienban, avatar, trangthai);

            return Json(new { Resulf = Context }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditPost()
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
        public JsonResult _EditPost()
        {

            JObject JObj = JObject.Parse(System.Web.HttpContext.Current.Request["DataSend"]);
            string tieude = "", noidung = "", gioithieu = "", avatar = "", lydo = "" , tacgia="";
            int phienban = 1, id = 0, trangthai = 0;

            DisplayUntils.GetJson<string>("tieude", JObj, out tieude);
            DisplayUntils.GetJson<string>("gioithieu", JObj, out gioithieu);
            DisplayUntils.GetJson<string>("noidung", JObj, out noidung);
            DisplayUntils.GetJson<string>("lydo", JObj, out lydo);
            DisplayUntils.GetJson<string>("tacgia", JObj, out tacgia);
            DisplayUntils.GetJson<int>("phienban", JObj, out phienban);            
            DisplayUntils.GetJson<int>("id", JObj, out id);
            DisplayUntils.GetJson<int>("trangthai", JObj, out trangthai);


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

            var Result = new ManagerPostBusiness();
            var context = Result.BN_UpdatePost(tieude, gioithieu, noidung,tacgia, lydo, phienban, id, trangthai, avatar);

            return Json(new { Result = context }, JsonRequestBehavior.AllowGet);
        }
    }
}