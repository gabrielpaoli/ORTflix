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
    public class UsuarioController : Controller
    {
        private readonly ORTflixDatabaseContext _context;

        public UsuarioController(ORTflixDatabaseContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Login()
        {
            return View(await _context.Usuarios.ToListAsync());
        }

        // GET: Usuario/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Usuario/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("name,email,password,role")] Usuario usuario)
        {
          
            if (ModelState.IsValid)
            {
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                HttpContext.Session.SetString("userName", usuario.name);
                HttpContext.Session.SetInt32("userId", usuario.Id);
                HttpContext.Session.SetInt32("userRole", usuario.role);
                return Redirect("/");
            }
            return View(usuario);
        }

        // POST: Usuario/CheckUsuario
        [HttpPost]
        public async Task<IActionResult> CheckUsuario(String email, String password)
        {
            var usuario = await _context.Usuarios.FirstOrDefaultAsync(m => m.email == email && m.password == password);
            if (usuario == null)
            {
                return Ok(StatusCode(403));
            }

            HttpContext.Session.SetString("userName", usuario.name);
            HttpContext.Session.SetInt32("userId", usuario.Id);
            HttpContext.Session.SetInt32("userRole", usuario.role);

            return Ok(StatusCode(200));
        }

        // POST: Usuario/Logout
        public async Task<IActionResult> Logout()
        {
            HttpContext.Session.Clear();
            return Redirect("/");
        }

    }
}
