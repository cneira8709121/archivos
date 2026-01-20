$(document).ready(function () {

    //$("#btnGuardar").on("click", GuardarPuntoNotificacion());

    //    $("#btnGuardar").click(function () {
    //        $.ajax({
    //            type: "POST",
    //            url: "Nueva.aspx/Guardar",
    //            data: "{strIdDirTerritorial: '" + $("#ddlDireccionesTerritoriales").val() + "', strIdPuntosNotificacion: ' " + $("#ddlPuntosNotificacion").val() + "'}",
    //            contentType: "application/json; charset=utf-8",
    //            dataType: "json",
    //            beforeSend: function (xhr) {
    //                //xhr.setRequestHeader("Authentication", "Basic " + encodeBase64(username + ":" + password)); //May need to use "Authorization" instead
    //            },
    //            success: function (result) {
    //                //$("#respuesta").append(" <b>exito</b>.");
    //            },
    //            error: function (result) {
    //                $("#respuesta").append(" <b>falló</b>.");
    //                //alert('Ocurrio un error');
    //            }
    //        });
    //    });

    // ----  Llamados Asincronos de DEPARTAMENTO I MUNICIPIO I ENTIDAD MUNICIPAL -------
    // ----  s.gutierrez@globant.    com04/04/2013

    function CambioPais() {
        //var idPais = $(this).find(":selected").val();
        var idPais = $('#ddlPais').val();
        $('#ddlDepartamento').attr('disabled', 'disabled'); // deshabilita el control hasta nuevo cambio
        $('#ddlDepartamento').empty().html('<option value="">[Seleccione Uno]</option>'); ;
        $('#ddlMunicipio').attr('disabled', 'disabled');
        $('#ddlMunicipio').empty().html('<option value="">[Seleccione Uno]</option>'); ;
        $('#ddlEntidadMunicipio').attr('disabled', 'disabled');
        $('#ddlEntidadMunicipio').empty().html('<option value="">[Seleccione Uno]</option>'); ;
        $.ajax({
            type: "POST",
            url: "Nueva.aspx/ObtenerDepartamentosPorPais",
            contentType: "application/json; charset=utf-8",
            data: "{ idPais:" + idPais + "}",
            dataType: "json",
            success: function (result) {
                if (result && result.d != null && result.d.length > 0) {
                    var dom_depar = $('#ddlDepartamento');
                    dom_depar.removeAttr('disabled');
                    var subModule = '';
                    subModule += '<option value="">[Seleccione Uno]</option>';
                    $(result.d).each(function (index, element) {
                        subModule += '<option value="' + element.Id + '">' + element.Nombre + '</option>';
                    });
                    dom_depar.html(subModule);
                }
                //else { alert("ERROR Pais"); }
            }
        });
    };

    function CambioDepartamento() {
        var idDepar = $(this).find(":selected").val();
        $('#ddlMunicipio').attr('disabled', 'disabled');
        $('#ddlMunicipio').empty().html('<option value="">[Seleccione Uno]</option>'); ;
        $('#ddlEntidadMunicipio').attr('disabled', 'disabled');
        $('#ddlEntidadMunicipio').empty().html('<option value="">[Seleccione Uno]</option>'); ;
        $.ajax({
            type: "POST",
            url: "Nueva.aspx/ObtenerMunicipiosPorDepar",
            contentType: "application/json; charset=utf-8",
            data: "{ idDepar:" + idDepar + "}",
            dataType: "json",
            success: function (result) {
                if (result && result.d != null && result.d.length > 0) {
                    var dom_muni = $('#ddlMunicipio');
                    dom_muni.removeAttr('disabled').empty();
                    var subModule = '';
                    subModule += '<option value="">[Seleccione Uno]</option>';
                    $(result.d).each(function (index, element) {
                        subModule += '<option value="' + element.Id + '">' + element.Nombre + '</option>';
                    });
                    dom_muni.html(subModule);
                }
                //else { alert("ERROR Departamento"); }
            }
        });
    };

    function CambioMunicipio() {
        var idMuni = $(this).find(":selected").val();
        $('#ddlEntidadMunicipio').attr('disabled', 'disabled');
        $('#ddlEntidadMunicipio').empty().html('<option value="">[Seleccione Uno]</option>');
        $.ajax({
            type: "POST",
            url: "Nueva.aspx/ObtenerEntidadesPorMuni",
            contentType: "application/json; charset=utf-8",
            data: "{ idMuni:" + idMuni + "}",
            dataType: "json",
            success: function (result) {
                if (result && result.d != null && result.d.length > 0) {
                    var dom_enti = $('#ddlEntidadMunicipio');
                    dom_enti.removeAttr('disabled').empty();
                    var subModule = '';
                    subModule += '<option value="">[Seleccione Uno]</option>';
                    $(result.d).each(function (index, element) {
                        subModule += '<option value="' + element.NId + '">' + element.CNombreEntidad + '</option>';
                    });
                    dom_enti.html(subModule);
                }
                //else { alert("ERROR Muncipio"); }
            }
        });
    };


    //    $("#ddlDireccionesTerritoriales").change(function () {
    //        $.ajax({
    //            type: "POST",
    //            url: "Nueva.aspx/ObtenerPuntosNotificacionPorIdDir",
    //            data: "{strIdDirTerritorial: '" + $("#ddlDireccionesTerritoriales").val() + "'}",
    //            contentType: "application/json; charset=utf-8",
    //            dataType: "json",
    //            beforeSend: function (xhr) {
    //                //xhr.setRequestHeader("Authentication", "Basic " + encodeBase64(username + ":" + password)); //May need to use "Authorization" instead
    //            },
    //            success: function (result) {
    //                //$("#respuesta").append(" <b>exito</b>.");
    //                var puntos = (typeof result.d) == 'string' ? eval('(' + result.d + ')') : result.d;

    //                $('#ddlPuntosNotificacion').empty();

    //                $('#ddlPuntosNotificacion').append(
    //                    $('<option></option>').val('0').html('[Seleccione Uno]')
    //                );

    //                if (puntos == null)
    //                    return;

    //                for (var i = 0; i < puntos.length; i++) {
    //                    var val = puntos[i].NId;
    //                    var text = puntos[i].CNombre;
    //                    var myCombo = $('#ddlPuntosNotificacion');
    //                    myCombo.append($('<option></option>').val(val).html(text));
    //                }
    //            },
    //            error: function (result) {
    //                $("#respuesta").append(" <b>falló</b>.");
    //                //alert('Ocurrio un error');
    //            }
    //        });
    //    });
    function GuardarPuntoNotificacion() {
        var idEntidadMunicipio = $('#ddlEntidadMunicipio :selected').val();
        if (idEntidadMunicipio != null && idEntidadMunicipio != 0) {
            $.ajax({
                type: "POST",
                url: "Nueva.aspx/Guardar",
                contentType: "application/json; charset=utf-8",
                data: "{ idEntidadMunicipio:" + idEntidadMunicipio + "}",
                dataType: "json",
                success: function () {
                    // Persistir los valores en sus correspondientes Hiddens
                    $('#hdnPais').val($('#ddlPais :selected').val());
                    $('#hdnDepartamento').val($('#ddlDepartamento :selected').val());
                    $('#hdnMunicipio').val($('#ddlMunicipio :selected').val());
                    $('#hdnEntidadMunicipio').val($('#ddlEntidadMunicipio :selected').val());
                    $find('mpopUpPuntosNotBehavior').hide();
                    return false;
                }
            });
        } else { alert('Seleccione una Entidad Punto de Notificacion'); }
    };

    // Inicializacion de elementos
    function Init() {
        if ($('#ddlPais').val() == '0') {
            $('#ddlPais').val('48');
            CambioPais();
        }
        $('#ddlDepartamento').attr('disabled', 'disabled');
        $('#ddlMunicipio').attr('disabled', 'disabled');
        $('#ddlEntidadMunicipio').attr('disabled', 'disabled');
    };

    //  Run time once
    Init();
    $('#ddlPais').change(CambioPais);
    $('#ddlDepartamento').change(CambioDepartamento);
    $('#ddlMunicipio').change(CambioMunicipio);
    $("#btnGuardar").click(GuardarPuntoNotificacion);
});

function CerrarVentanaPuntosNotificacion() {
    var modalPopupBehavior = $find('mpopUpPuntosNotBehavior');
    modalPopupBehavior.hide();
}

//function GuardarPuntoNotificacion() {
//    $.ajax({
//        type: "POST",
//        url: "Nueva.aspx/Guardar",
//        data: "{strIdDirTerritorial: '" + $("#ddlDireccionesTerritoriales").val() + "', strIdPuntosNotificacion: ' " + $("#ddlPuntosNotificacion").val() + "'}",
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        beforeSend: function (xhr) {
//            //xhr.setRequestHeader("Authentication", "Basic " + encodeBase64(username + ":" + password)); //May need to use "Authorization" instead
//        },
//        success: function (result) {
//            CerrarVentanaPuntosNotificacion();
//            //$("#respuesta").append(" <b>exito</b>.");
//        },
//        error: function (result) {
//            $("#respuesta").append(" <b>falló</b>.");
//            //alert('Ocurrio un error');
//        }
//    });
//}

//function getPuntos() {
//    $.ajax({
//        type: "POST",
//        url: "NotificacionService.svc/ObtenerPuntosNotificacionPorIdDirTerritorial",
//        data: "{idDirTerritorial: '" + $("#ddlDireccionesTerritoriales").val() + "', cError: ''}",
//        contentType: "application/json; charset=utf-8",
//        dataType: "json",
//        success: function (response) {
//            var puntos = (typeof response.d) == 'string' ? eval('(' + response.d + ')') : response.d;

//            for (var i = 0; i < models.length; i++) {
//                var val = puntos[i];
//                var text = puntos[i];
//                $('#ddlPuntosNotificacion').addOption(val, text, false);
//            }
//        }
//    });
//}
