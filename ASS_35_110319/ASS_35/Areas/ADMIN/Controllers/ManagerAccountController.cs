using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataContext;
using DataContext.AdminBusiness;
using DataContext.ClientBusiness;
using DataContext.Constants;
using DataContext.DAL;
using DataContext.Until;
using Newtonsoft.Json.Linq;

namespace ASS_35.Areas.ADMIN.Controllers
{
     [CheckSessionUser]
    public class ManagerAccountController : Controller
    {
        // GET: ADMIN/AdminManager
        public ActionResult ManagerAccount()
        {
            return PartialView();
        }

        public ActionResult _ManagerAccount(string searchData, string roleId = "2", int page = 1, string statusId = "-1")
        {
            ViewBag.pageSelected = page;
            ViewBag.size = 10;
            var db = new ManagerAccountBusiness();
            var result = db.GetListAccounts(roleId, searchData, page, ViewBag.size, statusId);
            if (result.TotalRecort % 10 == 0)
            {
                ViewBag.totalPage = result.TotalRecort / ViewBag.size;
            }
            else
            {
                ViewBag.totalPage = result.TotalRecort / ViewBag.size + 1;
            }
            return PartialView(result.Data);
        }

        public ActionResult AddManager()
        {
            return PartialView();
        }

        [HttpPost]
        public ActionResult _AddManager()
        {
            SystemMess systemMess = new SystemMess();
            JObject JObj = JObject.Parse(System.Web.HttpContext.Current.Request["data"]);
            string accEmail,
                confirmAccEmail,
                password,
                confirmPassword,
                role2,
                role3;
            DisplayUntils.GetJson<string>("accEmail", JObj, out accEmail);
            DisplayUntils.GetJson<string>("confirmAccEmail", JObj, out confirmAccEmail);
            DisplayUntils.GetJson<string>("password", JObj, out password);
            DisplayUntils.GetJson<string>("confirmPassword", JObj, out confirmPassword);
            DisplayUntils.GetJson<string>("role2", JObj, out role2);
            DisplayUntils.GetJson<string>("role3", JObj, out role3);
            if (!accEmail.Equals(confirmAccEmail))
            {
                systemMess.IsSuccess = false;
               // systemMess.Message = SystemMessageConst.ValidateConst.EmailConfirmNotCorrectEn;
                systemMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.EmailConfirmNotCorrect, SystemMessageConst.ValidateConst.EmailConfirmNotCorrectEn);
                return Json(new {result = systemMess}, JsonRequestBehavior.AllowGet);
            }
            if (!password.Equals(confirmPassword))
            {
                systemMess.IsSuccess = false;
              //  systemMess.Message = SystemMessageConst.systemmessage.ConfirmPasswordNotCorrect;
                systemMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.ConfirmPasswordNotCorrect, SystemMessageConst.systemmessage.ConfirmPasswordNotCorrectEn);
                return Json(new {result = systemMess}, JsonRequestBehavior.AllowGet);
            }
            var db = new ManagerAccountBusiness();
            m_taikhoan account = new m_taikhoan();
            account.email = accEmail;
            account.matkhau = password;
            var result = db.AddAccount(account, role2, role3);
            return Json(new {result}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EditManager(int id)
        {
            var db = new ManagerAccountBusiness();
            var result = db.GetAccountById(id);
            ViewBag.role2 = db.CheckAuByUserId(id, 2);
            ViewBag.role3 = db.CheckAuByUserId(id, 3);
            return PartialView(result);
        }

        public ActionResult _EditManager()
        {
            string id,
                status,
                role2,
                role3,
                reason;
            JObject JObj = JObject.Parse(System.Web.HttpContext.Current.Request["data"]);
            DisplayUntils.GetJson<string>("id", JObj, out id);
            DisplayUntils.GetJson<string>("status", JObj, out status);
            DisplayUntils.GetJson<string>("role2", JObj, out role2);
            DisplayUntils.GetJson<string>("role3", JObj, out role3);
            DisplayUntils.GetJson<string>("reason", JObj, out reason);
//            if (status == null)
//            {
//                SystemMess  systemMess = new SystemMess();
//                systemMess.IsSuccess = false;
//                systemMess.Message =
//                    DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.MustBeChooseStatus,
//                        SystemMessageConst.ValidateConst.MustBeChooseStatusEn);
//                return Json(new { result = systemMess }, JsonRequestBehavior.AllowGet);
//            }
            var db = new ManagerAccountBusiness();
            var result = db.EditAccount(id, status, role2, role3, reason);
            return Json(new {result}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DeleteAccount(string id)
        {
            var db = new ManagerAccountBusiness();
            var result = db.DeleteAccountById(id);
            return Json(new {result}, JsonRequestBehavior.AllowGet);
        }

        public ActionResult DelegateManager()
        {
            return PartialView();
        }

        public ActionResult EditDelegateManager(int id,string type)
        {
            var db = new ManagerAccountBusiness();
            var dbInfo = new AccountInfoBusiness();
            var result = db.GetAccountById(id);
            var lstMember = db.GetPersonInAccountById(id);
            ViewBag.lstMem = lstMember;
            ViewBag.targetAcc = id;
            ViewBag.type = type;
            ViewBag.company = dbInfo.GetCompanyInfoById(result.idcoquan);
            return PartialView(result);
        }

        public ActionResult EditDelegate(int id)
        {
            var db = new ManagerAccountBusiness();
            var dbInfo = new AccountInfoBusiness();
            var account = db.GetAccountById(id);
            var regisInfo = dbInfo.GetRegisById(id);
            var room = dbInfo.RoomOrder(regisInfo.idRoomOrder.Value);
            var RegisAct = db.GetActivityRegis(regisInfo.id);
            ViewBag.status = account.trangthai;
            ViewBag.myRole = AccountUntil.GetAccount().idquyentaikhoan;
            ViewBag.regis = regisInfo;
            ViewBag.room = room;
            ViewBag.regisAct = RegisAct;
            return PartialView(account);
        }

        public ActionResult _EditDelegateManager()
        {
            string id,
                status,
                reason;
            JObject JObj = JObject.Parse(System.Web.HttpContext.Current.Request["data"]);
            DisplayUntils.GetJson<string>("id", JObj, out id);
            DisplayUntils.GetJson<string>("status", JObj, out status);
            DisplayUntils.GetJson<string>("reason", JObj, out reason);
            string inviteDocument = null;
            var file = Request.Files["file1"];
            if (status == null)
            {
                SystemMess systemMess = new SystemMess();
                systemMess.IsSuccess = false;
                systemMess.Message =
                    DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.MustBeChooseStatus,
                        SystemMessageConst.ValidateConst.MustBeChooseStatusEn);
                return Json(new { result = systemMess }, JsonRequestBehavior.AllowGet);
            }
            var db = new ManagerAccountBusiness();
            var result = db.EditAccountDelegate(id, status, reason, file);
            return Json(new {result}, JsonRequestBehavior.AllowGet);
        }
        public ActionResult _EditDelegate()
        {
            string id,
                status,
                reason;
            JObject JObj = JObject.Parse(System.Web.HttpContext.Current.Request["data"]);
            DisplayUntils.GetJson<string>("id", JObj, out id);
            DisplayUntils.GetJson<string>("status", JObj, out status);
            DisplayUntils.GetJson<string>("reason", JObj, out reason);
           
            if (status == null)
            {
                SystemMess systemMess = new SystemMess();
                systemMess.IsSuccess = false;
                systemMess.Message =
                    DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.MustBeChooseStatus,
                        SystemMessageConst.ValidateConst.MustBeChooseStatusEn);
                return Json(new { result = systemMess }, JsonRequestBehavior.AllowGet);
            }
            var db = new ManagerAccountBusiness();
            var result = db.EditDelegate(id, status, reason);
            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult _DelegateManager(string searchData, int page = 1, string statusId = "-1")
        {
            ViewBag.pageSelected = page;
            ViewBag.size = 10;
            var db = new ManagerAccountBusiness();
            var result = db.GetListAccounts("5", searchData, page, ViewBag.size, statusId);
            if (result.TotalRecort % 10 == 0)
            {
                ViewBag.totalPage = result.TotalRecort / ViewBag.size;
            }
            else
            {
                ViewBag.totalPage = result.TotalRecort / ViewBag.size + 1;
            }
            return PartialView(result.Data);
        }

        public ActionResult ReporterManager()
        {
            return PartialView();
        }

        public ActionResult _ReporterManager(string searchData, int page = 1, string statusId = "-1")
        {
            ViewBag.pageSelected = page;
            ViewBag.size = 10;
            var db = new ManagerAccountBusiness();
            var result = db.GetListAccounts("4", searchData, page, ViewBag.size, statusId);
            if (result.TotalRecort % 10 == 0)
            {
                ViewBag.totalPage = result.TotalRecort / ViewBag.size;
            }
            else
            {
                ViewBag.totalPage = result.TotalRecort / ViewBag.size + 1;
            }
            return PartialView(result.Data);
        }

        public ActionResult ListMemberInAccount(List<m_thongtincanhan> data)
        {
            return PartialView(data);
        }

        public ActionResult EditMember(int id)
        {
            var db = new ManagerAccountBusiness();
            var result = db.GetMemberbyId(id);
            return PartialView(result);
        }
        [HttpPost]
        public ActionResult ActiveMember()
        {
            string memberStatus,
                memberId,
                feedback;
            JObject jObj = JObject.Parse(Request["infomationData"]);
            DisplayUntils.GetJson<string>("memberStatus", jObj, out memberStatus);
            DisplayUntils.GetJson<string>("memberId", jObj, out memberId);
            DisplayUntils.GetJson<string>("feedback", jObj, out feedback);
            if (memberStatus == null)
            {
                SystemMess systemMess = new SystemMess();
                systemMess.IsSuccess = false;
                systemMess.Message =
                    DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.MustBeChooseStatus,
                        SystemMessageConst.ValidateConst.MustBeChooseStatusEn);
                return Json(new { result = systemMess }, JsonRequestBehavior.AllowGet);
            }
            int _memberStatus;
            Int32.TryParse(memberStatus, out _memberStatus);
            int _memberId;
            Int32.TryParse(memberId, out _memberId);
            var db = new ManagerAccountBusiness();
            var result = db.EditStatusMember(_memberStatus, _memberId, feedback);
            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetListAct( int idregis)
        {
            var result = new BS_LoadActivity().GetListActById(idregis);
            return View(result);
        }
        //public ActionResult GetListOp(int id, int idregis)
        //{
        //    var result = new BS_LoadActivity().GetListOp(id);
        //    return View(result);
        //}

        public ActionResult GetHotel(int idroom)
        {
            var result = new BS_LoadActivity().GetHotelById(idroom);
            return View(result);
        }
    }
}