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
    public class MeetingController : Controller
    {
        // GET: ADMIN/Meeting
        public ActionResult ListMeeting()
        {
            return PartialView();
        }
        public ActionResult _ListMeeting(int status = -1)
        {
            var db = new MeetingBusiness();
            var result = db.GetListMeeting(status);
            return PartialView(result);
        }

        public ActionResult AddMeeting()
        {
            return PartialView();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult _AddMeeting()
        {
            m_cuochop meeting = new m_cuochop();
            string meetContents,
                meetName,
                meetNote,
                meetLanguage,
                venuePlace,
                startDate,
                startTime,
                endTime,
                venueStatus;
            JObject jObj = JObject.Parse(Request["infomationData"]);
            DisplayUntils.GetJson<string>("meetContents", jObj, out meetContents);
            DisplayUntils.GetJson<string>("meetName", jObj, out meetName);
            DisplayUntils.GetJson<string>("meetNote", jObj, out meetNote);
            DisplayUntils.GetJson<string>("meetLanguage", jObj, out meetLanguage);
            DisplayUntils.GetJson<string>("venuePlace", jObj, out venuePlace);
            DisplayUntils.GetJson<string>("startDate", jObj, out startDate);
            DisplayUntils.GetJson<string>("startTime", jObj, out startTime);
            DisplayUntils.GetJson<string>("endTime", jObj, out endTime);
            DisplayUntils.GetJson<string>("venueStatus", jObj, out venueStatus);
            var avarta = Request.Files["avarta"];
            if (avarta != null)
            {
                List<string> exImg = new List<string>();
                exImg.Add(".png");
                exImg.Add(".jpeg");
                exImg.Add(".jpg");
                exImg.Add(".gif");
                meeting.avatar = FileUtils.FileSave(avarta, UrlConstant.UrlConst.AccountImage, exImg);
                var checkFileImg1 =
                    FileUtils.ValidateFileUpload(meeting.avatar, "image", DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Avatar, SystemMessageConst.systemmessage.AvatarEn));
                if (checkFileImg1.IsSuccess == false)
                {
                    return Json(new { result = checkFileImg1 }, JsonRequestBehavior.AllowGet);
                }
            }
            int _meetLanguage;
            if (!Int32.TryParse(meetLanguage, out _meetLanguage))
            {
                _meetLanguage = 1;
            }
            if (_meetLanguage != 1 && _meetLanguage != 2)
            {
                _meetLanguage = 1;
            }
            meeting.noidung = meetContents;
            meeting.ghichu = meetNote;
            meeting.tencuochop = meetName;
            meeting.phienban = _meetLanguage;
            meeting.diadiemtochuc = venuePlace;
            meeting.trangthai = StatusUntil.CheckAddStatus(venueStatus);
            
            var db = new MeetingBusiness();
            var result = db.AddMeet(meeting, startDate, startTime,endTime);
            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult EditMeeting(int id)
        {
            var db = new MeetingBusiness();
            var result = db.GetMeetingById(id);
            ViewBag.id = id;
            return PartialView(result);
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult _EditMeeting()
        {

            m_cuochop venue = new m_cuochop();
            string meetContents,
                meetName,
                meetNote,
                meetLanguage,
                venuePlace,
                meetStatus,
                adminStatus,
                meetId,
                startDate,
                startTime,
                endTime,
                meetReason;
            JObject jObj = JObject.Parse(Request["informationData"]);
            DisplayUntils.GetJson<string>("meetContents", jObj, out meetContents);
            DisplayUntils.GetJson<string>("meetName", jObj, out meetName);
            DisplayUntils.GetJson<string>("meetNote", jObj, out meetNote);
            DisplayUntils.GetJson<string>("meetLanguage", jObj, out meetLanguage);
            DisplayUntils.GetJson<string>("venuePlace", jObj, out venuePlace);
            DisplayUntils.GetJson<string>("meetStatus", jObj, out meetStatus);
            DisplayUntils.GetJson<string>("meetId", jObj, out meetId);
            DisplayUntils.GetJson<string>("startDate", jObj, out startDate);
            DisplayUntils.GetJson<string>("startTime", jObj, out startTime);
            DisplayUntils.GetJson<string>("endTime", jObj, out endTime);
            DisplayUntils.GetJson<string>("meetReason", jObj, out meetReason);
            DisplayUntils.GetJson<string>("adminStatus", jObj, out adminStatus);
            if (adminStatus == null && meetStatus == "2")
            {
                SystemMess systemMess = new SystemMess();
                systemMess.IsSuccess = false;
                systemMess.Message =
                    DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.MustBeChooseStatus,
                        SystemMessageConst.ValidateConst.MustBeChooseStatusEn);
                return Json(new { result = systemMess }, JsonRequestBehavior.AllowGet);
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
                var checkFileImg1 =
                    FileUtils.ValidateFileUpload(venue.avatar, "image", DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Avatar, SystemMessageConst.systemmessage.AvatarEn));
                if (checkFileImg1.IsSuccess == false)
                {
                    return Json(new { result = checkFileImg1 }, JsonRequestBehavior.AllowGet);
                }
            }
            int _adminStatus;
            Int32.TryParse(adminStatus, out _adminStatus);
            int _meetLanguage;
            if (!Int32.TryParse(meetLanguage, out _meetLanguage))
            {
                _meetLanguage = 1;
            }
            if (_meetLanguage != 1 && _meetLanguage != 2)
            {
                _meetLanguage = 1;
            }
            int _meetId;
            Int32.TryParse(meetId, out _meetId);
            venue.id = _meetId;
            venue.noidung = meetContents;
            venue.ghichu = meetNote;
            venue.tencuochop = meetName;
            venue.phienban = _meetLanguage;
            venue.diadiemtochuc = venuePlace;
            venue.lydotuchoi = meetReason;
            venue.trangthai = StatusUntil.CheckAddStatus(meetStatus);
            if (_adminStatus != 0 && AccountUntil.GetAccount().idquyentaikhoan == SystemMessageConst.RoleConst.Admin)
            {
                venue.trangthai = _adminStatus;
            }
            var db = new MeetingBusiness();
            var result = db.EditMeet(venue, startDate, startTime,endTime);
            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DeleteMeeting(int id)
        {
            var db = new MeetingBusiness();
            var result = db.DeleteMeeting(id);
            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult _EditStatusMeeting()
        {
            string venueStatus,
                venueId,
                feedback;
            JObject jObj = JObject.Parse(Request["infomationData"]);
            DisplayUntils.GetJson<string>("meetStatus", jObj, out venueStatus);
            DisplayUntils.GetJson<string>("meetId", jObj, out venueId);
            DisplayUntils.GetJson<string>("feedback", jObj, out feedback);
            int _venueStatus;
            Int32.TryParse(venueStatus, out _venueStatus);
            int _venueId;
            Int32.TryParse(venueId, out _venueId);
            var db = new MeetingBusiness();
            var result = db.EditStatusMeeting(_venueStatus, _venueId, feedback);
            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }
    }
}