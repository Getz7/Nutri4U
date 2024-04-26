function checkUserRoleForPage(allowedRole) {
    let userRole = localStorage.getItem("rol");

    console.log(userRole);
    if (userRole === null) {
        showSwalAlert("Error", "No tienes Acceso", "", "error")
        setTimeout(function () {
            window.location.href = '/LandingPage/LandingPage';
        }, 1000);

    } else if (userRole !== allowedRole && userRole !== "cliente") {
        showSwalAlert("Error", "No tienes Acceso", "", "error")
        setTimeout(function () {
            window.location.href = '/LandingPage/LandingPage';
        }, 1000);
    }
}


document.addEventListener("DOMContentLoaded", function () {



    let allowedRole = "cliente";

    checkUserRoleForPage(allowedRole);



});


$(document).ready(function () {
    var currentUrl = window.location.href;
    var id = getLastPartOfUrl(currentUrl);
    console.log("Id:", id);
    $.ajax({

    headers:
        {
            'Accept': "application/json",
            'Content-Type': "application/json"
        },
        url: "https://localhost:7187/api/User/GetUserById?id=" + id,
        method: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        hasContent: true,
        success: function(user) {
            console.log(user, "user")
            $('#nombreUsuario').val(user.name);
            $('#cedula').val(user.idCard);
            $('#apellido').val(user.lastname);
            $('#correo').val(user.email);
            $('#edad').val(user.age);
            pass = user.password
            rol = user.role
        },
        error: function(error) {
            console.log("error", error)
        }
    });
});
function getLastPartOfUrl(url)
{
    var parts = url.split('/');
    return parts[parts.length - 1];
}
function checkBlankInputs()
{

    var inputs = document.querySelectorAll('.required');


    var hasEmptyInput = false;


    inputs.forEach(function(input) {

        if (input.value.trim() === '')
        {

            input.classList.add('input-error');
            hasEmptyInput = true;
        }
        else
        {

            input.classList.remove('input-error');
        }
    });

return hasEmptyInput;
}
function showSwalAlert(title, text, redirectionUrl, icon = 'success', confirmButtonText = 'Aceptar', cancelButtonText = '') {
    Swal.fire({
        title: title,
        text: text,
        icon: icon,
        confirmButtonText: confirmButtonText,
        cancelButtonText: cancelButtonText,
        showCancelButton: cancelButtonText != '',
        iconColor: 'white',
        color: 'white',
        background: icon === 'error' ? '#DB363B' : '#A5B463',
        confirmButtonColor: '#A5B463',
        customClass: {
            confirmButton: 'swal-button',
            cancelButton: 'swal-button-cancel'
        }
    }).then((result) => {
        if (redirectionUrl) {
            window.location.href = redirectionUrl
        }
    })
}
$("#btn-continue").click(function(e) {
    e.preventDefault()

    if (checkBlankInputs())
    {
        showSwalAlert("Campos incompletos", '', '', 'error')
    }
    else
    {
        let user = { }
        var currentUrl = window.location.href;
        var id = getLastPartOfUrl(currentUrl);
        user.Id = id;
        user.name = $("#nombreUsuario").val();
        user.idCard = $("#cedula").val();
        user.lastname = $("#apellido").val();
        user.email = $("#correo").val();
        user.age = $("#edad").val();
        user.role = rol;
        user.password = pass;

        console.log(user);

        $.ajax({
        headers:
            {
                'Accept': "application/json",
                'Content-Type': "application/json"
            },
            url: "https://localhost:7187" + "/api/User/Update",
            method: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            data: JSON.stringify(user),
            hasContent: true,
            success: function(data) {
                console.log(data)
                showSwalAlert("Usuario modificado con exito", "", "/Client/MenuCliente");
            },
            error: function(error) {
                console.log(error.errors)
                alert('Por favor, completa todos los campos');
            }
        });
    }
});
