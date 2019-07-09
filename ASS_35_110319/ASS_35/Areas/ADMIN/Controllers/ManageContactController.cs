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
    public class ManageContactController : Controller
    {
        // GET: ADMIN/ManageContact
        public ActionResult ListContact()
        {
            
           
            return PartialView();
        }

        public ActionResult _ListContact(int language)
        {
            var db = new ManagerContactBusiness();
            var result = db.GetContact(language);
            if (result != null)
            {
                ViewBag.content = result.noidung;
            }
            else
            {
                ViewBag.content = "";
            }
            return PartialView(result);
        }

        public ActionResult AddContact()
        {
            return PartialView();
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult _AddContact()
        {
            m_lienhe venue = new m_lienhe();
            string venueContents,
                venueName,
                venueAddress,
                venueLanguage,
                venueStatus;
            JObject jObj = JObject.Parse(Request["infomationData"]);
            DisplayUntils.GetJson<string>("venueContents", jObj, out venueContents);
            DisplayUntils.GetJson<string>("venueName", jObj, out venueName);
            DisplayUntils.GetJson<string>("venueAddress", jObj, out venueAddress);
            DisplayUntils.GetJson<string>("venueLanguage", jObj, out venueLanguage);
            DisplayUntils.GetJson<string>("venueLanguage", jObj, out venueStatus);
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
            venue.diachi = venueAddress;
            venue.tendonvi = venueName;
            venue.phienban = _venueLanguage;
            venue.trangthai = StatusUntil.CheckAddStatus(venueStatus);
            var db = new ManagerContactBusiness();
            var result = db.AddContact(venue);
            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult _EditContact()
        {
            m_lienhe venue = new m_lienhe();
            string
                venueContents,language;
//                venueName,
//                venueAddress,
//                venueLanguage,
//                venueId,
//                venueStatus,
//                adminStatus,
//                venueReason;
            JObject jObj = JObject.Parse(Request["infomationData"]);
            DisplayUntils.GetJson<string>("venueContents", jObj, out venueContents);
            DisplayUntils.GetJson<string>("language", jObj, out language);
            //            DisplayUntils.GetJson<string>("venueName", jObj, out venueName);
            //            DisplayUntils.GetJson<string>("venueAddress", jObj, out venueAddress);
            //            DisplayUntils.GetJson<string>("venueLanguage", jObj, out venueLanguage);
            //            DisplayUntils.GetJson<string>("venueId", jObj, out venueId);
            //            DisplayUntils.GetJson<string>("venueStatus", jObj, out venueStatus);
            //            DisplayUntils.GetJson<string>("adminStatus", jObj, out adminStatus);
            //            DisplayUntils.GetJson<string>("venueReason", jObj, out venueReason);
            //            if (adminStatus == null && venueStatus == "2")
            //            {
            //                SystemMess systemMess = new SystemMess();
            //                systemMess.IsSuccess = false;
            //                systemMess.Message =
            //                    DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.MustBeChooseStatus,
            //                        SystemMessageConst.ValidateConst.MustBeChooseStatusEn);
            //                return Json(new { result = systemMess }, JsonRequestBehavior.AllowGet);
            //            }
            //            int _venueLanguage;

            //            int _venueId;
            //            Int32.TryParse(venueId, out _venueId);
            //            int _adminStatus;
            //            Int32.TryParse(adminStatus, out _adminStatus);
            //            venue.id = _venueId;
            //            venue.trangthai = StatusUntil.CheckAddStatus(venueStatus);
            //            if (_adminStatus != 0 && AccountUntil.GetAccount().idquyentaikhoan == SystemMessageConst.RoleConst.Admin)
            //            {
            //                venue.trangthai = _adminStatus;
            //            }
            int _language;
            Int32.TryParse(language, out _language);
            venue.noidung = venueContents;
            venue.phienban = _language;
//            venue.diachi = venueAddress;
//            venue.tendonvi = venueName;
//            venue.phienban = _venueLanguage;
            var db = new ManagerContactBusiness();
            var result = db.EditContact(venue);
            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult EditContact(int id)
        {
            var db = new ManagerContactBusiness();
            var result = db.GetContactById(id);
            ViewBag.id = id;
            return PartialView(result);
        }

        public ActionResult DeleteContact(int id)
        {
            var db = new ManagerContactBusiness();
            var result = db.DeleteContact(id);
            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }
    }
}