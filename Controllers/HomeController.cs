using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ORTflix.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ORTflix.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections;

namespace ORTflix.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ORTflixDatabaseContext _context;

        public HomeController(ILogger<HomeController> logger, ORTflixDatabaseContext context)
        {
            _logger = logger;
            _context = context;

        }

        public async Task<IActionResult> Index()
        {

            List<Pelicula> peliculasPorCategoria = new List<Pelicula>();
            List<Pelicula> peliculasConMasVotos = new List<Pelicula>();

            var pelCategory1 = await _context.Peliculas.FirstOrDefaultAsync(p => p.GeneroPelicula == GeneroP.Terror);
            var pelCategory2 = await _context.Peliculas.FirstOrDefaultAsync(p => p.GeneroPelicula == GeneroP.CienciaFiccion);
            var pelCategory3 = await _context.Peliculas.FirstOrDefaultAsync(p => p.GeneroPelicula == GeneroP.Comedia);
            var pelCategory4 = await _context.Peliculas.FirstOrDefaultAsync(p => p.GeneroPelicula == GeneroP.Drama);

            var peltopfive = _context.Peliculas.OrderByDescending(p => p.VotoPositivo).Take(4);

            peliculasPorCategoria.Add(pelCategory1);
            peliculasPorCategoria.Add(pelCategory2);
            peliculasPorCategoria.Add(pelCategory3);
            peliculasPorCategoria.Add(pelCategory4);


            foreach (Pelicula element in peltopfive){
                peliculasConMasVotos.Add(element);
            }

            ViewBag.peliculasPorCategoria = peliculasPorCategoria;
            ViewBag.peliculasConMasVotos = peliculasConMasVotos;

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
