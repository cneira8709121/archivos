function MostrarInmuebles() {
    $("#divInmuebles").show();
    $("#divfecha").hide();
    $("#spAbandono").hide();
    $("#spDespojo").hide();
}


function MostrarMuebles() {
    $("#divInmuebles").hide();
    $("#divfecha").show();
    $("#spAbandono").hide();
    $("#spDespojo").hide();
}

function CambioHechoVictimizante() {
    var ddlHechosVictimizantes = $("#ddlHechosVictimizantes");
    var dvEstadoEnHecho = $("#dvEstadoEnHecho");
    $("#divfecha").show();
    $("#ddlPersonas").val('0');
    $("#chkVictima1").prop("checked", true);
    $("#chkEstadoHecho").html('');
    $("#trDatosVictima").hide();
    $("#spAbandono").hide();
    $("#spDespojo").hide();
    $("#chkDespojo").prop("checked", false);
    $("#chkAbandono").prop("checked", false);
    $("#lblAnexo12").hide();
    $("#divInmuebles").hide();
    $("#lblAn11").hide();
    $("#dvEstadoEnHecho").hide();

    if (ddlHechosVictimizantes.val() == "5126") {
        $("#lblAn11").show();
        $("#rbInmueble").prop("checked", false);
        $("#rbMueble").prop("checked", false);
        $("#spAbandono").hide();
        $("#spDespojo").hide();
    }
    else {
        if (ddlHechosVictimizantes.val() == "5127") {
            $("#lblAnexo12").show();
        }
        else
            $("#lblAnexo12").hide();

        $("#lblAn11").hide();
        $("#rbMueble").prop("checked", false);
        $("#spAbandono").hide();
        $("#spDespojo").hide();

    }
}

function MuestraAbandono() {
    $("#divfecha").hide();
    if ($("#chkAbandono").is(':checked')) {
        $("#spAbandono").show();
    }
    else {
        $("#spAbandono").hide();
    }

}

function MuestraDespojo() {
    $("#divfecha").hide();
    if ($("#chkDespojo").is(':checked')) {
        $("#spDespojo").show();
    }
    else {
        $("#spDespojo").hide();
    }
}
function checkVictima() {
    var trDatosVictima = $("#trDatosVictima");
    var dvEstadoEnHecho = $("#dvEstadoEnHecho");
    var ddlPersonas = $("#ddlPersonas");
    chkEstadodelHecho = '';
    var hdEstadoHecho = $("#chkEstadodelHecho");
    var items = "";
    var dvVictima1 = $("#dvVictima1");
    var ddlHechosVictimizantes = $("#ddlHechosVictimizantes");

    jQuery("#chkEstadodelHecho option").each(function () {
        if ($(this).val() == '0' || $(this).val() == '') {
            $(this).remove();
        }
    });

    $("#chkEstadodelHecho").val('');
    jQuery("#hdEstadoHecho").val('');

    $('#chkEstadodelHecho').get(0).selectedIndex = 0;

    if (ddlPersonas.val() > 0) {
        dvVictima1.show();
        trDatosVictima.show();

        if (ddlHechosVictimizantes.val() == "5119") {
            dvEstadoEnHecho.show();
            jQuery("#chkEstadodelHecho option").each(function () {
                if ($(this).val() != '0') {
                    $(this).remove();
                }
            });
            jQuery("#chkEstadodelHecho").append(
                 $('<option></option>').val(1).html("Se encuentra desaparecido")
                 );

        }
        if (ddlHechosVictimizantes.val() == "5120") {
            dvEstadoEnHecho.show();
            jQuery("#chkEstadodelHecho option").each(function () {
                if ($(this).val() != '0') {
                    $(this).remove();
                }
            });
            jQuery("#chkEstadodelHecho").append(
                $('<option></option>').val(1).html("Se desplazó")
                );

        }
        if (ddlHechosVictimizantes.val() == "5121") {
            dvEstadoEnHecho.show();
            jQuery("#chkEstadodelHecho option").each(function () {
                if ($(this).val() != '0') {
                    $(this).remove();
                }
            });
            jQuery("#chkEstadodelHecho").append(
         $('<option></option>').val(1).html("Persona fallecida")
     );
        }


        if (ddlHechosVictimizantes.val() == "5122") {

        }

        if (ddlHechosVictimizantes.val() == "5123") {
            dvEstadoEnHecho.show();
            jQuery("#chkEstadodelHecho option").each(function () {
                if ($(this).val() != '0') {
                    $(this).remove();
                }
            });
            jQuery("#chkEstadodelHecho").append(
                $('<option></option>').val(1).html("Persona secuestrada")
                );

        }

    } else {
        dvVictima1.hide();
        return false;
    }

}


function EstadoHecho() {
    jQuery("#hdEstadoHecho").val(jQuery("#chkEstadodelHecho").val());
}
function ValidarGuardar() {


    var ddlHechosVictimizantes = $("#ddlHechosVictimizantes");
    var ddlDptoHecho = $("#ddlDptoHecho");
    var ddlMunHecho = $("#ddlMunHecho");
    var dvMensajeValidacionHecho = $("#dvMensajeValidacionHecho");
    var lblMensajeValidacion = $("#lblMensajeValidacion");
    var ddlPersonas = $("#ddlPersonas");
    var hdPersonas = $('#hdPersonas');
    var chkDespojo = $("#chkDespojo");
    var chkAbandono = $("#chkAbandono");
    var rbInmueble = $("#rbInmueble");
    var rbMueble = $("#rbMueble");
    var hdFechaDeclaracion = $("#hdFechaDeclaracion");
    var txtfechaAbandono = $("#txtfechaAbandono");
    var TxtFechadespojo = $("#TxtFechadespojo");
    var txtFecha = $("#txtFecha");
    var lbPersonasAnexo = $("#lbPersonasAnexo");
    var textresultado = '';
    var resultado = true;
    var fechaDeclaracion = '';
    var chkVictima1 = $("#chkVictima1");
    var fechaHecho = '';
    var otroCual = $("#ddlHechosOtros");
    var mpopUpNuevoHecho = $("#mpopUpNuevoHecho");
    var tipoLugar = $("#MainContent_hvNuevo_LugarHecho_Entorno_ddl");
    var chkEstadodelHecho = $("#chkEstadodelHecho");


    if (ddlHechosVictimizantes.val() == "0") {
        textresultado += "- Debe seleccionar un hecho victimizante <br />";
        resultado = false;
    }

    if (ddlDptoHecho.val() == "0") {
        textresultado += "- Debe seleccionar un Departamento <br />";
        resultado = false;
    }
    if (ddlMunHecho.val() == "0") {
        textresultado += "- Debe seleccionar un Municipio <br />";
        resultado = false;
    }

    if (chkEstadodelHecho.val() > "0") {

    }

    if (ddlHechosVictimizantes.val() == "5126") {

        if (!rbInmueble.is(":checked") && !rbMueble.is(":checked")) {
            textresultado += "- Debe seleccionar si el Tipo de echo es Mueble o Inmueble";
            resultado = false;
        }
        if (rbInmueble.is(":checked")) {
            if (chkDespojo.is(":checked")) {
                if (TxtFechadespojo.val() == '') {
                    textresultado += "- La fecha de despojo es requerida <br />";
                    resultado = false;
                }
                else {
                    var fechaHecho = TxtFechadespojo.val();
                    var fechaDeclaracion = hdFechaDeclaracion.val()
                    if (Date.parse(fechaHecho) > Date.parse(fechaDeclaracion)) {
                        textresultado += "-La fecha de despojo no puede ser superior a la fecha de los hechos  <br />"
                        resultado = false;
                    }
                }
            }
            if (chkAbandono.is(":checked")) {
                if (txtfechaAbandono.val() == '') {
                    textresultado += "- La fecha de abandono es requerida  <br />";
                    resultado = false;
                }
                else {
                    fechaHecho = txtfechaAbandono.val();
                    fechaDeclaracion = hdFechaDeclaracion.val();
                    if (Date.parse(fechaHecho) > Date.parse(fechaDeclaracion)) {
                        textresultado += "-La fecha de abandono no puede ser superior a la fecha de los hechos <br /> "
                        resultado = false;
                    }
                }
            }


            if (!chkDespojo.is(":checked") && !chkAbandono.is(":checked")) {
                textresultado += "- Debe indicar si es abandono, despojo o ambos <br /> ";
                resultado = false;
            }
        } else {
            if (txtFecha.val() == '') {
                textresultado += "- Debe indicar la fecha del hecho  <br />";
                resultado = false;
            }
            else {
                fechaHecho = txtFecha.val();
                fechaDeclaracion = hdFechaDeclaracion.val();
                if ((Date.parse(fechaHecho)) > (Date.parse(fechaDeclaracion))) {
                    textresultado += "- La fecha de declaracion no puede ser superio a la fecha de los hechos  <br />";
                    resultado = false;
                }
            }
        }
    }
    else {
        if (txtFecha.val() == '') {
            textresultado += "- Debe indicar la fecha del hecho  <br />";
            resultado = false;
        }
        else {
            fechaHecho = txtFecha.val();
            fechaDeclaracion = hdFechaDeclaracion.val();
            if ((Date.parse(fechaHecho)) > (Date.parse(fechaDeclaracion))) {
                textresultado += "- La fecha de declaracion no puede ser superio a la fecha de los hechos  <br />";
                resultado = false;
            }
        }
    }
    var contVictima = 0;
    var personas = 0;
    $("#MainContent_hvNuevo_lbPersonasAnexo_lbx > option").each(function () {
        personas++;
        if (this.text.indexOf("(Victima 1)") > -1) {
            contVictima++;
        }
    });

    if (contVictima == 0) {
        textresultado += "- Debe seleccionar al menos una persona como victima 1 del hecho  <br />";
        resultado = false;
    }
    if (personas == 0) {
        textresultado += "- Debe seleccionar al menos una persona <br />";
        resultado = false;
    }



    if (ddlHechosVictimizantes.val() == "5127") {

        if (otroCual.val() == "0") {
            textresultado += "- Debe seleccionar Cual tipo de echo otro es  <br />";
            resultado = false;
        }
    }


    if (!resultado) {
        dvMensajeValidacionHecho.show();
        lblMensajeValidacion.html(textresultado);
    } else {
        ShowModConsult("mpopUpNuevoHecho_BehaviorNH");
        return false;
    }
    return resultado;
}

function CerrarVentanaHechos() {
    HidePopUp("mpopUpValidacionesBH");
    return false;
}

function ValidarPersonas() {
    var dvMensajeValidacionHecho = $("#dvMensajeValidacionHecho");
    var lblMensajeValidacion = $("#lblMensajeValidacion");
    var contVictima = 0;
    var chkVictima1 = $("#chkVictima1");
    var resultado = true;
    var textresultado = '';
    var cantidad = $("#MainContent_hvNuevo_lbPersonasAnexo_lbx option").size();
    if (cantidad > 0) {
        $("#MainContent_hvNuevo_lbPersonasAnexo_lbx > option").each(function () {

            if (this.text.indexOf("(Victima 1)") > -1) {
                contVictima++;
            }
        });


        if (contVictima > 0) {
            if (chkVictima1.is(":checked")) {
                textresultado += "- Solo pueden seleccionar una persona como victima 1 <br />";
                resultado = false;
            }
        }




        if (!resultado) {
            dvMensajeValidacionHecho.show();
            lblMensajeValidacion.html(textresultado);
        }
        else {
            dvMensajeValidacionHecho.hide();
            lblMensajeValidacion.html('');
        }

    }
    return resultado;
}
