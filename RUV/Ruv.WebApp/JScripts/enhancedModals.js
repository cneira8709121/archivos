function beginRequest(sender, args) {
    //ruv.log.trace('RequestHandler: Displaying mask and loading message');
    var mask = $('.mask');
    if (mask) {
        //ruv.log.trace('RequestHandler: Retrieving document: ');
        //ruv.log.trace(document);
        mask.css({ height: (document ? $(document).height() : 10000) });
        var object = mask.find('.waiting');
        if (object) {
            var windowHeight = $(window).height(), windowWidth = $(window).width();
            object.css({ 'top': windowHeight / 2 - object.height() / 2, 'left': windowWidth / 2 - object.width() / 2, 'right': windowWidth / 2 - object.width() / 2 });
        }
    }
    var holder = $('#UpdateProgress');
    if (holder) holder.show();
}
function ApplicationLoadHandler() {
    Sys.WebForms.PageRequestManager.getInstance().add_beginRequest(beginRequest);
}