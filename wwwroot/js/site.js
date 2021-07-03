// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


$(document).ready(function () {
    $("#loginForm").submit(function (event) {
        event.preventDefault();
        var email = $("#inputEmail").val();
        var password = $("#inputPassword").val();

        $.post('/Usuario/CheckUsuario', { email: email, password: password },
            function (data, statusText, xhr) {
                if (data.statusCode == 200) {
                    window.location.href = "/";
                } else {
                    $("#respuestaRegistro").empty();
                    $('<p>REGISTRO INCORRECTO</p>').appendTo('#respuestaRegistro');
                }
            }
        );

    });

});
