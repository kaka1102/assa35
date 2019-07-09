using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using DataContext.Constants;
using DataContext.DAL;
using DataContext.Until;
using DataContext;
using System.Web.UI;
using System.Web;
using System.Web.UI.WebControls;
using DataContext.DAL;



namespace DataContext.AdminBusiness
{
    public class LoginBusiness : System.Web.UI.Page
    {
        SystemMess SysMess = new SystemMess();

        ASS_35Entities entity = new ASS_35Entities();
        public SystemMess CheckLoginAdmin(string email, string passWord)
        {
            var errorEmail = new ValidateForm().CheckRequiteEmail(email, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.EmailAddress, SystemMessageConst.systemmessage.EmailAddressEn));
            if (errorEmail.error == null) { email = errorEmail.valueName; }
            else
            {
                SysMess.IsSuccess = false;
                SysMess.Message = errorEmail.error;
                return SysMess;
            }

            var errorPassWord = new ValidateForm().CheckRequiredField(passWord, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Password, SystemMessageConst.systemmessage.PasswordEn), 3);
            if (errorPassWord.error == null) { passWord = errorPassWord.valueName; }
            else { SysMess.IsSuccess = false; SysMess.Message = errorPassWord.error; return SysMess; }

            string decodePass = MD5.GeneratePasswordHash(passWord);

            var Resulf = entity.m_taikhoan.Where(m => m.email == email && m.matkhau == decodePass && m.trangthai.Value == 3).ToList().Select(m => new
            {
                m.id,
                m.email,
                m.idcoquan,
                trangthaiduatin = m.idcoquan != null ? m.m_thongtincoquan.trangthaiduatin.Value : 0,
                idloaitaikhoan = m.idloaitaikhoan.Value,
                avatar = !string.IsNullOrEmpty(m.avatar) ? m.avatar : "/Content/IMG/Icon/avatar.jpg",
                thongtincanhan = (m.idloaitaikhoan.Value == 1) ? (entity.m_thongtincanhan.Where(x => x.idtaikhoan == m.id).Select(x => x.hovaten).FirstOrDefault()) : (m.m_thongtincoquan.tencoquan)
            }).FirstOrDefault();

            if (Resulf != null)
            {
                var DanhSachQuyen = entity.m_quyentaikhoan.Where(z => z.idtaikhoan == Resulf.id && z.trangthai == 3).ToList().Select(z => new
                {
                    z.id,
                    tenquyen = z.m_quyen.tenquyen,
                    idquyen = z.idquyen.Value
                }).ToList();

                if (DanhSachQuyen.Count() == 1)
                {
                    SessionUser uSession = new SessionUser();
                    uSession.id = Resulf.id;
                    uSession.hovaten = new AccountUntil().GetFullNameByIdTK(Resulf.id);
                    uSession.email = Resulf.email;
                    uSession.idcoquan = Resulf.idcoquan;
                    uSession.sessionid = Page.Session.SessionID;
                    uSession.ip = HttpContext.Current.Request.UserHostAddress;
                    uSession.Agent = HttpContext.Current.Request.UserAgent;
                    uSession.ComputerName = System.Environment.MachineName;
                    
                    uSession.Tooken = MD5.RandomString(16);
                    uSession.idquyentaikhoan = DanhSachQuyen.Select(x => x.idquyen).FirstOrDefault();
                    uSession.avatar = Resulf.avatar;
                    uSession.typeAcc = Resulf.idloaitaikhoan;
                    uSession.trangthaiduatin = Resulf.trangthaiduatin;
                    Page.Session.Add("uSession", uSession);



                    SysMess.IsSuccess = true;
                    SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.IsLoginSuccessTrue, SystemMessageConst.systemmessage.IsLoginSuccessTrueEn);
                }
                else if (DanhSachQuyen.Count() >= 2)
                {

                    SessionUser uSession = new SessionUser();
                    uSession.id = Resulf.id;
                    uSession.hovaten = new AccountUntil().GetFullNameByIdTK(Resulf.id);
                    uSession.email = Resulf.email;
                    uSession.idcoquan = Resulf.idcoquan;
                    uSession.sessionid = Page.Session.SessionID;
                    uSession.ip = HttpContext.Current.Request.UserHostAddress;
                    uSession.Agent = HttpContext.Current.Request.UserAgent;
                    uSession.ComputerName = System.Environment.MachineName;
                    uSession.avatar = Resulf.avatar;
                    uSession.typeAcc = Resulf.idloaitaikhoan;
                    uSession.trangthaiduatin = Resulf.trangthaiduatin;

                    Page.Session.Add("uSession", uSession);

                    SysMess.IsSuccess = true;
                    SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.IsLoginSuccessTrue, SystemMessageConst.systemmessage.IsLoginSuccessTrueEn);
                    SysMess.ItemData = uSession;
                    SysMess.LstData = DanhSachQuyen.Cast<object>().ToList();
                }
                else
                {
                    SysMess.IsSuccess = false;
                    SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.IsLoginNotSuccess, SystemMessageConst.systemmessage.IsLoginNotSuccessEn);
                }
            }
            else
            {
                SysMess.IsSuccess = false;
                SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.IsLoginSuccessFalse, SystemMessageConst.systemmessage.IsLoginSuccessFalseEn);

            }
            return SysMess;
        }
        public SystemMess CheckLoginAccount(string email, string passWord)
        {
            var errorEmail = new ValidateForm().CheckRequiteEmail(email, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.EmailAddress, SystemMessageConst.systemmessage.EmailAddressEn));
            if (errorEmail.error == null) { email = errorEmail.valueName; }
            else
            {
                SysMess.IsSuccess = false;
                SysMess.Message = errorEmail.error;
                return SysMess;
            }

            var errorPassWord = new ValidateForm().CheckRequiredField(passWord, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Password, SystemMessageConst.systemmessage.PasswordEn), 3);
            if (errorPassWord.error == null) { passWord = errorPassWord.valueName; }
            else { SysMess.IsSuccess = false; SysMess.Message = errorPassWord.error; return SysMess; }

            string decodePass = MD5.GeneratePasswordHash(passWord);

            var Resulf = entity.m_taikhoan.Where(m => m.email == email && m.matkhau == decodePass && m.trangthai.Value == 3).ToList().Select(m => new
            {
                m.id,
                m.email,
                               
                idloaitaikhoan = m.idloaitaikhoan.Value,
                avatar = !string.IsNullOrEmpty(m.avatar) ? m.avatar : "/Content/IMG/Icon/avatar.jpg"
                
            }).FirstOrDefault();
           
            if (Resulf != null)
            {
                var DanhSachQuyen = entity.m_quyentaikhoan.Where(z => z.idtaikhoan == Resulf.id && z.trangthai == 3).ToList().Select(z => new
                {
                    z.id,
                    tenquyen = z.m_quyen.tenquyen,
                    idquyen = z.idquyen.Value
                }).ToList();

                if (DanhSachQuyen.Count() == 1)
                {
                   
                    SessionUser uSession = new SessionUser();
                    uSession.id = Resulf.id;
                   // uSession.hovaten = new AccountUntil().GetFullNameIdTK(Resulf.id);
                    uSession.email = Resulf.email;
                    uSession.hovaten = Resulf.email;
                    uSession.sessionid = Page.Session.SessionID;
                    uSession.ip = HttpContext.Current.Request.UserHostAddress;
                    uSession.Agent = HttpContext.Current.Request.UserAgent;
                    uSession.ComputerName = System.Environment.MachineName;
                    uSession.Tooken = MD5.RandomString(16);
                    uSession.idquyentaikhoan = DanhSachQuyen.Select(x => x.idquyen).FirstOrDefault();
                    uSession.avatar = Resulf.avatar;
                    uSession.typeAcc = Resulf.idloaitaikhoan;
                   
                    Page.Session.Add("uSession", uSession);



                    SysMess.IsSuccess = true;
                    SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.IsLoginSuccessTrue, SystemMessageConst.systemmessage.IsLoginSuccessTrueEn);
                }
                else if (DanhSachQuyen.Count() >= 2)
                {

                    SessionUser uSession = new SessionUser();
                    uSession.id = Resulf.id;
                  //  uSession.hovaten = new AccountUntil().GetFullNameIdTK(Resulf.id);
                    uSession.email = Resulf.email;
                    uSession.hovaten = Resulf.email;
                    uSession.sessionid = Page.Session.SessionID;
                    uSession.ip = HttpContext.Current.Request.UserHostAddress;
                    uSession.Agent = HttpContext.Current.Request.UserAgent;
                    uSession.ComputerName = System.Environment.MachineName;
                    uSession.avatar = Resulf.avatar;
                    uSession.typeAcc = Resulf.idloaitaikhoan;
                  

                    Page.Session.Add("uSession", uSession);

                    SysMess.IsSuccess = true;
                    SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.IsLoginSuccessTrue, SystemMessageConst.systemmessage.IsLoginSuccessTrueEn);
                    SysMess.ItemData = uSession;
                    SysMess.LstData = DanhSachQuyen.Cast<object>().ToList();
                }
                else
                {
                    SysMess.IsSuccess = false;
                    SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.IsLoginNotSuccess, SystemMessageConst.systemmessage.IsLoginNotSuccessEn);
                }
            }
            else
            {
                SysMess.IsSuccess = false;
                SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.IsLoginSuccessFalse, SystemMessageConst.systemmessage.IsLoginSuccessFalseEn);

            }
            return SysMess;
        }
        public SystemMess CheckRollAdmin(string email, string ComputerName, string ip, string sessionid, int id, int idquyentk)
        {
            SessionUser session = new DAL.SessionUser();
            session = (SessionUser)HttpContext.Current.Session["uSession"];

            var Check = entity.m_quyentaikhoan.Where(m => m.idquyen == idquyentk && m.trangthai == 3 && m.idtaikhoan == id).FirstOrDefault();
            if (Check != null)
            {
                if (session.email == email && session.ComputerName == ComputerName && session.ip == ip && session.sessionid == sessionid && session.id == id)
                {

                    session.Tooken = MD5.RandomString(16);
                    session.idquyentaikhoan = idquyentk;

                    SysMess.IsSuccess = true;
                    SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.IsLoginSuccessTrue, SystemMessageConst.systemmessage.IsLoginSuccessTrueEn);
                }
                else
                {
                    Session.Abandon();
                    SysMess.IsSuccess = false;
                    SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.IsLoginSuccessFalse, SystemMessageConst.systemmessage.IsLoginSuccessFalseEn);
                }
            }
            else
            {
                SysMess.IsSuccess = false;
                SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.IsLoginNotSuccess, SystemMessageConst.systemmessage.IsLoginNotSuccessEn);
            }

            return SysMess;
        }
        public SystemMess ResetPassWordAccount(string emailReset)
        {
            SystemMess SysMess = new DAL.SystemMess();
            var errorEmail = new ValidateForm().CheckRequiteEmail(emailReset, DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.EmailAddress, SystemMessageConst.systemmessage.EmailAddressEn));
            if (errorEmail.error == null) { emailReset = errorEmail.valueName; }
            else
            {
                SysMess.IsSuccess = false;
                SysMess.Message = errorEmail.error;
                return SysMess;
            }

            var checkExist = entity.m_taikhoan.Where(m => m.email == emailReset && m.trangthai == 3).ToList().Select(m => new
            {
                m.id,
                m.email,
                matkhau = m.matkhau,
                tendaydu = new AccountUntil().GetFullNameByIdTK(m.id),
            }).FirstOrDefault();
            if (checkExist != null)
            {
                var itemUpdate = entity.m_taikhoan.Where(m => m.email == emailReset && m.trangthai == 3).FirstOrDefault();

                string newPassword = MD5.RandomString(6);
                string encodePass = MD5.GeneratePasswordHash(newPassword);

                itemUpdate.matkhau = encodePass;
                entity.SaveChanges();

                string mailto = checkExist.email;
                string subject = DisplayUntils.ReturnMessageBylanguage(ListMailAddress.ContentEmailSystem.HeaderEmail, ListMailAddress.ContentEmailSystem.HeaderEmailEn);
                string body = "";
                body = string.Format("<div style='padding: 30px;border: 1px solid green;border-radius: 5px;'>"
                                 + "<div style='padding: 5px;'><label>" + DisplayUntils.ReturnMessageBylanguage(ListMailAddress.ContentEmailSystem.HelloAccount, ListMailAddress.ContentEmailSystem.HelloAccountEn) + " <span style='color:red;'>{0}</span></label></div><br />"
                                 + "<div style='padding: 5px;'><label>" + DisplayUntils.ReturnMessageBylanguage(ListMailAddress.ContentEmailSystem.ThankForSend, ListMailAddress.ContentEmailSystem.ThankForSendEn) + "</label></div><br />"
                                 + "<div style='padding: 5px;'><label>" + DisplayUntils.ReturnMessageBylanguage(ListMailAddress.ResetPassword.SendNewPassword1, ListMailAddress.ResetPassword.SendNewPasswordEn1) + "</label></div><br />"
                                 + "<div style='padding: 5px;'><label>" + DisplayUntils.ReturnMessageBylanguage(ListMailAddress.ResetPassword.SendNewPassword2, ListMailAddress.ResetPassword.SendNewPasswordEn2) + "</label></div><br />"
                                 + "<ul>"
                                 + "<li  style='padding: 5px;'><b> {1} </b></li>"
                                 + "</ul> "
                                 + DisplayUntils.ReturnMessageBylanguage(ListMailAddress.ContentEmailSystem.FooterEmail, ListMailAddress.ContentEmailSystem.FooterEmailEn)
                                 + "</div>",
                                 checkExist.tendaydu, newPassword);

                SendEmailUntil.SendMail(mailto, body, subject);

                SysMess.IsSuccess = true;
                SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.SendMailForgotPassword, SystemMessageConst.systemmessage.SendMailForgotPasswordEn);
                return SysMess;
            }
            else
            {
                SysMess.IsSuccess = false;
                SysMess.Message = DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.EmailIsNotExistOrLock, SystemMessageConst.systemmessage.EmailIsNotExistOrLockEn);
                return SysMess;
            }

        }
    }
}