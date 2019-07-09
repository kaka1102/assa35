
$(document).ready(function () {
    var url = location.href;
    var imgroot = url.match(/imgroot=([^&]*)/) ? url.match(/imgroot=([^&]*)/)[1] : null;
    var CKEditorFuncNum = url.match(/CKEditorFuncNum=([0-9]+)/) ? url.match(/CKEditorFuncNum=([0-9]+)/)[1] : null;

    $('#btnInputImgCK').change(function (e) {
        $('#gallery-content-center').empty();

        var files = e.target.files === undefined ? (e.target && e.target.value ? [{ name: e.target.value.replace(/^.+\\/, '') }] : []) : e.target.files
        $('#frmInput').empty();
        // $('#frmInput').append('<i class="glyphicon glyphicon-file"></i><span style="margin-left: 10px;">' + files.length + ' File được chọn</span>');
        $('#frmInput').append('<i class="glyphicon glyphicon-file"></i><span style="margin-left: 10px;">' + files.length + ReturnLanguageJs(Selectedfile, SelectedfileEn) + '</span>');

        var fm_data = new FormData();
        for (var i = 0; i < files.length; i++) {
            fm_data.append("file", files[i]);
        }
        var success = function (dx) {

            if (dx.data != null) {
                $.each(dx.data, function (i, file) {
                    ShowImages(file);
                });
            }
        }

        QTW_JQUERY.RUN_AJAX("ajaxPOST", "/Media/_AddImagesCKEditor", fm_data, success);

    });
    $('#btnSave').click(function () {
        var lstFile = [];
        var button = $('#gallery-content-center').find('img');
        $.each(button, function (i, v) {
            //  lstFile.push($(this).attr("src"));
            var src = $(this).attr("src");
            window.opener.CKEDITOR.tools.callFunction(CKEditorFuncNum, src);
        });
        window.close();
    });
   
    $('#btnInputVideoCK').change(function (e) {

        $('#view-item-video').empty();
        var files = e.target.files === undefined ? (e.target && e.target.value ? [{ name: e.target.value.replace(/^.+\\/, '') }] : []) : e.target.files
        $('#frmInput').empty();
     //   $('#frmInput').append('<i class="glyphicon glyphicon-file"></i><span style="margin-left: 10px;">' + files.length + ' File được chọn</span>');
        $('#frmInput').append('<i class="glyphicon glyphicon-file"></i><span style="margin-left: 10px;">' + files.length + ReturnLanguageJs(Selectedfile, SelectedfileEn) + '</span>');

        var fm_data = new FormData();
        for (var i = 0; i < files.length; i++) {
            fm_data.append("file", files[i]);
        }
        var success = function (dx) {
            if (dx.data != null) {
                $.each(dx.data, function (i, file) {
                    ShowVideo(file);
                });
            }
        }
        QTW_JQUERY.RUN_AJAX("ajaxPOST", "/Media/_AddVideoCKEditor", fm_data, success);

    });

    $('#btnInputVideoCK').click(function (e) {

        $('#frmInput').empty();
        //  $('#frmInput').append('<i class="glyphicon glyphicon-file"></i><span style="margin-left: 10px;">0 File được chọn</span>');
        $('#frmInput').append('<i class="glyphicon glyphicon-file"></i><span style="margin-left: 10px;">0 ' + ReturnLanguageJs(Selectedfile, SelectedfileEn) + '</span>');
        $('#view-item-video').empty();

        //var idItem = $('#view-item-video').find('video').attr("data-id");

        //var dataObject = {
        //    id: idItem,
        //};
        //var fm_data = new FormData();
        //fm_data.append("DataSend", JSON.stringify(dataObject));
        //var success = function (dx) {
        //    if (dx.IsSuccess) {
        //        $('#view-item-video').empty();
        //    }
        //}
        //QTW_JQUERY.RUN_AJAX("ajaxPOST", "/Media/_DeleteFile", fm_data, success);
    });

    $('#btnSavevideo').click(function () {
        var lstFile = [];
        var button = $('#view-item-video').find('video');
        $.each(button, function (i, v) {
            var src = $(this).attr("src");
            window.opener.CKEDITOR.tools.callFunction(CKEditorFuncNum, src);
        });
        window.close();
    });
});

function ShowImages(file) {

    var span = document.createElement("span");
    var link = document.createElement("a");
    var preview = document.createElement("img");
    var button = document.createElement("button")
    var close = document.createElement("i");

    span.appendChild(button);
    span.appendChild(preview);
    button.appendChild(close);

    span.setAttribute("style", "height: 100%;float: left;position: relative;")

    button.setAttribute("type", "button");
    button.setAttribute("type", "button");
    button.setAttribute("aria-label", ReturnLanguageJs(Delete, DeleteEn));
    button.setAttribute("class", "btn btn-danger btn-circle hint--top btnRemoveItems");
    button.setAttribute("style", "position: absolute;top: 0px;right: 0px;display: none;");
    // button.setAttribute("data-id", file.id);


    close.setAttribute("class", "fa fa-times");

    //  preview.setAttribute("title", file.tenfile);
    preview.setAttribute("src", file);
    preview.setAttribute("class", "all studio isotope-item");
    preview.setAttribute("data-toggle", "lightbox");
    preview.setAttribute("data-gallery", "multiimages");
    preview.setAttribute("href", file);


    $('#gallery-content-center').append(span);

    button.addEventListener("click", function () {
        var total = $('#gallery-content-center').find('span');
        var total = parseInt(total.length) - 1;
        $('#frmInput').empty();
        $('#frmInput').append('<i class="glyphicon glyphicon-file"></i><span style="margin-left: 10px;">' + total + ReturnLanguageJs(Selectedfile, SelectedfileEn)+'</span>');

        var idItem = $(this);
        idItem.parent().remove();
    });
}
function ShowVideo(file) {
    var html = "<div class='col-lg-6 col-md-6 col-sm-6 col-xs-6'>\
                                    <div class='el-card-item'>\
                                        <div class='el-card-avatar el-overlay-1' id='item-video'>\
                                            <video  src='" + file + "' style='width:600px;max-width:100%;' controls='' autobuffer >\
                                            </video>\
                                        </div>\
                                    </div>\
                                </div>";
    $('#view-item-video').append(html);
}
function validateInputFileuploadImages() {
    var ischeck = false;
    var item = document.getElementById("item-append-error");
    var img = $('#gallery-content-center').find('img');

    var parent = QTW_VALIDATION.common.removeValidation(item);
    if (parent != null) {
        if (img.length != "0") {
            ischeck = true;
        } else {
            QTW_VALIDATION.common.appendError(ReturnLanguageJs(IncorectorNotChoise, IncorectorNotChoiseEn), parent);
            ischeck = false;
        }
    }
    return ischeck;
}


function validateInputFileuploadVideo() {
    var ischeck = false;
    var item = document.getElementById("item-append-error");
    var img = $('#view-item-video').find('video');

    var parent = QTW_VALIDATION.common.removeValidation(item);
    if (parent != null) {
        if (img.length != "0") {
            ischeck = true;
        } else {
            QTW_VALIDATION.common.appendError(ReturnLanguageJs(IncorectorNotChoise, IncorectorNotChoiseEn), parent);
            ischeck = false;
        }
    }
    return ischeck;
}