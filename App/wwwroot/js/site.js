
let userPermissions = {
    Cliente: [
        "Client/MenuCLiente",
        "Client/PerfilCliente",
        "Client/UpdateUserCliente",
        "Client/ViewFoodClient"
    ],
    Nutricionista: [
        "Nutri/CreateFood",
        "Nutri/Menu",
        "Nutri/UpdateFood",
        "Nutri/UpdateUser",
        "Nutri/perfil",
        "Nutri/ViewClients",
        "Nutri/ViewFood",
        "Nutri/ViewSpecificFood"
    ]
}
function checkPageAccess(role, page) {
    if (!checkPagePermission(role, page)) {
        showSwalToast("No tienes permisos para acceder a esta página");
        // Redirect the user to a default page or handle unauthorized access as needed
        window.location.href = 'LandingPage/LandingPage';
    }
}
function showSwalToast(title, redirectionUrl, icon = 'success') {
    const Toast = Swal.mixin({
        toast: true,
        position: "top-end",
        showConfirmButton: false,
        timer: 3000,
        timerProgressBar: true,
        didOpen: (toast) => {
            toast.onmouseenter = Swal.stopTimer;
            toast.onmouseleave = Swal.resumeTimer;
        }
    });
    Toast.fire({
        icon: icon,
        title: title
    }).then(() => {
        if (redirectionUrl) {
            window.location.href = redirectionUrl
        }
    });
}

function checkPagePermission(role, page) {
    return userPermissions[getRoleName(role)] && userPermissions[getRoleName(role)].includes(page);
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



let currentRol = localStorage.getItem("rol");
let urlAdmin = "";
let urlCliente = "";
function validarUsuario(urlAdmin, urlCliente) {
    if (currentRol == "cliente") {
        return window.location.href = urlCliente;
    } else if (currentRol == "nutricionista") {
        return window.location.href = urlAdmin;
    }
}


