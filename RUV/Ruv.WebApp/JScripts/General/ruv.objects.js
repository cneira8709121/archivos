var ruv = ruv || {}; ruv.namespace('ruv.objects');
ruv.objects = (function (APP, $) {
    function displayMask() {
        var mask = $('#generalMask');
        if (mask && mask.length) {
            mask.css({ 'width': (document ? $(document).width() : 1000), 'height': (document ? $(document).height() : 10000) });
            mask.fadeIn(500).fadeTo("slow", 0.8);
            return mask;
        }
    }
    return {
        displayAsPopup: function (object) {
            displayMask();
            var windowHeight = $(window).height(), windowWidth = $(window).width();
            object.css({ 'top': windowHeight / 2 - object.height() / 2, 'left': windowWidth / 2 - object.width() / 2, 'right': windowWidth / 2 - object.width() / 2 });
            object.fadeIn(500);
        },
        addAsPopup: function ($html) {
            var mask = displayMask();
            var windowHeight = $(window).height(), windowWidth = $(window).width();
            $('body:first').append($html.addClass('controlInformationPopup').css({ 'display': 'none' }));
            $html.css({ 'top': windowHeight / 2 - $html.height() / 2 + window.scrollY, 'left': windowWidth / 2 - $html.width() / 2 });
            $html.fadeIn(500);
            mask.bind('click', function () {
                $html.fadeOut(300, function () { $html.remove(); });
                mask.fadeOut(300);
            });
        }
    };
} (ruv, jQuery));