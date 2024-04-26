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



let products = []; // Declara products como una variable global
window.onload = function () {
    // Realizar la solicitud GET al API para obtener los productos
    fetch("https://localhost:7187/api/Food/RetrieveAll")
        .then(response => {
            if (!response.ok) {
                throw new Error('Error al obtener los productos');
            }
            return response.json();
        })
        .then(data => {
            products = data;
            displayProducts(products);
        })
        .catch(error => {
            console.error('Error:', error);
        });

    const filterButton = document.getElementById('filter-button');
    filterButton.addEventListener('click', filterProducts);
};

function displayProducts(products) {
    let productsContainer = document.getElementById("products-container");

    if (products && products.length > 0) {
        productsContainer.innerHTML = "";

        products.forEach(product => {
            let productDiv = document.createElement("div");
            productDiv.classList.add("product");

            let productImageDiv = document.createElement("div"); // Creamos un div para la imagen
            productImageDiv.classList.add("product-image");

            let productImage = document.createElement("img"); // Creamos un elemento de imagen
            productImage.src = product.photo; // Establecemos la fuente de la imagen
            productImage.alt = product.nombre; // Establecemos el texto alternativo de la imagen
            productImageDiv.appendChild(productImage); // Añadimos la imagen al div de la imagen
            productDiv.appendChild(productImageDiv); // Añadimos el div de la imagen al producto

            let productDetailsDiv = document.createElement("div"); // Creamos un div para los detalles
            productDetailsDiv.classList.add("product-details");

            let productName = document.createElement("h3");
            productName.textContent = product.nombre;

            let detailButton = document.createElement("h4");
            detailButton.classList.add("detail-button");
            detailButton.textContent = "Mas Detalle";

            detailButton.addEventListener("click", function () {
                // Obtener el ID de producto del atributo personalizado
                let productId = product.id;

                // Llamar a la función deseada con el ID de producto como parámetro
                SpecificFood(productId);
            });

            productDetailsDiv.appendChild(productName);
            productDetailsDiv.appendChild(detailButton);
            productDiv.appendChild(productDetailsDiv); // Añadimos el div de los detalles al producto

            productsContainer.appendChild(productDiv);

            productDiv.addEventListener("mouseenter", function () {
                productDiv.classList.add("hovered");
            });

            productDiv.addEventListener("mouseleave", function () {
                productDiv.classList.remove("hovered");
            });
        });
    } else {
        productsContainer.innerHTML = "<p>No se encontraron productos.</p>";
    }
}

function SpecificFood(productId) {
    window.location.href = '/Client/ViewSpecificFoodClient/' + productId
}

// Función para filtrar productos
function filterProducts() {
    // Obtener todas las marcas seleccionadas
    const selectedBrands = [];
    document.querySelectorAll('.brand-checkbox').forEach(input => {
        if (input.checked) {
            selectedBrands.push(input.id);
        }
    });

    // Obtener todas las categorías seleccionadas
    const selectedCategories = [];
    document.querySelectorAll('.category-checkbox').forEach(input => {
        if (input.checked) {
            selectedCategories.push(input.id);
        }
    });

    let filteredProducts; // Declarar la variable fuera de las condiciones para que esté disponible después


    if (selectedBrands.length != 0) {
        filteredProducts = products.filter(product => {
            return selectedBrands.includes(product.marca);
        });
    } else {
        filteredProducts = products;
    }
    if (selectedCategories.length != 0) {
        filteredProducts = filteredProducts.filter(product => {
            return selectedCategories.includes(product.nombreCategoria);
        });
    }

    // Mostrar los productos filtrados
    displayProducts(filteredProducts);
}
