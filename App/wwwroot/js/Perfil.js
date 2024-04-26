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



let id = localStorage.getItem("id");
$.ajax({
    headers: {
        'Accept': "application/json",
        'Content-Type': "application/json"
    },
    url: "https://localhost:7187/api/User/GetUserById?id=" + id,
    method: "GET",
    contentType: "application/json;charset=utf-8",
    dataType: "json",
    hasContent: true,
    success: function (user) {
        $('#nombreUsuario').text(user.name);
        $('#cedula').text(user.idCard);
        $('#apellido').text(user.lastname);
        $('#correo').text(user.email);
        $('#edad').text(user.age);
    },
    error: function (error) {
        
    }
});

var btnModify = document.getElementById("btn-modify");
btnModify.addEventListener("click", function () {
    var id = localStorage.getItem("id");
    window.location.href = '/Nutri/UpdateUser/' + id

});
