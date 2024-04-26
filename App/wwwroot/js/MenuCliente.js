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



var slideIndex = 1;

// Function to show slides
function showSlides(n) {
    var i;
    var slides = document.getElementsByClassName("mySlides");
    var dots = document.getElementsByClassName("dot");

    if (n > slides.length) {
        slideIndex = 1;
    }

    if (n < 1) {
        slideIndex = slides.length;
    }

    for (i = 0; i < slides.length; i++) {
        slides[i].style.display = "none";
    }

    for (i = 0; i < dots.length; i++) {
        dots[i].className = dots[i].className.replace(" active", "");
    }

    slides[slideIndex - 1].style.display = "block";
    dots[slideIndex - 1].className += " active";
}

// Function to handle next/previous slides
function plusSlides(n) {
    showSlides(slideIndex += n);
}

// Function to handle current slide
function currentSlide(n) {
    showSlides(slideIndex = n);
}

// Initial slide show
showSlides(slideIndex);


$("#logout").click(function (e) {
    localStorage.clear();
    window.location.href = '/LandingPage/LandingPage';
});
