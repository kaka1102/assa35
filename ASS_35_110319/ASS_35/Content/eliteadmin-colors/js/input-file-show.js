
function previewImages(file) {
    var span = document.createElement("span");
    var link = document.createElement("a");
    var preview = document.createElement("img");
    var button = document.createElement("button")
    var close = document.createElement("i");

    span.appendChild(button);
    span.appendChild(preview);
    button.appendChild(close);

    var reader = new FileReader();

    reader.addEventListener("load", function () {

        span.setAttribute("style", "height: 100%;float: left;position: relative;")

        button.setAttribute("type", "button");
        button.setAttribute("type", "button");
        button.setAttribute("aria-label", "Xóa");
        button.setAttribute("class", "btn btn-danger btn-circle hint--top btnRemoveItems");
        button.setAttribute("style", "position: absolute;top: 0px;right: 0px;display: none;");

        close.setAttribute("class", "fa fa-times");

        preview.setAttribute("title", file.name);
        preview.setAttribute("src", reader.result);
        preview.setAttribute("class", "all studio isotope-item");
        preview.setAttribute("data-toggle", "lightbox");
        preview.setAttribute("data-gallery", "multiimages");
        preview.setAttribute("href", reader.result);

        $('#gallery-content-center').append(span);

        button.addEventListener("click", function () {
            var total = $('#gallery-content-center').find('span');
            var total = parseInt(total.length) - 1;
            $('#frmInput').empty();
            $('#frmInput').append('<i class="glyphicon glyphicon-file"></i><span style="margin-left: 10px;">' + total + ' File được chọn</span>');
            $(this).parent().remove();
        })
    }, false);

    if (file) {
        reader.readAsDataURL(file);
    }
}

function previewImagesAvatarPost(file) {
    var span = document.createElement("span");
    var link = document.createElement("a");
    var preview = document.createElement("img");
    var button = document.createElement("button")
    var close = document.createElement("i");

    span.appendChild(button);
    span.appendChild(preview);
    button.appendChild(close);

    var reader = new FileReader();

    reader.addEventListener("load", function () {

        span.setAttribute("style", "height: 100%;float: left;position: relative;")

        button.setAttribute("type", "button");
        button.setAttribute("type", "button");
        button.setAttribute("aria-label", "Xóa");
        button.setAttribute("class", "btn btn-danger btn-circle hint--top btnRemoveItems");
        button.setAttribute("style", "position: absolute;top: 0px;right: 0px;display: none;");

        close.setAttribute("class", "fa fa-times");

        preview.setAttribute("title", file.name);
        preview.setAttribute("src", reader.result);
        preview.setAttribute("class", "all studio isotope-item");
        preview.setAttribute("data-toggle", "lightbox");
        preview.setAttribute("data-gallery", "multiimages");
        preview.setAttribute("href", reader.result);

        $('#gallery-content-center').append(span);

        button.addEventListener("click", function () {
            var total = $('#gallery-content-center').find('span');
            var total = parseInt(total.length) - 1;
            $('#frmInput').empty();
            $('#frmInput').append('<i class="glyphicon glyphicon-file"></i><span style="margin-left: 10px;">' + total + ' File được chọn</span>');
            $('#gallery-content-center').empty();
        })
    }, false);

    if (file) {
        reader.readAsDataURL(file);
    }
}

function previewVideo(file, id) {
    var URL = window.URL || window.webkitURL;
    var video = document.getElementById(id);

    var url = URL.createObjectURL(file);
    video.src = url;
    video.load();

    video.onloadeddata = function () {
        video.pause();
    }
}

function previewFile(file) {

    var icon = "";
    if (file.type == "application/pdf") {
        icon = "fa fa-file-pdf-o";
    }
    else {
        icon = "fa fa-file-o";
    }

    $('#view-item-file').append("<li style='position: relative;'>\
                                        <div class='bg-purple'>\
                                             <i class='"+ icon + " text-white'></i>\
                                        </div>" + file.name + "\
                                        <span style='padding-right:50px' class='text-muted'>" + file.size + " kb</span>\
                                        <button type='button' aria-label='Xóa' class='btn btn-danger btn-circle hint--top btnRemoveItems' style='position: absolute;top: 15px;right: 15px;display: none;'><i class='fa fa-times'></i></button>\
                                 </li>");

    $('.btnRemoveItems').unbind();
    $('.btnRemoveItems').click(function () {
        var total = $('#view-item-file').find('li');
        var total = parseInt(total.length) - 1;

        $('#frmInput').empty();
        $('#frmInput').append('<i class="glyphicon glyphicon-file"></i><span style="margin-left: 10px;">' + total + ' File được chọn</span>');
        $(this).parent().remove();
    })
}

function dataURLtoFile(dataurl, filename) {
    var arr = dataurl.split(','), mime = arr[0].match(/:(.*?);/)[1],
        bstr = atob(arr[1]), n = bstr.length, u8arr = new Uint8Array(n);
    while (n--) {
        u8arr[n] = bstr.charCodeAt(n);
    }
    return new File([u8arr], filename, { type: mime });
}




// lay phan nay.....
$(document).ready(function () {

    // upload nhieu anh va luu luon vao db
    $('#btnInput').change(function (e) {
        var files = e.target.files === undefined ? (e.target && e.target.value ? [{ name: e.target.value.replace(/^.+\\/, '') }] : []) : e.target.files

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
            var total = $('#gallery-content-center').find('span');
            
            var total = parseInt(total.length);
            $('#frmInput').empty();
            $('#frmInput').append('<i class="glyphicon glyphicon-file"></i><span style="margin-left: 10px;">' + total + ReturnLanguageJs(Selectedfile, SelectedfileEn) + '</span>');
            $('#btnInput').val('');
        }

        QTW_JQUERY.RUN_AJAX("ajaxPOST", "/Media/_AddImages", fm_data, success);

    });
  
    $('#btnInputAvatarPost').change(function (e) {
     
        var item = document.getElementById("item-append-error");
        var img = $('#gallery-content-center').find('img');

        var parent = QTW_VALIDATION.common.removeValidation(item);
        $('#gallery-content-center').empty();

        var files = e.target.files === undefined ? (e.target && e.target.value ? [{ name: e.target.value.replace(/^.+\\/, '') }] : []) : e.target.files
        $('#frmInput').empty();
        $('#frmInput').append('<i class="glyphicon glyphicon-file"></i><span style="margin-left: 10px;">' + files.length + ReturnLanguageJs(Selectedfile, SelectedfileEn) + '</span>');

        $.each(files, function (i, file) {
            if (file.type.indexOf("image/") == 0) {
                previewImagesAvatarPost(file);

                var fileUpload, imgUpload;
                if ((fileUpload = file)) {
                    imgUpload = new Image();
                    imgUpload.onload = function () {
                        $('#gallery-content-center').append('<span style="padding-left: 20px;color: blue;font-weight: initial;">width: ' + this.width + 'px, height: ' + this.height + 'px</span>');
                    };
                    imgUpload.src = _URLx.createObjectURL(fileUpload);
                }
            }
        });
    });





    // upload 1 anh ko luu db
    $('#btnInputSingle').change(function (e) {
        var item = document.getElementById("item-append-error");
        var img = $('#gallery-content-center').find('img');

        var parent = QTW_VALIDATION.common.removeValidation(item);
        $('#gallery-content-center').empty();

        var files = e.target.files === undefined ? (e.target && e.target.value ? [{ name: e.target.value.replace(/^.+\\/, '') }] : []) : e.target.files
        $('#frmInput').empty();
        $('#frmInput').append('<i class="glyphicon glyphicon-file"></i><span style="margin-left: 10px;">' + files.length + ReturnLanguageJs(Selectedfile, SelectedfileEn) + '</span>');

        $.each(files, function (i, file) {
            if (file.type.indexOf("image/") == 0) {
                previewImages(file);
            }
        });
    });

    $('#btnInputVideo').change(function (e) {

        $('#view-item-video').empty();
        var files = e.target.files === undefined ? (e.target && e.target.value ? [{ name: e.target.value.replace(/^.+\\/, '') }] : []) : e.target.files
        $('#frmInput').empty();
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
        QTW_JQUERY.RUN_AJAX("ajaxPOST", "/Media/_AddVideo", fm_data, success);

    });

    $('#btnInputVideo').click(function (e) {

        $('#frmInput').empty();
        $('#frmInput').append('<i class="glyphicon glyphicon-file"></i><span style="margin-left: 10px;">0 ' + ReturnLanguageJs(Selectedfile, SelectedfileEn) + '</span>');
        var idItem = $('#view-item-video').find('video').attr("data-id");

        var dataObject = {
            id: idItem,
        };
        var fm_data = new FormData();
        fm_data.append("DataSend", JSON.stringify(dataObject));
        var success = function (dx) {
            if (dx.IsSuccess) {
                $('#view-item-video').empty();
            }
        }
        QTW_JQUERY.RUN_AJAX("ajaxPOST", "/Media/_DeleteFile", fm_data, success);
    });



    $('#btnInputFile').change(function (e) {

        var files = e.target.files === undefined ? (e.target && e.target.value ? [{ name: e.target.value.replace(/^.+\\/, '') }] : []) : e.target.files

        var fm_data = new FormData();
        for (var i = 0; i < files.length; i++) {
            fm_data.append("file", files[i]);
        }
        var success = function (dx) {
            if (dx.data != null) {
                $.each(dx.data, function (i, file) {
                    ShowFile(file);
                });
            }
            var total = $('#view-item-file').find('li');
            var total = parseInt(total.length);
            $('#frmInput').empty();
            $('#frmInput').append('<i class="glyphicon glyphicon-file"></i><span style="margin-left: 10px;">' + total + " file được chọn" + '</span>');
            $('#btnInputFile').val('');
        }

        QTW_JQUERY.RUN_AJAX("ajaxPOST", "/Media/_AddDocument", fm_data, success);
    });

    $('#btnInputFile1').change(function (e) {

        var files = e.target.files === undefined ? (e.target && e.target.value ? [{ name: e.target.value.replace(/^.+\\/, '') }] : []) : e.target.files

        var fm_data = new FormData();
        for (var i = 0; i < files.length; i++) {
            fm_data.append("file", files[i]);
        }
        var success = function (dx) {
            if (dx.data != null) {
                $.each(dx.data, function (i, file) {
                    ShowFile1(file);
                });
            }
            var total = $('#view-item-file1').find('li');
            var total = parseInt(total.length);
            $('#frmInput1').empty();
            $('#frmInput1').append('<i class="glyphicon glyphicon-file"></i><span style="margin-left: 10px;">' + total + ReturnLanguageJs(Selectedfile, SelectedfileEn) + '</span>');
            $('#btnInputFile1').val('');
        }

        QTW_JQUERY.RUN_AJAX("ajaxPOST", "/Media/_AddDocument", fm_data, success);
    });

    $('#btnInputFile2').change(function (e) {

        var files = e.target.files === undefined ? (e.target && e.target.value ? [{ name: e.target.value.replace(/^.+\\/, '') }] : []) : e.target.files

        var fm_data = new FormData();
        for (var i = 0; i < files.length; i++) {
            fm_data.append("file", files[i]);
        }
        var success = function (dx) {
            if (dx.data != null) {
                $.each(dx.data, function (i, file) {
                    ShowFile2(file);
                });
            }
            var total = $('#view-item-file2').find('li');
            var total = parseInt(total.length);
            $('#frmInput2').empty();
            $('#frmInput2').append('<i class="glyphicon glyphicon-file"></i><span style="margin-left: 10px;">' + total + ReturnLanguageJs(Selectedfile, SelectedfileEn) + '</span>');
            $('#btnInputFile2').val('');
        }

        QTW_JQUERY.RUN_AJAX("ajaxPOST", "/Media/_AddDocument", fm_data, success);
    });
 
});

function ShowImages(file) {

    var span = document.createElement("span");
    var link = document.createElement("a");
    var preview = document.createElement("img");
    var input = document.createElement("input");
    var button = document.createElement("button")
    var close = document.createElement("i");

    span.appendChild(button);
    span.appendChild(preview);
    span.appendChild(input);
    button.appendChild(close);

    span.setAttribute("style", "height: 100%;float: left;position: relative;")

    button.setAttribute("type", "button");
    button.setAttribute("type", "button");
    button.setAttribute("aria-label", ReturnLanguageJs(Delete, DeleteEn));
    button.setAttribute("class", "btn btn-danger btn-circle hint--top btnRemoveItems");
    button.setAttribute("style", "position: absolute;top: 0px;right: 0px;display: none;");
    button.setAttribute("data-id", file.id);


    close.setAttribute("class", "fa fa-times");

    preview.setAttribute("title", file.tenfile);
    preview.setAttribute("src", file.duongdanfile);
    preview.setAttribute("class", "all studio isotope-item");
    preview.setAttribute("data-toggle", "lightbox");
    preview.setAttribute("data-gallery", "multiimages");
    preview.setAttribute("href", file.duongdanfile);

    input.setAttribute("type", "text");
    input.setAttribute("name", "caption");
    input.setAttribute("class", "captionImage");
    input.setAttribute("placeholder", "Trích dẫn");
    input.setAttribute("data-id", file.id);

    $('#gallery-content-center').append(span);

    button.addEventListener("click", function () {
        var total = $('#gallery-content-center').find('span');
        
        var total = parseInt(total.length) - 1;
        $('#frmInput').empty();
        $('#frmInput').append('<i class="glyphicon glyphicon-file"></i><span style="margin-left: 10px;">' + total + ReturnLanguageJs(Selectedfile, SelectedfileEn) + '</span>');

        var idItem = $(this);

        var dataObject = {
            id: $(this).attr("data-id"),
            
        };
        var fm_data = new FormData();
        fm_data.append("DataSend", JSON.stringify(dataObject));
        var success = function (dx) {
            if (dx.IsSuccess) {
                idItem.parent().remove();
            }
        }
        QTW_JQUERY.RUN_AJAX("ajaxPOST", "/Media/_DeleteFile", fm_data, success);

    });
}
function ShowVideo(file) {
    var html = "<div class='col-lg-6 col-md-6 col-sm-6 col-xs-6'>\
                                    <div class='el-card-item'>\
                                        <div class='el-card-avatar el-overlay-1' id='item-video'>\
                                            <video data-id='" + file.id + "' src='" + file.duongdanfile + "' style='width:600px;max-width:100%;' controls='' autobuffer >\
                                            </video>\
                                        </div>\
                                        <div class='alb-info'>\
                                             <h5>" + file.tenfile + "</h5>\
                                        </div>\
                                    </div>\
                                </div>";
    $('#view-item-video').append(html);
}
function ShowFile(file) {

    var icon = "";
    if (file.mimetype == "application/pdf") {
        icon = "fa fa-file-pdf-o";
    }
    else {
        icon = "fa file-powerpoint";
    }
    
    $('#view-item-file').append("<li  style='position: relative;'>\
                                       <div style='width: 90%;' data-toggle='modal' data-target='.bs-example-modal-lg'  data-url='" + file.duongdanfile + "' data-title='" + file.tenfile + "' class='li-item-file li-file model_img img-responsive'>\
                                            <div class='bg-purple' style='width: 40px;float: left;'>\
                                                 <i class='"+ icon + " text-white'></i>\
                                            </div>\
                                            <div style='float: left;padding: 10px;width: 70%;'>\
                                                <span style='float:left'>" + file.tenfile + "</span>\
                                            </div>\
                                            <div style='float: right;padding: 10px;width: 20%;'>\
                                               <span style='padding-right:50px' class='text-muted'>" + file.size + " bytes</span>\
                                            </div>\
                                       </div>\
                                       <button type='button' data-id='" + file.id + "' aria-label='" + ReturnLanguageJs(Delete, DeleteEn) + "' class='btn btn-danger btn-circle hint--top btnRemoveItems' style='position: absolute;top: 15px;right: 15px;display: none;'><i class='fa fa-times'></i></button>\
                                 </li>");

    $('.btnRemoveItems').unbind();
    $('.btnRemoveItems').click(function () {
        var total = $('#view-item-file').find('li');
        var total = parseInt(total.length) - 1;
        $('#frmInput').empty();
        $('#frmInput').append('<i class="glyphicon glyphicon-file"></i><span style="margin-left: 10px;">' + total + ReturnLanguageJs(Selectedfile, SelectedfileEn) + '</span>');

        var idItem = $(this);
        var dataObject = {
            id: $(this).attr("data-id"),
        };
        var fm_data = new FormData();
        fm_data.append("DataSend", JSON.stringify(dataObject));
        var success = function (dx) {
            if (dx.IsSuccess) {
                idItem.parent().remove();
            }
        }
        QTW_JQUERY.RUN_AJAX("ajaxPOST", "/Media/_DeleteFile", fm_data, success);
    });

    $('.li-item-file').unbind();
    $('.li-item-file').click(function () {
        var name = $(this).attr('data-title');
        var url = $(this).attr('data-url');
        $('.modal-body').empty();
        $('#myLargeModalLabel').text('');
        $('#myLargeModalLabel').text(name);
        $('.modal-body').append("<embed src='/Media/ReadFilePDF?url=" + url + "' style='min-height: 500px; width: 100%;' />");
    });
}

function ShowFile1(file) {

    var icon = "";
    if (file.mimetype == "application/pdf") {
        icon = "fa fa-file-pdf-o";
    }
    else {
        icon = "fa fa-file-o";
    }
    $('#view-item-file1').append("<li  style='position: relative;'>\
                                       <div style='width: 90%;' data-toggle='modal' data-target='.bs-example-modal-lg'  data-url='" + file.duongdanfile + "' data-title='" + file.tenfile + "' class='li-item-file li-file model_img img-responsive'>\
                                            <div class='bg-purple' style='width: 40px;float: left;'>\
                                                 <i class='"+ icon + " text-white'></i>\
                                            </div>\
                                            <div style='float: left;padding: 10px;width: 70%;'>\
                                                <span style='float:left'>" + file.tenfile + "</span>\
                                            </div>\
                                            <div style='float: right;padding: 10px;width: 20%;'>\
                                               <span style='padding-right:50px' class='text-muted'>" + file.size + " bytes</span>\
                                            </div>\
                                       </div>\
                                       <button type='button' data-id='" + file.id + "' aria-label='" + ReturnLanguageJs(Delete, DeleteEn) + "' class='btn btn-danger btn-circle hint--top btnRemoveItems' style='position: absolute;top: 15px;right: 15px;display: none;'><i class='fa fa-times'></i></button>\
                                 </li>");

    $('.btnRemoveItems').unbind();
    $('.btnRemoveItems').click(function () {
        var total = $('#view-item-file1').find('li');
        var total = parseInt(total.length) - 1;
        $('#frmInput1').empty();
        $('#frmInput1').append('<i class="glyphicon glyphicon-file"></i><span style="margin-left: 10px;">' + total + ReturnLanguageJs(Selectedfile, SelectedfileEn) + '</span>');

        var idItem = $(this);
        var dataObject = {
            id: $(this).attr("data-id"),
        };
        var fm_data = new FormData();
        fm_data.append("DataSend", JSON.stringify(dataObject));
        var success = function (dx) {
            if (dx.IsSuccess) {
                idItem.parent().remove();
            }
        }
        QTW_JQUERY.RUN_AJAX("ajaxPOST", "/Media/_DeleteFile", fm_data, success);
    });

    $('.li-item-file').unbind();
    $('.li-item-file').click(function () {
        var name = $(this).attr('data-title');
        var url = $(this).attr('data-url');
        $('.modal-body').empty();
        $('#myLargeModalLabel').text('');
        $('#myLargeModalLabel').text(name);
        $('.modal-body').append("<embed src='/Media/ReadFilePDF?url=" + url + "' style='min-height: 500px; width: 100%;' />");
    });
}


function ShowFile2(file) {

    var icon = "";
    if (file.mimetype == "application/pdf") {
        icon = "fa fa-file-pdf-o";
    }
    else {
        icon = "fa fa-file-o";
    }
    $('#view-item-file2').append("<li  style='position: relative;'>\
                                       <div style='width: 90%;' data-toggle='modal' data-target='.bs-example-modal-lg'  data-url='" + file.duongdanfile + "' data-title='" + file.tenfile + "' class='li-item-file li-file model_img img-responsive'>\
                                            <div class='bg-purple' style='width: 40px;float: left;'>\
                                                 <i class='"+ icon + " text-white'></i>\
                                            </div>\
                                            <div style='float: left;padding: 10px;width: 70%;'>\
                                                <span style='float:left'>" + file.tenfile + "</span>\
                                            </div>\
                                            <div style='float: right;padding: 10px;width: 20%;'>\
                                               <span style='padding-right:50px' class='text-muted'>" + file.size + " bytes</span>\
                                            </div>\
                                       </div>\
                                       <button type='button' data-id='" + file.id + "' aria-label='" + ReturnLanguageJs(Delete, DeleteEn) + "' class='btn btn-danger btn-circle hint--top btnRemoveItems' style='position: absolute;top: 15px;right: 15px;display: none;'><i class='fa fa-times'></i></button>\
                                 </li>");

    $('.btnRemoveItems').unbind();
    $('.btnRemoveItems').click(function () {
        var total = $('#view-item-file2').find('li');
        var total = parseInt(total.length) - 1;
        $('#frmInput2').empty();
        $('#frmInput2').append('<i class="glyphicon glyphicon-file"></i><span style="margin-left: 10px;">' + total + ReturnLanguageJs(Selectedfile, SelectedfileEn) + '</span>');

        var idItem = $(this);
        var dataObject = {
            id: $(this).attr("data-id"),
        };
        var fm_data = new FormData();
        fm_data.append("DataSend", JSON.stringify(dataObject));
        var success = function (dx) {
            if (dx.IsSuccess) {
                idItem.parent().remove();
            }
        }
        QTW_JQUERY.RUN_AJAX("ajaxPOST", "/Media/_DeleteFile", fm_data, success);
    });

    $('.li-item-file').unbind();
    $('.li-item-file').click(function () {
        var name = $(this).attr('data-title');
        var url = $(this).attr('data-url');
        $('.modal-body').empty();
        $('#myLargeModalLabel').text('');
        $('#myLargeModalLabel').text(name);
        $('.modal-body').append("<embed src='/Media/ReadFilePDF?url=" + url + "' style='min-height: 500px; width: 100%;' />");
    });
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

function validateInputFileuploadImagesMenuPost() {
    var ischeck = false;
    var item = document.getElementById("item-append-error");
    var img = $('#gallery-content-center').find('img');



    var parent = QTW_VALIDATION.common.removeValidation(item);

    if (parent != null) {
        if (img.length != "0") {
            var width = img[0].naturalWidth;
            var height = img[0].naturalHeight;

            if (width == 1140 && height ==392) {
                ischeck = true;
            } else {
                QTW_VALIDATION.common.appendError(ReturnLanguageJs(IncorectorNotChoise, IncorectorNotChoiseEn), parent);
                ischeck = false;
            }
        } else {
            QTW_VALIDATION.common.appendError(ReturnLanguageJs(IncorectorNotChoise, IncorectorNotChoiseEn), parent);
            ischeck = false;
        }
    }
    return ischeck;
}

function validateInputFileDiaDiem() {
    var ischeck = false;
    var item = document.getElementById("item-append-error");
    var img = $('#gallery-content-center').find('img');



    var parent = QTW_VALIDATION.common.removeValidation(item);

    if (parent != null) {
        if (img.length != "0") {
            var width = img[0].naturalWidth;
            var height = img[0].naturalHeight;

            if (width > 1140 && height >= 690) {
                ischeck = true;
            } else {
                QTW_VALIDATION.common.appendError(ReturnLanguageJs(IncorectorNotChoise, IncorectorNotChoiseEn), parent);
                ischeck = false;
            }
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

function validateInputFileuploadDocument() {
    var ischeck = false;
    var item = document.getElementById("item-append-error");
    var img = $('#view-item-file').find('button');

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

