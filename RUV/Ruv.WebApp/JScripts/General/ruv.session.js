var ruv = ruv || {}; ruv.namespace('ruv.session');
ruv.session = (function (APP, $) {
    var that = this
      , timerHandler = null
      , maxTimeoutValue = 7200000
      , pageBodySection = $('body');
    function initializeSessionTimeout() {
        timerHandler = setTimeout('location = "/Logout.aspx"', maxTimeoutValue);
    }
    function restartSessionTimeout() {
        clearTimeout(timerHandler);
        initializeSessionTimeout();
    }
    initializeSessionTimeout();
    $('body').bind('click', function () {
        restartSessionTimeout();
        ruv.log.trace('Timer for session has been reset');
    });
} (ruv, jQuery));