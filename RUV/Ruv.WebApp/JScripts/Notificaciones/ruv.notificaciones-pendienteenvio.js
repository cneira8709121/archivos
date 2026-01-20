var ruv = ruv || {}; ruv.namespace('ruv.notificaciones_pendienteenvio');
ruv.notificaciones_pendienteenvio = (function (APP, $) {
    var filterPais = $('#filterPaisNotificacion')
      , filterDepartamento = $('#filterDepartamentoNotificacion')
      , filterMunicipio = $('#filterMunicipioNotificacion')
      , filterPuntoNotificacion = $('#filterPuntoNotificacion');

    function clearFilterDepartamento() {
        clearFilterMunicipio();
        filterDepartamento.children().remove();
        $('<option>', { value: '', selected: true }).html('-- Seleccione uno --').prependTo(filterDepartamento);
    }

    function clearFilterMunicipio() {
        clearFilterPuntoNotificacion();
        filterMunicipio.children().remove();
        $('<option>', { value: '', selected: true }).html('-- Seleccione uno --').prependTo(filterMunicipio);
    }

    function clearFilterPuntoNotificacion() {
        filterPuntoNotificacion.children().remove();
        $('<option>', { value: '', selected: true }).html('-- Seleccione uno --').prependTo(filterPuntoNotificacion);
    }

    function bindSelectedPaisChanged() {
        filterPais.change(function () {
            clearFilterDepartamento();
            var selectedValue = filterPais.find('option:selected').val();
            if (selectedValue != '') {
                $.ajax({
                    type: "GET",
                    url: ruv.url.root + 'Notificaciones/ConsultarNotificaciones.aspx/ObtenerDepartamentosPorPais',
                    contentType: "application/json; charset=utf-8",
                    data: { idPais: selectedValue },
                    dataType: "json",
                    success: function (response) {
                        if (response && response.d && response.d.length) {
                            $(response.d).each(function (index, element) {
                                $('<option>', { value: element.Id }).html(element.Nombre).appendTo(filterDepartamento);
                            });
                        }
                    }
                });
            }
        });
    }

    function bindSelectedDepartamentoChanged() {
        filterDepartamento.change(function () {
            clearFilterMunicipio();
            var selectedValue = filterDepartamento.find('option:selected').val();
            if (selectedValue != '') {
                $.ajax({
                    type: "GET",
                    url: ruv.url.root + 'Notificaciones/ConsultarNotificaciones.aspx/ObtenerMunicipiosPorDepartamento',
                    contentType: "application/json; charset=utf-8",
                    data: { idDepartamento: selectedValue },
                    dataType: "json",
                    success: function (response) {
                        if (response && response.d && response.d.length) {
                            $(response.d).each(function (index, element) {
                                $('<option>', { value: element.Id }).html(element.Nombre).appendTo(filterMunicipio);
                            });
                        }
                    }
                });
            }
        });
    }

    function bindSelectedMunicipioChanged() {
        filterMunicipio.change(function () {
            clearFilterPuntoNotificacion();
            var selectedValue = filterMunicipio.find('option:selected').val();
            if (selectedValue != '') {
                $.ajax({
                    type: "GET",
                    url: ruv.url.root + 'Notificaciones/ConsultarNotificaciones.aspx/ObtenerPuntosNotificacionPorMunicipio',
                    contentType: "application/json; charset=utf-8",
                    data: { idMunicipio: selectedValue },
                    dataType: "json",
                    success: function (response) {
                        if (response && response.d && response.d.length) {
                            $(response.d).each(function (index, element) {
                                $('<option>', { value: element.HashId }).html(element.Nombre).appendTo(filterPuntoNotificacion);
                            });
                        }
                    }
                });
            }
        });
    }

    function bindCheckAllNotificaciones() {
        $('#checkAllNotificaciones').bind('change', function () {
            var checked = $(this).attr('checked') != undefined;
            if (checked)
                $('span[data-selection="itemCheck"] input[type="checkbox"]').attr('checked', 'checked');
            else
                $('span[data-selection="itemCheck"] input[type="checkbox"]').removeAttr('checked');
        });
    }

    bindSelectedPaisChanged();
    bindSelectedDepartamentoChanged();
    bindSelectedMunicipioChanged();
    bindCheckAllNotificaciones();

    return {
        showDireccionCorrespondenciaPopup: function (idNotificacion, idPais, idDepartamento, idMunicipio, direccion) {
            var modalPopupBehavior = $find('mpopUpEdicionDireccionCorrespondenciaBehavior');
            modalPopupBehavior.show();
            ruv.notificaciones_editardireccioncorrespondencia.initialize(idNotificacion, idPais, idDepartamento, idMunicipio, direccion);
            return false;
        },
        showPuntoNotificacionPopup: function (idNotificacion, idPais, idDepartamento, idMunicipio, idPuntoAtencion, idDireccionTerritorial) {
            var modalPopupBehavior = $find('mpopUpEdicionPuntoNotificacionBehavior');
            modalPopupBehavior.show();
            ruv.notificaciones_editarpuntonotificacion.initialize(idNotificacion, idPais, idDepartamento, idMunicipio, idPuntoAtencion, idDireccionTerritorial);
            return false;
        }
    };
} (ruv, jQuery));