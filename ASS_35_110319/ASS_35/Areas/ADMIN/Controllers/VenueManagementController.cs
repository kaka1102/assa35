using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataContext;
using DataContext.AdminBusiness;
using DataContext.Constants;
using DataContext.DAL;
using DataContext.Until;
using Newtonsoft.Json.Linq;

namespace ASS_35.Areas.ADMIN.Controllers
{
    [CheckSessionUser]
    public class VenueManagementController : Controller
    {
        // GET: ADMIN/VenueManagement
        public ActionResult ListVenue()
        {
            return PartialView();
        }
        public ActionResult _ListVenue(int status = -1)
        {
            var db = new VenueManagementBusiness();
            var result = db.GetListVenue(status);
            return PartialView(result);
        }

        public ActionResult AddVenue()
        {
            return PartialView();
        }
        
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult _AddVenue()
        {
            SystemMess systemMess = new SystemMess();
            m_diadiemtochuc venue = new m_diadiemtochuc();
            string venueContents,
                venueName,
                venueNote,
                venueLanguage,
                venueStatus;
            JObject jObj = JObject.Parse(Request["infomationData"]);
            DisplayUntils.GetJson<string>("venueContents", jObj, out venueContents);
            DisplayUntils.GetJson<string>("venueName", jObj, out venueName);
            DisplayUntils.GetJson<string>("venueNote", jObj, out venueNote);
            DisplayUntils.GetJson<string>("venueLanguage", jObj, out venueLanguage);
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
            venue.tendiadiem = venueName;
            venue.phienban = _venueLanguage;
            venue.trangthai = StatusUntil.CheckAddStatus(venueStatus);
            var avarta = Request.Files["avarta"];
            if (avarta == null)
            {
                systemMess.IsSuccess = false;
                systemMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Missingavatars, SystemMessageConst.systemmessage.MissingavatarsEn);
                return Json(new { result = systemMess }, JsonRequestBehavior.AllowGet);
            }
            List<string> exImg = new List<string>();
            exImg.Add(".png");
            exImg.Add(".jpeg");
            exImg.Add(".jpg");
            exImg.Add(".gif");
            venue.avatar = FileUtils.FileSave(avarta, UrlConstant.UrlConst.AccountImage, exImg);
            var checkFileAvatar =
                FileUtils.ValidateFileUpload(venue.avatar, "image", DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Avatar, SystemMessageConst.systemmessage.AvatarEn));
            if (checkFileAvatar.IsSuccess == false)
            {
                return Json(new { result = checkFileAvatar }, JsonRequestBehavior.AllowGet);
            }
            var db = new VenueManagementBusiness();
            var result = db.AddVenue(venue);
            if (result.IsSuccess == false)
            {
                if (System.IO.File.Exists(Request.MapPath(venue.avatar)))
                {
                    System.IO.File.Delete(Request.MapPath(venue.avatar));
                }
            }
            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult EditVenue(int id)
        {
            var db = new VenueManagementBusiness();
            var result = db.GetVenueById(id);
            ViewBag.id = id;
            return PartialView(result);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult _EditVenue()
        {
            m_diadiemtochuc venue = new m_diadiemtochuc();
            string venueContents,
                venueName,
                venueNote,
                venueLanguage,
                venueId,
                venueStatus,
                adminStatus,
                meetReason;
            JObject jObj = JObject.Parse(Request["infomationData"]);
            DisplayUntils.GetJson<string>("venueContents", jObj, out venueContents);
            DisplayUntils.GetJson<string>("venueName", jObj, out venueName);
            DisplayUntils.GetJson<string>("venueNote", jObj, out venueNote);
            DisplayUntils.GetJson<string>("venueLanguage", jObj, out venueLanguage);
            DisplayUntils.GetJson<string>("venueId", jObj, out venueId);
            DisplayUntils.GetJson<string>("venueStatus", jObj, out venueStatus);
            DisplayUntils.GetJson<string>("adminStatus", jObj, out adminStatus);
            DisplayUntils.GetJson<string>("meetReason", jObj, out meetReason);
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
            int _venueStatus;
            Int32.TryParse(venueStatus, out _venueStatus);
            Int32.TryParse(venueId, out _venueId);
            venue.id = _venueId;
            venue.noidung = venueContents;
            venue.ghichu = venueNote;
            venue.tendiadiem = venueName;
            venue.phienban = _venueLanguage;
            venue.lydotuchoi = meetReason;
            venue.trangthai = StatusUntil.CheckAddStatus(venueStatus);
            if (_adminStatus != 0 && AccountUntil.GetAccount().idquyentaikhoan == SystemMessageConst.RoleConst.Admin)
            {
                venue.trangthai = _adminStatus;
            }
            var avarta = Request.Files["avarta"];
            if (avarta != null)
            {
                List<string> exImg = new List<string>();
                exImg.Add(".png");
                exImg.Add(".jpeg");
                exImg.Add(".jpg");
                exImg.Add(".gif");
                venue.avatar = FileUtils.FileSave(avarta, UrlConstant.UrlConst.AccountImage, exImg);
                var checkFileAvatar =
                    FileUtils.ValidateFileUpload(venue.avatar, "image", DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Avatar, SystemMessageConst.systemmessage.AvatarEn));
                if (checkFileAvatar.IsSuccess == false)
                {
                    return Json(new { result = checkFileAvatar }, JsonRequestBehavior.AllowGet);
                }
            }
          
            var db = new VenueManagementBusiness();
            var result = db.EditVenue(venue);
            if (result.IsSuccess == false)
            {
                if (System.IO.File.Exists(Request.MapPath(venue.avatar)))
                {
                    System.IO.File.Delete(Request.MapPath(venue.avatar));
                }
            }
            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult _EditStatusVenue()
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
            var db = new VenueManagementBusiness();
            var result = db.EditStatusVenue(_venueStatus, _venueId, feedback);
            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteVenue(int id)
        {
            var db = new VenueManagementBusiness();
            var result = db.DeleteVenue(id);
            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }
    }
}