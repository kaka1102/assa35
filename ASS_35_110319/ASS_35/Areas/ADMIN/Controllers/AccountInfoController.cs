using System;
using System.Collections.Generic;
using System.Globalization;
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
    public class AccountInfoController : Controller
    {
        // GET: ADMIN/AccountInfo
        public ActionResult AccountInfomation()
        {
            var db = new AccountInfoBusiness();
            var result = db.GetAccountInfoById();
            ViewBag.info = result;
            if (AccountUntil.GetAccount().idcoquan != null)
            {
                  ViewBag.company = db.GetCompanyInfoById(AccountUntil.GetAccount().idcoquan.Value);
            }
            ViewBag.account = db.GetAccountById();
            ViewBag.isDelegate = db.CheckRole(SystemMessageConst.RoleConst.Daibieu);
            ViewBag.isEditors = db.CheckRole(SystemMessageConst.RoleConst.Banbientap);
            ViewBag.isReport = db.CheckRole(SystemMessageConst.RoleConst.Phongvien);
            ViewBag.isSecretary = db.CheckRole(SystemMessageConst.RoleConst.Banthuky);
            ViewBag.isAdmin = db.CheckRole(SystemMessageConst.RoleConst.Admin);
            return PartialView();
        }

        public ActionResult MemberOfGroup()
        {
            return PartialView();
        }
        public ActionResult EditInfomation()
        {
            string fullName,
                phone,
                address,
                postion;
            JObject JObj = JObject.Parse(System.Web.HttpContext.Current.Request["data"]);
            DisplayUntils.GetJson<string>("fullName", JObj, out fullName);
            DisplayUntils.GetJson<string>("phone", JObj, out phone);
            DisplayUntils.GetJson<string>("address", JObj, out address);
            DisplayUntils.GetJson<string>("postion", JObj, out postion);
            var db = new AccountInfoBusiness();
            m_thongtincanhan person = new m_thongtincanhan();
            person.hovaten = fullName;
            person.diachi = address;
            person.chucvu = postion;
            person.sodienthoai = phone;
            var result = db.EditInfomation(person);
            return Json(new { result}, JsonRequestBehavior.AllowGet);
        }
        public ActionResult _MemberOfGroup()
        {
            var db = new AccountInfoBusiness();
            var result = db.GetListMemberInGroup();
            return PartialView(result);
        }

        public ActionResult AddMember()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult _AddMember()
        {
            string personName,
                personGender,
                personBirth,
                personEmail,
                personAddress,
                personPhone,
                personPosition,
                personCardId,
                personDateRange,
                personDateExp,
                personPressCarNo,
                personCardLocation,
                personStatusCar,
                personStatusHotel;
            JObject JObj = JObject.Parse(System.Web.HttpContext.Current.Request["data"]);
            DisplayUntils.GetJson<string>("personName", JObj, out personName);
            DisplayUntils.GetJson<string>("personGender", JObj, out personGender);
            DisplayUntils.GetJson<string>("personBirth", JObj, out personBirth);
            DisplayUntils.GetJson<string>("personEmail", JObj, out personEmail);
            DisplayUntils.GetJson<string>("personAddress", JObj, out personAddress);
            DisplayUntils.GetJson<string>("personPhone", JObj, out personPhone);
            DisplayUntils.GetJson<string>("personPosition", JObj, out personPosition);
            DisplayUntils.GetJson<string>("personCardId", JObj, out personCardId);
            DisplayUntils.GetJson<string>("personDateRange", JObj, out personDateRange);
            DisplayUntils.GetJson<string>("personDateExp", JObj, out personDateExp);
            DisplayUntils.GetJson<string>("personPressCarNo", JObj, out personPressCarNo);
            DisplayUntils.GetJson<string>("personCardLocation", JObj, out personCardLocation);
            DisplayUntils.GetJson<string>("personStatusCar", JObj, out personStatusCar);
            DisplayUntils.GetJson<string>("personStatusHotel", JObj, out personStatusHotel);
            m_thongtincanhan info = new m_thongtincanhan();
            SystemMess systemMess = new SystemMess();
            bool _personGender;
            bool.TryParse(personGender, out _personGender);
            info.hovaten = personName;
            info.gioitinh = _personGender;
            DateTime _personBirth;
            if (DateTime.TryParseExact(personBirth, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None,
                out _personBirth))
            {
                info.ngaysinh = _personBirth;
            }
            else
            {
                systemMess.IsSuccess = false;
               // systemMess.Message = "Ngay sinh khong dung dinh dang";
                systemMess.Message = string.Format(DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.DateNotCorrectFormat, SystemMessageConst.ValidateConst.DateNotCorrectFormatEn), DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.DateOfBirth, SystemMessageConst.systemmessage.DateOfBirthEn));
                return Json(new {result = systemMess}, JsonRequestBehavior.AllowGet);
            }
            int _personStatusCar;
            int _personStatusHotel;
            Int32.TryParse(personStatusCar, out _personStatusCar);
            Int32.TryParse(personStatusHotel, out _personStatusHotel);
            info.dangkyxe = _personStatusCar;
            info.dangkyphong = _personStatusHotel;
            info.email = personEmail;
            info.diachi = personAddress;
            info.sodienthoai = personPhone;
            info.chucvu = personPosition;
            info.cmthochieu = personCardId;
            info.noicap = personCardLocation;
            DateTime _personDateRange;
            if (DateTime.TryParseExact(personDateRange, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None,
                out _personDateRange))
            {
                info.ngaycap = _personDateRange;
            }
            else
            {
                systemMess.IsSuccess = false;
               // systemMess.Message = string.Format(SystemMessageConst.ValidateConst.DateNotCorrectFormat, "Ngày cấp");
                systemMess.Message = string.Format(DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.DateNotCorrectFormat, SystemMessageConst.ValidateConst.DateNotCorrectFormatEn), DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.DateRange, SystemMessageConst.systemmessage.DateRangeEn));
                return Json(new {result = systemMess}, JsonRequestBehavior.AllowGet);
            }
            ;
            DateTime _personDateExp;
            if (DateTime.TryParseExact(personDateExp, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None,
                out _personDateExp))
            {
                info.ngayhethan = _personDateExp;
                ;
            }
            ;
            info.sothenhabao = personPressCarNo;


            var img1 = Request.Files["file1"];
            var img2 = Request.Files["file2"];
            var img3 = Request.Files["file3"];
            if (img1 == null || img2 == null)
            {
                systemMess.IsSuccess = false;
             //   systemMess.Message = "Thiếu ảnh chứng minh thư/hộ chiếu";
                systemMess.Message = string.Format(DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.CheckNotEmpty, SystemMessageConst.ValidateConst.CheckNotEmptyEn), DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Photo_of_ID_Card_Passport, SystemMessageConst.systemmessage.Photo_of_ID_Card_PassportEn));
                return Json(new {result = systemMess}, JsonRequestBehavior.AllowGet);
            }
            List<string> exFileDoc = new List<string>();
            exFileDoc.Add(".pdf");
            List<string> exImg = new List<string>();
            exImg.Add(".png");
            exImg.Add(".jpeg");
            exImg.Add(".jpg");
            exImg.Add(".gif");
            info.anhcmtmattruoc = FileUtils.FileSave(img1, UrlConstant.UrlConst.AccountImage, exImg);
            info.anhcmtmatsau = FileUtils.FileSave(img2, UrlConstant.UrlConst.AccountImage, exImg);
            info.fileanhthe = FileUtils.FileSave(img3, UrlConstant.UrlConst.AccountImage, exImg);
            var checkFileImg1 =
                FileUtils.ValidateFileUpload(info.anhcmtmattruoc, "image", DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Photo_of_ID_Card_Passport_Back, SystemMessageConst.systemmessage.Photo_of_ID_Card_Passport_BackEn));
            if (checkFileImg1.IsSuccess == false)
            {
                return Json(new {result = checkFileImg1}, JsonRequestBehavior.AllowGet);
            }
            var checkFileImg2 =
                FileUtils.ValidateFileUpload(info.anhcmtmatsau, "image", DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Photo_of_ID_Card_Passport_S, SystemMessageConst.systemmessage.Photo_of_ID_Card_Passport_S_En));
            if (checkFileImg2.IsSuccess == false)
            {
                return Json(new {result = checkFileImg2}, JsonRequestBehavior.AllowGet);
            }
            var checkFileImg3 =
                FileUtils.ValidateFileUpload(info.fileanhthe, "image", DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Photo_employee_Card, SystemMessageConst.systemmessage.Photo_employee_CardEn));
            if (checkFileImg3.IsSuccess == false)
            {
                return Json(new { result = checkFileImg3 }, JsonRequestBehavior.AllowGet);
            }
            var db = new AccountInfoBusiness();
            var result = db.AddMember(info);
            if (result.IsSuccess == false)
            {
                if (System.IO.File.Exists(Request.MapPath(info.anhcmtmattruoc)))
                {
                    System.IO.File.Delete(Request.MapPath(info.anhcmtmattruoc));
                }
                if (System.IO.File.Exists(Request.MapPath(info.anhcmtmatsau)))
                {
                    System.IO.File.Delete(Request.MapPath(info.anhcmtmatsau));
                }
            }
            return Json(new {result}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditMember(int id)
        {
            var db = new ManagerAccountBusiness();
            var result = db.GetMemberbyId(id);
            ViewBag.id = id;
            return PartialView(result);
        }

        public ActionResult _EditMember()
        {
            string personId,
                personName,
                personGender,
                personBirth,
                personEmail,
                personAddress,
                personPhone,
                personPosition,
                personCardId,
                personDateRange,
                personDateExp,
                personPressCarNo,
                personCardLocation,
                personStatusCar,
                personStatusHotel;
            JObject JObj = JObject.Parse(System.Web.HttpContext.Current.Request["data"]);
            DisplayUntils.GetJson<string>("personId", JObj, out personId);
            DisplayUntils.GetJson<string>("personName", JObj, out personName);
            DisplayUntils.GetJson<string>("personGender", JObj, out personGender);
            DisplayUntils.GetJson<string>("personBirth", JObj, out personBirth);
            DisplayUntils.GetJson<string>("personEmail", JObj, out personEmail);
            DisplayUntils.GetJson<string>("personAddress", JObj, out personAddress);
            DisplayUntils.GetJson<string>("personPhone", JObj, out personPhone);
            DisplayUntils.GetJson<string>("personPosition", JObj, out personPosition);
            DisplayUntils.GetJson<string>("personCardId", JObj, out personCardId);
            DisplayUntils.GetJson<string>("personDateRange", JObj, out personDateRange);
            DisplayUntils.GetJson<string>("personDateExp", JObj, out personDateExp);
            DisplayUntils.GetJson<string>("personPressCarNo", JObj, out personPressCarNo);
            DisplayUntils.GetJson<string>("personCardLocation", JObj, out personCardLocation);
            DisplayUntils.GetJson<string>("personStatusCar", JObj, out personStatusCar);
            DisplayUntils.GetJson<string>("personStatusHotel", JObj, out personStatusHotel);
            m_thongtincanhan info = new m_thongtincanhan();
            SystemMess systemMess = new SystemMess();
            bool _personGender;
            bool.TryParse(personGender, out _personGender);
            int _personId;
            Int32.TryParse(personId, out _personId);
            info.id = _personId;
            info.hovaten = personName;
            info.gioitinh = _personGender;
            DateTime _personBirth;
            if (DateTime.TryParseExact(personBirth, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None,
                out _personBirth))
            {
                info.ngaysinh = _personBirth;
            }
            else
            {
                systemMess.IsSuccess = false;
              //  systemMess.Message = "Ngay sinh khong dung dinh dang";
                systemMess.Message = string.Format(DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.DateNotCorrectFormat, SystemMessageConst.ValidateConst.DateNotCorrectFormatEn), DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.DateOfBirth, SystemMessageConst.systemmessage.DateOfBirthEn));
                return Json(new {result = systemMess}, JsonRequestBehavior.AllowGet);
            }
            int _personStatusCar;
            int _personStatusHotel;
            Int32.TryParse(personStatusCar, out _personStatusCar);
            Int32.TryParse(personStatusHotel, out _personStatusHotel);
            info.dangkyxe = _personStatusCar;
            info.dangkyphong = _personStatusHotel;
            info.email = personEmail;
            info.diachi = personAddress;
            info.sodienthoai = personPhone;
            info.chucvu = personPosition;
            info.cmthochieu = personCardId;
            info.noicap = personCardLocation;
            DateTime _personDateRange;
            if (DateTime.TryParseExact(personDateRange, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None,
                out _personDateRange))
            {
                info.ngaycap = _personDateRange;
            }
            else
            {
                systemMess.IsSuccess = false;
              //  systemMess.Message = string.Format(SystemMessageConst.ValidateConst.DateNotCorrectFormat, "Ngày cấp");
                systemMess.Message = string.Format(DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.DateNotCorrectFormat, SystemMessageConst.ValidateConst.DateNotCorrectFormatEn), DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.DateRange, SystemMessageConst.systemmessage.DateRangeEn));
                return Json(new {result = systemMess}, JsonRequestBehavior.AllowGet);
            }
            ;
            DateTime _personDateExp;
            if (DateTime.TryParseExact(personDateExp, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None,
                out _personDateExp))
            {
                info.ngayhethan = _personDateExp;
            }
            info.sothenhabao = personPressCarNo;

            if (Request.Files.Count > 0)
            {
                var img1 = Request.Files["file1"];
                var img2 = Request.Files["file2"];
                var img3 = Request.Files["file3"];

                List<string> exFileDoc = new List<string>();
                exFileDoc.Add(".pdf");
                List<string> exImg = new List<string>();
                exImg.Add(".png");
                exImg.Add(".jpeg");
                exImg.Add(".jpg");
                exImg.Add(".gif");
                if (img1 != null)
                {
                    info.anhcmtmattruoc = FileUtils.FileSave(img1, UrlConstant.UrlConst.AccountImage, exImg);
                    var checkFileImg1 =
                        FileUtils.ValidateFileUpload(info.anhcmtmattruoc, "image",
                           DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Photo_of_ID_Card_Passport_Back, SystemMessageConst.systemmessage.Photo_of_ID_Card_Passport_BackEn));
                    if (checkFileImg1.IsSuccess == false)
                    {
                        return Json(new {result = checkFileImg1}, JsonRequestBehavior.AllowGet);
                    }
                }
                if (img2 != null)
                {
                    info.anhcmtmatsau = FileUtils.FileSave(img2, UrlConstant.UrlConst.AccountImage, exImg);
                    var checkFileImg2 =
                        FileUtils.ValidateFileUpload(info.anhcmtmatsau, "image", DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Photo_of_ID_Card_Passport_S, SystemMessageConst.systemmessage.Photo_of_ID_Card_Passport_S_En));
                    if (checkFileImg2.IsSuccess == false)
                    {
                        return Json(new {result = checkFileImg2}, JsonRequestBehavior.AllowGet);
                    }
                }
                if (img3 != null)
                {
                    info.fileanhthe = FileUtils.FileSave(img3, UrlConstant.UrlConst.AccountImage, exImg);
                    var checkFileImg3 =
                        FileUtils.ValidateFileUpload(info.fileanhthe, "image", DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Photo_employee_Card, SystemMessageConst.systemmessage.Photo_employee_CardEn));
                    if (checkFileImg3.IsSuccess == false)
                    {
                        return Json(new { result = checkFileImg3 }, JsonRequestBehavior.AllowGet);
                    }
                }
            }
            var db = new AccountInfoBusiness();
            var result = db.EditMember(info);
            if (result.IsSuccess == false)
            {
                if (System.IO.File.Exists(Request.MapPath(info.anhcmtmattruoc)))
                {
                    System.IO.File.Delete(Request.MapPath(info.anhcmtmattruoc));
                }
                if (System.IO.File.Exists(Request.MapPath(info.anhcmtmatsau)))
                {
                    System.IO.File.Delete(Request.MapPath(info.anhcmtmatsau));
                }
                if (System.IO.File.Exists(Request.MapPath(info.fileanhthe)))
                {
                    System.IO.File.Delete(Request.MapPath(info.fileanhthe));
                }
            }
            return Json(new {result}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteMember(int id)
        {
            var db = new AccountInfoBusiness();
            var result = db.DeleteMember(id);
            return Json(new {result}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ChangePassword()
        {
            return PartialView();
        }
        public ActionResult _ChangePassword()
        {
            SystemMess systemMess = new SystemMess();
            ;
            string oldPass,
                newPass,
                confirmNewPass;
            JObject jObj = JObject.Parse(System.Web.HttpContext.Current.Request["data"]);
            DisplayUntils.GetJson<string>("oldPass", jObj, out oldPass);
            DisplayUntils.GetJson<string>("newPass", jObj, out newPass);
            DisplayUntils.GetJson<string>("confirmNewPass", jObj, out confirmNewPass);
            if (newPass != confirmNewPass)
            {
                systemMess.IsSuccess = false;
               // systemMess.Message = SystemMessageConst.systemmessage.ConfirmPasswordNotCorrect;

                systemMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.ConfirmPasswordNotCorrect, SystemMessageConst.systemmessage.ConfirmPasswordNotCorrectEn);
                return Json(new { result = systemMess }, JsonRequestBehavior.AllowGet);
            }
            var db = new AccountInfoBusiness();
            var result = db.ChangePassword(oldPass,newPass);
            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }
    }
}