using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace ORTflix.Models
{
    public class VotosDelUsuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        public Usuario Usuario { get; set; }
        public Pelicula Pelicula { get; set; }
        public Boolean TipoDeVoto { get; set; }

    }
}
