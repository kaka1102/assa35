using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataContext.Constants
{
    public static class ListMailAddress
    {
        public static class SystemEmailAddress
        {
            public const string EmailAdmin = "assa35vn@gmail.com";
            public const string PassWordEmailAdmin = "assa35vn@";

            //public const string EmailAdmin = "pasbiz@quantriwebhanoi.com";
            //public const string PassWordEmailAdmin = "Qtw1234$";
        }
       
        public static class ContentEmail
        {
            public const string FooterEmail = "<div style='padding: 5px;'><label>Mọi thắc mắc vui lòng liên hệ <span style='color:red;'> Hotline (+84) 024 32272861</span> để được tư vấn.</label></div><br />"
                                             + "<div style='padding: 5px;text-align: center;'><span>------ Trân trọng cảm ơn ------ </span></div><br />"
                                             + "<div style='padding: 5px;'><label>Cục quản lý môi trường y tế - Bộ y tế</label><br />"
                                             + "<label>Địa chỉ : Tòa nhà Tổng cục Dân số kế hoạch hóa gia đình (Tầng 12,13) – Ngõ 8, Tôn Thất Thuyết, Mỹ đình 2, Nam Từ Liêm, Hà Nội.</label></div>";
        }

        public static class ContentEmailSystem
        {
            public const string FooterEmail = "<div style='padding: 5px;'><label>Mọi thắc mắc vui lòng liên hệ: <span style='color:red;'>"
                                             + "<ul>"
                                             + "<li  style='padding: 5px;'><b> Điện thoại: (04)36281193 – 36281191 </b></li>"
                                             + "<li  style='padding: 5px;'><b> Fax: (04) 38697886. </b></li>"
                                             + "</ul>"
                                             + "</span></label></div><br />"
                                             + "<div style='padding: 5px;text-align: center;'><span>------ Trân trọng cảm ơn ------ </span></div><br /></div>";

            public const string FooterEmailEn = "<div style='padding: 5px;'><label>Any questions please contact: <span style='color:red;'>"
                                                 + "<ul>"
                                                 + "<li  style='padding: 5px;'><b> Tel: (04)36281193 – 36281191 </b></li>"
                                                 + "<li  style='padding: 5px;'><b> Fax: (04) 38697886. </b></li>"
                                                 + "</ul>"
                                                 + "</span></label></div><br />"
                                                 + "<div style='padding: 5px;text-align: center;'><span>------ Special thanks ------ </span></div><br /></div>";

            public const string HeaderEmail = "THÔNG BÁO TỪ BAN QUẢN TRỊ ASSA 35";
            public const string HeaderEmailEn = "MESSAGE FROM THE ASSA 35 SYSTEM";

            public const string HelloAccount = "Xin chào ";
            public const string HelloAccountEn = "Dear ";

            public const string ThankForSend = "Cảm ơn bạn đã quan tâm tới hội nghị Assa 35. ";
            public const string ThankForSendEn = "Thank you for your interest in the 35th assa conference. ";

        }
        public static class ResetPassword
        {
            public const string SendNewPassword1 = "Chúng tôi đã nhận được yêu cầu reset mật khẩu tài khoản của bạn trong hệ thống.";
            public const string SendNewPasswordEn1 = "We already received request to reset your password account in your system. ";
            public const string SendNewPassword2 = "Đây là mật khẩu mới của bạn:";
            public const string SendNewPasswordEn2 = "This is your password:";

        }
        public static class Topic
        {
            public const string DeleteTopPic1 = "Chủ đề của bạn với tiêu đề : ";
            public const string DeleteTopPicEn1 = "Your subject with title : ";

            public const string DeletePost1 = "Bài viết của bạn với tiêu đề : ";
            public const string DeletePostEn1 = "Your article with the title : ";


            public const string DeleteTopPic2 = "Vừa bị xóa khỏi hệ thống do vị phạm các điều khoản mà ban tổ chức đã quy định";
            public const string DeleteTopPicEn2 = "Deleted from the system due to violation of the terms that the organizers have specified";

            public const string DeleteTopPic3 = "Chúng tôi gửi thông báo này tới bạn và mong rằng bạn tuân thủ đúng các quy định mà ban tổ chức đã đề ra.";
            public const string DeleteTopPicEn3 = "We send this notice to you and hope you comply with the rules set by the organizers.";

        }

        public static class RegisterAccountAdmin
        {
            public const string Content1 = "Bạn đã được Ban quản trị Hội nghị Assa lần thứ 35 cấp quyền truy cập tài khoản với vai trò là: ";
            public const string ContentEn1 = "You have been granted access to your account by the 35th Assa board as: ";

            public const string Content2 = "Thông tin đăng nhập của bạn trên hệ thống Assa 35 là: ";
            public const string ContentEn2 = "Your login information on the Assa 35 system is: ";

            public const string Content3 = "Bạn vui lòng click vào link phía dưới để đăng nhập vào hệ thống: ";
            public const string ContentEn3 = "Please click on the link below to login to the system: ";
        }

        public static class ChangeStatusAccountAdmin
        {
            public const string Content1 = "Ban quản trị Hội nghị Assa lần thứ 35 xin thông báo : ";
            public const string ContentEn1 = "The Management Board of the 35 th Assa Conference would like to announce: ";

            public const string Content2 = "Tài khoản: ";
            public const string ContentEn2 = "Account: ";

            public const string Content3 = " đã được kích hoạt thành công ";
            public const string ContentEn3 = " has been successfully activated ";

            public const string Content4 = " đã bị khóa ";
            public const string ContentEn4 = " blocked ";

            public const string Content5 = " đã bị xóa khỏi hệ thống ";
            public const string ContentEn5 = " has been removed from the system ";

            public const string Content6 = " Lý do : ";
            public const string ContentEn6 = " Reason : ";

        }

        public static class ChangeAuthorAccountAdmin
        {
            public const string Content1 = "Ban quản trị Hội nghị Assa lần thứ 35 xin thông báo : ";
            public const string ContentEn1 = "The Management Board of the 35 th Assa Conference would like to announce: ";

            public const string Content2 = "Tài khoản: ";
            public const string ContentEn2 = "Account: ";

            public const string addsecretary = " đã được thêm quyền ban thư ký";
            public const string addsecretaryEn = " was added the authority of the secretary ";

            public const string removesecretary = " đã bị hủy quyền ban thư ký";
            public const string removesecretaryEn = " canceled the secretary's authority ";

            public const string addeditorial = " đã được thêm quyền ban biên tập ";
            public const string addeditorialEn = " has been added editorial rights ";

            public const string removeeditorial = " đã bị hủy bỏ quyền ban biên tập ";
            public const string removeeditorialEn = " has been revoked editorial rights ";

        }


        public static class AccpectAcountUser
        {
            public const string Content1 = "Yêu cầu của bạn đã được quản trị viên phê duyệt. ";
            public const string ContentEn1 = "Your request has been approved by the administrator. ";

            public const string Content2 = "Thông tin đăng nhập hệ thống của bạn là: ";
            public const string ContentEn2 = "Your system login information is: ";

            public const string Content3 = "Vui lòng nhấp vào đây để đăng nhập và cập nhật thêm thông tin: ";
            public const string ContentEn3 = "Please click here to login and update more information: ";
        }

        public static class DeleteMeeting
        {
            public const string Content1 = "Ban quản trị Hội nghị Assa lần thứ 35 xin thông báo : ";
            public const string ContentEn1 = "The Management Board of the 35 th Assa Conference would like to announce: ";

            public const string Content2 = "Cuộc họp: ";
            public const string ContentEn2 = "Meeting: ";

            public const string removemeeting = "  đã bị xóa bỏ khỏi hệ thống ";
            public const string removemeetingEn = " has been removed from the system ";
        }

        public static class DeleteNotification
        {
            public const string Content1 = "Ban quản trị Hội nghị Assa lần thứ 35 xin thông báo : ";
            public const string ContentEn1 = "The Management Board of the 35 th Assa Conference would like to announce: ";

            public const string Content2 = "Thông báo: ";
            public const string ContentEn2 = "Notification: ";

            public const string removeNotification = "  đã bị xóa bỏ khỏi hệ thống ";
            public const string removeNotificationEn = " has been removed from the system ";
        }
        public  static class  EmailReceiper
        {
            public const string emailReceiper = "canquy@gmail.com";
        }

        public static class DeleteVenue
        {
            public const string Content1 = "Ban quản trị Hội nghị Assa lần thứ 35 xin thông báo : ";
            public const string ContentEn1 = "The Management Board of the 35 th Assa Conference would like to announce: ";

            public const string Content2 = "Địa điểm: ";
            public const string ContentEn2 = "Location: ";

            public const string removeNotification = "  đã bị xóa bỏ khỏi hệ thống ";
            public const string removeNotificationEn = " has been removed from the system ";
        }
       
    }
}