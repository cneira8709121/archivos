$(document).ready(function () {

    //$('#btnAdd').click(CargarTareas);

    //    $('#btnAdd').click(function () {
    //        //$.blockUI({ message: '<h1> Processing...</h1>' });
    //        var ControlName = "~/ListaTareas/UCTarea.ascx";
    //        $.ajax({
    //            type: "POST",
    //            url: "TestPage.aspx/Adicionar",
    //            data: "{ controlName:'" + ControlName + "'}",
    //            contentType: "application/json; charset=utf-8",
    //            dataType: "json",
    //            success: function (response) {
    //                //$.unblockUI();
    //                $('#divTareas').append(response.d);
    //                //$('#divTareas').html('con esto otro');
    //            },
    //            error: function (msg) {
    //                //$.unblockUI();
    //                alert(msg);
    //                $('#divTareas').html(msg.d);
    //            }
    //        });
    //        return false;
    //    });

    var count = 1;
    if (count == 1) {
        CargarTareas(count);
        count++;
    }

    $(window).scroll(function () {
        if ($(window).scrollTop() == $(document).height() - $(window).height()) {
            //alert('More data ' + count);
            CargarTareas(count);
            count++;
        }
    });

});

function CargarTareas(count) {
    //$.blockUI({ message: '<h1> Processing...</h1>' }); url: "Test/TestPage.aspx/Adicionar",
    var ControlName = "~/ListaTareas/UCTarea.ascx";
    var strFiltro = $("input[id$=HFFiltroPor]").val();
    var strOrden = $("input[id$=HFOrdenPor]").val();
    $.ajax({
        type: "POST",
        url: "Default.aspx/Adicionar",
        data: "{ controlName:'" + ControlName + "', count:'" + count + "', strFilter:'" + strFiltro + "', strOrder:'" + strOrden + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (response) {
            //$.unblockUI();
            $('#divTareas').append(response.d);
            //$('#divTareas').append("<div style='width: 250px; height: 70px; text-align: center;' id='MainContent_UCListaTareas1_UC1_pnlFormulario' class='tarea'><span style='font-size: large; font-weight: bold; text-decoration: none;' id='MainContent_UCListaTareas1_UC1_lblFormulario' class='lbl'>AF0000130047</span>&nbsp;&nbsp;<input id='MainContent_UCListaTareas1_UC1_imgTrabajar' name='ctl00$MainContent$UCListaTareas1$UC1$imgTrabajar' src='App_Themes/RUVTheme/Imagenes/Trabajar.png' type='image'><div style='text-align: left;'><span id='MainContent_UCListaTareas1_UC1_lblEstado' class='lbl'>Valoración Pendiente Por Revisión</span><br><span id='MainContent_UCListaTareas1_UC1_lblFecha' class='lbl'>20/12/2012</span>    </div></div>");
            //$('#pnlTareasPendientes').append(response.d);
            //$('#pnlTareasPendientes').html("<b>Hello world!</b>");
            //$("#__VIEWSTATE").remove();
            //$("#__EVENTVALIDATION").remove();
            //$(":hidden").remove();
            //$('').html($(""));
            $('#divTareas :hidden').remove();
            $('#divTareas #__VIEWSTATE').remove();
        },
        error: function (msg) {
            //$.unblockUI();
            //alert(msg);
            $('#divTareas').html(msg.d);
        }
    });
    return false;
}