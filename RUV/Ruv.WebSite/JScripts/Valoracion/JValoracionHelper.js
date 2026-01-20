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