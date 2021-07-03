using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ORTflix.Context;
using ORTflix.Models;
using Microsoft.AspNetCore.Http;

namespace ORTflix.Controllers
{
    public class PeliculasController : Controller
    {
        private readonly ORTflixDatabaseContext _context;
        private int loggedId;
        private int loggedRole;
        public PeliculasController(ORTflixDatabaseContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            if (httpContextAccessor.HttpContext.Session.GetInt32("userId") != null)
            {
                loggedId = (int)httpContextAccessor.HttpContext.Session.GetInt32("userId");
            }
            if (httpContextAccessor.HttpContext.Session.GetInt32("userRole") != null)
            {
                loggedRole = (int)httpContextAccessor.HttpContext.Session.GetInt32("userRole");
            }
        }

        // GET: Peliculas
        public async Task<IActionResult> Index()
        {

            if (loggedRole != 1)
            {
                return Redirect("/");
            }
            return View(await _context.Peliculas.ToListAsync());
        }

        public async Task<IActionResult> Catalogo()
        {
            if (loggedId == 0)
            {
                return Redirect("/Usuario/Login");
            }
            return View(await _context.Peliculas.ToListAsync());
        }

        public async Task<IActionResult> MisPeliculas()
        {
            if (loggedId == 0)
            {
                return Redirect("/Usuario/Login");
            }

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(m => m.Id == loggedId);
            if (usuario == null)
            {
                return StatusCode(400);
            }

            var pu = _context.PeliculasDelUsuario.Where(b => b.Usuario == usuario).Include(c => c.Pelicula).ToList();

            return View(pu);
        }

        // GET: Peliculas/Buscador/
        public async Task<IActionResult> Buscador(String titulo, String director, int ano, int genero, String order)
        {

            if (loggedId == 0)
            {
                return Redirect("/Usuario/Login");
            }

            IQueryable<Pelicula> query = _context.Peliculas;

            if (titulo == null && director == null && ano == 0 && genero == 0 && order == null)
            {
                query = _context.Peliculas;
            }

            if (titulo != null)
            {
                query = query.Where(p => p.Titulo.Contains(titulo));
            }

            if (director != null)
            {
                query = query.Where(p => p.Director.Contains(director));
            }

            if (ano != 0)
            {
                query = query.Where(p => p.ano < ano);
            }
            
            if (genero != 0){
                switch (genero){
                    case 1:
                        query = query.Where(p => p.GeneroPelicula == GeneroP.CienciaFiccion);
                        break;
                    case 2:
                        query = query.Where(p => p.GeneroPelicula == GeneroP.Comedia);
                        break;
                    case 3:
                        query = query.Where(p => p.GeneroPelicula == GeneroP.Drama);
                        break;
                    case 4:
                        query = query.Where(p => p.GeneroPelicula == GeneroP.Terror);
                        break;
                    case 5:
                        query = query.Where(p => p.GeneroPelicula == GeneroP.Accion);
                        break;
                    case 6:
                        query = query.Where(p => p.GeneroPelicula == GeneroP.Animacion);
                        break;
                    case 7:
                        query = query.Where(p => p.GeneroPelicula == GeneroP.Policial);
                        break;
                    case 8:
                        query = query.Where(p => p.GeneroPelicula == GeneroP.Thriller);
                        break;
                }
            }

            if (order != null)
            {
                if(order == "votoPositivo") { 
                    query = query.OrderByDescending(p => p.VotoPositivo);
                }
                else if (order == "votoNegativo"){
                    query = query.OrderByDescending(p => p.VotoNegativo);
                }
                else if (order == "titulo"){
                    query = query.OrderBy(p => p.Titulo);
                }else if(order == "ano"){
                    query = query.OrderByDescending(p => p.ano);
                }
            }

            List<Pelicula> peliculas = query.ToList();

            return View(peliculas);
        }

        // GET: Peliculas/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            if (loggedRole != 1)
            {
                return Redirect("/");
            }
            if (id == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Peliculas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pelicula == null)
            {
                return NotFound();
            }

            return View(pelicula);
        }

        public async Task<IActionResult> DetallePelicula(int? id)

        {
            if (loggedId == 0)
            {
                return Redirect("/Usuario/Login");
            }

            if (id == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Peliculas
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pelicula == null)
            {
                return NotFound();
            }

            return View(pelicula);
        }


        // GET: Peliculas/Create
        public IActionResult Create()
        {
            if (loggedRole != 1)
            {
                return Redirect("/");
            }
            return View();
        }

        // POST: Peliculas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Titulo,ano,GeneroPelicula,Imagen,Description,Director")] Pelicula pelicula)
        {
            if (loggedRole != 1)
            {
                return Redirect("/");
            }
            if (ModelState.IsValid)
            {
                _context.Add(pelicula);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pelicula);
        }

        // GET: Peliculas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (loggedRole != 1)
            {
                return Redirect("/");
            }
            if (id == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Peliculas.FindAsync(id);
            if (pelicula == null)
            {
                return NotFound();
            }
            return View(pelicula);
        }

        // POST: Peliculas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Titulo,ano,GeneroPelicula,Imagen,VotoPositivo,VotoNegativo,Description,Director")] Pelicula pelicula)
        {
            if (loggedRole != 1)
            {
                return Redirect("/");
            }
            if (id != pelicula.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pelicula);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PeliculaExists(pelicula.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(pelicula);
        }


        // POST: Peliculas/DataPeliculasPorUsuarioAgregadasAFavoritos
        [HttpPost]
        public async Task<IActionResult> DataPeliculasPorUsuarioAgregadasAFavoritos(int usuarioId)
        {
            if (loggedId == 0)
            {
                return Redirect("/Usuario/Login");
            }

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(m => m.Id == loggedId);
            if (usuario == null)
            {
                return StatusCode(400);
            }

            var pu = _context.PeliculasDelUsuario.Where(b => b.Usuario == usuario).Include(c => c.Pelicula).ToList();
            return Ok(pu);
        }

        // POST: Peliculas/DataPeliculasPorUsuarioValoradas
        [HttpPost]
        public async Task<IActionResult> DataPeliculasPorUsuarioValoradas(int usuarioId)
        {
            if (loggedId == 0)
            {
                return Redirect("/Usuario/Login");
            }

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(m => m.Id == loggedId);
            if (usuario == null)
            {
                return StatusCode(400);
            }

            var pu = _context.VotosDelUsuario.Where(b => b.Usuario == usuario).Include(c => c.Pelicula).ToList();
            return Ok(pu);
        }


        // POST: Peliculas/GuardarPelicula
        [HttpPost]
        public async Task<IActionResult> GuardarPelicula(int usuarioId, int peliculaId)
        {
            if (loggedId == 0)
            {
                return Redirect("/Usuario/Login");
            }
            var code = StatusCode(400);

            var usuario = await _context.Usuarios.FirstOrDefaultAsync(m => m.Id == loggedId);
            var pelicula = await _context.Peliculas.FirstOrDefaultAsync(m => m.Id == peliculaId);

            if (usuario != null && pelicula != null)
            {

                var peliculaF = await _context.PeliculasDelUsuario.FirstOrDefaultAsync(m => m.Usuario == usuario && m.Pelicula == pelicula);

                if (peliculaF == null)
                {
                    PeliculasDelUsuario peliculaDelUsuario = new PeliculasDelUsuario { Usuario = usuario, Pelicula = pelicula };
                    _context.PeliculasDelUsuario.Add(peliculaDelUsuario);
                }
                else
                {
                    _context.PeliculasDelUsuario.Remove(peliculaF);
                }

                await _context.SaveChangesAsync();
                code = StatusCode(200);

            }

            return code;
        }

        // POST: Peliculas/VotoPelicula
        [HttpPost]
        public async Task<IActionResult> VotoPelicula(int usuarioId, int peliculaId, Boolean tipo)
        {
            if (loggedId == 0)
            {
                return Redirect("/Usuario/Login");
            }
            var code = StatusCode(400);

            var pelicula = await _context.Peliculas.FirstOrDefaultAsync(m => m.Id == peliculaId);
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(m => m.Id == loggedId);
            var votosDelUsuario = await _context.VotosDelUsuario.FirstOrDefaultAsync(m => m.Usuario == usuario && m.Pelicula == pelicula);

            if (pelicula != null || usuario != null)
            {
                if(tipo) {
                    if (votosDelUsuario == null) { 
                        pelicula.VotoPositivo += 1;
                    }else{
                        if(!votosDelUsuario.TipoDeVoto){
                            pelicula.VotoNegativo -= 1;
                            pelicula.VotoPositivo += 1;
                        }
                    }
                }
                else {
                    if (votosDelUsuario == null){
                        pelicula.VotoNegativo += 1;
                    }else{
                        if (votosDelUsuario.TipoDeVoto){
                            pelicula.VotoNegativo += 1;
                            pelicula.VotoPositivo -= 1;
                        }
                    }
                }

                _context.Peliculas.Update(pelicula);
                await _context.SaveChangesAsync();

                var voto = await _context.VotosDelUsuario.FirstOrDefaultAsync(m => m.Usuario == usuario && m.Pelicula == pelicula);

                if (voto == null){
                    VotosDelUsuario votoDelUsuario = new VotosDelUsuario { Usuario = usuario, Pelicula = pelicula, TipoDeVoto = tipo };
                    _context.VotosDelUsuario.Add(votoDelUsuario);
                }
                else
                {
                    voto.TipoDeVoto = tipo;
                    _context.VotosDelUsuario.Update(voto);
                }

                await _context.SaveChangesAsync();

                code = StatusCode(200);
            }

            return code;
        }


        // GET: Peliculas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (loggedRole != 1)
            {
                return Redirect("/");
            }
            if (id == null)
            {
                return NotFound();
            }

            var pelicula = await _context.Peliculas.FirstOrDefaultAsync(m => m.Id == id);
            if (pelicula == null)
            {
                return NotFound();
            }

            return View(pelicula);
        }

        // POST: Peliculas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (loggedRole != 1)
            {
                return Redirect("/");
            }
            var pelicula = await _context.Peliculas.FindAsync(id);
            _context.Peliculas.Remove(pelicula);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PeliculaExists(int id)
        {
            return _context.Peliculas.Any(e => e.Id == id);
        }
    }
}
