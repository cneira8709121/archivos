var ruv = ruv || {}; ruv.namespace('ruv.log');
ruv.log = (function (APP, $) {
    return {
        trace: function (object) {
            if (window.console && window.console.log)
                window.console.log(object);
        }
    };
} (ruv, jQuery));