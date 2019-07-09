
var modal = (function () {
    var
    method = {},
    $overlay,
    $modal,
    $content,
    $ct_titlt,
    $ct_body,
    $close;

    // Center the modal in the viewport
    method.center = function () {
        var top, left;

        top = Math.max($(window).height() - $modal.outerHeight(), 0) / 2;
        left = Math.max($(window).width() - $modal.outerWidth(), 0) / 2;

        $modal.css({
            top: top + $(window).scrollTop(),
            left: left + $(window).scrollLeft()
        });
    };

    // Open the modal
    method.open = function (settings) {
        $content.empty().append(settings.content);
        $modal.css({
            width: settings.width || 'auto',
            height: settings.height || 'auto'
        });

        method.center();
        $(window).bind('resize.modal', method.center);
        $modal.show();
        $overlay.show();
    };

    // Close the modal
    method.close = function () {
        $modal.hide();
        $overlay.hide();
        $content.empty();
        $(window).unbind('resize.modal');
    };

    // Generate the HTML and add it to the document
    $overlay = $('<div id="overlay"></div>');
    $modal = $('<div id="modal"></div>');
    $content = $('<div id="content"></div>');
    $close = $('<a id="close" href="#">close</a>');


    $modal.hide();
    $overlay.hide();
    $modal.append($content, $close);

    $(document).ready(function () {
        $('body').append($overlay, $modal);
    });

    $close.click(function (e) {
        e.preventDefault();
        method.close();
    });

    return method;
}());


var modalNotSetEmpty = (function () {
    var
    method = {},
    $overlay,
    $modal,
    $content,
    $ct_titlt,
    $ct_body,
    $close;

    // Center the modal in the viewport
    method.center = function () {
        var top, left;

        top = Math.max($(window).height() - $modal.outerHeight(), 0) / 2;
        left = Math.max($(window).width() - $modal.outerWidth(), 0) / 2;

        $modal.css({
            top: top + $(window).scrollTop(),
            left: left + $(window).scrollLeft()
        });
    };

    // Open the modal
    method.open = function (settings) {
        $content.empty().append(settings.content);
        $modal.css({
            width: settings.width || 'auto',
            height: settings.height || 'auto'
        });

        method.center();
        $(window).bind('resize.modal', method.center);
        $modal.show();
        $overlay.show();
    };

    //set hide
    method.set_hide = function () {
        $overlay.hide();
        $modal.hide();
    }
    //set show
    method.set_show = function () {
        $overlay.show();
        $modal.show();
    }

    // Close the modal
    method.close = function () {
        $modal.hide();
        $overlay.hide();
        //  $content.empty();
        $(window).unbind('resize.modal');
        resetDataModal($modal);
    };

    // Generate the HTML and add it to the document
    $overlay = $('<div id="overlayNotSetEmpty"></div>');
    $modal = $('<div id="modalNotSetEmpty"></div>');
    $content = $('<div id="contentNotSetEmpty"></div>');
    $close = $('<a id="closeNotSetEmpty" href="#">close</a>');


    $modal.hide();
    $overlay.hide();
    $modal.append($content, $close);

    $(document).ready(function () {
        $('body').append($overlay, $modal);
    });

    $close.click(function (e) {
        e.preventDefault();
        method.close();
    });

    return method;
}());
function resetDataModal(element) {
    var a = element[0].id;
    var b = element.find('input[type="text"]');
    $.each(b, function (i, v) {
        v.value = "";
    });
    var c = element.find('select');
    $.each(c, function (i1, v1) {
        var a = v1.id;
        var aaa = $('#' + a).children(':first')
        aaa.attr('selected', 'true');
    });
}

