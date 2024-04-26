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

$("#btn-continue").click(function (e) {
    e.preventDefault()

    if (checkBlankInputs()) {
        showSwalAlert("Campos incompletos", '', '', 'error')
    } else {
        let user = {}
        user.idCard = $("#txt-cedula").val();
        user.name = $("#txt-name").val();
        user.lastname = $("#txt-lastName").val();
        user.age = parseInt($("#txt-age").val());
        user.role = $("#roleType").val();
        user.email = $("#text-email").val();
        user.password = $("#text-password").val();

        console.log(user);

        $.ajax({
            headers: {
                'Accept': "application/json",
                'Content-Type': "application/json"
            },
            url: "https://localhost:7187" + "/api/User/Create",
            method: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            data: JSON.stringify(user),
            hasContent: true,
            success: function (data) {
                console.log(data)
                showSwalAlert("Usuario registrado con exito", "", "/LandingPage/LandingPage");
            },
            error: function (error) {
                console.log(error.errors)
                alert('Por favor, completa todos los campos');
            }
        });
    }
});