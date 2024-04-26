
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

$(document).ready(function () {
    var currentUrl = window.location.href;
    var id = getLastPartOfUrl(currentUrl);
    console.log("Id:", id);
    $.ajax({

        headers: {
            'Accept': "application/json",
            'Content-Type': "application/json"
        },
        url: "https://localhost:7187/api/Food/GetFoodById?id=" + id,
        method: "GET",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        hasContent: true,
        success: function (result) {
            console.log(result, "Result")
            $("#txt-brand").val(result.marca);
            $("#categoryType").val(result.nombreCategoria);
            $("#txt-name").val(result.nombre);
            $("#portionGrams").val(result.porcionGramos);
            $("#portionHome").val(result.porcionCasera);
            $("#excFlour").val(result.intercambioHarina);
            $("#excDairySkim").val(result.intercambioLacteoDescremado);
            $("#excDairySemi").val(result.intercambioLacteoSemi);
            $("#excDairyWhole").val(result.intercambioLacteoEntero);
            $("#excFat").val(result.intercambioGrasa);
            $("#excLeanMeat").val(result.intercambioCarneMagra);
            $("#excSemiLeanMeat").val(result.intercambioCarneSemi);
            $("#excFatMeat").val(result.intercambioCarneGrasa);
            $("#excLeguminous").val(result.intercambioLeguminosa);
            $("#excSugar").val(result.intercambioAzucar);
            $("#excFruit").val(result.intercambioFruta);
            $("#excVegetable").val(result.intercambioVegetal);
            var excFreeValue = result.intercambioLibre;
            var excFreeText = excFreeValue ? "1" : "0";
            $("#excFree").val(excFreeText);
            $("#image").val(result.photo);
        },
        error: function (error) {
            console.log("error", error)
        }
    });
});
function getLastPartOfUrl(url) {
    var parts = url.split('/');
    return parts[parts.length - 1];
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
        var currentUrl = window.location.href;
        var id = getLastPartOfUrl(currentUrl);
        food.Id = id;
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
            url: "https://localhost:7187" + "/api/Food/Update",
            method: "POST",
            contentType: "application/json;charset=utf-8",
            dataType: "json",
            data: JSON.stringify(food),
            hasContent: true,
            success: function (data) {
                console.log(data)
                showSwalAlert("Alimento modificado con exito", "", "/Nutri/Menu");
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
