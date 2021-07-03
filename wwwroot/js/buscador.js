var getUrlParameter = function getUrlParameter(sParam) {
    var sPageURL = window.location.search.substring(1),
        sURLVariables = sPageURL.split('&'),
        sParameterName,
        i;

    for (i = 0; i < sURLVariables.length; i++) {
        sParameterName = sURLVariables[i].split('=');

        if (sParameterName[0] === sParam) {
            return typeof sParameterName[1] === undefined ? true : decodeURIComponent(sParameterName[1]);
        }
    }
    return false;
};

var titulo = getUrlParameter('titulo');
var director = getUrlParameter('director');
var ano = getUrlParameter('ano');
var genero = getUrlParameter('genero');
var order = getUrlParameter('order');

if (titulo !== false) {
    document.getElementById("titulo").value = titulo.replace("+", " ");
}

if (director !== false) {
    document.getElementById("director").value = director.replace("+", " ");
}

if (ano !== false) {
    document.getElementById("ano").value = ano.replace("+", " ");
}

if (order !== false) {
    document.getElementById("genero").value = genero.replace("+", " ");
}

if (order !== false) {
    document.getElementById("order").value = order.replace("+", " ");
}

console.log(titulo);
console.log(director);
console.log(ano);