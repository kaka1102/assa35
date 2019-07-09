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
    public class MediaController : Controller
    {
        ASS_35Entities entity = new ASS_35Entities();

        public ActionResult ImagesManager()
        {
            SessionUser session = (SessionUser)HttpContext.Session["uSession"];
            @ViewBag.quyen = session.idquyentaikhoan;
            @ViewBag.title = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.ImagesList, SystemMessageConst.systemmessage.ImagesListEn);
            return PartialView();
        }
        public JsonResult ViewImage()
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

            var context = new MediaBusiness();
            var Result = context.LoadAllLibraryImages(bsearch, search, trangthai, CurentPage);

            return Json(new { data = Result.data, draw = draw, recordsFiltered = Result.totalCount, recordsTotal = Result.totalCount }, JsonRequestBehavior.AllowGet);

            //var ListShow = Result.LstData.Skip(minrow).Take(length);
            //return Json(new { data = ListShow, draw = draw, recordsFiltered = Result.LstData.Count(), recordsTotal = Result.LstData.Count() }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddNewLibraryImages()
        {
            SessionUser session = (SessionUser)HttpContext.Session["uSession"];
            @ViewBag.roll = session.idquyentaikhoan;
            return PartialView();
        }

        [HttpPost]
        public JsonResult _AddNewLibraryImages()
        {
            List<ImageCaption> lstFile = (List<ImageCaption>)Newtonsoft.Json.JsonConvert.DeserializeObject(System.Web.HttpContext.Current.Request["lstFile"], typeof(List<ImageCaption>));
            JObject JObj = JObject.Parse(System.Web.HttpContext.Current.Request["DataSend"]);
            string tenchude = "", gioithieu;
            int phienban = 1, trangthai = 4;

            DisplayUntils.GetJson<string>("tenchude", JObj, out tenchude);
            DisplayUntils.GetJson<string>("gioithieu", JObj, out gioithieu);
            DisplayUntils.GetJson<int>("phienban", JObj, out phienban);
            DisplayUntils.GetJson<int>("trangthai", JObj, out trangthai);

            var Result = new MediaBusiness();
            var Context = Result.BN_AddNewLibraryImages(tenchude, gioithieu, phienban, trangthai, lstFile);

            return Json(new { Resulf = Context }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult _DeleteLibraryImages()
        {
            JObject JObj = JObject.Parse(System.Web.HttpContext.Current.Request["DataSend"]);
            int id = 0;
            DisplayUntils.GetJson<int>("id", JObj, out id);


            var Result = new MediaBusiness();
            var context = Result.BN_DeleteLibaryImages(id);

            return Json(new { Result = context }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditLibraryImages()
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
        public JsonResult _EditLibraryImages()
        {
            List<ImageCaption> lstOldFIle = (List<ImageCaption>)Newtonsoft.Json.JsonConvert.DeserializeObject(System.Web.HttpContext.Current.Request["lstOldFIle"], typeof(List<ImageCaption>));
            List<ImageCaption> lstFile = (List<ImageCaption>)Newtonsoft.Json.JsonConvert.DeserializeObject(System.Web.HttpContext.Current.Request["lstFile"], typeof(List<ImageCaption>));

            JObject JObj = JObject.Parse(System.Web.HttpContext.Current.Request["DataSend"]);
            string tenchude = "", gioithieu, lydo = "";
            int phienban = 1, id = 0, trangthai = 0;

            DisplayUntils.GetJson<string>("tenchude", JObj, out tenchude);
            DisplayUntils.GetJson<string>("gioithieu", JObj, out gioithieu);
            DisplayUntils.GetJson<int>("phienban", JObj, out phienban);
            DisplayUntils.GetJson<int>("id", JObj, out id);
            DisplayUntils.GetJson<int>("trangthai", JObj, out trangthai);
            DisplayUntils.GetJson<string>("lydo", JObj, out lydo);

            var Result = new MediaBusiness();
            var context = Result.BN_UpdateLibraryImages(tenchude, gioithieu, phienban, id, trangthai, lydo, lstFile, lstOldFIle);

            return Json(new { Result = context }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult VideoManager()
        {
            SessionUser session = (SessionUser)HttpContext.Session["uSession"];
            @ViewBag.quyen = session.idquyentaikhoan;
            @ViewBag.title = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.VideoList, SystemMessageConst.systemmessage.VideoListEn);
            return PartialView();
        }
        public JsonResult ViewVideo()
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

            var context = new MediaBusiness();
            var Result = context.LoadAllLibraryVideo(bsearch, search, trangthai, CurentPage);

            return Json(new { data = Result.data, draw = draw, recordsFiltered = Result.totalCount, recordsTotal = Result.totalCount }, JsonRequestBehavior.AllowGet);

            //var ListShow = Result.LstData.Skip(minrow).Take(length);
            //return Json(new { data = ListShow, draw = draw, recordsFiltered = Result.LstData.Count(), recordsTotal = Result.LstData.Count() }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddNewLibraryVideo()
        {
            SessionUser session = (SessionUser)HttpContext.Session["uSession"];
            @ViewBag.roll = session.idquyentaikhoan;
            return PartialView();
        }

        [HttpPost]
        public JsonResult _AddNewLibraryVideo()
        {
            JObject JObj = JObject.Parse(System.Web.HttpContext.Current.Request["DataSend"]);
            string tenchude = "", gioithieu, linkvideo;
            int phienban = 1, trangthai = 4;

            DisplayUntils.GetJson<string>("tenchude", JObj, out tenchude);
            DisplayUntils.GetJson<string>("gioithieu", JObj, out gioithieu);
            DisplayUntils.GetJson<string>("linkvideo", JObj, out linkvideo);
            DisplayUntils.GetJson<int>("phienban", JObj, out phienban);
            DisplayUntils.GetJson<int>("trangthai", JObj, out trangthai);

            var Result = new MediaBusiness();
            var Context = Result.BN_AddNewLibraryVideo(tenchude, gioithieu, phienban, linkvideo, trangthai);

            return Json(new { Resulf = Context }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult _DeleteLibraryVideo()
        {
            JObject JObj = JObject.Parse(System.Web.HttpContext.Current.Request["DataSend"]);
            int id = 0;
            DisplayUntils.GetJson<int>("id", JObj, out id);


            var Result = new MediaBusiness();
            var context = Result.BN_DeleteLibaryVideo(id);

            return Json(new { Result = context }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult EditLibraryVideo()
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
        public JsonResult _EditLibraryVideo()
        {
            JObject JObj = JObject.Parse(System.Web.HttpContext.Current.Request["DataSend"]);
            string tenchude = "", gioithieu, linkvideo = "", lydo = "";
            int phienban = 1, id = 0, trangthai = 0;

            DisplayUntils.GetJson<string>("tenchude", JObj, out tenchude);
            DisplayUntils.GetJson<string>("gioithieu", JObj, out gioithieu);
            DisplayUntils.GetJson<int>("phienban", JObj, out phienban);
            DisplayUntils.GetJson<string>("linkvideo", JObj, out linkvideo);
            DisplayUntils.GetJson<int>("id", JObj, out id);
            DisplayUntils.GetJson<int>("trangthai", JObj, out trangthai);
            DisplayUntils.GetJson<string>("lydo", JObj, out lydo);

            var Result = new MediaBusiness();
            var context = Result.BN_UpdateLibraryVideo(tenchude, gioithieu, phienban, linkvideo, id, trangthai, lydo);

            return Json(new { Result = context }, JsonRequestBehavior.AllowGet);
        }




        /// <summary>
        /// các hàm  thêm ,xóa file 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult _AddImages()
        {
            SessionUser session = (SessionUser)HttpContext.Session["uSession"];
            List<object> lstFileAdd = new List<object>();
            var file = Request.Files;
            for (int i = 0; i < file.Count; i++)
            {
                var fileImages = Request.Files[i];
                string url = MultiFileUntils.SaveMultiFile(fileImages, UrlConstant.UrlConst.LibraryImage, ListExtentionFile.ExtentionFile.exImg);

                if (!string.IsNullOrEmpty(url))
                {
                    m_mediadetails m_detail = new m_mediadetails();
                    m_detail.duongdanfile = url;
                    m_detail.trangthai = 2;
                    m_detail.kieufile = "images";
                    m_detail.ngaytao = DateTime.Now;
                    m_detail.tenfile = fileImages.FileName;
                    m_detail.size = fileImages.ContentLength;
                    m_detail.mimetype = fileImages.ContentType;
                    m_detail.idtaikhoan = session.id;
                    entity.m_mediadetails.Add(m_detail);
                    entity.SaveChanges();
                    lstFileAdd.Add(m_detail);
                }
            }
            return Json(new { data = lstFileAdd }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult _AddVideo()
        {
            SessionUser session = (SessionUser)HttpContext.Session["uSession"];
            List<object> lstFileAdd = new List<object>();
            var file = Request.Files;
            for (int i = 0; i < file.Count; i++)
            {
                var fileImages = Request.Files[i];
                string url = MultiFileUntils.SaveMultiFile(fileImages, UrlConstant.UrlConst.LibraryVideo, ListExtentionFile.ExtentionFile.exVideo);

                if (!string.IsNullOrEmpty(url))
                {
                    m_mediadetails m_detail = new m_mediadetails();
                    m_detail.duongdanfile = url;
                    m_detail.trangthai = 2;
                    m_detail.kieufile = "video";
                    m_detail.ngaytao = DateTime.Now;
                    m_detail.tenfile = fileImages.FileName;
                    m_detail.size = fileImages.ContentLength;
                    m_detail.mimetype = fileImages.ContentType;
                    m_detail.idtaikhoan = session.id;
                    entity.m_mediadetails.Add(m_detail);
                    entity.SaveChanges();
                    lstFileAdd.Add(m_detail);
                }
            }
            return Json(new { data = lstFileAdd }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult _AddDocument()
        {
            SessionUser session = (SessionUser)HttpContext.Session["uSession"];
            List<object> lstFileAdd = new List<object>();
            var file = Request.Files;
            for (int i = 0; i < file.Count; i++)
            {
                var fileImages = Request.Files[i];
                string url = MultiFileUntils.SaveMultiFile(fileImages, UrlConstant.UrlConst.LibraryDocument, ListExtentionFile.ExtentionFile.exDocument);

                if (!string.IsNullOrEmpty(url))
                {
                    m_mediadetails m_detail = new m_mediadetails();
                    m_detail.duongdanfile = url;
                    m_detail.trangthai = 2;
                    m_detail.kieufile = "document";
                    m_detail.ngaytao = DateTime.Now;
                    m_detail.tenfile = fileImages.FileName;
                    m_detail.size = fileImages.ContentLength;
                    m_detail.mimetype = fileImages.ContentType;
                    m_detail.idtaikhoan = session.id;
                    entity.m_mediadetails.Add(m_detail);
                    entity.SaveChanges();
                    lstFileAdd.Add(m_detail);
                }
            }
            return Json(new { data = lstFileAdd }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult _DeleteFile()
        {
            bool IsSuccess = false;
            JObject JObj = JObject.Parse(System.Web.HttpContext.Current.Request["DataSend"]);
            int id = 0;

            DisplayUntils.GetJson<int>("id", JObj, out id);

            var Item = entity.m_mediadetails.Where(m => m.id == id).FirstOrDefault();
            if (Item != null)
            {
                bool ck = new DeleteFileUntil().DeleteFileByPath(Item.duongdanfile);
                entity.m_mediadetails.Remove(Item);
                entity.SaveChanges();
                IsSuccess = true;
            }
            return Json(new { IsSuccess = IsSuccess }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public JsonResult _DeleteListFile()
        {
            bool IsSuccess = false;
            int idFile = 0;
            List<int> lstFile = (List<int>)Newtonsoft.Json.JsonConvert.DeserializeObject(System.Web.HttpContext.Current.Request["lstFile"], typeof(List<int>));

            for (int i = 0; i < lstFile.Count; i++)
            {
                idFile = lstFile[i];
                var Item = entity.m_mediadetails.Where(m => m.id == idFile).FirstOrDefault();
                if (Item != null)
                {
                    bool ck = new DeleteFileUntil().DeleteFileByPath(Item.duongdanfile);
                    entity.m_mediadetails.Remove(Item);
                    entity.SaveChanges();
                    IsSuccess = true;
                }
            }

            return Json(new { IsSuccess = IsSuccess }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult _AddImagesCKEditor()
        {
            SessionUser session = (SessionUser)HttpContext.Session["uSession"];
            List<string> lstFileAdd = new List<string>();
            var file = Request.Files;
            for (int i = 0; i < file.Count; i++)
            {
                var fileImages = Request.Files[i];
                string url = MultiFileUntils.SaveMultiFile(fileImages, UrlConstant.UrlConst.PostImages, ListExtentionFile.ExtentionFile.exImg);

                if (!string.IsNullOrEmpty(url))
                {
                    lstFileAdd.Add(url);
                }
            }
            return Json(new { data = lstFileAdd }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult _AddVideoCKEditor()
        {
            SessionUser session = (SessionUser)HttpContext.Session["uSession"];
            List<string> lstFileAdd = new List<string>();
            var file = Request.Files;
            for (int i = 0; i < file.Count; i++)
            {
                var fileImages = Request.Files[i];
                string url = MultiFileUntils.SaveMultiFile(fileImages, UrlConstant.UrlConst.PostVideo, ListExtentionFile.ExtentionFile.exVideo);

                if (!string.IsNullOrEmpty(url))
                {
                    lstFileAdd.Add(url);
                }
            }
            return Json(new { data = lstFileAdd }, JsonRequestBehavior.AllowGet);
        }

        public FileStreamResult ReadFilePDF(string url)
        {
            MemoryStream workStream = new MemoryStream();
            string pdfPath = HttpContext.Server.MapPath(url);
            bool check = System.IO.File.Exists(pdfPath);
            if (check)
            {
                WebClient client = new WebClient();
                Byte[] buffer = client.DownloadData(pdfPath);
                workStream.Write(buffer, 0, buffer.Length);
                workStream.Position = 0;

                return new FileStreamResult(workStream, "application/pdf");
            }
            else
            {
                string pdfPath1 = HttpContext.Server.MapPath("/Content/IMG/Icon/not-found.pdf");
                WebClient client = new WebClient();
                Byte[] buffer = client.DownloadData(pdfPath1);
                workStream.Write(buffer, 0, buffer.Length);
                workStream.Position = 0;

                return new FileStreamResult(workStream, "application/pdf");
            }

        }
    }
}