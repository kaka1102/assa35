
function formatNumber(nStr, decSeperate, groupSeperate) {
    nStr += '';
    x = nStr.split(decSeperate);
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + groupSeperate + '$2');
    }
    return x1 + x2;
}
// dialog thông báo khi xóa(gửi id)
function DialogConfirmDelete(id_xoa, id_table, linkAshx, idButton_Dell, noidung) {
    $('.' + idButton_Dell).attr('disabled', 'true');

    swal({
        title: 'Thông báo xóa',
        text: "Bạn có chắc sẽ xóa " + noidung + " này không ?",
        type: 'warning',
        showCancelButton: true,
        confirmButtonColor: '#DD6B55',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Vâng, tôi đồng ý !',
        cancelButtonText: 'Không. cảm ơn!'
    }, function checkAccept(kq) {
        if (kq) {
            fun_Delete(id_xoa, id_table, linkAshx, idButton_Dell)
        }
        $('.' + idButton_Dell).removeAttr('disabled');
    });
}
// function delete (gửi id)
function fun_Delete(id_xoa, id_table, linkAshx, idButton_Dell) {
    var statusLoad = loadingform(document.body);
    //$.getJSON(linkAshx, { id_xoa: id_xoa }, function (kq) {
    //    console.log(kq);
    //    if (kq.success == true) {
    //        swal('Thông báo ', kq.msg, 'success')
    //    } else {
    //        swal('Thông báo ', kq.msg, 'error')
    //    }
    //    $('.' + idButton_Dell).removeAttr('disabled');
    //    id_table.fnDraw();
    //    statusLoad.hide();
    //});

    $.ajax({
        type: "POST",
        url: linkAshx,
        data: id_xoa,
        contentType: false,
        processData: false,
        success: function (kq) {
            var data = JSON.parse(kq);
            if (data.success) {
                swal('Thông báo ', data.msg, 'success')
            } else {
                swal('Thông báo ', data.msg, 'error')
            }
            $('.' + idButton_Dell).removeAttr('disabled');
            id_table.fnDraw();
            statusLoad.hide();
        }
    });

}



// dialog thông báo khi update
function DialogConfirmUpdate(id_table, linkAshx, fm_data, idButton_Update) {
    idButton_Update.setAttribute('disabled', 'true');
    swal({
        title: 'Thông báo cập nhật',
        text: "Bạn có chắc sẽ cập nhật nội dung như trên không ?",
        type: 'question',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Vâng, tôi đồng ý !',
        cancelButtonText: 'Không. cảm ơn!'
    }).then(function checkAccept(kq) {
        if (kq) {
            fun_Update(id_table, linkAshx, fm_data, idButton_Update)
        }
    }, function dismiss(result) {
        if (result === 'cancel') {
            swal(
              'Hủy bỏ ',
              'Lệnh cập nhật đã bị hủy bỏ ',
              'info'
            )
        }
        id_table.fnDraw();
        idButton_Update.removeAttribute('disabled');
    });
}

// function update
function fun_Update(id_table, linkAshx, fm_data, idButton_Update) {
    var statusLoad = loadingform(document.body);
    $.ajax({
        type: "POST",
        url: linkAshx,
        data: fm_data,
        contentType: false,
        processData: false,
        success: function (kq) {
            var data = JSON.parse(kq);
            if (data.success) {
                swal('Thông báo ', data.msg, 'success')
            } else {
                swal('Thông báo ', data.msg, 'error')
            }
            idButton_Update.removeAttribute('disabled');
            id_table.fnDraw();
            statusLoad.hide();
        }
    });

}

// dialog thông báo khi them moi
function DialogConfirmAddNew(id_table, linkAshx, fm_data, idButton_AddNew, idBoxForm) {
    idButton_AddNew.setAttribute('disabled', 'true');
    swal({
        title: 'Thông báo thêm mới',
        text: "Bạn có chắc sẽ thêm mới nội dung như trên không ?",
        type: 'question',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Vâng, tôi đồng ý !',
        cancelButtonText: 'Không. cảm ơn!'
    }).then(function checkAccept(kq) {
        if (kq) {
            fun_AddNew(id_table, linkAshx, fm_data, idButton_AddNew, idBoxForm)
        }
    }, function dismiss(result) {
        if (result === 'cancel') {
            swal(
              'Hủy bỏ ',
              'Lệnh thêm mới đã bị hủy bỏ ',
              'info'
            )
        }
        idButton_AddNew.removeAttribute('disabled');
    });
}
// function add new 
function fun_AddNew(id_table, linkAshx, fm_data, idButton_AddNew, idBoxForm) {
    var statusLoad = loadingform(document.body);
    $.ajax({
        type: "POST",
        url: linkAshx,
        data: fm_data,
        contentType: false,
        processData: false,
        success: function (kq) {
            var data = JSON.parse(kq);
            if (data.success) {
                QTW_JQUERY.resetData(idBoxForm);
                swal('Thông báo ', data.msg, 'success')
            } else {
                swal('Thông báo ', data.msg, 'error')
            }
            idButton_AddNew.removeAttribute('disabled');
            id_table.fnDraw();
            statusLoad.hide();
        }
    });
}


// dialog thông báo khi thao tác lệnh gì đó 
function DialogConfirmSendData(id_table, linkAshx, fm_data, idButton_Send, noidung) {
    idButton_Send.setAttribute('disabled', 'true');
    swal({
        title: 'Thông báo',
        text: 'Bạn có chắc sẽ ' + noidung + ' không ?',
        type: 'question',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Vâng, tôi đồng ý !',
        cancelButtonText: 'Không. cảm ơn!'
    }).then(function checkAccept(kq) {
        if (kq) {
            fun_Send(id_table, linkAshx, fm_data, idButton_Send)
            modal.close();
        }
    }, function dismiss(result) {
        if (result === 'cancel') {
            swal(
              'Hủy bỏ ',
              'Lệnh ' + noidung + ' đã bị hủy bỏ ',
              'info'
            )
        }
        idButton_Send.removeAttribute('disabled');
    });
}
// function send
function fun_Send(id_table, linkAshx, fm_data, idButton_Send) {
    var statusLoad = loadingform(document.body);
    $.ajax({
        type: "POST",
        url: linkAshx,
        data: fm_data,
        contentType: false,
        processData: false,
        success: function (kq) {
            var data = JSON.parse(kq);
            if (data.success) {
                swal('Thông báo ', data.msg, 'success')
                setTimeout(setboxtransform(0), 5000);
            } else {
                swal('Thông báo ', data.msg, 'error')
            }
            idButton_Send.removeAttribute('disabled');
            id_table.fnDraw();
            statusLoad.hide();
        }
    });

}


// dialog thông báo khi xóa(gửi id)
function DialogConfirmDeleteCallBack(id_xoa, id_table, linkAshx, typeAshx, idButton_Dell, noidung, fnFunction) {
    $('.' + idButton_Dell).attr('disabled', 'true');
    swal({
        title: 'Thông báo xóa',
        text: "Bạn có chắc sẽ xóa " + noidung + " này không ?",
        type: 'question',
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: 'Vâng, tôi đồng ý !',
        cancelButtonText: 'Không. cảm ơn!'
    }).then(function checkAccept(kq) {
        if (kq) {
            fun_DeleteCallBack(id_xoa, id_table, linkAshx, typeAshx, idButton_Dell, fnFunction)
        }
    }, function dismiss(result) {
        if (result === 'cancel') {
            swal(
              'Hủy bỏ ',
              'Lệnh xóa đã bị hủy bỏ ',
              'info'
            )
        }
        $('.' + idButton_Dell).removeAttr('disabled');
    });
}
// function delete (gửi id)
function fun_DeleteCallBack(id_xoa, id_table, linkAshx, typeAshx, idButton_Dell, fnFunction) {
    var statusLoad = loadingform(document.body);
    $.getJSON(linkAshx, { type: typeAshx, id_xoa: id_xoa }, function (kq) {
        if (kq.success == true) {
            swal('Thông báo ', kq.msg, 'success')
        } else {
            swal('Thông báo ', kq.msg, 'error')
        }
        $('.' + idButton_Dell).removeAttr('disabled');

        id_table.fnDraw();
        statusLoad.hide();
        fnFunction(kq);
    });

}

