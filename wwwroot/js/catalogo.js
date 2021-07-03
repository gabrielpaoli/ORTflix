let dataUserId = $('.itemPelicula').attr('data-userid');
let peliculas = $('.itemPelicula');

$.post('/Peliculas/DataPeliculasPorUsuarioAgregadasAFavoritos', { usuarioId: dataUserId },
    function (data) {
        peliculas.each(function (index, item) {
            let itemClassName = item.className;
            itemClassName = itemClassName.split('-');
            let itemFirstClass = item.className.split(' ');
            $.each(data, function (indexP, itemP) {
                if (itemP.pelicula.id === parseInt(itemClassName[1])) {
                    $('.' + itemFirstClass[1] + ' .fa-heart').css('color', 'pink');
                }
            });
        });
    }
);


$.post('/Peliculas/DataPeliculasPorUsuarioValoradas', { usuarioId: dataUserId },
    function (data) {
        peliculas.each(function (index, item) {
            let itemClassName = item.className;
            itemClassName = itemClassName.split('-');
            let itemFirstClass = item.className.split(' ');
            $.each(data, function (indexP, itemP) {
                if (itemP.pelicula.id === parseInt(itemClassName[1])) {
                    if (itemP.tipoDeVoto) {
                        $('.' + itemFirstClass[1] + ' .fa-thumbs-up').css('color', 'green');
                    } else {
                        $('.' + itemFirstClass[1] + ' .fa-thumbs-down').css('color', 'red');
                    }
                }
            });
        });
    }
);


$(".VotUp").on('click', function (event) {
    let usuarioId = $(this).attr('data-userid');
    let peliculaId = $(this).attr('data-peliculaid');
    let tipo = true;

    $.post('/Peliculas/VotoPelicula', { usuarioId: usuarioId, peliculaId: peliculaId, tipo: tipo },
        function (returnedData) {
            console.log(returnedData);
        }
    );

    $(this).parent().find('.fa-thumbs-down').css('color', 'white');
    $(this).find('.fa-thumbs-up').css('color', 'green');
});

$(".VotDown").on('click', function (event) {
    let usuarioId = $(this).attr('data-userid');
    let peliculaId = $(this).attr('data-peliculaid');
    let tipo = false;

    $.post('/Peliculas/VotoPelicula', { usuarioId: usuarioId, peliculaId: peliculaId, tipo: tipo },
        function (returnedData) {
            console.log(returnedData);
        }
    );
    $(this).parent().find('.fa-thumbs-up').css('color', 'white');
    $(this).find('.fa-thumbs-down').css('color', 'red');
});

$(".addFavorites").on('click', function (event) {
    let usuarioId = $(this).attr('data-userid');
    let peliculaId = $(this).attr('data-peliculaid');

    $.post('/Peliculas/GuardarPelicula', { usuarioId: usuarioId, peliculaId: peliculaId },
        function (returnedData) {
            if (window.location.pathname === '/Peliculas/MisPeliculas') {
                location.reload();
            }
        }
    );

    let color = $(this).find('.fa-heart').css('color');

    if (color === 'rgb(255, 192, 203)') {
        $(this).find('.fa-heart').css('color', 'white');
    } else {
        $(this).find('.fa-heart').css('color', 'pink');
    }


});
