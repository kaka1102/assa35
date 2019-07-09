using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataContext;
using DataContext.AdminBusiness;
using DataContext.ClientBusiness;
using DataContext.CommonBusiness;
using DataContext.Constants;
using DataContext.Until;

namespace ASS_35.Areas.ADMIN.Controllers
{
    public class CommonController : Controller
    {
        // GET: ADMIN/Common
        public ActionResult ListCountry(string name,int idSelected = 238)
        {
            ViewBag.name = name;
            ViewBag.idSelected = idSelected;
            var db = new CommonBusiness();
            var result = db.ListCountry();
            return PartialView(result);
        }

        public ActionResult ListRole(string name)
        {
            ViewBag.name = name;
            var db = new CommonBusiness();
            var result = db.ListRole();
            return PartialView(result);
        }

        public ActionResult PageList(int pageSelected = 1,int totalPage = 1)
        {
            ViewBag.totalPage = totalPage;
            ViewBag.pageSelected = pageSelected;
            return PartialView();
        }
        public ActionResult AccountStatus(int id)
        {
            ViewBag.id = id;
            return PartialView();
        }
        public ActionResult NotifiStatus(int id)
        {
            ViewBag.id = id;
            return PartialView();
        }
        public ActionResult AccountMemberStatus(int id)
        {
            ViewBag.id = id;
            return PartialView();
        }
        public ActionResult Status(int id)
        {
            ViewBag.id = id;
            return PartialView();
        }
        public ActionResult Status2(int id)
        {
            ViewBag.id = id;
            return PartialView();
        }
        public ActionResult StatusReadAndNotRead(string name, bool? idSelected)
        {
            ViewBag.name = name;
            ViewBag.idSelected = idSelected;
            return PartialView();
        }
        [CheckSessionUser]
        public ActionResult ListStatus(string name,int? idSelected,bool hasAll = false)
        {
            ViewBag.name = name;
            ViewBag.idSelected = idSelected;
            ViewBag.hasAll = hasAll;
            var db = new CommonBusiness();
            var result = db.ListStatus();
            return PartialView(result);
        }
        [CheckSessionUser]
        public ActionResult ShowCurrentAccountStatus(int status)
        {
            if (status == SystemMessageConst.StatusConst.Active)
            {
                ViewBag.value = (DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.AccountStatusConst.Active,
                    SystemMessageConst.AccountStatusConst.ActiveEn));
            }
            if (status == SystemMessageConst.StatusConst.Deactive)
            {
                ViewBag.value = (DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.AccountStatusConst.Deactive,
                    SystemMessageConst.AccountStatusConst.DeactiveEn));
            }
            if (status == SystemMessageConst.StatusConst.Pending)
            {
                ViewBag.value = (DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.AccountStatusConst.Pending,
                    SystemMessageConst.AccountStatusConst.PendingEn));
            }
            return PartialView();
        }
        [CheckSessionUser]
        public ActionResult ListAccountStatus(string name, int? idSelected, bool hasAll = false,bool isDisable = false)
        {
            ViewBag.name = name;
            ViewBag.idSelected = idSelected;
            ViewBag.hasAll = hasAll;
            ViewBag.isDisable = isDisable;
            var db = new CommonBusiness();
            var result = db.ListStatus();
            return PartialView(result);
        }
        [CheckSessionUser]
        public ActionResult ListStatusActiveAndDeActive(string name, int? idSelected,bool isAdmin = false,bool hasAll = false,bool isDisable = false)
        {
            ViewBag.idSelected = idSelected;
            ViewBag.name = name;
            ViewBag.isDisable = isDisable;
            ViewBag.check = isAdmin;
            ViewBag.hasAll = hasAll;
            return PartialView();
        }

        [CheckSessionUser]
        public ActionResult Notification_status_cbx(string name, int? idSelected, bool isAdmin = false, bool hasAll = false, bool isDisable = false)
        {
            ViewBag.idSelected = idSelected;
            ViewBag.name = name;
            ViewBag.isDisable = isDisable;
            ViewBag.check = isAdmin;
            ViewBag.hasAll = hasAll;
            return PartialView();
        }
        [CheckSessionUser]
        public ActionResult ListVenue(string name, int? idSelected,bool isDisable = false)
        {
            var db = new CommonBusiness();
            var result = db.ListVenue();
            ViewBag.isDisable = isDisable;
            ViewBag.idSelected = idSelected;
            ViewBag.name = name;
            return PartialView(result);
        }
        [CheckSessionUser]
        public ActionResult GetAuByAccountId(int accountId)
        {
            var db = new CommonBusiness();
            var result = db.ListAuByAccId(accountId);
            return PartialView(result);
        }

        public ActionResult AccountType(int id)
        {
            ViewBag.id = id;
            return PartialView();
        }
        [CheckSessionUser]
        public ActionResult ListFileByNotifiId(int id,List<m_filethongbao> lstFile)
        {
            var db = new CommonBusiness();
            var result = db.ListFileByNotifiId(id,lstFile);
            return PartialView(result);
        }
        [CheckSessionUser]
        public ActionResult ListReAndDe(int? idNotifica,bool isDisable = false)
        {
            var db = new CommonBusiness();
            if (idNotifica != null)
            {
                var lstPerson = db.GetListPersonReceiveNotification(idNotifica.Value);
                ViewBag.lstPerson = lstPerson;
            }
            ViewBag.isDisable = isDisable;
            var result = db.GetListReAndDe();
            ViewBag.listRe = result.Where(ob => ob.type == SystemMessageConst.RoleConst.Phongvien).ToList();
            ViewBag.listDe = result.Where(ob => ob.type == SystemMessageConst.RoleConst.Daibieu).ToList();
            return PartialView();
        }
        [CheckSessionUser]
        public ActionResult ListStatusRadioPost(string name, int? idSelected,int? status,bool hasRemoveSite = false)
        {
            ViewBag.idSelected = idSelected;
            ViewBag.name = name;
            ViewBag.status = status;
            ViewBag.hasRemoveSite = hasRemoveSite;
            return PartialView();
        }
        [CheckSessionUser]
        public ActionResult ListSaveButton(int? status,int? authorId,bool removeButton = false,bool isNotifi = false)
        {
            ViewBag.removeButton = removeButton;
            ViewBag.status = status;
            ViewBag.authorId = authorId;
            ViewBag.myRole = AccountUntil.GetAccount().idquyentaikhoan;
            if (status == SystemMessageConst.StatusConst.Active)
            {
                if (isNotifi)
                {
                    ViewBag.value = Resources.HomeText.Notifi_Active;
                }
                else
                {
                      ViewBag.value = (DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.AccountStatusConst.Active,
                    SystemMessageConst.AccountStatusConst.ActiveEn));
                }
              
            }
            if (status == SystemMessageConst.StatusConst.Deactive)
            {
                if (isNotifi)
                {
                    ViewBag.value = Resources.HomeText.Notifi_Deactive;
                }
                else
                {
                    ViewBag.value = (DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.AccountStatusConst.Deactive,
                        SystemMessageConst.AccountStatusConst.DeactiveEn));
                }
                
            }
            if (status == SystemMessageConst.StatusConst.Pending)
            {
                if (isNotifi)
                {
                    ViewBag.value = Resources.HomeText.Notifi_Pending;
                }
                else
                {
                    ViewBag.value = (DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.AccountStatusConst.Pending,
                        SystemMessageConst.AccountStatusConst.PendingEn));
                }
               
            }
            if (status == SystemMessageConst.StatusConst.SaveDraft)
            {
                ViewBag.value = (DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.AccountStatusConst.Draft,
                    SystemMessageConst.AccountStatusConst.DraftEn));
            }
            if (status == SystemMessageConst.StatusConst.RemoveOnSite)
            {
                ViewBag.value = (DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.AccountStatusConst.RemoveOnSite,
                    SystemMessageConst.AccountStatusConst.RemoveOnSiteEn));
            }
            return PartialView();
        }
        [CheckSessionUser]
        public ActionResult AccountManagerButton(int? status,bool hasInvitations = false)
        {
            ViewBag.status = status;
            ViewBag.hasInvitations = hasInvitations;
            ViewBag.myRole = AccountUntil.GetAccount().idquyentaikhoan;
            return PartialView();
        }
        [CheckSessionUser]
        public ActionResult MemberManagerButton(int? status)
        {
            ViewBag.status = status;
            if (status == SystemMessageConst.StatusConst.Active)
            {
                ViewBag.value = (DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.AccountStatusConst.MemberActive,
                    SystemMessageConst.AccountStatusConst.MemberActiveEn));
            }
            if (status == SystemMessageConst.StatusConst.Deactive)
            {
                ViewBag.value = (DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.AccountStatusConst.MemberDeactive,
                    SystemMessageConst.AccountStatusConst.MemberDeactive));
            }
            if (status == SystemMessageConst.StatusConst.Pending)
            {
                ViewBag.value = (DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.AccountStatusConst.Pending,
                    SystemMessageConst.AccountStatusConst.PendingEn));
            }
            return PartialView();
        }

        //[CheckSessionUser]
        //public ActionResult GetlistAct(int idregis)
        //{
        //    var result = new AccountInfoBusiness().listAct(idregis);

            
        //    return PartialView(result);
        //}
    }
}