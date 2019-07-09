using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using DataContext.Constants;
using DataContext.DAL;
//using Spire.Doc;
//using Spire.Pdf;
//using Spire.Doc.Documents;
//using Spire.Doc.Fields;

namespace DataContext.Until
{
    public class SendEmailUntil
    {
        public static bool SendMail(string toAddress, string body, string subject)
        {
            bool success = true;
            var fromAddress = ListMailAddress.SystemEmailAddress.EmailAdmin;
            string fromPassword = ListMailAddress.SystemEmailAddress.PassWordEmailAdmin;
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress(fromAddress);
                mail.To.Add(toAddress);
                mail.Subject = subject;
                mail.Body = body;              
                mail.IsBodyHtml = true;
                SmtpServer.Port = 25;
                SmtpServer.Credentials = new System.Net.NetworkCredential(fromAddress, fromPassword);
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);

            }
            catch (Exception ex)
            {
                success = false;
            }
            return success;
        }
        public static bool SendMailWithAtt(string toAddress, string body, string subject, string file,string fileName)
        {
            bool success = true;
            var fromAddress = ListMailAddress.SystemEmailAddress.EmailAdmin;
            string fromPassword = ListMailAddress.SystemEmailAddress.PassWordEmailAdmin;
            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress(fromAddress);
                mail.To.Add(toAddress);
                mail.Subject = subject;
                mail.Body = body;
                mail.Attachments.Add(new Attachment(file, fileName));
                mail.IsBodyHtml = true;
                SmtpServer.Port = 25;
                SmtpServer.Credentials = new System.Net.NetworkCredential(fromAddress, fromPassword);
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);

            }
            catch (Exception ex)
            {
                success = false;
            }
            return success;
        }

        //public static void MakeInfoWithAtt(string guestName, m_registration_info regis, m_room_order room)
        //{
        //    Document doc = new Document();
        //    Paragraph paragraph = doc.AddSection().AddParagraph();
        //    Spire.Doc.Fields.TextRange text = paragraph.AppendText("thông tin khách" + guestName);
        //    Paragraph paragraph1 = doc.Sections[0].AddParagraph();
        //    // tên văn bản mới là text1
        //    Spire.Doc.Fields.TextRange text2 = paragraph1.AppendText("Gender: " + (regis.gender == 1 ? "Male" : "Female"));
        //    paragraph1.AppendText("Country :" + regis.country);
        //    paragraph1.AppendText("Adress : " + regis.address);
        //    paragraph1.AppendText("Deparment : " + regis.department);
        //    paragraph1.AppendText("Position : " + regis.position);
        //    paragraph1.AppendText("Telephone : " + regis.telephone);
        //    paragraph1.AppendText("Cellphone : " + regis.cellphone);
        //    paragraph1.AppendText("Institution : " + regis.organization);
        //    string mealprefer = "";
        //    if (regis.mealprefer == 1)
        //    {
        //        mealprefer = "Vegetarian";
        //    }
        //    else if (regis.mealprefer == 2)
        //    {
        //        mealprefer = "Non-Vegetarian";
        //    }
        //    else if (regis.mealprefer == 3)
        //    {
        //        mealprefer = "Hala-Food";
        //    }
        //    else if (regis.mealprefer == 4)
        //    {
        //        mealprefer = "Normal";
        //    }
        //    else mealprefer = "Other";
        //    paragraph1.AppendText("Email : " + regis.email);
        //    paragraph1.AppendText("Meal Prefer : " + mealprefer);
        //    paragraph1.AppendText("Meal Other : " + regis.meal_other);
        //    paragraph1.AppendText("Institution : " + regis.organization);
        //    paragraph1.AppendText("Institution : " + regis.organization);

        //    string path = "~/Content/IMG/Registration/" + guestName + ".pdf";
        //    doc.SaveToFile(path, Spire.Doc.FileFormat.PDF);

        //    SendMailWithAtt("canquy@gmail.com", "Test", guestName, path, guestName);

        //}

        //public static void SendMailInfo(string emailAddress, string path, string name)
        //{
        //    var message = string.Format(SystemMessageConst.SendEmailConst.SendMailInfor, name,path);
        //    var subjectInfo = string.Format(SystemMessageConst.SendEmailConst.SubjectInfor, name);
        //    SendMail(emailAddress,message, subjectInfo);
        //}
        //public static void SendMailInfoToVSS(string emailAddress, string path, string name)
        //{
        //    var message = string.Format(SystemMessageConst.SendEmailConst.SendMailInforToVss, name, path);
        //    var subjectInfo = string.Format(SystemMessageConst.SendEmailConst.SubjectInfor, name);
        //    SendMail(emailAddress, message, subjectInfo);
        //}
        public static void SendMailConfirmAccount(string emailAddress, string link, string name)
        {
            var messageEn = string.Format(SystemMessageConst.SendEmailConst.SendMailConfirmEn, name, link);
            var message = string.Format(SystemMessageConst.SendEmailConst.SendMailConfirm, name, link);
            SendMail(emailAddress,  messageEn,  SystemMessageConst.SendEmailConst.SubjectConfirmEmailEn);
        }
        public static void SendMailChangeStatus(string emailAddress, int status)
        {
            var message = string.Format(SystemMessageConst.SendEmailConst.ContentsTitle, emailAddress);
            if (status == SystemMessageConst.StatusConst.Active)
            {
                message = message + "được kích hoạt thành công.";
            }
            if (status == SystemMessageConst.StatusConst.Deactive)
            {
                message = message + "bị khóa";
            }
            if (status == SystemMessageConst.StatusConst.Delete)
            {
                message = message + "bị xóa khỏi hệ thống !";
            }
            SendMail(emailAddress, message, SystemMessageConst.SendEmailConst.Subject);
        }
        public static void SendMailRemovePost(string emailAddress, string topicName)
        {
            var message = string.Format(SystemMessageConst.SendEmailConst.RemovePost, topicName);
            SendMail(emailAddress, message, SystemMessageConst.SendEmailConst.Subject);
        }

        public static void SendMailNotificalRegiterAccount(string emailAddress)
        {
            var message = string.Format(SystemMessageConst.SendEmailConst.ContentsRegisterWait, emailAddress);
            SendMail(emailAddress, message, SystemMessageConst.SendEmailConst.Subject);
        }
        public static void SendMailRegiterAccount(string emailAddress, string password)  //bo?
        {
            var message = string.Format(SystemMessageConst.SendEmailConst.ContentsRegister, emailAddress, password);
            SendMail(emailAddress, message, SystemMessageConst.SendEmailConst.Subject);
        }

        public static void SendMailChangeAu(string emailAddress, int au, string type)
        {
            var message = string.Format(SystemMessageConst.SendEmailConst.ContentsTitle, emailAddress);
            if (au == 2)
            {
                message = message + type + " quyền biên tập";
                SendMail(emailAddress, message, SystemMessageConst.SendEmailConst.Subject);
            }
            if (au == 3)
            {
                message = message + type + " quyền thư ký";
                SendMail(emailAddress, message, SystemMessageConst.SendEmailConst.Subject);
            }
        }



        /// <summary>
        /// long viet
        /// </summary>
        /// <param name="emailAddress"></param>
        /// <param name="status"></param>
        public static void _SendMailChangeStatusAccount(string emailAddress, int status,string reason)
        {

            string message = "";
            if (status == SystemMessageConst.StatusConst.Active)
            {
                message = DisplayUntils.ReturnMessageBylanguage(ListMailAddress.ChangeStatusAccountAdmin.Content3, ListMailAddress.ChangeStatusAccountAdmin.ContentEn3);
            }
            if (status == SystemMessageConst.StatusConst.Deactive)
            {
                message = DisplayUntils.ReturnMessageBylanguage(ListMailAddress.ChangeStatusAccountAdmin.Content4, ListMailAddress.ChangeStatusAccountAdmin.ContentEn4) + "<br>" + DisplayUntils.ReturnMessageBylanguage(ListMailAddress.ChangeStatusAccountAdmin.Content6, ListMailAddress.ChangeStatusAccountAdmin.ContentEn6) + reason + "<br>";
            }
            if (status == SystemMessageConst.StatusConst.Delete)
            {
                message = DisplayUntils.ReturnMessageBylanguage(ListMailAddress.ChangeStatusAccountAdmin.Content5, ListMailAddress.ChangeStatusAccountAdmin.ContentEn5);
            }



            string mailto = emailAddress;
            string subject = DisplayUntils.ReturnMessageBylanguage(ListMailAddress.ContentEmailSystem.HeaderEmail, ListMailAddress.ContentEmailSystem.HeaderEmailEn);
            string body = "";
            body = string.Format("<div style='padding: 30px;border: 1px solid green;border-radius: 5px;'>"
                            + "<div style='padding: 5px;'><label>" + DisplayUntils.ReturnMessageBylanguage(ListMailAddress.ContentEmailSystem.HelloAccount, ListMailAddress.ContentEmailSystem.HelloAccountEn) + " <span style='color:red;'>{0}</span></label></div><br />"
                            + "<div style='padding: 5px;'><label>" + DisplayUntils.ReturnMessageBylanguage(ListMailAddress.ChangeStatusAccountAdmin.Content1, ListMailAddress.ChangeStatusAccountAdmin.ContentEn1) + "</label></div><br />"
                            + "<div style='padding: 5px;'><label>" + DisplayUntils.ReturnMessageBylanguage(ListMailAddress.ChangeStatusAccountAdmin.Content2, ListMailAddress.ChangeStatusAccountAdmin.ContentEn2) + "<span style='color:red;'>{1}</span>" + message + "</label></div><br />"
                            + DisplayUntils.ReturnMessageBylanguage(ListMailAddress.ContentEmailSystem.FooterEmail, ListMailAddress.ContentEmailSystem.FooterEmailEn)
                            + "</div>",
                            emailAddress, emailAddress);

            bool a = SendEmailUntil.SendMail(mailto, body, subject);
        }

        
        public static void _SendMailChangeAuAcountAdmin(string emailAddress, string type)
        {

            string mailto = emailAddress;
            string subject = DisplayUntils.ReturnMessageBylanguage(ListMailAddress.ContentEmailSystem.HeaderEmail, ListMailAddress.ContentEmailSystem.HeaderEmailEn);
            string body = "";
            body = string.Format("<div style='padding: 30px;border: 1px solid green;border-radius: 5px;'>"
                            + "<div style='padding: 5px;'><label>" + DisplayUntils.ReturnMessageBylanguage(ListMailAddress.ContentEmailSystem.HelloAccount, ListMailAddress.ContentEmailSystem.HelloAccountEn) + " <span style='color:red;'>{0}</span></label></div><br />"
                            + "<div style='padding: 5px;'><label>" + DisplayUntils.ReturnMessageBylanguage(ListMailAddress.ChangeAuthorAccountAdmin.Content1, ListMailAddress.ChangeAuthorAccountAdmin.ContentEn1) + "</label></div><br />"
                            + "<div style='padding: 5px;'><label>" + DisplayUntils.ReturnMessageBylanguage(ListMailAddress.ChangeAuthorAccountAdmin.Content2, ListMailAddress.ChangeAuthorAccountAdmin.ContentEn2) + "<span style='color:red;'>{1}</span>" + type + "</label></div><br />"
                            + DisplayUntils.ReturnMessageBylanguage(ListMailAddress.ContentEmailSystem.FooterEmail, ListMailAddress.ContentEmailSystem.FooterEmailEn)
                            + "</div>",
                            emailAddress, emailAddress);

            bool a = SendEmailUntil.SendMail(mailto, body, subject);
        }


        public static void _SendEmailAcceptAcountUser(string nameShow, string emailAddress, string password,string inviteUrl)
        {

            string mailto = emailAddress;
            string subject = DisplayUntils.ReturnMessageBylanguage(ListMailAddress.ContentEmailSystem.HeaderEmail, ListMailAddress.ContentEmailSystem.HeaderEmailEn);
            string body = "";
            body = string.Format("<div style='padding: 30px;border: 1px solid green;border-radius: 5px;'>"
                            + "<div style='padding: 5px;'><label>" + DisplayUntils.ReturnMessageBylanguage(ListMailAddress.ContentEmailSystem.HelloAccount, ListMailAddress.ContentEmailSystem.HelloAccountEn) + " <span style='color:red;'>{0}</span></label></div><br />"
                            + "<div style='padding: 5px;'><label>" + DisplayUntils.ReturnMessageBylanguage(ListMailAddress.AccpectAcountUser.Content1, ListMailAddress.AccpectAcountUser.ContentEn1) + "</label></div><br />"
                            + "<div style='padding: 5px;'><label>" + DisplayUntils.ReturnMessageBylanguage(ListMailAddress.AccpectAcountUser.Content2, ListMailAddress.AccpectAcountUser.ContentEn2) + "</label></div><br />"
                            + "<ul>"
                            + "<li  style='padding: 5px;'>" + DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Username, SystemMessageConst.systemmessage.UsernameEn) + " <b> {1} </b></li>"
                            + "<li  style='padding: 5px;'>" + DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Password, SystemMessageConst.systemmessage.PasswordEn) + " <b> {2} </b></li>"
                                 + "<li  style='padding: 5px;'><a href='{3}' download >" + DisplayUntils.ReturnMessageBylanguage (SystemMessageConst.systemmessage.ClickHereToGet, SystemMessageConst.systemmessage.ClickHereToGetEn) + DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Appointment, SystemMessageConst.systemmessage.AppointmentEn) + "</a> <b></b></li>"
                                 + "<li  style='padding: 5px;'><a href='{4}'>" + DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.ClickHereToLogin, SystemMessageConst.systemmessage.ClickHereToLoginEn)+"</a> <b></b></li>"
                            + "</ul> "
                            + DisplayUntils.ReturnMessageBylanguage(ListMailAddress.ContentEmailSystem.FooterEmail, ListMailAddress.ContentEmailSystem.FooterEmailEn)
                            + "</div>",
                         nameShow, emailAddress, password,inviteUrl, "http://" + HttpContext.Current.Request.Url.Host+SystemMessageConst.Url.Login);

            bool a = SendEmailUntil.SendMail(mailto, body, subject);
        }
        public static void _SendEmailAcceptAcountDelegate(string nameShow, string emailAddress, string password)
        {

            string mailto = emailAddress;
            string subject = DisplayUntils.ReturnMessageBylanguage(ListMailAddress.ContentEmailSystem.HeaderEmail, ListMailAddress.ContentEmailSystem.HeaderEmailEn);
            string body = "";
            body = string.Format("<div style='padding: 30px;border: 1px solid green;border-radius: 5px;'>"
                            + "<div style='padding: 5px;'><label>" + DisplayUntils.ReturnMessageBylanguage(ListMailAddress.ContentEmailSystem.HelloAccount, ListMailAddress.ContentEmailSystem.HelloAccountEn) + " <span style='color:red;'>{0}</span></label></div><br />"
                            + "<div style='padding: 5px;'><label>" + DisplayUntils.ReturnMessageBylanguage(ListMailAddress.AccpectAcountUser.Content1, ListMailAddress.AccpectAcountUser.ContentEn1) + "</label></div><br />"
                            + "<div style='padding: 5px;'><label>" + DisplayUntils.ReturnMessageBylanguage(ListMailAddress.AccpectAcountUser.Content2, ListMailAddress.AccpectAcountUser.ContentEn2) + "</label></div><br />"
                            + "<ul>"
                            + "<li  style='padding: 5px;'>" + DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Username, SystemMessageConst.systemmessage.UsernameEn) + " <b> {1} </b></li>"
                            + "<li  style='padding: 5px;'>" + DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.Password, SystemMessageConst.systemmessage.PasswordEn) + " <b> {2} </b></li>"
                                 
                                 + "<li  style='padding: 5px;'><a href='{3}'>" + DisplayUntils.ReturnMessageBylanguage(SystemMessageConst.systemmessage.ClickHereToLogin, SystemMessageConst.systemmessage.ClickHereToLoginEn) + "</a> <b></b></li>"
                            + "</ul> "
                            + DisplayUntils.ReturnMessageBylanguage(ListMailAddress.ContentEmailSystem.FooterEmail, ListMailAddress.ContentEmailSystem.FooterEmailEn)
                            + "</div>",
                         nameShow, emailAddress, password,  "https://" +  SystemMessageConst.Url.HostLogin);

            bool a = SendEmailUntil.SendMail(mailto, body, subject);
        }


        public static void _SendMailRemovePost(string emailAddress, string topicName)
        {

            string mailto = emailAddress;
            string subject = DisplayUntils.ReturnMessageBylanguage(ListMailAddress.ContentEmailSystem.HeaderEmail, ListMailAddress.ContentEmailSystem.HeaderEmailEn);
            string body = "";
            body = string.Format("<div style='padding: 30px;border: 1px solid green;border-radius: 5px;'>"
                            + "<div style='padding: 5px;'><label>" + DisplayUntils.ReturnMessageBylanguage(ListMailAddress.ContentEmailSystem.HelloAccount, ListMailAddress.ContentEmailSystem.HelloAccountEn) + " <span style='color:red;'>{0}</span></label></div><br />"
                            + "<div style='padding: 5px;'><label>" + DisplayUntils.ReturnMessageBylanguage(ListMailAddress.DeleteMeeting.Content1, ListMailAddress.DeleteMeeting.ContentEn1) + "</label></div><br />"
                            + "<div style='padding: 5px;'><label>" + DisplayUntils.ReturnMessageBylanguage(ListMailAddress.DeleteMeeting.Content2, ListMailAddress.DeleteMeeting.ContentEn2) + "<span style='color:red;'>{1}</span>" + DisplayUntils.ReturnMessageBylanguage(ListMailAddress.DeleteMeeting.removemeeting, ListMailAddress.DeleteMeeting.removemeetingEn) + "</label></div><br />"
                            + DisplayUntils.ReturnMessageBylanguage(ListMailAddress.ContentEmailSystem.FooterEmail, ListMailAddress.ContentEmailSystem.FooterEmailEn)
                            + "</div>",
                          emailAddress, topicName);

            bool a = SendEmailUntil.SendMail(mailto, body, subject);
        }


        public static void _SendMailRemoveNotification(string emailAddress, string topicName)
        {

            string mailto = emailAddress;
            string subject = DisplayUntils.ReturnMessageBylanguage(ListMailAddress.ContentEmailSystem.HeaderEmail, ListMailAddress.ContentEmailSystem.HeaderEmailEn);
            string body = "";
            body = string.Format("<div style='padding: 30px;border: 1px solid green;border-radius: 5px;'>"
                            + "<div style='padding: 5px;'><label>" + DisplayUntils.ReturnMessageBylanguage(ListMailAddress.ContentEmailSystem.HelloAccount, ListMailAddress.ContentEmailSystem.HelloAccountEn) + " <span style='color:red;'>{0}</span></label></div><br />"
                            + "<div style='padding: 5px;'><label>" + DisplayUntils.ReturnMessageBylanguage(ListMailAddress.DeleteNotification.Content1, ListMailAddress.DeleteNotification.ContentEn1) + "</label></div><br />"
                            + "<div style='padding: 5px;'><label>" + DisplayUntils.ReturnMessageBylanguage(ListMailAddress.DeleteNotification.Content2, ListMailAddress.DeleteNotification.ContentEn2) + "<span style='color:red;'>{1}</span>" + DisplayUntils.ReturnMessageBylanguage(ListMailAddress.DeleteNotification.removeNotification, ListMailAddress.DeleteNotification.removeNotificationEn) + "</label></div><br />"
                            + DisplayUntils.ReturnMessageBylanguage(ListMailAddress.ContentEmailSystem.FooterEmail, ListMailAddress.ContentEmailSystem.FooterEmailEn)
                            + "</div>",
                          emailAddress, topicName);

            bool a = SendEmailUntil.SendMail(mailto, body, subject);
        }

        public static void _SendMailRemoveLocation(string emailAddress, string topicName)
        {

            string mailto = emailAddress;
            string subject = DisplayUntils.ReturnMessageBylanguage(ListMailAddress.ContentEmailSystem.HeaderEmail, ListMailAddress.ContentEmailSystem.HeaderEmailEn);
            string body = "";
            body = string.Format("<div style='padding: 30px;border: 1px solid green;border-radius: 5px;'>"
                            + "<div style='padding: 5px;'><label>" + DisplayUntils.ReturnMessageBylanguage(ListMailAddress.ContentEmailSystem.HelloAccount, ListMailAddress.ContentEmailSystem.HelloAccountEn) + " <span style='color:red;'>{0}</span></label></div><br />"
                            + "<div style='padding: 5px;'><label>" + DisplayUntils.ReturnMessageBylanguage(ListMailAddress.DeleteVenue.Content1, ListMailAddress.DeleteVenue.ContentEn1) + "</label></div><br />"
                            + "<div style='padding: 5px;'><label>" + DisplayUntils.ReturnMessageBylanguage(ListMailAddress.DeleteVenue.Content2, ListMailAddress.DeleteVenue.ContentEn2) + "<span style='color:red;'>{1}</span>" + DisplayUntils.ReturnMessageBylanguage(ListMailAddress.DeleteVenue.removeNotification, ListMailAddress.DeleteVenue.removeNotificationEn) + "</label></div><br />"
                            + DisplayUntils.ReturnMessageBylanguage(ListMailAddress.ContentEmailSystem.FooterEmail, ListMailAddress.ContentEmailSystem.FooterEmailEn)
                            + "</div>",
                          emailAddress, topicName);

            bool a = SendEmailUntil.SendMail(mailto, body, subject);
        }
    }
}