$("#btnLogin").click(function (e) {
    if (checkBlankInputs()) {
        showSwalAlert("Campos incompletos", '', '', 'error')
    } else {
        e.preventDefault()
        let user = {}
        let API_URL_BASE = "https://localhost:7187";
        user.correo = $("#txt-email").val();
        user.password = $("#txt-password").val();
        if (user.correo == '' || user.password == '') {
            console.log("Aqui va la alerta")
            return
        }

        $.ajax({

            headers: {
                'Accept': "application/json",
                'Content-Type': "application/json"
            },
            url: API_URL_BASE + "/api/User/Login?correo=" + user.correo + "&password=" + user.password,
            method: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            hasContent: true,
            success: function (result) {
                if (result.id != null && result.role === "nutricionista") {
                    localStorage.setItem("id", result.id);
                    localStorage.setItem("rol", result.role);
                    console.log(localStorage.setItem("rol", result.role))
                    window.location.href = '/Nutri/Menu';
                } else if (result.id != null && result.role === "cliente") {
                    localStorage.setItem("id", result.id);
                    localStorage.setItem("rol", result.role);
                    console.log(localStorage.setItem("rol", result.role))
                    window.location.href = '/Client/MenuCliente';
                } else if (result.identificationNumber == null && result.idRol === 0) {
                    showSwalToast("Verifique que los datos sean correctos!!", "", "error");
                }

            },
            error: function (error) {
                console.log("error", error)
            }
        });
    }
});

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
function checkBlankInputs() {

    var inputs = document.querySelectorAll('.required');


    var hasEmptyInput = false;


    inputs.forEach(function (input) {

        if (input.value.trim() === '') {

            input.classList.add('input-error');
            hasEmptyInput = true;
        } else {

            input.classList.remove('input-error');
        }
    });


    return hasEmptyInput;
}
