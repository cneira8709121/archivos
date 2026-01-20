function Bloquear() {
    document.getElementById('btnAccion').disabled = true;
}
function Desbloquear() {
    document.getElementById('btnAccion').disabled = false;
}

function Mensaje(visible, objeto) {
    var mensaje = document.getElementById(objeto);
    if (visible) {
        mensaje.style.display = 'block';
    }
    else {
        mensaje.style.display = 'none';
    }
}

function FinalizarValoracion() {
    HidePopUp('mpopGuardarBehavior');
    ShowModConsult(null, 'Guardando Valoración');
}

function EditarValoracionWPF() {
    if ($("#txtObservacionEditar").val() != '') {
        var observacion = $("#txtObservacionEditar").val();
        var nav = navigator.userAgent.toLocaleLowerCase();
        var idValoracion = $('#hdnIdValoracion').val();
        var idDeclaracion = $('#hdnIdDeclaracion').val();
        var loguin = $('#hdnLogin').val();
        var password = encodeURIComponent($('#hdnPassword').val());
        var code = getNameURLWeb() + "/GuardarEditar";
        $.ajax({
            type: "POST",
            url: code,
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: "{ observacion: '" + observacion + "' }",
            error: function (result) {
                alert('Ocurrio un error ');
            }
        });
        HidePopUp('mpopUpEditarBehavior');
        var url = $('#hdnUrl').val();
        //window.open(url + 'clientapp/Ruv.WPF.Captura.application?IdVal=' + idValoracion + '&IdDec=' + idDeclaracion + '&Log=' + loguin + '&Pas=' + password + '&Url=' + url);
        location.href = url + 'clientapp/Ruv.WPF.Captura.application?IdVal=' + idValoracion + '&IdDec=' + idDeclaracion + '&Log=' + loguin + '&Pas=' + password + '&Url=' + url;
    }
    else {
        alert("Indique las razón para ingresar a editar la declaración");
    }
    
    return false;
}