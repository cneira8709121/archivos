$(document).ready(function () {

    //  Run time once
    //Init();
    $('#ddlPais').change(CambioPais);
    $('#ddlDepartamento').change(CambioDepartamento);
    $('#ddlMunicipio').change(CambioMunicipio);
    $("#btnGuardar").click(GuardarPuntoNotificacion);
});

function CambioPais() {
    var idPais = $('#ddlPais').val();
    //$('#ddlDepartamento').attr('disabled', 'disabled'); // deshabilita el control hasta nuevo cambio
    $('#ddlDepartamento').empty().html('<option value="">[Seleccione Uno]</option>'); ;
    //$('#ddlMunicipio').attr('disabled', 'disabled');
    $('#ddlMunicipio').empty().html('<option value="">[Seleccione Uno]</option>'); ;
    //$('#ddlEntidadMunicipio').attr('disabled', 'disabled');
    $('#ddlEntidadMunicipio').empty().html('<option value="">[Seleccione Uno]</option>'); ;
    $.ajax({
        type: "GET",
        url: "ConsultarNotificaciones.aspx/ObtenerDepartamentosPorPais",
        contentType: "application/json; charset=utf-8",
        data: { idPais: idPais },
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
            else {
                //alert("ERROR Pais"); 
            }
        }
    });
};

function CambioDepartamento() {
    //var idDepar = $(this).find(":selected").val();
    var idDepar = $('#ddlDepartamento').val();
    //$('#ddlMunicipio').attr('disabled', 'disabled');
    $('#ddlMunicipio').empty().html('<option value="">[Seleccione Uno]</option>'); ;
    //$('#ddlEntidadMunicipio').attr('disabled', 'disabled');
    $('#ddlEntidadMunicipio').empty().html('<option value="">[Seleccione Uno]</option>'); ;
    $.ajax({
        type: "GET",
        url: "ConsultarNotificaciones.aspx/ObtenerMunicipiosPorDepartamento",
        contentType: "application/json; charset=utf-8",
        data: { idDepartamento: idDepar },
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
            else {
                //alert("ERROR Departamento"); 
            }
        }
    });
};

function CambioMunicipio() {
    //var idMuni = $(this).find(":selected").val();
    var idMuni = $('#ddlMunicipio').val();
    //$('#ddlEntidadMunicipio').attr('disabled', 'disabled');
    $('#ddlEntidadMunicipio').empty().html('<option value="">[Seleccione Uno]</option>');
    $.ajax({
        type: "GET",
        url: "ConsultarNotificaciones.aspx/ObtenerEntidadesPorMunicipio",
        contentType: "application/json; charset=utf-8",
        data: { idMunicipio: idMuni },
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
            else {
                //alert("ERROR Muncipio"); 
            }
        }
    });
};

function GuardarPuntoNotificacion() {
    var idNotificacion = $("input[id$=HFIdNotificacion]").val();
    var idPais = $('#ddlPais :selected').val();
    var idDepartamento = $('#ddlDepartamento :selected').val();
    var idMunicipio = $('#ddlMunicipio :selected').val();
    var direccion = $("input[id$=txtDireccion]").val();
   
    $.ajax({
            type: "POST",
            url: "ConsultarNotificaciones.aspx/Guardar",
            contentType: "application/json; charset=utf-8",
            data: "{idNotificacion:'" + idNotificacion + "', idPais:'" + idPais + "', idDepartamento:'" + idDepartamento + "', idMunicipio:'" + idMunicipio + "', direccion:'" + direccion + "'}",
            dataType: "json",
            success: function (response) {
                alert(response);
                //CerrarVentanaPuntosNotificacion();
                console.log(response);
                return false;
            }
        });
};

//// Inicializacion de elementos
//function Init() {
//    if ($('#ddlPais').val() == '0') {
//        $('#ddlPais').val('48');
//        CambioPais();
//    }

    //$('#ddlDepartamento').attr('disabled', 'disabled');
    //$('#ddlMunicipio').attr('disabled', 'disabled');
//};

var IdPais;
var IdDepartamento;
var IdMunicipio;

function CargarDepartamento() {
    //var idPais = $('#ddlPais').val();
    var idPais = IdPais;
    //$('#ddlDepartamento').attr('disabled', 'disabled'); // deshabilita el control hasta nuevo cambio
    $('#ddlDepartamento').empty().html('<option value="">[Seleccione Uno]</option>'); ;
    //$('#ddlMunicipio').attr('disabled', 'disabled');
    $('#ddlMunicipio').empty().html('<option value="">[Seleccione Uno]</option>'); ;
    //$('#ddlEntidadMunicipio').attr('disabled', 'disabled');
    $.ajax({
        type: "GET",
        url: "ConsultarNotificaciones.aspx/ObtenerDepartamentosPorPais",
        contentType: "application/json; charset=utf-8",
        data: { idPais: idPais },
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
            else {
                //alert("ERROR Pais"); 
            }

            //$('#ddlPais').val(IdPais);
            $('#ddlDepartamento').val(IdDepartamento);
        }
    });
};

function CargarMunicipio() {
    //var idDepar = $(this).find(":selected").val();
    //var idDepar = $('#ddlDepartamento').val();
    var idDepar = IdDepartamento;
    //$('#ddlMunicipio').attr('disabled', 'disabled');
    $('#ddlMunicipio').empty().html('<option value="">[Seleccione Uno]</option>'); ;
    //$('#ddlEntidadMunicipio').attr('disabled', 'disabled');
    $.ajax({
        type: "GET",
        url: "ConsultarNotificaciones.aspx/ObtenerMunicipiosPorDepartamento",
        contentType: "application/json; charset=utf-8",
        data: { idDepartamento: idDepar },
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
            else {
                //alert("ERROR Departamento"); 
            }

            //$('#ddlDepartamento').val(IdDepartamento);
            $('#ddlMunicipio').val(IdMunicipio);
        }
    });
};



function CerrarVentanaPuntosNotificacion() {
    var modalPopupBehavior = $find('mpopUpPuntosNotBehavior');
    modalPopupBehavior.hide();
}
