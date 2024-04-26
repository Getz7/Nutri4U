function checkUserRoleForPage(allowedRole) {
    let userRole = localStorage.getItem("rol");

    console.log(userRole);
    if (userRole === null) {
        showSwalAlert("Error", "No tienes Acceso", "", "error")
        setTimeout(function () {
            window.location.href = '/LandingPage/LandingPage';
        }, 1000);

    } else if (userRole !== allowedRole && userRole !== "nutricionista") {
        showSwalAlert("Error", "No tienes Acceso", "", "error")
        setTimeout(function () {
            window.location.href = '/LandingPage/LandingPage';
        }, 1000);
    }
}


document.addEventListener("DOMContentLoaded", function () {



    let allowedRole = "nutricionista";

    checkUserRoleForPage(allowedRole);



});















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
        let food = {}
        food.marca = $("#txt-brand").val();
        food.nombreCategoria = $("#categoryType").val();
        food.nombre = $("#txt-name").val();
        food.porcionGramos = $("#portionGrams").val();
        food.porcionCasera = $("#portionHome").val();
        food.intercambioHarina = $("#excFlour").val();
        food.intercambioLacteoDescremado = $("#excDairySkim").val();
        food.intercambioLacteoSemi = $("#excDairySemi").val();
        food.intercambioLacteoEntero = $("#excDairyWhole").val();
        food.intercambioGrasa = $("#excFat").val();
        food.intercambioCarneMagra = $("#excLeanMeat").val();
        food.intercambioCarneSemi = $("#excSemiLeanMeat").val();
        food.intercambioCarneGrasa = $("#excFatMeat").val();
        food.intercambioLeguminosa = $("#excLeguminous").val();
        food.intercambioAzucar = $("#excSugar").val();
        food.intercambioFruta = $("#excFruit").val();
        food.intercambioVegetal = $("#excVegetable").val();
        food.intercambioLibre = Boolean(parseInt($("#excFree").val()));
        food.photo = $("#image").val();
       
        console.log(food);

        $.ajax({
            headers: {
                'Accept': "application/json",
                'Content-Type': "application/json"
            },
            url: "https://localhost:7187" + "/api/Food/Create",
            method: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            data: JSON.stringify(food),
            hasContent: true,
            success: function (data) {
                console.log(data)
                showSwalAlert("Alimento registrado con exito", "", "/Nutri/Menu");
            },
            error: function (error) {
                console.log(error.errors)
                alert('Por favor, completa todos los campos');
            }
        });
    }

    function isValidImageUrl(url) {
        // Regular expression for a basic URL validation
        var urlPattern = /^(https?:\/\/)?([\w.]+\/?)\S*$/;
        return urlPattern.test(url);
    }
});
