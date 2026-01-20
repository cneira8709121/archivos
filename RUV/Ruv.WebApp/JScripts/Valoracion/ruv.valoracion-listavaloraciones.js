var ruv = ruv || {}; ruv.namespace('ruv.valoracion_listavaloraciones');
ruv.valoracion_listavaloraciones = (function (APP, $) {
	function bindWarningCloseBehavior(closeButton) {
		closeButton.bind('click', function() {
			var element = closeButton.closest('div.gridWarning');
			if (element && element.length) element.remove();
		});
	}
    function bindWarningRowsBehavior() {
    	$('.gridWarningValue').each(function() {
    		var trigger = $(this);
    		if (trigger.val() != '') {
    			var prefix = '';
    			trigger.siblings('.gridWarningPrefix').each(function() {
    				prefix += $(this).val();
    			});
    			var popup = $('<div>', { class: 'gridWarning' });
    			//$('<p>', { class: 'gridWarningPrefix' }).html(prefix).appendTo(popup);
    			$('<p>', { class: 'gridWarningValue' }).html('(' + prefix + ') ' + trigger.val()).appendTo(popup);
    			$('<a>', { class: 'warningClose' }).html('X').appendTo(popup);
    			popup.appendTo(trigger.closest('div'));
    			var relativeObject = trigger.closest('tr');
    			var relativePos = relativeObject.position(), relativeWidth = relativeObject.width(), relativeHeight = relativeObject.height();
    			popup.css({ 'top': relativePos.top + relativeHeight - popup.height(), 'left': relativePos.left + relativeWidth - popup.width() - 1 });
    			bindWarningCloseBehavior(popup.find('a.warningClose'));
    		}
    	});
    }
    $(function() { bindWarningRowsBehavior(); });
    return { };
} (ruv, jQuery));