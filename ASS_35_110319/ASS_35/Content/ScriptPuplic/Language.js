
function getCookie(cname) {
    var name = cname + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}


function ReturnLanguageJs(contentVn, contentEn) {
    var user = getCookie("Language");
    if (user == "en") {
        return contentEn;
    } else {
        return contentVn;
    }
}

var Mustbegreater = "Phải lớn hơn hoặc bằng";
var MustbegreaterEn = "Must be greater than or equal to";

var Characters = " ký tự";
var CharactersEn = " characters";

var From = " từ ";
var FromEn = " from ";

var To = " đến ";
var ToEn = " to ";

var Mustbefrom = "Phải từ ";
var MustbefromEn = "Must be from ";

var NotEmptyDataField = "Trường dữ liệu không thể để trống ";
var NotEmptyDataFieldEn = "Data field can not be empty ";

var Incorrectlinkformat = "Không đúng định dạng link ";
var IncorrectlinkformatEn = "Incorrect link format ";

var OnlyOneVideo = "Chỉ được thêm một video ";
var OnlyOneVideoEn = "Only one video added ";

var Malformed = "Không đúng định dạng ";
var MalformedEn = "Malformed ";

var Incorrectfaxformat = "Không đúng định dạng fax ";
var IncorrectfaxformatEn = "Incorrect fax format ";

var Yourchoice = "Mời bạn lựa chọn ";
var YourchoiceEn = "Your choice ";

var TryPassword = "Nhập lại mật khẩu không đúng ";
var TryPasswordEn = "Password incorrect, please try again ";

var EnterPAss = "Hãy nhập lại mật khẩu của bạn ";
var EnterPAssEn = "Please enter your password again ";

var Notification = "Thông báo ";
var NotificationEn = "Notification ";

var Transferafter = "Tự chuyển sau 2 giây";
var TransferafterEn = "Transfer after 2 seconds ";

var Yes_agree = "Vâng, tôi đồng ý !";
var Yes_agreeEn = "Yes, I agree !";

var No_Thank = "Không, cảm ơn !";
var No_ThankEn = "No, thanks !";

var ExxitAfter2s = "Thoát sau 2 giây";
var ExxitAfter2sEn = "Exit after 2 seconds";

var Accountsignedsomewhere = "Tài khoản của bạn đã đăng nhập ở một nơi khác";
var AccountsignedsomewhereEn = "Your account is signed in somewhere else";

var Err_operation = "Đã xảy ra lỗi trong thao tác";
var Err_operationEn = "There was an error in the operation";

var Accountunderattack = "Tài khoản của bạn đang bị tấn công ....";
var AccountunderattackEn = "Your account is under attack ....";

var Invalidaccess = "Truy cập không hợp lệ";
var InvalidaccessEn = "Invalid access";

var Selectedfile = " File được chọn";
var SelectedfileEn = " Selected file";

var Delete = "Xóa";
var DeleteEn = "Delete";

var IncorectorNotChoise = "Không chọn tệp tải lên hoặc chọn định dạng sai";
var IncorectorNotChoiseEn = "Do not select the file upload or choose the wrong format";

