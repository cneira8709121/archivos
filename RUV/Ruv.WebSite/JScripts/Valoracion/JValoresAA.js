
function CambioTipoAA() {
    var dvMotivacionInclusion = $('#dvIncluidos');
    var dvMotivacionNoInclusion = $('#dvNoIncluido');
    var dvArticulo1 = $('#dvArticulo1');
    var dvArticulo2 = $('#dvArticulo2');
    var checkedOption = $('#rbtLTipoActo input[type=radio]:checked');

    var valoresIngresados = dvMotivacionInclusion.find('textarea').val().trim() != '' || dvMotivacionNoInclusion.find('textarea').val().trim() != '' || dvArticulo1.find('textarea').val().trim() != '' || dvArticulo2.find('textarea').val().trim() != '';

    ruv.log.trace('Valoracion::ValoresActosAdministrativos: Se ha determinado que ' + (valoresIngresados ? 'hay por lo menos un valor ingresado' : 'no se ha ingresado ningun valor'));

    if (valoresIngresados) {
        ruv.log.trace('Valoracion::ValoresActosAdministrativos: Como se ha ingresado por lo menos un valor, se generará una confirmacion');
        if (window.confirm('Se borrarán los valores ya ingresados para los Actos Administrativos. Seguro que quiere continuar?')) {
            ruv.log.trace('Valoracion::ValoresActosAdministrativos: Se ha confirmado la eliminación del texto');
            DesplegarPanelesAA(checkedOption, true);
        }
        else {
            var valorAnterior = ObtenerSeleccion();
            $('#rbtLTipoActo input[type=radio]').each(function () {
                if ($(this).val() == valorAnterior) $(this).prop('checked', true);
                else $(this).prop('checked', false);
            });
        }
    }
    else {
        DesplegarPanelesAA(checkedOption, true);
    }
}

function SeleccionTipoAA() {
    DesplegarPanelesAA($('#rbtLTipoActo input[type=radio]:checked'), false);
}

function DesplegarPanelesAA(checkedRadio, limpiarCampos) {
    var dvMotivacionInclusion = $('#dvIncluidos');
    var dvMotivacionNoInclusion = $('#dvNoIncluido');
    var dvArticulo1 = $('#dvArticulo1');
    var dvArticulo2 = $('#dvArticulo2');
    
    if (checkedRadio.val() == '1') {
        dvMotivacionInclusion.show();
        dvMotivacionNoInclusion.hide();
        dvArticulo1.show();
        dvArticulo2.hide();
    }
    else if (checkedRadio.val() == '2') {
        dvMotivacionInclusion.hide();
        dvMotivacionNoInclusion.show();
        dvArticulo1.show();
        dvArticulo2.hide();
    }
    else if (checkedRadio.val() == '3') {
        dvMotivacionInclusion.show();
        dvMotivacionNoInclusion.show();
        dvArticulo1.show();
        dvArticulo2.show();
    }
    if (limpiarCampos) {
        dvMotivacionInclusion.find('textarea').val('');
        dvMotivacionInclusion.find('.nicEdit-main').html('');
        dvMotivacionNoInclusion.find('textarea').val('');
        dvMotivacionNoInclusion.find('.nicEdit-main').html('');
        dvArticulo1.find('textarea').val('');
        dvArticulo1.find('.nicEdit-main').html('');
        dvArticulo2.find('textarea').val('');
        dvArticulo2.find('.nicEdit-main').html('');
    }
}

function ObtenerSeleccion() {
    if ($('#dvIncluidos').is(':visible') && $('#dvNoIncluido').is(':visible')) return '3';
    else if ($('#dvIncluidos').is(':visible')) return '1';
    else return '2';
}

function CerrarVentanaCamposAA(){
    var modalPopupBehavior = $find('mpopUpValoresAABehavior');
    modalPopupBehavior.hide();
}