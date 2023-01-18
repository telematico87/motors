$(document).ready(function () {

    listarMarcas();
    listarEstadoCivil();
    listaTipoVivienda();
    listarTipoDocumento();
    listaRangoIngreso();
    listaMontoFinanciar();
    listaInteresCompra();
    listaDepartamento();
    listaAntiguedadLaboral();
    listaSituacionLaboral();
    listaFinanciera();
    ValidaSoloTexto();
    ValidaSoloNumeros();    

    $("#frmEfectiva").submit(function (event) {
        GuardarFinanciamiento();
        event.preventDefault();
    });

    $("#cboMarca").change(function () {
        idMarca = $(this).find(':selected').data('id')
        listarProductosPorMarca(idMarca);
    });

    $("#cboModelo").change(function () {
        let price = $(this).find(':selected').data('price')
        $("#txtPrecio").val(price);
        $("#txtMontoFinanciar").val(price);
    });

    $("#txtMontoInicial").keyup(function () {

        calcularMontoAFinanciar();
    });

    $("#txtMontoInicial").blur(function () {
        calcularMontoAFinanciar();
    });

    $("#cboDepartamento").on('change', function () {

        var cboDpto = $("#cboDepartamento").val();
        var splitdepart = cboDpto.split('|');
        var departamento = splitdepart[0];
        $.ajax({
            method: "GET",
            url: "/Financiamiento/listarProvincias",
            data: { departamento },
            success: function (result) {
                console.log(result);
                let Marca = result;
                llenarCombo(Marca, "cboProvincia", "NombreUbigeo", "NombreUbigeo", "0")
            }
        });

    });
});

function calcularMontoAFinanciar() {
    let price = $("#cboModelo").find(':selected').data('price')
    let inicial = $("#txtMontoInicial").val();
    if (inicial > price) {
        alertify.warning('Monto Inicial no puede ser mayor al precio total');
        return false;
    }

    let financiamiento = price - inicial;
    $("#txtMontoFinanciar").val(financiamiento);
};

function ValidaSoloTexto() {
    $("#txtNombre").bind('keypress', function (event) {
        var regex = new RegExp("^[a-zA-Z ]+$");
        var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
        if (!regex.test(key)) {
            event.preventDefault();
            return false;
        }
    });

    $("#txtApellido").bind('keypress', function (event) {
        var regex = new RegExp("^[a-zA-Z ]+$");
        var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
        if (!regex.test(key)) {
            event.preventDefault();
            return false;
        }
    });
}

function ValidaSoloNumeros() {
    $("#txtCelular").bind('keypress', function (event) {
        var regex = new RegExp("^[0-9]+$");
        var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
        if (!regex.test(key)) {
            event.preventDefault();
            return false;
        }
    });

    $("#txtNroDocumento").bind('keypress', function (event) {
        var regex = new RegExp("^[0-9]+$");
        var key = String.fromCharCode(!event.charCode ? event.which : event.charCode);
        if (!regex.test(key)) {
            event.preventDefault();
            return false;
        }
    });
}

function GuardarFinanciamiento() {

    var Nombre = $("#txtNombre").val();
    var Apellido = $("#txtApellido").val();
    var Celular = $("#txtCelular").val();
    var Correo = $("#txtCorreo").val();
    var FechaNacimiento = $("#txtFechaNacimiento").val();
    var TipoDocumento = $("#cboTipoDocumento").val();
    var NroDocumento = $("#txtNroDocumento").val();
    var EstadoCivil = $("#cboEstadoCivil").val();

    var InteresCompra = $("#cboInteresCompra").val();
    var Departamento = $("#cboDepartamento").val();
    var Provincia = $("#cboProvincia").val();
    var Distrito = 00;
    var TipoVivienda = $("#cboTipoVivienda").val();
    var SituacionLaboral = $("#cboSituacionLaboral").val();
    var AntiguedadLaboral = $("#cboAntiguedadLaboral").val();

    var IDMarca = $("#cboMarca").find(':selected').data('id');
    var Marca = $("#cboMarca").val();
    var IDModelo = $("#cboModelo").val();
    var Modelo = $("#cboModelo").find('option:selected').text();
    var IngresoNeto = $("#txtIngresoNeto").val();
    var MontoInicial = $("#txtMontoInicial").val();
    var Ocupacion = $("#txtOcupacion").val();
    var TipoFinanciera = $("#cboTipoFinanciera").val();

    var PoliticaPrivacidad = document.getElementById('PoliticaPrivacidad').checked;
    var AceptoComunicaciones = document.getElementById('PoliticaPrivacidad').checked;

    if (Nombre == "" || Nombre.length == 0) {
        alertify.warning('Ingrese Nombres');
        return false;
    } else if (Apellido == "" || Apellido.length == 0) {
        alertify.warning('Ingrese Apellidos');
        return false;
    } else if (Celular == "" || Celular.length == 0) {
        alertify.warning('Ingrese Nro de Celular');
        return false;
    } else if (Correo == "" || Correo.length == 0) {
        alertify.warning('Ingrese Correo Electrónico');
        return false;
    } else if (FechaNacimiento == "") {
        alertify.warning('Ingrese Fecha de Nacimiento');
        return false;
    } else if (NroDocumento == "" || NroDocumento.length == 0) {
        alertify.warning('Ingrese Nro de Documento');
        return false;
    } else if (EstadoCivil == "0") {
        alertify.warning('Seleccione Estado Civil');
        return false;
    } else if (InteresCompra == "0") {
        alertify.warning('Seleccione Interés de Compra');
        return false;
    } else if (Departamento == "0") {
        alertify.warning('Seleccione Departamento');
        return false;
    } else if (Provincia == "0") {
        alertify.warning('Seleccione Provincia');
        return false;
    } else if (TipoVivienda == "0") {
        alertify.warning('Seleccione Tipo de Vivienda');
        return false;
    } else if (SituacionLaboral == "0") {
        alertify.warning('Seleccione Situacion Laboral');
        return false;
    } else if (AntiguedadLaboral == "0") {
        alertify.warning('Seleccione Antiguedad Laboral');
        return false;
    } else if (IDMarca == "0") {
        alertify.warning('Seleccione Marca');
        return false;
    } else if (IDModelo == "0") {
        alertify.warning('Seleccione Modelo');
        return false;
    } else if (IngresoNeto == "0") {
        alertify.warning('Ingrese Ingreso Neto');
        return false;
    } else if (MontoInicial == "0") {
        alertify.warning('Ingrese Monto Inicial');
        return false;
    } else if (Ocupacion == "" || Ocupacion.length == 0) {
        alertify.warning('Ingrese Ocupación');
        return false;
    } else if (!PoliticaPrivacidad) {
        alertify.warning('Debe aceptar nuestras políticas de privacidad');
        return false;
    }
    else {

        var model = new Object();
        model.Nombre = Nombre;
        model.Apellido = Apellido;
        model.Celular = Celular;
        model.Correo = Correo;
        model.FechaNacimiento = FechaNacimiento;
        model.TipoDocumento = TipoDocumento;
        model.NroDocumento = NroDocumento;
        model.SituacionSentimental = EstadoCivil;

        model.InteresCompra = InteresCompra;
        model.Departamento = Departamento;
        model.Provincia = Provincia;
        model.Distrito = Distrito;
        model.TipoVivienda = TipoVivienda;
        model.IDSituacionLaboral = SituacionLaboral;
        model.AntiguedadLaboral = AntiguedadLaboral;

        model.IDMarca = IDMarca;
        model.Marca = Marca;
        model.IDModelo = IDModelo;
        model.Modelo = Modelo;
        model.IngresoNeto = IngresoNeto;
        model.MontoInicial = MontoInicial;

        model.Ocupacion = Ocupacion
        model.TipoFinanciera = TipoFinanciera;
        model.PoliticaPrivacidad = PoliticaPrivacidad;
        model.AceptoComunicaciones = AceptoComunicaciones;

        $.ajax({
            method: "POST",
            url: "/Financiamiento/GuardarFinanciamiento",
            data: {
                model
            },
            success: function (result) {

                Swal.fire("Gracias por tu Preferencia!", 'Enviado Correctamente', "success");
                window.location.href = "/Financiamiento/PaginaPrincipal";
            }

        });
    }
}

function listarMarcas() {
    $.ajax({
        method: "GET",
        url: "/Financiamiento/listarMarca",
        success: function (result) {
            let Marca = result;
            llenarMarcas(Marca, "cboMarca", "Descripcion", "ID", "0")
        }
    });
}

function listarProductosPorMarca(idMarca) {
    $.ajax({
        method: "GET",
        url: "/Products/productosByMarcaID?idMarca=" + idMarca,
        success: function (result) {
            let Modelos = result;
            llenarModels(Modelos, "cboModelo", "Name", "Id", "Money", "Price", "0")
        }
    });
}

function listarEstadoCivil() {
    $.ajax({
        method: "GET",
        url: "/Financiamiento/listarEstadoCivil",
        success: function (result) {
            let Marca = result;
            llenarCombo(Marca, "cboEstadoCivil", "Valor", "Valor", "0")
        }
    });
}

function listarTipoDocumento() {
    $.ajax({
        method: "GET",
        url: "/Financiamiento/listaTipoDocumento",
        success: function (result) {
            let tipoDocumento = result;
            llenarCombo(tipoDocumento, "cboTipoDocumento", "Valor", "Valor", "0")
        }
    });
}

function listaTipoVivienda() {
    $.ajax({
        method: "GET",
        url: "/Financiamiento/listaTipoVivienda",
        success: function (result) {
            let Marca = result;
            llenarCombo(Marca, "cboTipoVivienda", "Valor", "Codigo", "0")
        }
    });
}

function listaRangoIngreso() {
    $.ajax({
        method: "GET",
        url: "/Financiamiento/listaRangoIngreso",
        success: function (result) {
            let Marca = result;
            llenarCombo(Marca, "cboRangoIngresos", "Valor", "Codigo", "0")
        }
    });
}

function listaAntiguedadLaboral() {
    $.ajax({
        method: "GET",
        url: "/Financiamiento/listarAntiguedadLaboral",
        success: function (result) {
            let Marca = result;
            llenarCombo(Marca, "cboAntiguedadLaboral", "Valor", "Codigo", "0")
        }
    });
}

function listaSituacionLaboral() {
    $.ajax({
        method: "GET",
        url: "/Financiamiento/listaSituacionLaboral",
        success: function (result) {
            let SituacionLaboral = result;
            llenarCombo(SituacionLaboral, "cboSituacionLaboral", "Valor", "Codigo", "0")
        }
    });
}

function listaFinanciera() {
    $.ajax({
        method: "GET",
        url: "/Financiamiento/listarFinanciera",
        success: function (result) {
            let Marca = result;
            llenarFinanciera(Marca, "cboTipoFinanciera", "Valor", "Codigo", "0")
        }
    });
}

function listaInteresCompra() {
    $.ajax({
        method: "GET",
        url: "/Financiamiento/listaInteresCompra",
        success: function (result) {
            let Marca = result;
            llenarCombo(Marca, "cboInteresCompra", "Valor", "Codigo", "0")
        }
    });
}

function listaMontoFinanciar() {
    $.ajax({
        method: "GET",
        url: "/Financiamiento/listaMontoFinanciar",
        success: function (result) {
            let Marca = result;
            llenarCombo(Marca, "cboMontoFinanciar", "Valor", "Codigo", "0")
        }
    });
}

function listaDepartamento() {
    $.ajax({
        method: "GET",
        url: "/Financiamiento/listarDepartamentos",
        success: function (result) {
            let lista = result;
            llenarComboDpto(lista, "cboDepartamento", "NombreUbigeo", "CodDep", "0")
        }
    });
}

function llenarComboDpto(data, id, propiedadMostrar, propiedadId, valueDefecto = "") {

    var contenido = ""
    var elemento;
    contenido += "<option value='" + valueDefecto + "'>--Seleccionar--</option>"
    for (var j = 0; j < data.length; j++) {
        elemento = data[j];
        contenido +=
            "<option value='" + elemento[propiedadId] + "|" + elemento[propiedadMostrar] + "'  >" + elemento[propiedadMostrar] + "</option>"
    }
    contenido += "";
    document.getElementById(id).innerHTML = contenido;
}

function llenarCombo(data, id, propiedadMostrar, propiedadId, valueDefecto = "") {

    var contenido = ""
    var elemento;
    contenido += "<option value='" + valueDefecto + "'>--Seleccionar--</option>"
    for (var j = 0; j < data.length; j++) {
        elemento = data[j];
        contenido +=
            "<option value='" + elemento[propiedadId] + "' >" + elemento[propiedadMostrar] + "</option>"
    }
    contenido += "";
    document.getElementById(id).innerHTML = contenido;
}

function llenarMarcas(data, id, propiedadMostrar, propiedadId, valueDefecto = "") {

    var contenido = ""
    var elemento;
    contenido += "<option data-id='0' value='" + valueDefecto + "'>--Seleccionar--</option>"
    for (var j = 0; j < data.length; j++) {
        elemento = data[j];
        contenido +=
            "<option data-id='" + elemento[propiedadId] + "' value='" + elemento[propiedadMostrar] + "' >" + elemento[propiedadMostrar] + "</option>"
    }
    contenido += "";
    document.getElementById(id).innerHTML = contenido;
}

function llenarModels(data, id, propiedadMostrar, propiedadId, money, price, valueDefecto = "") {

    var contenido = ""
    var elemento;
    contenido += "<option data-money='" + money + "' data-price='0' value='" + valueDefecto + "'>--Seleccionar--</option>"
    for (var j = 0; j < data.length; j++) {
        elemento = data[j];
        contenido +=
            "<option data-money='" + elemento[money] + "' data-price='" + elemento[price] + "' value='" + elemento[propiedadId] + "' >" + elemento[propiedadMostrar] + "</option>"
    }
    contenido += "";
    document.getElementById(id).innerHTML = contenido;
}

function llenarFinanciera(data, id, propiedadMostrar, propiedadId, valueDefecto = "") {

    var contenido = ""
    var elemento;
    for (var j = 0; j < data.length; j++) {
        elemento = data[j];
        contenido +=
            "<option value='" + elemento[propiedadId] + "' >" + elemento[propiedadMostrar] + "</option>"
    }
    contenido += "";
    document.getElementById(id).innerHTML = contenido;
}