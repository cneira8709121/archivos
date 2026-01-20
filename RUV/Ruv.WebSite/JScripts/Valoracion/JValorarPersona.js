var Principios;
var UltimoEstado;
var ObservacionesEstado;

function CambioVictima() {
    var chkVictima = $('#chkVictima');
    var ddlEstado = $('#ddlEstado');
    var chkObservacionEstado = $('#chkObservacionEstado');
    var chkAfectado = $('#chkAfectado');
    if (chkVictima.is(':checked')) {
        $(ddlEstado).removeAttr('disabled');
        $(chkObservacionEstado).attr('checked', false);
    }
    else {
        if (chkAfectado.is(':checked')) {
            $(ddlEstado).val('6');
            CambioEstado();
            $(ddlEstado).attr('disabled', '-1');
            $(chkObservacionEstado).attr('checked', false);
            $(chkObservacionEstado).css('display', 'none');
        }
        else {
            $(ddlEstado).val('7');
            CambioEstado();
            $(ddlEstado).attr('disabled', '-1');
            $(chkObservacionEstado).attr('checked', false);
            $(chkObservacionEstado).css('display', 'none');
        }
    }
}

function CambioAfectado() {
    var chkVictima = $('#chkVictima');
    var ddlEstado = $('#ddlEstado');
    var chkObservacionEstado = $('#chkObservacionEstado');
    var chkAfectado = $('#chkAfectado');
    if (chkAfectado.is(':checked') && !chkVictima.is(':checked')) {
        $(ddlEstado).val('6');
        CambioEstado();
        $(ddlEstado).attr('disabled', '-1');
        $(chkObservacionEstado).attr('checked', false);
        $(chkObservacionEstado).css('display', 'none');
    }
    if (!chkAfectado.is(':checked') && !chkVictima.is(':checked')) {
        $(ddlEstado).val('7');
        CambioEstado();
        $(ddlEstado).attr('disabled', '-1');
        $(chkObservacionEstado).attr('checked', false);
        $(chkObservacionEstado).css('display', 'none');
    }
}

function CambioEstado() {
    var lblMensajeEstado = $('#lblMensajeEstado');
    var chkVictima = $('#chkVictima');
    var ddlEstado = $('#ddlEstado');
    var chkAfectado = $('#chkAfectado');
    var ddlObservacionEstado = $('#ddlObservacion');
    var afectacion = $('#hlAfectaciones');
    var chkLAfectaciones = $('#dvAfectacion');
    var estado = ddlEstado.val();
    var observacion = ddlObservacionEstado.val();

    $('#ddlObservacion option').each(function () {
        $(this).remove();
    });

    ddlObservacionEstado.append('<option value=0>[Seleccione Uno]</option>');

    $.each(ObservacionesEstado, function (index, observacionestado) {
        if (observacionestado.EstadoId == estado) {
            ddlObservacionEstado.append('<option value=' + observacionestado.Id + '>' + observacionestado.Nombre + '</option>');
        }
    });
    ddlObservacionEstado.val(observacion);

    if ((chkVictima.is(':checked') && estado == 6) && (chkVictima.is(':checked') && estado == 7)) {
        lblMensajeEstado.text('No es posible seleccionar este estado cuando la persona es Victima');
        Mensaje(true, 'dvMensajeEstado');
        ddlEstado.val(0);
        CambioEstado();
    }
    else {
        if (estado != 0) {
            lblMensajeEstado.text("Se cambio el estado a: " + $("#ddlEstado option:selected").text());
            Mensaje(true, 'dvMensajeEstado');
        }
    }

    if (estado != null) {
        if (estado == 1) {
            if (observacion > 2) {
                ddlObservacionEstado.val(0);
            }
            if (chkAfectado.is(':checked')) {
                chkVictima.attr("checked", true);
            }
        }
        if (estado == 2) {
            if (observacion == 1 || observacion == 2) {
                ddlObservacionEstado.val(0);
            }
        }
        if (estado == 0) {
            $('#chkLPrincipios input[type=checkbox]').each(function () {
                $(this).attr("checked", false);
            });
        }
        if (estado == 5) {
            chkLAfectaciones.css('display', 'none');
            afectacion.text('Ver afectaciones...');
        }
        else {
            afectacion.text('Ocultar afectaciones...');
        }
        if (estado != 1 && estado != 2) {
            ddlObservacionEstado.val(0);
        }



        $.each(Principios, function (index, principio) {
            if (principio.EstadoId != estado) {
                $('#chkLPrincipios input[type=checkbox]').each(function () {
                    if ($(this).val() == principio.Id.toString()) {
                        $(this).attr("checked", false);
                        $(this).parent().parent().parent().css('display', 'none');
                    }
                });
            }
            else {
                $('#chkLPrincipios input[type=checkbox]').each(function () {
                    if ($(this).val() == principio.Id.toString()) {
                        $(this).parent().parent().parent().css('display', 'block');
                    }
                });
            }
        });
    }
}

function ClickVerAfectacion() {
    var btnAfectacion = $('#hlAfectaciones');
    var dvAfectaciones = $('#dvAfectacion');
    if (dvAfectaciones.css('display') != 'none') {
        btnAfectacion.text('Ver afectaciones...');
        dvAfectaciones.css('display', 'none');
    }
    else {
        btnAfectacion.text('Ocultar afectaciones...');
        dvAfectaciones.css('display', 'block');
    }
}

$(document).ready(function () {
    $.ajax({
        type: "POST",
        url: "Nueva.aspx/ObtenerPrincipios",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            Principios = response.d;
        },
        error: function (result) {
            alert('Ocurrio un error cargando los principios recargue la pagina para intentar de nuevo');
        }
    });

    $.ajax({
        type: "POST",
        url: "Nueva.aspx/ObtenerObservacionesEstado",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            ObservacionesEstado = response.d;
        },
        error: function (result) {
            alert('Ocurrio un error cargando los principios recargue la pagina para intentar de nuevo');
        }
    });
});