/*Muestra el PopUp de AjaxControlToolkit*/
function ShowModConsult(NombreControl, mensaje, validationGroupName, idLabel) {
    if (NombreControl == null) {
        if(mensaje != null)
        {
            var texto = document.getElementById('mpCargando_lblMensajeModalpoup');
            texto.innerHTML = mensaje;
        }

        if (typeof(Page_ClientValidate) === 'function') {
            if(validationGroupName == null)
                Page_ClientValidate();
            else
            {
                Page_ClientValidate(validationGroupName);

                for (i=0; i < Page_Validators.length; i++)
                {
                    if(!Page_Validators[i].isvalid && Page_Validators[i].validationGroup === validationGroupName)
                        return false; 
                }
            }
        }
        //var modalPopupBehavior = $find('mpGeneralBehavior');
        //modalPopupBehavior.show();
        return true;
    }
    else {
        if (mensaje != null) {
            if (idLabel != null) {
                var label = $("#"+idLabel);
                label.html(mensaje);
            }
        }
        var modalPopupBehavior = $find(NombreControl);
        modalPopupBehavior.show();
        return false;
    }
}

function displayASPNETControl(controlName, initialization) {
    var modalPopupBehavior = $find(controlName);
    modalPopupBehavior.show();
    if (initialization) initialization();
    return false;
}

/*Oculta el PopUp de AjaxControlToolkit*/
function HidePopUp(NombreControl){
    if (NombreControl == null) {
        var modalPopupBehavior = $find('mpGeneralBehavior');
        if(modalPopupBehavior != null)
            modalPopupBehavior.hide();
        return true;
    }
    else {
        var modalPopupBehavior = $find(NombreControl);
        modalPopupBehavior.hide();
        return false;
    }
}

/*Llama para descargar archivos por medio de Ventana emergente*/
function Descargar(Archivo) {
    var arch = Archivo;
    rutaUrl = '/Descargar.aspx?Arch=' + arch;
    //window.location.href = rutaUrl;
    window.open(rutaUrl);
}

/*Para mostrar un mensaje en la parte superior de la pagina, se usa primero asignado el mensaje a lblMensaje de no tener nada se coloca Se guardo correctamente */
function Mensaje(visible) {
    var mensaje = document.getElementById('dvMensajeGuardar');
    var texto = document.getElementById('lblMensaje');
    var tiempo;
    if (visible) {
        mensaje.style.display = 'block';
        if (texto.innerHTML == "") {
            texto.innerHTML = "Se guardo correctamente";
        }
        tiempo = setTimeout("Mensaje(false)", 10000);
    }
    else {
        mensaje.style.display = 'none';
    }
}

function pageLoad(){
    $(function () {
        $('.lnkNegro').poshytip({
            className: 'tip-darkgray',
            bgImageFrameSize: 11,
            alignY: 'bottom',
            offsetX: -25
        });
        $('.imgPequeñaMenu').poshytip({
            className: 'tip-darkgray',
            bgImageFrameSize: 11,
            alignY: 'bottom',
            offsetX: -25
        });
    });
}

function UpdateGrdDocumentos(elem) {
    $($(elem).closest('tr').find('td')[2]).html('Si');
    var destinationURL = window.location.href + "?NumeroFormulario=" + $($(elem).closest('tr').find('td')[1]).html();
    $(elem).attr('onclick', 'return false');
    return false;
}


function getNameURLWeb() {
    var sPath = window.location.pathname;
    var sPage = sPath.substring(sPath.lastIndexOf('/') + 1);
    return sPage;
}