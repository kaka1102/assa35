using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using DataContext;
using DataContext.AdminBusiness;
using DataContext.Constants;
using DataContext.DAL;
using DataContext.Socket;
using DataContext.Until;
using Newtonsoft.Json.Linq;

namespace ASS_35.Areas.ADMIN.Controllers
{
    [CheckSessionUser]
    public class NotificationManagerController : Controller
    {
        // GET: ADMIN/NotificationManager
        public ActionResult ListNotification()
        {
            return PartialView();
        }
        public ActionResult _ListNotification(int status = -1)
        {
            var db = new NotificationManagerBusiness();
            ViewBag.lstFile = db.GetListFile();
            var result = db.GetListNotification(status);
            ViewBag.idNguoitao = AccountUntil.GetAccount().id;
            return PartialView(result);
        }
        public ActionResult ListNotificationPerson()
        {
            return PartialView();
        }
        public ActionResult _ListNotificationPerson()
        {
            var db = new NotificationManagerBusiness();
            ViewBag.lstFile = db.GetListFile();
            var result = db.GetListNotificationPerson();
            ViewBag.idNguoitao = AccountUntil.GetAccount().id;
            return PartialView(result);
        }
        public ActionResult AddNotification()
        {
            return PartialView();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult _AddNotification()
        {
            m_thongbao venue = new m_thongbao();
            List<m_filethongbao> lstFile = new List<m_filethongbao>();
            string venueContents,
                venueName,
                venueNote,
                venueLanguage,
                venueTarget,
                venueStatus;
            JObject jObj = JObject.Parse(Request["infomationData"]);
            DisplayUntils.GetJson<string>("venueContents", jObj, out venueContents);
            DisplayUntils.GetJson<string>("venueName", jObj, out venueName);
            DisplayUntils.GetJson<string>("venueNote", jObj, out venueNote);
            DisplayUntils.GetJson<string>("venueLanguage", jObj, out venueLanguage);
            DisplayUntils.GetJson<string>("venueTarget", jObj, out venueTarget);
            DisplayUntils.GetJson<string>("venueStatus", jObj, out venueStatus);
            int _venueLanguage;
            if (!Int32.TryParse(venueLanguage, out _venueLanguage))
            {
                _venueLanguage = 1;
            }
            if (_venueLanguage != 1 && _venueLanguage != 2)
            {
                _venueLanguage = 1;
            }
            venue.noidung = venueContents;
            venue.ghichu = venueNote;
            venue.tenthongbao = venueName;
            venue.phienban = _venueLanguage;
            venue.trangthai = StatusUntil.CheckAddStatus(venueStatus);
            var lstFilePdf = Request.Files;
            if (lstFilePdf != null)
            {
                for (int i=0;i <lstFilePdf.Count;i++)
                {
                    m_filethongbao fileNotifi = new m_filethongbao();
                    List<string> exFile = new List<string>();
                    exFile.Add(".pdf");
                    fileNotifi.linkpath = FileUtils.FileSave(lstFilePdf[i], UrlConstant.UrlConst.AccountImage, exFile);
                    var checkFileAvatar =
                        FileUtils.ValidateFileUpload(fileNotifi.linkpath, "pdf", DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.File_att, SystemMessageConst.systemmessage.File_attEn));
                    if (checkFileAvatar.IsSuccess == false)
                    {
                        return Json(new { result = checkFileAvatar }, JsonRequestBehavior.AllowGet);
                    }
                    fileNotifi.trangthai = SystemMessageConst.StatusConst.Active;
                    fileNotifi.tenfile = lstFilePdf[i].FileName;
                    fileNotifi.filetype = "pdf";
                    lstFile.Add(fileNotifi);
                }
            }
            var db = new NotificationManagerBusiness();
            var result = db.AddNotification(venue, lstFile, venueTarget);
            if (result.IsSuccess == false)
            {
                foreach (var item in lstFile)
                {
                    if (System.IO.File.Exists(Request.MapPath(item.linkpath)))
                    {
                        System.IO.File.Delete(Request.MapPath(item.linkpath));
                    }
                }
                
            }
            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult EditNotification(int id)
        {
            var db = new NotificationManagerBusiness();
            var result = db.GetNotifyById(id);
            ViewBag.id = id;
            return PartialView(result);
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult _EditNotification()
        {
            m_thongbao venue = new m_thongbao();
            List<m_filethongbao> lstFile = new List<m_filethongbao>();
            string venueContents,
                venueName,
                venueNote,
                venueLanguage,
                venueId,
                venueStatus,
                venueTarget,
                adminStatus,
                venueReason;
            JObject jObj = JObject.Parse(Request["infomationData"]);
            DisplayUntils.GetJson<string>("venueContents", jObj, out venueContents);
            DisplayUntils.GetJson<string>("venueName", jObj, out venueName);
            DisplayUntils.GetJson<string>("venueNote", jObj, out venueNote);
            DisplayUntils.GetJson<string>("venueLanguage", jObj, out venueLanguage);
            DisplayUntils.GetJson<string>("venueId", jObj, out venueId);
            DisplayUntils.GetJson<string>("venueStatus", jObj, out venueStatus);
            DisplayUntils.GetJson<string>("venueTarget", jObj, out venueTarget);
            DisplayUntils.GetJson<string>("adminStatus", jObj, out adminStatus);
            DisplayUntils.GetJson<string>("venueReason", jObj, out venueReason);
            if (adminStatus == null && venueStatus == "2")
            {
                SystemMess systemMess = new SystemMess();
                systemMess.IsSuccess = false;
                systemMess.Message =
                    DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.MustBeChooseStatus,
                        SystemMessageConst.ValidateConst.MustBeChooseStatusEn);
                return Json(new { result = systemMess }, JsonRequestBehavior.AllowGet);
            }
            int _venueLanguage;
            int _adminStatus;
            Int32.TryParse(adminStatus, out _adminStatus);
            if (!Int32.TryParse(venueLanguage, out _venueLanguage))
            {
                _venueLanguage = 1;
            }
            if (_venueLanguage != 1 && _venueLanguage != 2)
            {
                _venueLanguage = 1;
            }
            int _venueId;
            Int32.TryParse(venueId, out _venueId);
            venue.id = _venueId;
            venue.noidung = venueContents;
            venue.ghichu = venueNote;
            venue.tenthongbao = venueName;
            venue.phienban = _venueLanguage;
            venue.lido = venueReason;
            venue.trangthai = StatusUntil.CheckAddStatus(venueStatus);
            if (_adminStatus != 0 && AccountUntil.GetAccount().idquyentaikhoan == SystemMessageConst.RoleConst.Admin)
            {
                venue.trangthai = _adminStatus;
            }
            var lstFilePdf = Request.Files;
            if (lstFilePdf != null)
            {
                for (int i = 0; i < lstFilePdf.Count; i++)
                {
                    m_filethongbao fileNotifi = new m_filethongbao();
                    List<string> exFile = new List<string>();
                    exFile.Add(".pdf");
                    fileNotifi.linkpath = FileUtils.FileSave(lstFilePdf[i], UrlConstant.UrlConst.AccountImage, exFile);
                    var checkFileAvatar =
                        FileUtils.ValidateFileUpload(fileNotifi.linkpath, "pdf", DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.File_att, SystemMessageConst.systemmessage.File_attEn));
                    if (checkFileAvatar.IsSuccess == false)
                    {
                        return Json(new { result = checkFileAvatar }, JsonRequestBehavior.AllowGet);
                    }
                    fileNotifi.trangthai = SystemMessageConst.StatusConst.Pending;
                    fileNotifi.tenfile = lstFilePdf[i].FileName;
                    fileNotifi.filetype = "pdf";
                    lstFile.Add(fileNotifi);
                }
            }
            var db = new NotificationManagerBusiness();
            var result = db.EditNotification(venue, lstFile, venueTarget);
            if (result.IsSuccess == false)
            {
                foreach (var item in lstFile)
                {
                    if (System.IO.File.Exists(Request.MapPath(item.linkpath)))
                    {
                        System.IO.File.Delete(Request.MapPath(item.linkpath));
                    }
                }

            }
            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteNotification(int id)
        {
            var db = new NotificationManagerBusiness();
            var result = db.DeleteNotification(id);
            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ListFileByNotifiId(int id,int? status,int? idAuthor)
        {
            ViewBag.id = id;
            ViewBag.status = status;
            ViewBag.idAuthor = idAuthor;
            var db = new NotificationManagerBusiness();
            var result = db.GetListFileByNotifiId(id);
            return PartialView(result);
        }

        public ActionResult DeleteNotificationFile(int id)
        {
            var db = new NotificationManagerBusiness();
            var result = db.DeleteNotificationFile(id);
            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult _EditStatusNotification()
        {
            m_diadiemtochuc venue = new m_diadiemtochuc();
            string venueStatus,
                venueId,
                feedback;
            JObject jObj = JObject.Parse(Request["infomationData"]);
            DisplayUntils.GetJson<string>("venueStatus", jObj, out venueStatus);
            DisplayUntils.GetJson<string>("venueId", jObj, out venueId);
            DisplayUntils.GetJson<string>("feedback", jObj, out feedback);
            int _venueStatus;
            Int32.TryParse(venueStatus, out _venueStatus);
            int _venueId;
            Int32.TryParse(venueId, out _venueId);
            venue.id = _venueId;
            var db = new NotificationManagerBusiness();
            var result = db.EditStatusNotification(_venueStatus, _venueId, feedback);
            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditStatusFile(int id)
        {
            ViewBag.id = id;
            var db = new NotificationManagerBusiness();
            var result = db.GetFileById(id);
            ViewBag.idNotifi = result.id_thongbao;
            return PartialView(result);
        }

        [HttpPost]
        public ActionResult _EditStatusFile()
        {
            m_diadiemtochuc venue = new m_diadiemtochuc();
            string venueStatus,
                venueId,
                feedback;
            JObject jObj = JObject.Parse(Request["infomationData"]);
            DisplayUntils.GetJson<string>("venueStatus", jObj, out venueStatus);
            DisplayUntils.GetJson<string>("venueId", jObj, out venueId);
            DisplayUntils.GetJson<string>("feedback", jObj, out feedback);
            int _venueStatus;
            Int32.TryParse(venueStatus, out _venueStatus);
            int _venueId;
            Int32.TryParse(venueId, out _venueId);
            venue.id = _venueId;
            var db = new NotificationManagerBusiness();
            var result = db.EditStatusFile(_venueStatus, _venueId, feedback);
            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult NotificationAlert(bool status = false)
        {
            var db = new NotificationManagerBusiness();
            var result = db.GetMyNotificationNotRead();
            ViewBag.roleId = AccountUntil.GetAccount().idquyentaikhoan;
            return PartialView(result);
        }
        public ActionResult _NotificationAlert()
        {
            var db = new NotificationManagerBusiness();
            var result = db.GetMyNotificationNotRead();
            ViewBag.roleId = AccountUntil.GetAccount().idquyentaikhoan;
            return PartialView(result);
        }

        public ActionResult NotificationAlertAccountReportPending(bool status = false)
        {
            var db = new NotificationManagerBusiness();
            var result = db.GetListAccountReporterPending();
            ViewBag.roleId = AccountUntil.GetAccount().idquyentaikhoan;
            return PartialView(result.Count);
        }
        public ActionResult _NotificationAlertAccountReportPending()
        {
            var db = new NotificationManagerBusiness();
            var result = db.GetListAccountReporterPending();
            ViewBag.roleId = AccountUntil.GetAccount().idquyentaikhoan;
            return PartialView(result);
        }

        public ActionResult AccountPendingControlLink(int idAcc)
        {
            var db = new NotificationManagerBusiness();
            var idRole = db.GetFirtRoleAccount(idAcc);
            return Json(new { idRole }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult NotificaMessageUpdateRead(int id)
        {
            var db = new NotificationManagerBusiness();
            var result = db.ReadNotificaStatus(id);
            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }
        public void ProcessRequest()
        {
           bool check = SocketProcessRequest.ProcessRequest();
            if (!check)
            {
                Session["uSession"] = null;
            }
            else
            {
                var myId = AccountUntil.GetAccount().id;
                SocketProcessRequest.SocketRequestAll(myId.ToString(), 1);
            }

        }
    }
}