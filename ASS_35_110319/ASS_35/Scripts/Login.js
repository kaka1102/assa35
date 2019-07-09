
$(document).ready(function () {
    LoginAdmin();
    ResetPassWord();
});


function LoginAdmin() {
    $('#btnsubmit').click(function () {
        var fm_data = new FormData();
        var Obj_data = {
            Email: $('#Email').val(),
            PassWord: $('#Password').val(),
            res: res
        }
        
        
        fm_data.append("DataSend", JSON.stringify(Obj_data));
        
        
        var success = function (dx) {
         
            if (dx.Resulf.IsSuccess && dx.Resulf.LstData == null) {
                window.location = "/Admin/Index";
            }
            else if (dx.Resulf.IsSuccess && dx.Resulf.LstData != null && dx.Resulf.ItemData != null) {
                $("#loginform").slideUp(), $("#choseRoll").fadeIn();
                BindRollToCombobox(dx.Resulf.LstData);
                AcceptRoll(dx.Resulf.ItemData);
            }
            else {
                QTW_RUN_MESS.alter_Message_Error(dx.Resulf.Message);
            }
        }
      QTW_JQUERY.RUN_AJAX("ajaxPOST", "/Login/LoginAdmin", fm_data, success);
  

        //var fun = function () {
        //    QTW_JQUERY.RUN_AJAX("ajaxPOST", "/Login/LoginAdmin", fm_data, success);
        //}
        //QTW_RUN_MESS.alter_Message_Question_Callbaclk("Bạn có muốn login không ?", fun);

    });
}
function BindRollToCombobox(data) {
    $('#cbb_Roll').empty();
    $.each(data, function (i, v) {
        $('#cbb_Roll').append('<option value=' + v.idquyen + '>' + v.tenquyen + '</option>');
    });

}
function AcceptRoll(dataSession) {
    $('#btnAccept').click(function () {
        var objS = {
            idquyentk: $('#cbb_Roll').val()
        }
        var fm_data = new FormData();
        fm_data.append("DataSend", JSON.stringify(dataSession));
        fm_data.append("objS", JSON.stringify(objS))

        var success = function (dx) {
            if (dx.Resulf.IsSuccess && dx.Resulf.LstData == null) {
                window.location = "/admin/Index";
            }
            else {
                QTW_RUN_MESS.alter_Message_Error(dx.Resulf.Message);
            }
        }
        QTW_JQUERY.RUN_AJAX("ajaxPOST", "/Login/CheckSelectRollAdmin", fm_data, success);
    });
}

function ResetPassWord() {
    $('#btnResetPass').click(function () {
        var emailReset = $('#emailReset').val();

        var fm_data = new FormData();
        var obj_Data =  {
            emailReset: emailReset
        }
        fm_data.append("DataSend", JSON.stringify(obj_Data));
        var success = function (dx) {
            if (dx.Result.IsSuccess && dx.Result.LstData == null) {
                window.location = "/Admin/Index";
            }
            else {
                QTW_RUN_MESS.alter_Message_Error(dx.Result.Message);
            }
        }
        QTW_JQUERY.RUN_AJAX("ajaxPOST", "/Login/ResetPassWord", fm_data, success);
    });
}