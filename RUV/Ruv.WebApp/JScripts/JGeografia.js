var code = getNameURLWeb() + "/ObtenerGeografia";
function loadGeografia(ddlpais, ddlDpto) {
    loadPaises(ddlpais, ddlDpto);
}

function CambioPais(ddlpais, ddlDpto, hfPais) {
    var pais = $("#" + ddlpais).val();
    $("#" + hfPais).val(pais);
    loadDeptoPorPais(pais, ddlDpto);
}

function CambioDpto(ddlDpto, ddlMun, hfDpto) {
    var dpto = $("#" + ddlDpto).val();
    $("#" + hfDpto).val(dpto);
    loadMunicipioPorDpto(dpto, ddlMun);
}

function CambioMun(ddlMun, hfMun) {
    var munId = $("#" + ddlMun).val();
    $("#" + hfMun).val(munId);
}

function loadPaises(ddlpais, ddlDpto) {
    var items = "";
    items = "<option value='0'>[Seleccione un Pais]</option>"
    $.ajax({
        type: "POST",
        url: code,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: "{ padreId: '0', nivel: '1' }",
        success: function (response) {
            $.each(response.d, function (index, pais) {
                if (pais.Id != null) {
                    items += "<option value=" + pais.Id + ">" + pais.Nombre + "</option>";
                }
            });
            $("#" + ddlpais).html(items);
            $("#" + ddlpais).val('48');
            loadDeptoPorPais('48', ddlDpto);
        },
        error: function (result) {
            alert('Ocurrio un error cargando los Paises');
        }
    });
}

function loadDeptoPorPais(pais, ddlDpto) {
    var items = "";
    items = "<option value='0'>[Seleccione un Departamento]</option>"
    $.ajax({
        type: "POST",
        url: code,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: "{ padreId: '" + pais + "', nivel: '2' }",
        success: function (response) {
            $.each(response.d, function (index, departamento) {
                if (departamento.Id != null) {
                    items += "<option value=" + departamento.Id + ">" + departamento.Nombre + "</option>";
                }
            });
            $('#' + ddlDpto).html(items);
        },
        error: function (result) {
            alert('Ocurrio un error cargando los Departamentos');
        }
    });
}

function loadMunicipioPorDpto(dpto, ddlMun) {
    var items = "";
    items = "<option value='0'>[Seleccione un Municipio]</option>"
    $.ajax({
        type: "POST",
        url: code,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: "{ padreId: '" + dpto + "', nivel: '3' }",
        success: function (response) {
            $.each(response.d, function (index, municipio) {
                if (municipio.Id != null) {
                    items += "<option value=" + municipio.Id + ">" + municipio.Nombre + "</option>";
                }
            });
            $('#' + ddlMun).html(items);
        },
        error: function (result) {
            alert('Ocurrio un error cargando los Municipios');
        }
    });
}

function setGeografia(munId, dptoId, paisId, ddlMun, ddlDpto, ddlPais) {
    loadAndSetPais(munId, dptoId, paisId, ddlMun, ddlDpto, ddlPais);
}

function loadAndSetPais(munId, dptoId, paisId, ddlMun, ddlDpto, ddlPais) {
    var items = "";
    items = "<option value='0'>[Seleccione un Pais]</option>"
    $.ajax({
        type: "POST",
        url: code,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: "{ padreId: '0', nivel: '1' }",
        success: function (response) {
            $.each(response.d, function (index, pais) {
                if (pais.Id != null) {
                    items += "<option value=" + pais.Id + ">" + pais.Nombre + "</option>";
                }
            });
            $("#" + ddlPais).html(items);
            $("#" + ddlPais).val(paisId);
            $("#hfPais" + ddlPais).val(paisId);
            loadAndSetDpto(paisId, dptoId, munId, ddlDpto, ddlMun);
        },
        error: function (result) {
            alert('Ocurrio un error cargando los Paises');
        }
    });
}

function loadAndSetDpto(paisId, dptoId, munId, ddlDpto, ddlMun) {
    var items = "";
    items = "<option value='0'>[Seleccione un Departamento]</option>"
    $.ajax({
        type: "POST",
        url: code,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: "{ padreId: '" + paisId + "', nivel: '2' }",
        success: function (response) {
            $.each(response.d, function (index, departamento) {
                if (departamento.Id != null) {
                    items += "<option value=" + departamento.Id + ">" + departamento.Nombre + "</option>";
                }
            });
            $("#" + ddlDpto).html(items);
            $("#" + ddlDpto).val(dptoId);
            $("#hfDpto" + ddlDpto).val(dptoId);
            loadAndSetMun(dptoId, munId, ddlMun);
        },
        error: function (result) {
            alert('Ocurrio un error cargando los Departamentos');
        }
    });
}

function loadAndSetMun(DptoId, MunId, ddlMun) {
    var items = "";
    items = "<option value='0'>[Seleccione un Municipio]</option>"
    $.ajax({
        type: "POST",
        url: code,
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        data: "{ padreId: '" + DptoId + "', nivel: '3' }",
        success: function (response) {
            $.each(response.d, function (index, municipio) {
                if (municipio.Id != null) {
                    items += "<option value=" + municipio.Id + ">" + municipio.Nombre + "</option>";
                }
            });
            $("#" + ddlMun).html(items);
            $("#" + ddlMun).val(MunId);
            $("#hfMun" + ddlMun).val(MunId);
        },
        error: function (result) {
            alert('Ocurrio un error cargando los Municipios');
        }
    });
}

function ClearGoegrafia(ddlPais, ddlDepto, ddlMun) {
    loadPaises(ddlPais, ddlDepto);
    var itemsDpto = "";
    itemsDpto = "<option value='0'>[Seleccione un Departamento]</option>"
    $("#" + ddlDepto).html(itemsDpto);
    var itemsMun = "";
    itemsMun = "<option value='0'>[Seleccione un Municipio]</option>"
    $("#" + ddlMun).html(itemsMun);
}