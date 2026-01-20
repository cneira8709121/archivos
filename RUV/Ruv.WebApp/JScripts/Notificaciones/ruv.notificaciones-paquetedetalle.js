var ruv = ruv || {}; ruv.namespace('ruv.notificaciones_paquetedetalle');
ruv.notificaciones_paquetedetalle = (function (APP, $) {
    function bindHistory() {
        $('.historicoNotificacion').each(function (index, element) {
            $(element).bind('click', function (event) {
                $.ajax({
                    type: "GET",
                    url: ruv.url.root + 'Notificaciones/PaqueteDetalle.aspx/ObtenerHistoricoNotificacion',
                    contentType: "application/json; charset=utf-8",
                    data: { idNotificacion: $(element).data().idnotificacion },
                    dataType: "json",
                    success: function (response) {
                        if (response && response.d && response.d.length) {
                            var container = $('<div>', { 'class': 'heavyList' }).append($('<ul>'));
                            $(response.d).each(function (index, element) {
                                $('<li>')
                                    .append($('<span>', { 'class': 'subtitle' }).html(element.FechaModificacionString))
                                    .append($('<div>', { 'class': 'heavyListElement' })
                                        .append($('<div>').append($('<strong>').html('Destino'))
                                                          .append($('<span>').html(element.Destino)).append($('<br>'))
                                                          .append($('<span>', { 'class': 'break' }).html(element.DireccionNotificacion))
                                                          .append($('<span>', { 'class': 'subtle' }).html('Teléfono: ' + element.TelefonoNotificacion)))
                                        .append($('<div>').append($('<strong>').html('Identificación'))
                                                          .append($('<span>').html('Paquete: ' + element.Paquete)).append($('<br>'))
                                                          .append($('<span>', { 'class': 'break' }).html('Codigo Guía: ' + element.CodigoGuia)))
                                        .append($('<div>').append($('<strong>').html('Estado'))
                                                          .append($('<span>').html(element.Estado))
                                                          .append($('<span>', { 'class': 'subtle' }).html(element.EstadoYFechaCourier)))
                                        .append($('<div>').append($('<strong>').html('Notificación'))
                                                          .append($('<span>').html(element.AtencionNotificacion))
                                                          .append($('<span>', { 'class': 'subtle' }).html(element.FechaFinalString ? 'Vencimiento: ' + element.FechaFinalString : '')))
                                ).appendTo(container.find('ul'));
                            });
                            ruv.objects.addAsPopup($('<div>', { 'id': 'historicoNotificaciones', 'class': 'overlayWindow' }).css({ 'min-width': '650px' }).append(container));
                        }
                    }
                });
                event.preventDefault();
                return false;
            });
        });
    }

    bindHistory();

    return {};
} (ruv, jQuery));