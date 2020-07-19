using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Data
{
    public class PersonaContexto : DbContext
    {
        public PersonaContexto(DbContextOptions<PersonaContexto> options) : base(options)
        {

        }

        //crear nuestro dbset IMPORTANTE
        public DbSet<Persona> PersonaItems { get; set; }
    }
}
