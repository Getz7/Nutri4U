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


let idUser = localStorage.getItem("id");
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
            populateContent(result)
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
// Define a mapping object for custom display names
var keyDisplayNames = {
    'marca': 'Marca',
    'nombreCategoria': 'Categoría',
    'porcionGramos': 'Porción en Gramos',
    'porcionCasera': 'Porción Casera',
    'intercambioHarina': 'Intercambio Harina',
    'intercambioLacteoDescremado': 'Intercambio Lacteo Descremado',
    'intercambioLacteoSemi': 'Intercambio Lacteo Semi-Descremado',
    'intercambioLacteoEntero': 'Intercambio Lacteo Entero',
    'intercambioGrasa': 'Intercambio Grasa',
    'intercambioCarneMagra': 'Intercambio Carne Magra',
    'intercambioCarneSemi': 'Intercambio Carne Semi-Magra',
    'intercambioCarneGrasa': 'Intercambio Carne Grasa',
    'intercambioLeguminosa': 'Intercambio Leguminosa',
    'intercambioFruta': 'Intercambio Fruta',
    'intercambioAzucar': 'Intercambio Azucar',
    'intercambioVegetal': 'Intercambio Vegetal',
    'intercambioLibre': 'Intercambio Libre'
};

function populateContent(data) {
    var photoDiv = document.getElementById('photo');
    var infoDiv = document.getElementById('info');

    if (photoDiv && infoDiv) {
        var img = document.createElement('img');
        img.src = data.photo;
        img.alt = "Product Photo";
        img.classList.add("photo"); 

        // Appending the img element to the photoDiv
        photoDiv.innerHTML = ''; // Clearing any existing content
        photoDiv.appendChild(img);

        // Creating a div for the heading
        var headingDiv = document.createElement('div');

        // Creating a heading element
        var heading = document.createElement('h1');
        heading.textContent = data.nombre;

        // Appending the heading to the headingDiv
        headingDiv.appendChild(heading);

        // Creating a div for the list
        var listDiv = document.createElement('div');

        // Creating a list element for displaying other attributes
        var list = document.createElement('ul');

        // Iterating over the attributes in data and creating list items for each
        for (var key in data) {
            if (data.hasOwnProperty(key) && key !== 'photo' && key !== 'nombre' && data[key] !== 0) {
                var value = data[key];
                if (key === 'id') continue;
                if (value == '0') continue;
                if (key === 'intercambioLibre') {
                    value = value ? 'Yes' : 'No';
                }
                var displayName = keyDisplayNames[key] || key; // Use custom display name if available, otherwise use key
                var listItem = document.createElement('li');
                listItem.textContent = displayName + ': ' + value;
                list.appendChild(listItem);
            }
        }

        // Appending the list to the listDiv
        headingDiv.appendChild(list);

        // Appending the headingDiv and listDiv elements to the infoDiv
        infoDiv.innerHTML = ''; // Clearing any existing content
        infoDiv.appendChild(headingDiv);
    } else {
        console.log("One or both of the elements with IDs 'photo' and 'info' do not exist.");
    }
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

var btnModify = document.getElementById("btn-modify");
btnModify.addEventListener("click", function () {

    var currentUrl = window.location.href;
    var idAlimento = getLastPartOfUrl(currentUrl);

    let Userfood = {}
    Userfood.idUsuario = idUser;
    Userfood.idAlimento = idAlimento;

    $.ajax({
        headers: {
            'Accept': "application/json",
            'Content-Type': "application/json"
        },
        url: "https://localhost:7187" + "/api/User/CreateUserFood",
        method: "POST",
        contentType: "application/json;charset=utf-8",
        dataType: "json",
        data: JSON.stringify(Userfood),
        hasContent: true,
        success: function (data) {
            console.log(data)
            showSwalAlert("Alimento agregado con exito", "", "/Client/MenuCliente");
        },
        error: function (error) {
            console.log(error.errors)
            alert('Por favor, completa todos los campos');
        }
    });

});