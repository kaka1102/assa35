using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using DataContext.Constants;
using DataContext.DAL;
using DataContext.Socket;
using DataContext.Until;

//using System.Data.Objects;

namespace DataContext.AdminBusiness
{
    public class ManagerAccountBusiness
    {
        public ManagerAccount GetListAccounts(string roleId, string searchText, int page, int pageSize, string status)
        {
            ManagerAccount managerAccount = new ManagerAccount();
            try
            {
                using (var entities = new ASS_35Entities())
                {
                    int _roleId;
                    int _status;
                    Int32.TryParse(roleId, out _roleId);
                    Int32.TryParse(status, out _status);
                    ObjectParameter outPut = new ObjectParameter("totalCount", typeof(Int32));
                    var result = entities.ManagerAccount(_roleId, _status, searchText, page, pageSize, outPut).ToList();
                    managerAccount.Data = result;
                    managerAccount.TotalRecort = Int32.Parse(outPut.Value.ToString());
                    return managerAccount;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public m_thongtincanhan GetMemberbyId(int id)
        {
            try
            {
                using (var entities = new ASS_35Entities())
                {
                    var member = entities.m_thongtincanhan.FirstOrDefault(ob => ob.id == id && ob.trangthai != SystemMessageConst.StatusConst.Delete);
                    return member;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public m_taikhoan GetAccountById(int id)
        {
            try
            {
                using (var entities = new ASS_35Entities())
                {
                    var result = entities.m_taikhoan.FirstOrDefault(ob => ob.id == id);
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public SystemMess DeleteAccountById(string id)
        {
            SystemMess systemMess = new SystemMess();
            try
            {
                using (var entities = new ASS_35Entities())
                {
                    int _id;
                    Int32.TryParse(id, out _id);
                    var check = entities.m_taikhoan.FirstOrDefault(ob => ob.id == _id);
                    if (check == null)
                    {
                        systemMess.IsSuccess = false;
                        //    systemMess.Message = SystemMessageConst.systemmessage.AccountNotExist;
                        systemMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.AccountNotExist, SystemMessageConst.systemmessage.AccountNotExistEn);
                        return systemMess;
                    }
                    check.trangthai = SystemMessageConst.StatusConst.Delete;
                    entities.SaveChanges();
                    systemMess.IsSuccess = true;
                    //  systemMess.Message = SystemMessageConst.systemmessage.DeleteSuccess;
                    systemMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.DeleteSuccess, SystemMessageConst.systemmessage.DeleteSuccessEn);
                    return systemMess;
                }
            }
            catch (Exception ex)
            {
                systemMess.IsSuccess = false;
                systemMess.Message = ex.ToString();
                return systemMess;
            }
        }
        public bool CheckAuByUserId(int id, int role)
        {
            try
            {
                using (var entities = new ASS_35Entities())
                {
                    var result = entities.m_quyentaikhoan.FirstOrDefault(ob => ob.idtaikhoan == id && ob.idquyen == role && ob.trangthai == SystemMessageConst.StatusConst.Active);
                    if (result != null)
                    {
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        public string ValidateAddAccount(m_taikhoan account, string role2, string role3)
        {
            var result = new ValidateForm.dataInput();
            var check = new ValidateForm();
            if (role2 != "true" && role3 != "true")
            {
                //   return SystemMessageConst.systemmessage.MustChooseAu;
                return DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.MustChooseAu, SystemMessageConst.systemmessage.MustChooseAuEn);
            }
            result = check.CheckRequiteEmail(account.email, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.EmailAddress, SystemMessageConst.systemmessage.EmailAddressEn));
            if (result.error != null)
            {
                return result.error;
            }

            result = check.CheckRequiredField(account.matkhau, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Password, SystemMessageConst.systemmessage.PasswordEn), 5, 20);
            if (result.error != null)
            {
                return result.error;
            }
            return null;
        }

        public SystemMess AddAccount(m_taikhoan account, string role2, string role3)
        {
            SystemMess systemMess = new SystemMess();
            try
            {
                var check = ValidateAddAccount(account, role2, role3);
                if (check != null)
                {
                    systemMess.IsSuccess = false;
                    systemMess.Message = check;
                    return systemMess;
                }
                using (var entities = new ASS_35Entities())
                {
                    var checkExistedEmail = entities.m_taikhoan.FirstOrDefault(ob => ob.email == account.email);
                    if (checkExistedEmail != null)
                    {
                        systemMess.IsSuccess = false;
                        // systemMess.Message = SystemMessageConst.systemmessage.EmailIsExisted;
                        systemMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.EmailIsExisted, SystemMessageConst.systemmessage.EmailIsExistedEn);
                        return systemMess;
                    }
                    var thisPass = account.matkhau;
                    account.trangthai = SystemMessageConst.StatusConst.Active;
                    account.idloaitaikhoan = 1;
                    account.ngaytao = DateTime.Now;
                    account.matkhau = MD5.GeneratePasswordHash(thisPass);
                    entities.m_taikhoan.Add(account);
                    entities.SaveChanges();

                    m_quyentaikhoan au = new m_quyentaikhoan();
                    au.ngaytao = DateTime.Now;
                    au.trangthai = SystemMessageConst.StatusConst.Active;
                    au.idtaikhoan = account.id;
                    string tenquyentk = "";
                    if (role2 == "true")
                    {
                        au.idquyen = 2;
                        entities.m_quyentaikhoan.Add(au);
                        entities.SaveChanges();

                        tenquyentk = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Editorial_board, SystemMessageConst.systemmessage.Editorial_boardEn);
                    }

                    if (role3 == "true")
                    {
                        au.idquyen = 3;
                        entities.m_quyentaikhoan.Add(au);
                        entities.SaveChanges();
                        tenquyentk = tenquyentk + "," + DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Secretariat, SystemMessageConst.systemmessage.SecretariatEn);
                    }

                    // send mail

                    string mailto = account.email;
                    string subject = DisplayUntils.ReturnMessageBylanguage(ListMailAddress.ContentEmailSystem.HeaderEmail, ListMailAddress.ContentEmailSystem.HeaderEmailEn);
                    string body = "";
                    body = string.Format("<div style='padding: 30px;border: 1px solid green;border-radius: 5px;'>"
                                    + "<div style='padding: 5px;'><label>" + DisplayUntils.ReturnMessageBylanguage(ListMailAddress.ContentEmailSystem.HelloAccount, ListMailAddress.ContentEmailSystem.HelloAccountEn) + " <span style='color:red;'>{0}</span></label></div><br />"
                                    + "<div style='padding: 5px;'><label>" + DisplayUntils.ReturnMessageBylanguage(ListMailAddress.RegisterAccountAdmin.Content1, ListMailAddress.RegisterAccountAdmin.ContentEn1) + "<span style='color:red;'>{1}</span></label></div><br />"
                                    + "<div style='padding: 5px;'><label>" + DisplayUntils.ReturnMessageBylanguage(ListMailAddress.RegisterAccountAdmin.Content2, ListMailAddress.RegisterAccountAdmin.ContentEn2) + "</label></div><br />"
                                    + "<ul>"
                                    + "<li  style='padding: 5px;'>" + DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.EmailAddress, SystemMessageConst.systemmessage.EmailAddressEn) + " <b> {2} </b></li>"
                                    + "<li  style='padding: 5px;'>" + DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Password, SystemMessageConst.systemmessage.PasswordEn) + " <b> {3} </b></li>"
                                    + "</ul> "
                                    + "<div style='padding: 5px;'><label>" + DisplayUntils.ReturnMessageBylanguage(ListMailAddress.RegisterAccountAdmin.Content3, ListMailAddress.RegisterAccountAdmin.ContentEn3) + "<span style='color:red;'>{4}</span></label></div><br />"
                                    + DisplayUntils.ReturnMessageBylanguage(ListMailAddress.ContentEmailSystem.FooterEmail, ListMailAddress.ContentEmailSystem.FooterEmailEn)
                                    + "</div>",
                                    account.email, tenquyentk, account.email, thisPass, "http://" + HttpContext.Current.Request.Url.Host + "/ADMIN/Admin/Index");

                    bool a = SendEmailUntil.SendMail(mailto, body, subject);

                    systemMess.IsSuccess = true;
                    //   systemMess.Message = SystemMessageConst.systemmessage.RegisterSuccessfull;
                    systemMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.RegisterSuccessfull, SystemMessageConst.systemmessage.RegisterSuccessfullEn);
                    return systemMess;
                }
            }
            catch (Exception ex)
            {
                systemMess.IsSuccess = false;
                systemMess.Message = ex.ToString();
                return systemMess;
            }
        }

        public SystemMess EditAccount(string id, string status, string role2, string role3,string reason)
        {
            SystemMess systemMess = new SystemMess();
            if (role2 != "true" && role3 != "true")
            {
                systemMess.IsSuccess = false;
                systemMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.MustChooseAu, SystemMessageConst.systemmessage.MustChooseAuEn);
                return systemMess;
            }
            try
            {
                using (var entities = new ASS_35Entities())
                {
                    int _id;
                    Int32.TryParse(id, out _id);
                    int _status;
                    int.TryParse(status, out _status);

                    var checkExisted = entities.m_taikhoan.FirstOrDefault(ob => ob.id == _id);
                    if (checkExisted == null)
                    {
                        systemMess.IsSuccess = false;
                        systemMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.AccountNotExist, SystemMessageConst.systemmessage.AccountNotExistEn);
                        return systemMess;
                    }
                 
                    // send email khi tai khoan doi trang thai
                    
                    if (_status != checkExisted.trangthai && _status != 0)
                    {
                        checkExisted.trangthai = _status;
                        entities.SaveChanges();
                        SendEmailUntil._SendMailChangeStatusAccount(checkExisted.email, _status, reason);
                    }
                    m_quyentaikhoan au = new m_quyentaikhoan();
                    au.ngaytao = DateTime.Now;
                    au.trangthai = SystemMessageConst.StatusConst.Active;
                    au.idtaikhoan = _id;
                    var checkRole3 = entities.m_quyentaikhoan.FirstOrDefault(ob => ob.idtaikhoan == _id && ob.idquyen == 3);
                    var checkRole2 = entities.m_quyentaikhoan.FirstOrDefault(ob => ob.idtaikhoan == _id && ob.idquyen == 2);

                    if (role2 == "true")
                    {

                        if (checkRole2 == null)
                        {
                            au.idquyen = 2;
                            entities.m_quyentaikhoan.Add(au);
                            entities.SaveChanges();

                            SendEmailUntil._SendMailChangeAuAcountAdmin(checkExisted.email, DisplayUntils.ReturnMessageBylanguage(ListMailAddress.ChangeAuthorAccountAdmin.addeditorial, ListMailAddress.ChangeAuthorAccountAdmin.addeditorialEn));
                        }
                        else
                        {
                            if (checkRole2.trangthai != SystemMessageConst.StatusConst.Active)
                            {
                                checkRole2.trangthai = SystemMessageConst.StatusConst.Active;
                                entities.SaveChanges();
                                SendEmailUntil._SendMailChangeAuAcountAdmin(checkExisted.email, DisplayUntils.ReturnMessageBylanguage(ListMailAddress.ChangeAuthorAccountAdmin.addeditorial, ListMailAddress.ChangeAuthorAccountAdmin.addeditorialEn));
                            }
                        }
                    }
                    else
                    {
                        if (checkRole2 != null)
                        {
                            checkRole2.trangthai = SystemMessageConst.StatusConst.Deactive;
                            entities.SaveChanges();
                            SendEmailUntil._SendMailChangeAuAcountAdmin(checkExisted.email, DisplayUntils.ReturnMessageBylanguage(ListMailAddress.ChangeAuthorAccountAdmin.removeeditorial, ListMailAddress.ChangeAuthorAccountAdmin.removeeditorialEn));
                        }
                    }

                    if (role3 == "true")
                    {
                        if (checkRole3 == null)
                        {
                            au.idquyen = 3;
                            entities.m_quyentaikhoan.Add(au);
                            entities.SaveChanges();
                            SendEmailUntil._SendMailChangeAuAcountAdmin(checkExisted.email, DisplayUntils.ReturnMessageBylanguage(ListMailAddress.ChangeAuthorAccountAdmin.addsecretary, ListMailAddress.ChangeAuthorAccountAdmin.addsecretaryEn));
                        }
                        else
                        {
                            if (checkRole3.trangthai != SystemMessageConst.StatusConst.Active)
                            {
                                checkRole3.trangthai = SystemMessageConst.StatusConst.Active;
                                entities.SaveChanges();
                                SendEmailUntil._SendMailChangeAuAcountAdmin(checkExisted.email, DisplayUntils.ReturnMessageBylanguage(ListMailAddress.ChangeAuthorAccountAdmin.addsecretary, ListMailAddress.ChangeAuthorAccountAdmin.addsecretaryEn));
                            }
                        }
                    }
                    else
                    {
                        if (checkRole3 != null)
                        {
                            checkRole3.trangthai = SystemMessageConst.StatusConst.Deactive;
                            entities.SaveChanges();
                            SendEmailUntil._SendMailChangeAuAcountAdmin(checkExisted.email, DisplayUntils.ReturnMessageBylanguage(ListMailAddress.ChangeAuthorAccountAdmin.removesecretary, ListMailAddress.ChangeAuthorAccountAdmin.removesecretaryEn));
                        }
                    }
                    if (_status == SystemMessageConst.StatusConst.Deactive)
                    {
                        checkExisted.lydo = (string.IsNullOrEmpty(reason)
                            ? DisplayUntils.ReturnMessageBylanguage(
                                SystemMessageConst.ValidateConst.LockAccountDefaultReason,
                                SystemMessageConst.ValidateConst.LockAccountDefaultReasonEn)
                            : reason);
                    }
                    //gui socket

                    //kiem tra danh sach id can gui
                    var lstAdmin = entities.m_taikhoan
                        .Where(ob => ob.m_quyentaikhoan.Any(x => x.idquyen == SystemMessageConst.RoleConst.Admin &&
                                                                 x.idtaikhoan == ob.id) && ob.trangthai ==
                                     SystemMessageConst.StatusConst.Active).ToList();
                    foreach (var item in lstAdmin)
                    {
                        SocketProcessRequest.SocketRequest("10", item.id.ToString(), 10);
                    }
                    systemMess.IsSuccess = true;
                    systemMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.EditSuccess, SystemMessageConst.systemmessage.EditSuccessEn);
                    return systemMess;
                }
            }
            catch (Exception ex)
            {
                systemMess.IsSuccess = false;
                systemMess.Message = ex.ToString();
                return systemMess;
            }
        }

        public SystemMess EditAccountDelegate(string id, string status,string reason,HttpPostedFileBase file)
        {
            SystemMess systemMess = new SystemMess();

            try
            {
                using (var entities = new ASS_35Entities())
                {
                    int _id;
                    Int32.TryParse(id, out _id);
                    int _status;
                    int.TryParse(status, out _status);
                    var checkExisted = entities.m_taikhoan.FirstOrDefault(ob => ob.id == _id);

                    if (checkExisted == null)
                    {
                        systemMess.IsSuccess = false;
                        systemMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.AccountNotExist, SystemMessageConst.systemmessage.AccountNotExistEn);
                        return systemMess;
                    }
                    if (checkExisted.trangthai == SystemMessageConst.StatusConst.Pending && _status == SystemMessageConst.StatusConst.Active)
                    {
                        if (file == null)
                        {
                            systemMess.IsSuccess = false;
                            systemMess.Message = string.Format(DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.ValidateConst.CheckNotEmpty, SystemMessageConst.ValidateConst.CheckNotEmptyEn), DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Appointment, SystemMessageConst.systemmessage.AppointmentEn));
                            return systemMess;
                        }
                        else
                        {
                            List<string> exImg = new List<string>();
                            exImg.Add(".pdf");
                            checkExisted.giaymoi = FileUtils.FileSave(file, UrlConstant.UrlConst.AccountImage, exImg);
                            var checkFileImg1 =
                                FileUtils.ValidateFileUpload(checkExisted.giaymoi, "pdf",
                                    DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Appointment,
                                        SystemMessageConst.systemmessage.AppointmentEn));
                            if (checkFileImg1.IsSuccess == false)
                            {
                                systemMess.IsSuccess = false;
                                systemMess.Message = checkFileImg1.Message;
                                return systemMess;
                            }
                        }
                    }
                   
                    if (_status != checkExisted.trangthai)
                    {
                        if (checkExisted.matkhau == null && _status == SystemMessageConst.StatusConst.Active)
                        {
                            string randomContents = "abcdefghijklmnopqrstuvwxyz0123456789";
                            string passGen = DisplayUntils.RandomPass(randomContents, 5);
                            checkExisted.matkhau = MD5.GeneratePasswordHash(passGen);
                            string nameShow = new AccountUntil().GetFullNameByIdTK(checkExisted.id);
                            SendEmailUntil._SendEmailAcceptAcountUser(nameShow, checkExisted.email, passGen, "http://" + HttpContext.Current.Request.Url.Host + checkExisted.giaymoi);
                        }
                        else
                        {
                            SendEmailUntil._SendMailChangeStatusAccount(checkExisted.email, _status, reason);
                        }
                    }
                    checkExisted.trangthai = _status;
                    if (_status == SystemMessageConst.StatusConst.Deactive)
                    {
                          checkExisted.lydo = (string.IsNullOrEmpty(reason)
                        ? DisplayUntils.ReturnMessageBylanguage(
                            SystemMessageConst.ValidateConst.LockAccountDefaultReason,
                            SystemMessageConst.ValidateConst.LockAccountDefaultReasonEn)
                        : reason);
                    }
                  
                    entities.SaveChanges();
                    systemMess.IsSuccess = true;
                    systemMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.EditSuccess, SystemMessageConst.systemmessage.EditSuccessEn);
                    return systemMess;
                }
            }
            catch (Exception ex)
            {
                systemMess.IsSuccess = false;
                systemMess.Message = ex.ToString();
                return systemMess;
            }
        }
        public SystemMess EditDelegate(string id, string status, string reason)
        {
            SystemMess systemMess = new SystemMess();

            try
            {
                using (var entities = new ASS_35Entities())
                {
                    int _id;
                    Int32.TryParse(id, out _id);
                    int _status;
                    int.TryParse(status, out _status);
                    var checkExisted = entities.m_taikhoan.FirstOrDefault(ob => ob.id == _id);

                    if (checkExisted == null)
                    {
                        systemMess.IsSuccess = false;
                        systemMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.AccountNotExist, SystemMessageConst.systemmessage.AccountNotExistEn);
                        return systemMess;
                    }
                   

                    if (_status != checkExisted.trangthai)
                    {
                        if (checkExisted.matkhau == null && _status == SystemMessageConst.StatusConst.Active)
                        {
                            string randomContents = "abcdefghijklmnopqrstuvwxyz0123456789";
                            string passGen = DisplayUntils.RandomPass(randomContents, 5);
                            checkExisted.matkhau = MD5.GeneratePasswordHash(passGen);
                            string nameShow = new AccountUntil().GetFullNameByIdTK(checkExisted.id);
                            SendEmailUntil._SendEmailAcceptAcountDelegate(nameShow, checkExisted.email, passGen);
                        }
                        else
                        {
                            SendEmailUntil._SendMailChangeStatusAccount(checkExisted.email, _status, reason);
                        }
                    }
                    checkExisted.trangthai = _status;
                    if (_status == SystemMessageConst.StatusConst.Deactive)
                    {
                        checkExisted.lydo = (string.IsNullOrEmpty(reason)
                      ? DisplayUntils.ReturnMessageBylanguage(
                          SystemMessageConst.ValidateConst.LockAccountDefaultReason,
                          SystemMessageConst.ValidateConst.LockAccountDefaultReasonEn)
                      : reason);
                    }

                    entities.SaveChanges();
                    systemMess.IsSuccess = true;
                    systemMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.EditSuccess, SystemMessageConst.systemmessage.EditSuccessEn);
                    return systemMess;
                }
            }
            catch (Exception ex)
            {
                systemMess.IsSuccess = false;
                systemMess.Message = ex.ToString();
                return systemMess;
            }
        }


        public List<m_thongtincanhan> GetPersonInAccountById(int idAcc)
        {
            try
            {
                using (var entities = new ASS_35Entities())
                {
                    var result = entities.m_thongtincanhan.Where(ob => ob.idtaikhoan == idAcc && ob.trangthai != SystemMessageConst.StatusConst.Delete).ToList();
                    return result;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public SystemMess EditStatusMember(int? status, int id, string feedback)
        {
            SystemMess systemMess = new SystemMess();
            try
            {
                using (var entities = new ASS_35Entities())
                {
                    var myRoleId = AccountUntil.GetAccount().idquyentaikhoan;
                    if (myRoleId != SystemMessageConst.RoleConst.Admin)
                    {
                        systemMess.IsSuccess = false;
                     //   systemMess.Message = SystemMessageConst.systemmessage.NoData;
                          systemMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.NoData, SystemMessageConst.systemmessage.NoDataEn);
                        return systemMess;
                    }
                    var result = entities.m_thongtincanhan.FirstOrDefault(ob => ob.id == id && ob.trangthai != SystemMessageConst.StatusConst.Delete);

                    if (result == null)
                    {
                        systemMess.IsSuccess = false;
                      //  systemMess.Message = SystemMessageConst.systemmessage.NoData;
                        systemMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.NoData, SystemMessageConst.systemmessage.NoDataEn);
                        return systemMess;
                    }
                    result.trangthai = status;
                    if (status == SystemMessageConst.StatusConst.Deactive && string.IsNullOrEmpty(feedback))
                    {
                      //  result.lydo = SystemMessageConst.systemmessage.RejectTopicMessage;
                        result.lydo = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Violation_of_the_terms_of_the_organizers, SystemMessageConst.systemmessage.Violation_of_the_terms_of_the_organizersEn);
                    }
                    else
                    {
                        result.lydo = feedback;
                    }
                    entities.SaveChanges();
                    systemMess.IsSuccess = true;
                    systemMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.EditSuccess, SystemMessageConst.systemmessage.EditSuccessEn);
                    return systemMess;
                }
            }
            catch (Exception ex)
            {
                systemMess.IsSuccess = false;
                systemMess.Message = ex.ToString();
                return systemMess;
            }
        }

        public List<Sp_GetActivityRegis_Result> GetActivityRegis(int id)
        {
            try
            {
                using (var entities = new ASS_35Entities())
                {
                    var result = entities.Sp_GetActivityRegis(id).ToList();
                    return result;
                }
            }
            catch (Exception e)
            {
                return null;
            }
        }

        
    }
}