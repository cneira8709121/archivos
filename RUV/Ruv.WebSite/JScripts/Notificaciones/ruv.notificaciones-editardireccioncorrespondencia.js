var ruv = ruv || {}; ruv.namespace('ruv.notificaciones_editardireccioncorrespondencia');
ruv.notificaciones_editardireccioncorrespondencia = (function (APP, $) {
    var hiddenIdNotificacion = $('#direccionCorrespondenciaIdNotificacion')
      , selectionPais = $('#direccionCorrespondenciaPais')
      , selectionDepartamento = $('#direccionCorrespondenciaDepartamento')
      , selectionMunicipio = $('#direccionCorrespondenciaMunicipio')
      , inputDireccion = $('#direccionCorrespondenciaDireccion');

    function clearSelectionDepartamento() {
        clearSelectionMunicipio();
        selectionDepartamento.children().remove();
        $('<option>', { value: '', selected: true }).html('[Seleccione Uno]').prependTo(selectionDepartamento);
    }

    function clearSelectionMunicipio() {
        selectionMunicipio.children().remove();
        $('<option>', { value: '', selected: true }).html('[Seleccione Uno]').prependTo(selectionMunicipio);
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

    function saveCorrespondenciaInformation() {
        var selectedPais = selectionPais.find('option:selected').val();
        var selectedDepartamento = selectionDepartamento.find('option:selected').val();
        var selectedMunicipio = selectionMunicipio.find('option:selected').val();
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
        else if (!enteredDireccion || enteredDireccion == '') {
            window.alert('Debe especificar la dirección de correspondencia');
        }
        else {
            ruv.log.trace(hiddenIdNotificacion.val());
            $.ajax({
                type: "POST",
                url: ruv.url.root + 'Notificaciones/ConsultarNotificaciones.aspx/GuardarDireccionCorrespondencia',
                contentType: "application/json; charset=utf-8",
                data: "{ idNotificacion: '" + hiddenIdNotificacion.val() + "', idPais: '" + selectedPais + "', idDepartamento: '" + selectedDepartamento + "', idMunicipio: '" + selectedMunicipio + "', direccion: '" + enteredDireccion + "'}",
                dataType: "json",
                success: function (response) {
                    if (response.d) window.location.href = window.location.href;
                    else (window.alert('No se pudo actualiza la información. Por favor intente mas tarde'));
                }
            });
        }
    }

    function bindSaveCorrespondenciaInformation() {
        $('#btnGuardar').unbind('click').bind('click', function () {
            saveCorrespondenciaInformation();
            return false;
        });
    }

    bindSelectedPaisChanged();
    bindSelectedDepartamentoChanged();
    bindSaveCorrespondenciaInformation();

    return {
        initialize: function (idNotificacion, idPais, idDepartamento, idMunicipio, direccion) {
            ruv.log.trace('Initializing edition of Direccion Correspondencia...');
            hiddenIdNotificacion.val(idNotificacion);
            if (idPais && idPais > 0) {
                selectionPais.val(idPais);
                changePais(idPais, function () {
                    if (idDepartamento && idDepartamento > 0) {
                        selectionDepartamento.val(idDepartamento);
                        changeDepartamento(idDepartamento, function () {
                            if (idMunicipio && idMunicipio > 0) {
                                selectionMunicipio.val(idMunicipio);
                            }
                        });
                    }
                });
            }
            else {
                selectionPais.val(48);
                changePais(48);
            }
            inputDireccion.val(direccion);
        }
    };
} (ruv, jQuery));