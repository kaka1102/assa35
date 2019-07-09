using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
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
    public class AccountController : Controller
    {
        // GET: ADMIN/Account
        public ActionResult RegisterAccount()
        {
            return PartialView();
        }

        public ActionResult ConfirmEmail(string id)
        {
            var db = new AccountBusiness();
            var result = db.GetConfirmEmail(id);
            return PartialView(result);
        }
        [HttpPost]
        public ActionResult _RegisterAccount()
        {
            JObject JObj = JObject.Parse(System.Web.HttpContext.Current.Request["data"]);
            string accEmail,
                accAu,
                accType,
                companyName,
                companyAddress,
                companyFax,
                companyEmail,
                companyPhone,
                companyCountry,
                companyStatus,
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
                personPark,
                personRoom;
            DisplayUntils.GetJson<string>("accEmail", JObj, out accEmail);
            DisplayUntils.GetJson<string>("accAu", JObj, out accAu);
            DisplayUntils.GetJson<string>("accType", JObj, out accType);
            DisplayUntils.GetJson<string>("companyName", JObj, out companyName);
            DisplayUntils.GetJson<string>("companyAddress", JObj, out companyAddress);
            DisplayUntils.GetJson<string>("companyFax", JObj, out companyFax);
            DisplayUntils.GetJson<string>("companyEmail", JObj, out companyEmail);
            DisplayUntils.GetJson<string>("companyPhone", JObj, out companyPhone);
            DisplayUntils.GetJson<string>("companyCountry", JObj, out companyCountry);
            DisplayUntils.GetJson<string>("companyStatus", JObj, out companyStatus);
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
            DisplayUntils.GetJson<string>("personPark", JObj, out personPark);
            DisplayUntils.GetJson<string>("personRoom", JObj, out personRoom);
            int _companyStatus;
            int _personPark;
            int _personRoom;
            bool _personGender;
            bool.TryParse(personGender, out _personGender);
            Int32.TryParse(companyStatus, out _companyStatus);
            Int32.TryParse(personPark, out _personPark);
            Int32.TryParse(personRoom, out _personRoom);
            m_taikhoan acc = new m_taikhoan();
            m_thongtincanhan info = new m_thongtincanhan();
            m_thongtincoquan comInfo = new m_thongtincoquan();
            m_quyentaikhoan au = new m_quyentaikhoan();
            SystemMess systemMess = new SystemMess();
            //tao object taikhoan
            int _accType;
            Int32.TryParse(accType, out _accType);
            int _companyCountry;
            Int32.TryParse(companyCountry, out _companyCountry);
            acc.email = accEmail;
            acc.ngaytao = DateTime.Now;
            acc.idloaitaikhoan = _accType;
            //tao object co quan
            comInfo.tencoquan = companyName;
            comInfo.diachicoquan = companyAddress;
            comInfo.quocgia = _companyCountry;
            comInfo.emailcoquan = companyEmail;
            comInfo.sodienthoai = companyPhone;
            comInfo.sofax = companyFax;
            comInfo.trangthaiduatin = _companyStatus;
            List<string> exImg = new List<string>();
            exImg.Add(".png");
            exImg.Add(".jpeg");
            exImg.Add(".jpg");
//            exImg.Add(".gif");
            var fileDocument = Request.Files["file1"];
         
            if (acc.idloaitaikhoan == SystemMessageConst.AccountType.Personal)
            {
                //tao object thog tin ca nhan
                info.hovaten = personName;
                info.gioitinh = _personGender;
                info.noicap = personCardLocation;
                info.dangkyphong = _personRoom;
                info.dangkyxe = _personPark;
                DateTime _personBirth;
                if (DateTime.TryParseExact(personBirth, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None,
                    out _personBirth))
                {
                    info.ngaysinh = _personBirth;
                }
                else
                {
                    systemMess.IsSuccess = false;
                   // systemMess.Message = "Ngay sinh khong dung dinh dang";
                    systemMess.Message = string.Format(DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.DateNotCorrectFormat, SystemMessageConst.ValidateConst.DateNotCorrectFormatEn), DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.DateOfBirth, SystemMessageConst.systemmessage.DateOfBirthEn));

                    return Json(new { result = systemMess }, JsonRequestBehavior.AllowGet);
                }
                info.email = personEmail;
                info.diachi = personAddress;
                info.sodienthoai = personPhone;
                info.chucvu = personPosition;
                info.cmthochieu = personCardId;
                DateTime _personDateRange;
                if (DateTime.TryParseExact(personDateRange, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None,
                    out _personDateRange))
                {
                    info.ngaycap = _personDateRange;
                }
                else
                {
                    systemMess.IsSuccess = false;
                   // systemMess.Message = string.Format(SystemMessageConst.ValidateConst.DateNotCorrectFormat, "Ngày cấp");

                    systemMess.Message = string.Format(DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.DateNotCorrectFormat, SystemMessageConst.ValidateConst.DateNotCorrectFormatEn), DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.DateRange, SystemMessageConst.systemmessage.DateRangeEn));
                    return Json(new { result = systemMess }, JsonRequestBehavior.AllowGet);
                }
                ;
                DateTime _personDateExp;
                if (DateTime.TryParseExact(personDateExp, "dd-MM-yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None,
                        out _personDateExp) == true)
                {
                    info.ngayhethan = _personDateExp;
                }
                info.sothenhabao = personPressCarNo;
                var img1 = Request.Files["file2"];
                var img2 = Request.Files["file3"];
                var img3 = Request.Files["file4"];
                if (img1 == null || img2 == null)
                {
                    systemMess.IsSuccess = false;
                   // systemMess.Message = "Thiếu ảnh chứng minh thư / hộ chiếu";
                    systemMess.Message = string.Format(DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.CheckNotEmpty, SystemMessageConst.ValidateConst.CheckNotEmptyEn), DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Photo_of_ID_Card_Passport, SystemMessageConst.systemmessage.Photo_of_ID_Card_PassportEn));
                    return Json(new { result = systemMess }, JsonRequestBehavior.AllowGet);
                }
                if (img3 == null)
                {
                    systemMess.IsSuccess = false;
                   // systemMess.Message = "Thiếu ảnh thẻ nhân viên";
                    systemMess.Message = string.Format(DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.CheckNotEmpty, SystemMessageConst.ValidateConst.CheckNotEmptyEn), DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Photo_employee_Card, SystemMessageConst.systemmessage.Photo_employee_CardEn));
                    return Json(new { result = systemMess }, JsonRequestBehavior.AllowGet);
                }
                info.anhcmtmattruoc = FileUtils.FileSave(img1, UrlConstant.UrlConst.AccountImage, exImg);
                info.anhcmtmatsau = FileUtils.FileSave(img2, UrlConstant.UrlConst.AccountImage, exImg);
                info.fileanhthe = FileUtils.FileSave(img3, UrlConstant.UrlConst.AccountImage, exImg);
                var checkFileImg1 =
                    FileUtils.ValidateFileUpload(info.anhcmtmattruoc, "image", DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Photo_of_ID_Card_Passport_Back, SystemMessageConst.systemmessage.Photo_of_ID_Card_Passport_BackEn));
                if (checkFileImg1.IsSuccess == false)
                {
                    return Json(new { result = checkFileImg1 }, JsonRequestBehavior.AllowGet);
                }
                var checkFileImg2 =
                    FileUtils.ValidateFileUpload(info.anhcmtmatsau, "image", DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Photo_of_ID_Card_Passport_S, SystemMessageConst.systemmessage.Photo_of_ID_Card_Passport_S_En));
                if (checkFileImg2.IsSuccess == false)
                {
                    return Json(new { result = checkFileImg2 }, JsonRequestBehavior.AllowGet);
                }
                var checkFileImg3 =
                    FileUtils.ValidateFileUpload(info.fileanhthe, "image", DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Photo_employee_Card, SystemMessageConst.systemmessage.Photo_employee_CardEn));
                if (checkFileImg3.IsSuccess == false)
                {
                    return Json(new { result = checkFileImg2 }, JsonRequestBehavior.AllowGet);
                }
            }
            //tao object quyen tai khoan
            int _accAu;
            Int32.TryParse(accAu, out _accAu);
            au.idquyen = _accAu;
            if (fileDocument == null)
            {
                systemMess.IsSuccess = false;
              //  systemMess.Message = "Thiếu file công văn";
                systemMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.FileDocumentary, SystemMessageConst.systemmessage.FileDocumentaryEn);
                return Json(new { result = systemMess }, JsonRequestBehavior.AllowGet);
            }
         
            comInfo.filecongvan = FileUtils.FileSave(fileDocument, UrlConstant.UrlConst.AccountImage, exImg);

            var checkFileCongVan = FileUtils.ValidateFileUpload(comInfo.filecongvan, "image", DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Documentary, SystemMessageConst.systemmessage.DocumentaryEn));
            if (checkFileCongVan.IsSuccess == false)
            {
                return Json(new {result = checkFileCongVan}, JsonRequestBehavior.AllowGet);
            }
            var db = new AccountBusiness();
            var result = db.RegisterAccount(acc, info, comInfo, au);
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
                if (System.IO.File.Exists(Request.MapPath(comInfo.filecongvan)))
                {
                    System.IO.File.Delete(Request.MapPath(comInfo.filecongvan));
                }
            }
            return Json(new {result}, JsonRequestBehavior.AllowGet);
        }
    }
}