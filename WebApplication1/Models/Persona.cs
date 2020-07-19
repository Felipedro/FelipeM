using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    [Table("Persona")]
    public class Persona
    {
        [Key]
        public int idPersona { get; set; }

        public string nombre { get; set; }

        public string apellido { get; set; }

        public string tipoDocumento { get; set; }

        public string documento { get; set; }

        public string pais { get; set; }

        public string sexo { get; set; }

        public string email { get; set; }

        public string telefono { get; set; }

        public DateTime fechaNacimiento { get; set; }
    }
}