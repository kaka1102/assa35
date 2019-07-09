using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Web;

namespace DataContext.Constants
{
    // Model Constants
    public static class SystemMessageConst // Lớp static SystemMessageConst
    {
        public static class systemmessage
        {
            public const string IsCreateReportFailse = "Không có dữ liệu, không thể tạo báo cáo";
            public const string IsCreateReportSuccess = "Tạo báo cáo thành công";
            public const string IsCreateReportSuccessEn = "Create a successful report";
            public const string IsLoadallSuccess = "Tải dữ liệu thành công";

            public const string IsLoginSuccessTrue = "Đăng nhập thành công";
            public const string IsLoginSuccessTrueEn = "Login successfull";
            public const string IsLoginSuccessFalse = "Đăng nhập thất bại";
            public const string IsLoginSuccessFalseEn = "Login fail";

            public const string AccountLoggedAlert = "Tài khoản của bạn đang đăng nhập ở một nơi khác !";
            public const string AccountLoggedAlertEn = "Your account logged in somewhere else !";

            public const string IsLogoutSuccess = "Đăng xuất thành công";
            public const string IsLogoutSuccessEn = "Logout successfull";

            public const string IsLoginNotSuccess = "Tài khoản đang bị khóa hoặc chưa được kích hoạt";
            public const string IsLoginNotSuccessEn = "Account is locked or not activated";

            public const string IncorrectCodeActive = "Nhập sai code active";
            public const string IncorrectCodeActiveEn = "Incorrect code active";

            public const string CreateAccountSuccess = "Tạo tài khoản thành công";
            public const string CreateAccountSuccessEn = "Create account successfull";

            public const string AddSuccess = "Thêm thành công";
            public const string AddSuccessEn = "Add successfull";

            public const string BuySuccess = "Thanh toán thành công";
            public const string BuySuccessEn = "Check out successfull";

            public const string SaveSuccess = "Cập nhật thành công";
            public const string SaveSuccessEn = "Update successful";

            public const string IsNotExist = "Dữ liệu không tồn tại";
            public const string IsNotExistEn = "This Data is not existed!";

            public const string PhoneIsExisted = "Số điện thoại đã tồn tại";
            public const string PhoneIsExistedEn = "Phone is existed";
            public const string CompanyPhone = "Số điện thoại cơ quan";
            public const string CompanyPhoneEn = "Company phone";

            public const string EmailIsExisted = "Email đã tồn tại";
            public const string EmailIsExistedEn = "Email is existed";
            public const string EmailIsNotExistOrLock = "Email không tồn tại hoặc bị khóa";
            public const string EmailIsNotExistOrLockEn = "Email is not existed or lock";
            public const string EmailIsNotExist = "Email không tồn tại";
            public const string EmailIsNotExistEn = "Email is not existed";
            public const string EmailAndPhoneIsExisted = "Email hoặc số điện thoại này đã được đăng ký";
            public const string EmailAndPhoneIsExistedEn = "Email or phone is existed";
            public const string EmailConfirmSuccessFull = "Xác nhận email thành công";
            public const string EmailConfirmSuccessFullEn = "Successful email validation. Please wait for activation email by ASSA35VN";
            public const string EmailConfirmUnSuccessFull = "Xác nhận email thất bại";
            public const string EmailConfirmUnSuccessFullEn = "Email confirmation failed";
            public const string EmailConfirmIsNotCorrect = "Xác nhận email không chính xác";
            public const string EmailConfirmIsNotCorrectEn = "Email confirm is not correct";
            public const string EmailRegisterAccount = "Email đăng ký tài khoản";
            public const string EmailRegisterAccountEn = "Email register account";
            public const string CompanyEmail = "Email cơ quan";
            public const string CompanyEmailEn = "Company email";
            public const string EmailConfirm = "Email xác nhận tài khoản";
            public const string EmailConfirmEn = "Account confirmation email";
            public const string Email = "Email cá nhân";
            public const string EmailEn = "Email personal";
            public const string EmailAddress = "Địa chỉ email";
            public const string EmailAddressEn = "Email address";
            public const string Username = "Tên đăng nhập: ";
            public const string UsernameEn = "Username: ";

            public const string EditSuccess = "Cập nhật thành công";
            public const string EditSuccessEn = "Update successfull";

            public const string ConfirmAfterDelete = "Bạn có chắc chắn muốn xóa !";
            public const string DeleteSuccess = "Xóa thành công";
            public const string DeleteSuccessEn = "Delete successfull";
            public const string DeleteFail = "Xóa thất bại";
            public const string DeleteFailEn = "Delete fail";

            public const string DataIsEmpty = "Chưa có dữ liệu";
            public const string DataIsEmptyEn = "Data not available";
            public const string WereAreSendMailConfirm = "Đăng ký tài khoản thành công,Mời bạn kiểm tra hòm thư để xác nhận mail";

            public const string SendMailActive = "Mail kích hoạt đã được gửi";
            public const string SendMailActiveEn = "This mail confirm has been send to your mail";
            public const string SendMailForgotPassword = "Mail lấy lại mật khẩu đã được gửi";
            public const string SendMailForgotPasswordEn = "Mail forgotpassword has been send";

            public const string MustCheckAgree = "Bạn chưa chọn đồng ý";
            public const string MustCheckAgreeEn = "You must be check agree";

            public const string DateTimeIsNotCorrectFormat = "Sai định dạng ngày tháng";

            public const string DateStartMeetingNotNull = "Ngày diễn ra không được để trống";
            public const string DateStartMeetingNotNullEn = "Organization Day Not Null";

            public const string TimeStartNotNull = "Thời gian bắt đầu không được để trống";
            public const string TimeStartNotNullEn = "Start time can not be empty";
            public const string TimeEndNotNull = "Thời gian kết thúc không được để trống";
            public const string TimeEndNotNullEn = "End time can not be empty";

            public const string PlaceEn = "place";
            public const string Place = "Đia điểm tổ chức";

            public const string InforMeeting = "Thông tin cuộc họp";
            public const string InforMeetingEn = "Infor Meeting";

            public const string SendSuccess = "Gửi thành công";
            public const string SendSuccessEn = "Send Success";

            public const string StatusHide = "Hide";
            public const string StatusShow = "Show";

            public const string AccountNotExist = "Tài khoảng không tồn tại";
            public const string AccountNotExistEn = "This Account is not exist !";

            public const string PasswordNotCorrect = "Mật khẩu không chính xác";
            public const string PasswordNotCorrectEn = "Your password is not correct !";
            public const string ConfirmPasswordNotCorrect = "Xác nhận mật khẩu không chính xác";
            public const string ConfirmPasswordNotCorrectEn = "Confirm password is not correct !";
            public const string OldPasswordNotCorrect = "Mật khẩu cũ không chính xác";
            public const string OldPasswordNotCorrectEn = "Old password is not correct";
            public const string NewPassword = "Mật khẩu mới";
            public const string NewPasswordEn = "New Password";

            public const string Password = "Mật khẩu: ";
            public const string PasswordEn = "Password: ";

            public const string City = "Thành phố";
            public const string CityEn = "City";
            public const string Phone = "Số điện thoại";
            public const string PhoneEn = "Phone";
            public const string Address = "Địa chỉ";
            public const string AddressEn = "Address";
            public const string Address2 = "Địa chỉ 2";
            public const string Address2En = "Address2";
            public const string Fullname = "Họ và tên";
            public const string FullnameEn = "Fullname";
            public const string MessageEn = "Message";
            public const string Message = "Nội dung tin nhắn";
            public const string DefaultImage = "/Content/dist/img/default-image.gif";
            public const string Position = "Chức vụ";
            public const string PositionEn = "Position";
            public const string PersonCardId = "Số chứng minh thư / Hộ chiếu";
            public const string PersonCardIdEn = "Card id / Passport";
            public const string Photo_of_ID_Card_Passport_Backside = "Ảnh chứng minh thư / Hộ chiếu mặt trước không được để trống";
            public const string Photo_of_ID_Card_Passport_BacksideEn = "Photo of ID Card / Passport Backside is not empty";
            public const string Photo_of_ID_Card_Passport_Front = "Ảnh chứng minh thư / Hộ chiếu mặt sau không được để trống";
            public const string Photo_of_ID_Card_Passport_FrontEn = "Photo of ID Card / Passport Front is not empty";

            public const string DateOfBirth = "Ngày sinh";
            public const string DateOfBirthEn = " Date of birth";

            public const string Localtion = "Vị trí";
            public const string LocaltionEn = " Location";

            public const string DateRange = "Ngày cấp";
            public const string DateRangeEn = " Date range";

            public const string PlaceOfIssue = "Nơi cấp";
            public const string PlaceOfIssueEn = "Place Of Issue";

            public const string Photo_of_ID_Card_Passport = "Ảnh chứng minh thư / Hộ chiếu";
            public const string Photo_of_ID_Card_PassportEn = "Photo of ID Card / Passport";

            public const string Photo_of_ID_Card_Passport_Back = "Ảnh chứng minh thư / Hộ chiếu mặt trước";
            public const string Photo_of_ID_Card_Passport_BackEn = "Photo of ID Card / Passport Backside";

            public const string ClickHereToGet = "Click vào đây để lấy ";
            public const string ClickHereToGetEn = "Click here to get ";
            public const string Appointment = "giấy mời tham gia hội nghị ";
            public const string AppointmentEn = "appointment";
            public const string ClickHereToLogin = "Click vào đây để đăng nhập.";
            public const string ClickHereToLoginEn = "Click here to login ";
            public const string Photo_of_ID_Card_Passport_S = "Ảnh chứng minh thư / Hộ chiếu mặt sau";
            public const string Photo_of_ID_Card_Passport_S_En = "Photo of ID Card / Passport Front";

            public const string Photo_employee_Card = "Ảnh thẻ nhân viên";
            public const string Photo_employee_CardEn = "Photo employee Card";

            public const string CardNumber = "Số thẻ nhà báo";
            public const string CardNumberEn = "Card Number";




            public const string NoData = "Không có dữ liệu";
            public const string NoDataEn = "No Data";

            public const string CartEmpty = "Giỏ hàng trống !";
            public const string CartEmptyEn = "Your cart is empty !";

            public const string CaptchaIncorrect = "Nhập lại captcha";
            public const string CaptchaIncorrectEn = "Incorrect captcha";

            public const string ArticleExisted = "Mã sản phẩm đã tồn tại";
            public const string PhoneConfirmMes = "Một tin nhắn chứa mã xác nhận đã được gửi đến số của bạn , hãy nhập mã xác nhận đó để hoàn tất đăng ký !";
            public const string PhoneConfirmMesEn = "A message containing a confirmation code has been sent to your number, please enter the confirmation code to complete the registration !";
            public const string BataMessageActiveCode = "Ban dung ma so sau day de hoan tat dang ky : ";
            public const string RegisterSuccessfull = "Đăng ký tài khoản thành công !, hãy vào mail để xác nhận tài khoản";
            public const string RegisterSuccessfullEn = "Account registration successful !, please check your email then confirm email .";
            public const string MustChooseAu = "Bạn phải chọn quyền tài khoản !";
            public const string MustChooseAuEn = "You must select the account permissions !";
            public const string VenueNotCorrect = "Địa điểm tổ chức ko hợp lệ !";
            public const string VenueNotCorrectEn = "Place is invalid !";
            public const string RejectTopicMessage = "Từ chối do vi phạm nội quy !";

            public const string CompanyName = "Tên cơ quan";
            public const string CompanyNameEn = "Company Name";

            public const string CompanyAddress = "Địa chỉ cơ quan";
            public const string CompanyAddressEn = "Company Address";

            public const string CompanyFax = "Fax cơ quan";
            public const string CompanyFaxEn = "Company Fax";

            public const string FileDocumentary = "File công văn không được để trống";
            public const string FileDocumentaryEn = "File documentary is not empty";

            public const string Documentary = "Công văn";
            public const string DocumentaryEn = "Documentary";


            public const string DocumentList = "Danh sách tài liệu";
            public const string DocumentListEn = "Document List";
            public const string ImagesList = "Danh sách chủ đề ảnh";
            public const string ImagesListEn = "Picture theme list";
            public const string VideoList = "Danh sách chủ video";
            public const string VideoListEn = "Video topic list";

            public const string Listofarticles = "Danh sách bài viết";
            public const string ListofarticlesEn = "List of articles";

            public const string IsDeleted = "Đã bị xóa ";
            public const string IsDeletedEn = "Has been deleted ";
            public const string Deactive = "Từ chối phế duyệt ";
            public const string DeactiveEn = "Deactive ";
            public const string Pending = "Chờ phê duyệt ";
            public const string PendingEn = "Pending ";
            public const string Active = "Đã đẩy lên website ";
            public const string ActiveEn = "Active ";
            public const string Save_Draft = "Lưu tạm ";
            public const string Save_DraftEn = "Save draft ";
            public const string RemoveOnSite = "Gỡ khỏi website ";
            public const string RemoveOnSiteEn = "Remove on site ";
            public const string Unknown = "Không xác định ";
            public const string UnknownEn = "Unknown ";
            public const string Notapproved = "Chưa phê duyệt ";
            public const string NotapprovedEn = "Not approved ";
            public const string Topic_name = "Tên chủ đề ";
            public const string Topic_nameEn = "Topic name ";
            public const string Introduce = "Giới thiệu ";
            public const string IntroduceEn = "Introduce ";
            public const string Iframe = "Mã nhúng ";
            public const string IframeEn = "Iframe ";

            public const string Title = "Tiêu đề ";
            public const string TitleEn = "Title ";
            public const string Content = "Nội dung ";
            public const string ContentEn = "Content ";

            public const string Select_Languge = "Mời bạn chọn phiên bản ngôn ngữ";
            public const string Select_LangugeEn = "Please select the language version";
            public const string Not_Select_Document = "Bạn chưa chọn tài liệu cho chủ đề này";
            public const string Not_Select_DocumentEn = "You have not selected a document for this topic";

            public const string Not_Select_Avatar_Post = "Bạn chưa chọn avatar cho bài viết";
            public const string Not_Select_Avatar_PostEn = "You have no avatar selected for the post";

            public const string Violation_of_the_terms_of_the_organizers = "Vi phạm điều khoản ban tổ chức đề ra";
            public const string Violation_of_the_terms_of_the_organizersEn = "Violation of the terms of the organizers";

            public const string Not_Select_Images = "Bạn chưa chọn ảnh cho chủ đề này";
            public const string Not_Select_ImagesEn = "You have not selected a images for this topic";

            public const string Update_and_approve_topic_successfully = "Cập nhật thông tin và phê duyệt chủ đề thành công";
            public const string Update_and_approve_topic_successfullyEn = "Update and approve topic successfully";
            public const string Update_topic_successfully = "Cập nhật thông tin chủ đề thành công";
            public const string Update_topic_successfullyEn = "Update topic successfully";
            public const string Refusal_to_approve_the_topic_successfully = "Từ chối phê duyệt chủ đề thành công";
            public const string Refusal_to_approve_the_topic_successfullyEn = "Refusal to approve the topic successfully";
            public const string Remove_the_theme_from_the_website_successfully = "Gỡ chủ đề khỏi website thành công";
            public const string Remove_the_theme_from_the_website_successfullyEn = "Remove the theme from the website successfully";
            public const string Successful_topic_change = "Thay đổi trạng thái chủ đề thành công";
            public const string Successful_topic_changeEn = "Successful topic change";
            public const string You_can_not_change_topic_content = "Bạn không thể thay đổi nội dung chủ đề";
            public const string You_can_not_change_topic_contentEn = "You can not change topic content";
            public const string Failed_to_update_topic_information = "Không thể cập nhật thông tin chủ đề";
            public const string Failed_to_update_topic_informationEn = "Failed to update topic information";


            public const string Update_and_approve_post_successfully = "Cập nhật thông tin và phê duyệt bài viết thành công";
            public const string Update_and_approve_post_successfullyEn = "Update and approve post successfully";
            public const string Update_post_successfully = "Cập nhật thông tin bài viết thành công";
            public const string Update_post_successfullyEn = "Update post successfully";
            public const string Refusal_to_approve_the_post_successfully = "Từ chối phê duyệt bài viết thành công";
            public const string Refusal_to_approve_the_post_successfullyEn = "Refusal to approve the post successfully";
            public const string Remove_the_post_from_the_website_successfully = "Gỡ bài viết khỏi website thành công";
            public const string Remove_the_post_from_the_website_successfullyEn = "Remove post from the website successfully";
            public const string Successful_post_change = "Thay đổi trạng thái bài viết thành công";
            public const string Successful_post_changeEn = "Successful post change";
            public const string You_can_not_change_post_content = "Bạn không thể thay đổi nội dung bài viết";
            public const string You_can_not_change_post_contentEn = "You can not change post content";
            public const string Failed_to_update_post_information = "Không thể cập nhật thông tin bài viết";
            public const string Failed_to_update_post_informationEn = "Failed to update post information";


            public const string Unit_name = "Tên đơn vị ";
            public const string Unit_nameEn = "Unit name ";

            public const string Editorial_board = "Ban biên tập ";
            public const string Editorial_boardEn = "Editorial board ";
            public const string Secretariat = "Ban thư ký ";
            public const string SecretariatEn = "Secretariat ";


            public const string NameMeeting = "Tên cuộc họp ";
            public const string NameMeetingEn = "Name of the meeting ";
            public const string Note = "Ghi chú ";
            public const string NoteEn = "Note ";
            public const string Reason = "lý do ";
            public const string ReasonEn = "reason ";

            public const string File_att = "File đính kèm ";
            public const string File_attEn = "File attach ";

            public const string Name_venue = "Tên địa điểm tổ chức ";
            public const string Name_venueEn = "Name of the venue ";

            public const string Statisticsreporters = "Thống kê theo phóng viên ";
            public const string StatisticsreportersEn = "Statistics according to reporters";

            public const string Statisticsdelegates = "Thống kê theo đại biểu ";
            public const string StatisticsdelegatesEn = "Statistics of the delegates";

            public const string TitleReporters = "THỐNG KÊ PHÓNG VIÊN(ĐOÀN PHÓNG VIÊN) THAM GIA HỘI NGHỊ ";
            public const string TitleReportersEn = "STATISTICS OF REPORTER (GROUP REPORTER) CONFERENCE";

            public const string TitleDelegate = "THỐNG KÊ ĐẠI BIỂU(ĐOÀN ĐẠI BIỂU) THAM GIA HỘI NGHỊ ";
            public const string TitleDelegateEn = "STATISTICS OF DELEGATE (GROUP DELEGATE) CONFERENCE";

            public const string Rp_STT = "STT ";
            public const string Rp_STTEn = "ID";
            public const string Rp_AccountEmail = "EMAIL TÀI KHOẢN ";
            public const string Rp_AccountEmailEn = "EMAIL ACCOUNT";
            public const string Rp_Namecompany = "TÊN CƠ QUAN ";
            public const string Rp_NamecompanyEn = "NAME OF AGENCY";
            public const string Rp_Datecreated = "NGÀY TẠO ";
            public const string Rp_DatecreatedEn = "DATE CREATED";
            public const string Rp_TypeAccount = "LOẠI TÀI KHOẢN";
            public const string Rp_TypeAccountEn = "TYPE OF ACCOUNTS";
            public const string Rp_Emaicompany = "EMAIL CƠ QUAN";
            public const string Rp_EmaicompanyEn = "EMAIL AGENCY";
            public const string Rp_Addconpany = "ĐỊA CHỈ CƠ QUAN";
            public const string Rp_AddconpanyEn = "WORK PLACE";
            public const string Rp_Phonecompany = "SỐ ĐIỆN THOẠI";
            public const string Rp_PhonecompanyEn = "PHONE";
            public const string Rp_Fullname = "HỌ VÀ TÊN";
            public const string Rp_FullnameEn = "FULL NAME";
            public const string Rp_Position = "CHỨC VỤ";
            public const string Rp_PositionEn = "POSITION";
            public const string Rp_Email = "EMAIL";
            public const string Rp_EmailEn = "EMAIL";

            public const string Rp_ListMember = "DANH SÁCH THÀNH VIÊN";
            public const string Rp_ListMemberEn = "MEMBERS LIST";


            public const string Rp_Address = "ĐỊA CHỈ";
            public const string Rp_AddressEn = "ADDRESS";
            public const string Rp_Registerroom = "ĐĂNG KÝ PHÒNG";
            public const string Rp_RegisterroomEn = "REGISTER ROOM";

            public const string Rp_Registervehicle = "ĐĂNG KÝ XE";
            public const string Rp_RegistervehicleEn = "VEHICLE REGISTRATION";

            public const string Missingavatars = "Thiếu ảnh đại diện";
            public const string MissingavatarsEn = "Missing avatars";

            public const string Avatar = "Ảnh đại diện";
            public const string AvatarEn = "Avatar";
            public const string ManageIntroductionConference = "Quản lý bài viết giới thiệu về hội nghị";
            public const string ManageIntroductionConferenceEn = "Manage Introduction Conference";

            public const string ManageSpeaker = "Quản lý người phát biểu";
            public const string ManageSpeakerEn = "Manage Speaker";

            public const string GUESTSPEAKER = "Khách mời phát biểu";
            public const string GUESTSPEAKEREN = "Guest speaker";

            public const string KEYNOTESPEAKER = "Diễn giả chính";
            public const string KEYNOTESPEAKEREN = "Keynote speaker";

            public const string PANELIST = "Người điều tra";
            public const string PANELISTEN = "PaneList";

            public const string ManageMeeting = "Quản lý cuộc họp";
            public const string ManageMeetingEn = "Manage Meeting";

            // Activity VN
            public const string NameActivity = "Tên hoạt động";
            public const string DesActivity = "Mô tả hoạt động";
            public const string StartDate = "Ngày diễn ra";
            public const string ActivityType = "Loại hình hoạt động";
            public const string Stt = "Thứ tự diễn ra";

            // Activity EN
            public const string NameActivityEn = "Name of Activity";
            public const string DesActivityEn = "Description";
            public const string StartDateEn= "Start date";
            public const string ActivityTypeEn = "Activity Type";
            public const string SttEn = "Order number";

            //Registration form 
            public const string TitleNameVi = "Xưng tên";
            public const string TitleNameEn = "Title Name";

            public const string SpecifyVi = "Chỉ định";
            public const string SpecifyEn = "specify";

            public const string LastNameVi = "Họ";
            public const string LastNameEn = "Last Name";

            public const string FirstNameVi = "Tên";
            public const string FirstNameEn = "First Name";

            public const string MiddleInitialVi = "Tên Đệm";
            public const string MiddleInitialEn = "Middle Initial";

            public const string OrganizationVi = "Tổ chức";
            public const string OrganizationEn = "Organization";

            public const string DepartmentVi = "Phòng";
            public const string DepartmentEn = "Institution";

            public const string TeleNumberVi = "Số điện thoại";
            public const string TeleNumberEn = "Telephone Number";

            public const string FaxNumVi = "Số fax";
            public const string FaxNumEn = "Fax Number";

            public const string EmailAddVi = "Địa chỉ Email";
            public const string EmailAddEn = "Email Addres";

            public const string DietaryReqVi = "Ghi chú thực phẩm";
            public const string DietaryReqEn = "Dietary Requirement";

            public const string MealOtherReqEn = "Meal specify requirement";

            public const string per1Vi = "Người thứ nhất";
            public const string per1En = "Person 1";

            public const string per2Vi = "Người thứ hai";
            public const string per2En = "Person 2";

            public const string per3Vi = "Người thứ ba";
            public const string per3En = "Person 3";

            public const string specReVi = "Yêu cầu đặc biệt";
            public const string specReEn = "Special requirements";
            
            

            public const string ConAirVi = "Máy bay đến";
            public const string ConAirEn = "Connecting Fight from";

            public const string ConFlightVi = "Số hiệu máy bay đến ";
            public const string ConFlightEn = "Connecting Flight No";

            public const string OtherReqVi = "Yêu cầu khác ";
            public const string OtherReqEn = "Other requirements";

            public const string DepConAirVi = "Máy bay khởi hành   ";
            public const string DepConAirEn = "Departure Connecting Airline";

            public const string DepConFlightVi = "Số hiệu máy bay khởi hành ";
            public const string DepConFlightEn = "Departure Connecting  Flight";

            public const string ErrorHappenVi = "Có lỗi xảy ra khi cập nhật";
            public const string ErrorHappenEn = "Something goes wrong !";
        }


        // Đối tượng static ko đổi ValidateConst
        public static class ValidateConst
        {
            public const string MinlengthOfText = "Độ dài của {0} phải lớn hơn {1} ký tự."; // Phần tử MinlengthOfText truyền vào 2 tham số
            public const string MinlengthOfTextEn = "Length of {0} must be greater {1} characters.";
            public const string EmailNotCorrectFormat = "Email không đúng định dạng";
            public const string EmailNotCorrectFormatEn = "Email is not correct Format";
            public const string CheckNotEmpty = "{0} không được để trống";
            public const string CheckNotEmptyEn = "{0} is not empty";
            public const string MinMaxlengthOfText = "Độ dài của {0} phải nhỏ hơn {1} và  lớn hơn {2} ký tự."; // Phần tử MinlengthOfText truyền vào 3 tham số
            public const string MinMaxlengthOfTextEn = "Length of {0} must be smaller {1} and greater {2} characters.";
            public const string MaxlengthOfText = "Độ dài của {0} phải nhỏ hơn {1} ký tự.";
            public const string MaxlengthOfTextEn = "Length of {0} must be smaller {1} characters.";
            public const string EmailConfirmNotCorrect = "Xác nhận Email không chính xác !";
            public const string EmailConfirmNotCorrectEn = "Email confirm is not correct !";
            public const string FileNotCorrectFormat = "File {0} không đúng định dạng";
            public const string FileNotCorrectFormatEn = "File {0} is not correct format";
            public const string DateNotCorrectFormat = "{0} không đúng định dạng";
            public const string DateNotCorrectFormatEn = "{0} is not correct format";
            public const string DateNotValid = "{0} không hợp lệ";
            public const string DateNotValidEn = "{0} not valid";
            public const string TimeNotCorrectFormat = "{0} không đúng định dạng";
            public const string TimeNotCorrectFormatEn = "{0} is not correct format";
            public const string EndDateNotCorrect = "Thời gian kết thúc không hợp lệ";
            public const string EndDateNotCorrectEn = "Endate is not valid";
            public const string IsNotCorrect = " không chính xác";
            public const string IsNotCorrectEn = " is not correct";

            public const string IsOnlyVideo = " chỉ dùng một video";
            public const string IsOnlyVideoEn = " only one video";

            public const string DataIsNotCorrect = " dữ liệu không phù hợp đã bị xóa";
            public const string DataIsNotCorrectEn = " invalid data has been deleted";

            public const string DateTimeNotValid = "Thời gian bị trùng với bài viết đã đăng";
            public const string DateTimeNotValidEn = "The time is the same as the post";

            public const string MustBeChooseStatus = "Chưa chọn trạng thái";
            public const string MustBeChooseStatusEn = "Must be choose status !";

            public const string LockAccountDefaultReason = "vi phạm quy định của ban tổ chức !";
            public const string LockAccountDefaultReasonEn = "violate the regulations !";
        }
        public static class Url
        {
            public const string Login = "/login";
            public const string LoginClient = "/loginClient";
            public const string HostLogin = "admin.assa35.com";
        }
        public static class StatusConst
        {
            public const int Delete = 0;
            public const int Deactive = 1;
            public const int Pending = 2;
            public const int Active = 3;
            public const int SaveDraft = 4;
            public const int RemoveOnSite = 5;
        }
        public static class AccountStatusConst
        {
            public const string Active = "Đang sử dụng";
            public const string NotificaActive = "Đã gửi";
            public const string NotificaActiveEn = "Active";
            public const string MemberActive = "Đã xác nhận";
            public const string MemberActiveEn = "Active";
            public const string MemberDeactive = "Từ chối phê duyệt";
            public const string MemberDeactiveEn = "Deactive";
            public const string ActiveEn = "Active";
            public const string Pending = "Chờ kích hoạt";
            public const string PendingEn = "Pending";
            public const string Deactive = "Khóa";
            public const string DeactiveEn = "Deactive";
            public const string Draft = "Nháp";
            public const string DraftEn = "Draft";
            public const string RemoveOnSite = "Gỡ khỏi website";
            public const string RemoveOnSiteEn = "Remove on website";
        }
        public static class RoleConst
        {
            public const int Admin = 1;
            public const int Banbientap = 2;
            public const int Banthuky = 3;
            public const int Phongvien = 4;
            public const int Daibieu = 5;
        }
        public static class AccountType
        {
            public const int Personal = 1;
            public const int Group = 2;
        }
        public static class LanguageVersion
        {
            public const int English = 2;
            public const int Vietnam = 1;
        }
        public static class SendEmailConst
        {
            public const string Subject = "Thông báo";
            public const string SubjectInfor = "Thông tin khách {0}";
            public const string SubjectConfirmEmail = "Thông báo xác nhận tài khoản";
            public const string SubjectConfirmEmailEn = "Confirm Account for Notification";
            
            public const string ContentsTitle = "Ban quản trị xin thông báo .Tài khoản {0} đã ";
            public const string RemovePost = "Ban quản trị xin thông báo .{0} đã bị xóa bỏ khỏi hệ thống ";
            public const string ContentsRegister = "Ban quản trị xin thông báo .Tài khoản {0} đã đăng ký thành công và mật khẩu đăng nhập của bạn là : {1} .";
            public const string ContentsRegisterWait = "Ban quản trị xin thông báo .Tài khoản {0} đã đăng ký thành công , sau khi admin kiểm tra thông tin sẽ phản hồi ngay cho bạn";

            public const string SendMailConfirm =
                    "<p style='font-size: 12.0pt; margin: 1em 0;'><span style='font-size:11.0pt;font-family: Calibri,sans-serif'>Xin chào {0},</span></p> <br/>" +
                    "<p style='font-size: 12.0pt; margin: 1em 0;'><span style='font-size:11.0pt;font-family: Calibri,sans-serif'>Cảm ơn bạn đã đăng ký trên website (https://assa35.com/) và cho chúng tôi biết bạn sẽ tham dự hội nghị ASSA 35 và các hoạt động liên quan. Vui lòng bấm vào liên hết sau để xác thực thông tin email bạn đăng ký là chính xác: <a href='{1}'>Click vào đây</a></span></p><br/>" +
                    "<p style='font-size: 12.0pt; margin: 1em 0;'><span style='font-size:11.0pt;font-family: Calibri,sans-serif'>Nếu bạn có bất cứ thắc mắc nào khác, xin vui lòng liên hệ:</span></p><br/>" +
                    "<p style='font-size: 12.0pt; margin: 1em 0;'><span style='font-size:11.0pt;font-family: Calibri,sans-serif'>- Tel: (84-24)36281193 – 36281191 </span></p>" +
                    "<p style='font-size: 12.0pt; margin: 1em 0;'><span style='font-size:11.0pt;font-family: Calibri,sans-serif'>- Fax: (84-24) 38697886.</span></p>"

                ;

            public const string SendMailConfirmEn =
                    "<p style='font-size: 12.0pt; margin: 1em 0;'><span style='font-size:11.0pt;font-family: Calibri,sans-serif'>Dear {0}, </span></p>" +
                    "<p style='font-size: 12.0pt; margin: 1em 0;'><span style='font-size:11.0pt;font-family: Calibri,sans-serif'>Welcome to 35th ASSA Board Meeting and associated activities!</span></p> " +
                    "<p style='font-size: 12.0pt; margin: 1em 0;'><span style='font-size:11.0pt;font-family: Calibri,sans-serif'>Thank you for your registration on the website (https://assa35.com/) and reached out to let us know that you will be attending the meetings and other related activities.</span></p>"+
                    "<p style='font-size: 12.0pt; margin: 1em 0;'><span style='font-size:11.0pt;font-family: Calibri,sans-serif'>Please kindly click the following link to confirm that your registered email is correct. <a href='{1}'>Your confirmation link</a></span><br/></p>" +
                    "<p style='font-size: 12.0pt; margin: 1em 0;'><span style='font-size:11.0pt;font-family: Calibri,sans-serif'>In terms of accommodation, we will send you the confirmation at soonest time. Should you have any questions please do not hesitate to contact:</span></p>" +
                    "<p style='font-size: 12.0pt; margin: 1em 0;'><span style='font-size:11.0pt;font-family: Calibri,sans-serif'>- Tel: (84-24)36281193 – 36281191</span></p>" +
                    "<p style='font-size: 12.0pt; margin: 1em 0;'><span style='font-size:11.0pt;font-family: Calibri,sans-serif'>- Fax: (84-24) 38697886</span></p>"
                ;

            public const string SendMailInfor = "<p style='font-siz`e: 12.0pt; margin: 1em 0;'><span style='font-size:11.0pt;font-family: Calibri,sans-serif'>Dear VC and Mice 9, </span></p>" +
                                                "<p style='font-size: 12.0pt; margin: 1em 0;'><span style='font-size:11.0pt;font-family: Calibri,sans-serif'>Thông tin của khách: {0} ở bên dưới tệp đính kèm </span></p>" +                                               
                                                "<p style='font-size: 12.0pt; margin: 1em 0;'><span style='font-size:11.0pt;font-family: Calibri,sans-serif'>Cảm ơn chị !</span></p>"
                ;
            public const string SendMailInforToVss = "<p style='font-siz`e: 12.0pt; margin: 1em 0;'><span style='font-size:11.0pt;font-family: Calibri,sans-serif'>Dear VSS, </span></p>" +
                                                     "<p style='font-size: 12.0pt; margin: 1em 0;'><span style='font-size:11.0pt;font-family: Calibri,sans-serif'>Thông tin của khách: {0} ở bên dưới tệp đính kèm </span></p>" +
                                                "<p style='font-size: 12.0pt; margin: 1em 0;'><span style='font-size:11.0pt;font-family: Calibri,sans-serif'>Cảm ơn chị !</span></p>"
                ;
        }
    }
}