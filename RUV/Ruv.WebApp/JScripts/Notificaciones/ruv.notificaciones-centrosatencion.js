var ruv = ruv || {}; ruv.namespace('ruv.notificaciones_centrosatencion');
ruv.notificaciones_centrosatencion = (function (APP, $) {
    var filterPais = $('#filterPais')
      , filterDepartamento = $('#filterDepartamento')
      , filterMunicipio = $('#filterMunicipio');

    function clearFilterDepartamento() {
        clearFilterMunicipio();
        filterDepartamento.children().remove();
        $('<option>', { value: '', selected: true }).html('-- Seleccione uno --').prependTo(filterDepartamento);
    }

    function clearFilterMunicipio() {
        filterMunicipio.children().remove();
        $('<option>', { value: '', selected: true }).html('-- Seleccione uno --').prependTo(filterMunicipio);
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

    bindSelectedPaisChanged();
    bindSelectedDepartamentoChanged();

    return {};
} (ruv, jQuery));