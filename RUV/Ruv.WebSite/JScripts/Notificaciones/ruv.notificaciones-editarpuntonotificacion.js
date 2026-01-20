var ruv = ruv || {}; ruv.namespace('ruv.notificaciones_editarpuntonotificacion');
ruv.notificaciones_editarpuntonotificacion = (function (APP, $) {
    var hiddenIdNotificacion = $('#puntoNotificacionIdNotificacion')
      , selectionPais = $('#puntoNotificacionPais')
      , selectionDepartamento = $('#puntoNotificacionDepartamento')
      , selectionMunicipio = $('#puntoNotificacionMunicipio')
      , selectionPuntoNotificacion = $('#puntoNotificacionPuntoNotificacion')
      , inputDireccion = $('#puntoNotificacionDireccion');

    function clearSelectionDepartamento() {
        clearSelectionMunicipio();
        selectionDepartamento.children().remove();
        $('<option>', { value: '', selected: true }).html('[Seleccione Uno]').prependTo(selectionDepartamento);
    }

    function clearSelectionMunicipio() {
        clearSelectionPuntoNotificacion();
        selectionMunicipio.children().remove();
        $('<option>', { value: '', selected: true }).html('[Seleccione Uno]').prependTo(selectionMunicipio);
    }

    function clearSelectionPuntoNotificacion() {
        selectionPuntoNotificacion.children().remove();
        $('<option>', { value: '', selected: true }).html('[Seleccione Uno]').prependTo(selectionPuntoNotificacion);
    }

    function changePais(selected, callback) {
        clearSelectionDepartamento();
        if (selected && selected != '') {
            $.ajax({
                type: "GET",
                url: ruv.url.root + 'Notificaciones/ConsultarNotificaciones.aspx/ObtenerDepartamentosPorPais',
                contentType: "application/json; charset=utf-8",
                data: { idPais: selected },
                dataType: "json",
                success: function (response) {
                    if (response && response.d && response.d.length) {
                        $(response.d).each(function (index, element) {
                            $('<option>', { value: element.Id }).html(element.Nombre).appendTo(selectionDepartamento);
                        });
                        if (callback) callback();
                    }
                }
            });
        }
    }

    function bindSelectedPaisChanged() {
        selectionPais.unbind('change').bind('change', function () {
            changePais(selectionPais.find('option:selected').val());
        });
    }

    function changeDepartamento(selected, callback) {
        clearSelectionMunicipio();
        if (selected && selected != '') {
            $.ajax({
                type: "GET",
                url: ruv.url.root + 'Notificaciones/ConsultarNotificaciones.aspx/ObtenerMunicipiosPorDepartamento',
                contentType: "application/json; charset=utf-8",
                data: { idDepartamento: selected },
                dataType: "json",
                success: function (response) {
                    if (response && response.d && response.d.length) {
                        $(response.d).each(function (index, element) {
                            $('<option>', { value: element.Id }).html(element.Nombre).appendTo(selectionMunicipio);
                        });
                        if (callback) callback();
                    }
                }
            });
        }
    }

    function bindSelectedDepartamentoChanged() {
        selectionDepartamento.unbind('change').bind('change', function () {
            changeDepartamento(selectionDepartamento.find('option:selected').val());
        });
    }

    function changeMunicipio(selected, callback) {
        clearSelectionPuntoNotificacion();
        if (selected && selected != '') {
            $.ajax({
                type: "GET",
                url: ruv.url.root + 'Notificaciones/ConsultarNotificaciones.aspx/ObtenerPuntosNotificacionPorMunicipio',
                contentType: "application/json; charset=utf-8",
                data: { idMunicipio: selected },
                dataType: "json",
                success: function (response) {
                    if (response && response.d && response.d.length) {
                        $(response.d).each(function (index, element) {
                            $('<option>', { value: element.HashId }).html(element.Nombre).appendTo(selectionPuntoNotificacion);
                        });
                        if (callback) callback();
                    }
                }
            });
        }
    }

    function bindSelectedMunicipioChanged() {
        selectionMunicipio.unbind('change').bind('change', function () {
            changeMunicipio(selectionMunicipio.find('option:selected').val());
        });
    }

    //Luego de cambiar el punto de notificación, se carga su dirección
    function changePuntoNotificacion(selected, tipoPunto, callback) {
        inputDireccion.val('');
        if (selected && selected != '') {
            var strs = selected.split("-")
            if (strs.length > 1) 
            {
                selected = strs[1];
                tipoPunto = strs[0] == "PA" ? 0 : 1;
            }
            $.ajax({
                type: "GET",
                url: ruv.url.root + 'Notificaciones/ConsultarNotificaciones.aspx/ObtenerDireccionPorPuntoNotificacion',
                contentType: "application/json; charset=utf-8",
                data: { idPuntoNotificacion: selected, tipoPuntoNotificacion: tipoPunto },
                dataType: "json",
                success: function (response) {
                    if (response) {
                        inputDireccion.val(response.d);
                        if (callback) callback();
                    }
                }
            });
        }
    }

    function bindSelectedPuntoNotificacionChanged() {
        selectionPuntoNotificacion.unbind('change').bind('change', function () {
            changePuntoNotificacion(selectionPuntoNotificacion.find('option:selected').val());
        });
    }

    function savePuntoAtencionInformation() {
        var selectedPais = selectionPais.find('option:selected').val();
        var selectedDepartamento = selectionDepartamento.find('option:selected').val();
        var selectedMunicipio = selectionMunicipio.find('option:selected').val();
        var selectedPuntoNotificacion = selectionPuntoNotificacion.find('option:selected').val();
        var enteredDireccion = inputDireccion.val();
        if (!selectedPais || selectedPais == '' || parseInt(selectedPais) <= 0) {
            window.alert('Debe especificar el país');
        }
        else if (!selectedDepartamento || selectedDepartamento == '' || parseInt(selectedDepartamento) <= 0) {
            window.alert('Debe especificar el departamento');
        }
        else if (!selectedMunicipio || selectedMunicipio == '' || parseInt(selectedMunicipio) <= 0) {
            window.alert('Debe especificar el municipio');
        }
        else if (!selectedPuntoNotificacion || selectedPuntoNotificacion == '' || parseInt(selectedPuntoNotificacion) <= 0) {
            window.alert('Debe especificar el punto donde se realizará la notificacion');
        }
        else if (!enteredDireccion || enteredDireccion == '') {
            window.alert('Debe especificar la dirección de punto de notificación');
        }
        else {
            ruv.log.trace(hiddenIdNotificacion.val());
            $.ajax({
                type: "POST",
                url: ruv.url.root + 'Notificaciones/ConsultarNotificaciones.aspx/GuardarPuntoNotificacion',
                contentType: "application/json; charset=utf-8",
                data: "{ idNotificacion: '" + hiddenIdNotificacion.val() + "', puntoNotificacion: '" + selectedPuntoNotificacion + "', direccion: '" + enteredDireccion + "'}",
                dataType: "json",
                success: function (response) {
                    if (response.d) window.location.href = window.location.href;
                    else (window.alert('No se pudo actualiza la información. Por favor intente mas tarde'));
                }
            });
        }
    }

    function bindSavePuntoAtencionInformation() {
        $('#btnGuardarPuntoNotificacion').unbind('click').bind('click', function () {
            savePuntoAtencionInformation();
            return false;
        });
    }

    bindSelectedPaisChanged();
    bindSelectedDepartamentoChanged();
    bindSelectedMunicipioChanged();
    bindSelectedPuntoNotificacionChanged();
    bindSavePuntoAtencionInformation();

    return {
        initialize: function (idNotificacion, idPais, idDepartamento, idMunicipio, idPuntoAtencion, idDireccionTerritorial) {
            ruv.log.trace('Initializing Edition of Punto de Notificacion...');
            hiddenIdNotificacion.val(idNotificacion);
            if (idPais && idPais > 0) {
                selectionPais.val(idPais);
                changePais(idPais, function () {
                    if (idDepartamento && idDepartamento > 0) {
                        selectionDepartamento.val(idDepartamento);
                        changeDepartamento(idDepartamento, function () {
                            if (idMunicipio && idMunicipio > 0) {
                                selectionMunicipio.val(idMunicipio);
                                changeMunicipio(idMunicipio, function () {
                                    ruv.log.trace('PuntoAtencion initialized to value ' + idPuntoAtencion);
                                    ruv.log.trace('DireccionTerritorial initialized to value ' + idDireccionTerritorial);
                                    if (idPuntoAtencion && idPuntoAtencion > 0) {
                                        selectionPuntoNotificacion.val('PA-' + idPuntoAtencion);
                                        changePuntoNotificacion(idPuntoAtencion, 0);
                                    }
                                    else if (idDireccionTerritorial && idDireccionTerritorial > 0) {
                                        selectionPuntoNotificacion.val('DT-' + idDireccionTerritorial);
                                        changePuntoNotificacion(idDireccionTerritorial, 1);
                                    }
                                    //inputDireccion.val(direccion);
                                });
                            }
                        });
                    }
                });
            }
            else {
                selectionPais.val(48);
                changePais(48);
            }
        }
    };
} (ruv, jQuery));