using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;


namespace ORTflix.Models
{
    public class Pelicula
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        public String Titulo { get; set; }
        public int ano { get; set; }
        public GeneroP GeneroPelicula { get; set; }
        public String Imagen { get; set; }
        public int VotoPositivo { get; set; } = 0;
        public int VotoNegativo { get; set; } = 0;
        public String Director { get; set; }
        public String Description { get; set; }

    }
}
