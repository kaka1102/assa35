function CallAjaxClient(url, dataPost) {
    $.ajax({
        type: "POST",
        url: url,
        data: dataPost,
        processData: false,
        contentType: false,
        success: function (response, textStatus, xhr) {
            console.log("ok");
        },
        error: function (xhr, textStatus, errorThrown) {
            console.log("error");
        }
    });
}
function getHashUrlVars() {
    var vars = [], hash;
    var hashes = window.location.href.slice(window.location.href.indexOf('#') + 1).split('&');
    var curPage = window.location.href.slice(0, window.location.href.indexOf('#'));
    vars.push(curPage);
    for (var i = 0; i < hashes.length; i++) {
        hash = hashes[i].split('=');
        vars.push(hash[0]);
        vars[hash[0]] = hash[1];
    }
    return vars;
}