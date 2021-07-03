using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ORTflix.Models;

namespace ORTflix.Context
{
    public class ORTflixDatabaseContext : DbContext
    {
        public
     ORTflixDatabaseContext(DbContextOptions<ORTflixDatabaseContext> options)
     : base(options)
        {
        }
        public DbSet<Pelicula> Peliculas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<PeliculasDelUsuario> PeliculasDelUsuario { get; set; }
        public DbSet<VotosDelUsuario> VotosDelUsuario { get; set; }
    }
}