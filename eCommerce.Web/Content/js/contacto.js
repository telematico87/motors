
$(document).ready(function () {
      
   


});

$("#frmContacto").submit(function (event) {
    debugger;
    GuardarContacto();
    event.preventDefault();
})

$("#frmContacto").validate({
    errorClass: "alert alert-danger",
    errorElement: "div",
    rules: {
        Nombre: {
            required: true,
            minlength: 1,
            maxlength: 50
        },
        Email: {
            required: true,
            minlength: 1,
            maxlength: 50
        },
        Asunto: {
            required: true,
            minlength: 1,
            maxlength: 100
        },
        Mensaje: {
            required: true,
            minlength: 1,
            maxlength: 1000
        },

    },
    messages: {
        Nombre: {
            required: "Nombre Requerido",
            minlength: "Minimo 1",
            maxlength: "Máximo 50"
        },
        Email: {
            required: "Email Requerido",
            minlength: "Minimo 1",
            maxlength: "Máximo 50"
        },
        Asunto: {
            required: "Asunto Requerido",
            minlength: "Minimo 1",
            maxlength: "Máximo 50"
        },
        Mensaje: {
            required: "Mensaje Requerido",
            minlength: "Minimo 1",
            maxlength: "Máximo 100"
        }


    },
    highlight: function (element, errorClass) {
        $(element).removeClass(errorClass);
    }
});

function GuardarContacto() {
    debugger;
    $(".errorsRow .errorMessages", "#frmContacto").html("");
    $(".errorsRow", "#frmContacto").hide(200);
    $(".errorsRow .errorMessages", "#frmContacto").hide();

    if ($("#frmContacto").valid()) {
        var Nombre = $("#txtNombre").val();
        var Email = $("#txtEmail").val();
        var Asunto = $("#txtAsunto").val();
        var Mensaje = $("#txtMensaje").val();

        var model = new Object();
        model.Nombre = Nombre;
        model.Email = Email;
        model.Asunto = Asunto;
        model.Mensaje = Mensaje;

        $.ajax({
            method: "POST",
            url: "/Contacto/GuardarContacto",
            data: {
                model
            },
            success: function (result) {
                debugger;
                Swal.fire("Bien Hecho!", 'Enviado Correctamente', "success");
                window.location.href = '@Url.Home()';
            }

        });

    }
}

