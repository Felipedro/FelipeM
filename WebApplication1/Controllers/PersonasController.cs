using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonasController : ControllerBase
    {
        private readonly PersonaContexto _context;

        public PersonasController(PersonaContexto contexto)
        {
            _context = contexto;
        }

        //GET: api/personas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Persona>>> GetPersonaItems()
        {
            return await _context.PersonaItems.ToListAsync();
        }

        //GET: api/personas/estadisticas
        [HttpGet("estadisticas")]
        public IActionResult GetPersonaEstadisticas()
        {
            int cantidadMasculino = 0;
            int cantidadFemenino = 0;
            int cantidadArgentinos = 0;
            foreach (var item in _context.PersonaItems)
            {
                item.pais = !string.IsNullOrWhiteSpace(item.pais) ? item.pais.ToLower() : "";
                cantidadMasculino = cantidadMasculino + (item.sexo == "M" ? 1 : 0);
                cantidadFemenino = cantidadFemenino + (item.sexo == "F" ? 1 : 0);
                cantidadArgentinos = cantidadArgentinos + (item.pais == "argentina" ? 1 : 0);
            }
            int porcentajeArgentinos = 0;
            if (cantidadArgentinos > 0)
            {
                porcentajeArgentinos = cantidadArgentinos * 100 / _context.PersonaItems.Count();
            }
            Estadistica estadistica = new Estadistica
            {
                cantidad_mujeres = cantidadFemenino.ToString(),
                cantidad_hombres = cantidadMasculino.ToString(),
                porcentaje_argentinos = porcentajeArgentinos.ToString()
            };
            string estadisticaJson = JsonConvert.SerializeObject(estadistica, Formatting.Indented);
            
            return Ok(estadisticaJson);
        }

        public class Estadistica
        {
            public string cantidad_mujeres { get; set; }
            public string cantidad_hombres { get; set; }
            public string porcentaje_argentinos { get; set; }
        }

        //GET: api/personas/id
        [HttpGet("{id}")]
        public async Task<ActionResult<Persona>> GetPersonaItem(int id)
        {
            var personaItem = await _context.PersonaItems.FindAsync(id);

            if (personaItem == null)
            {
                return NotFound();
            }

            return personaItem;
        }


        //POST: api/personas
        [HttpPost]
        public async Task<ActionResult<Persona>> PostPersonaItem(Persona persona)
        {
            string verificacion = Verificacion(persona);
            if (verificacion != "Ok")
            {
                return BadRequest(verificacion);
            }
            _context.PersonaItems.Add(persona);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPersonaItem), new { id = persona.idPersona }, persona);
        }

        private string Verificacion(Persona persona)
        {
            int edad = DateTime.Today.Year - persona.fechaNacimiento.Year;
            persona.sexo = persona.sexo.ToUpper();
            if (persona.fechaNacimiento.Month > DateTime.Today.Month)
            {
                --edad;
            }
            else if (persona.fechaNacimiento.Month == DateTime.Today.Month && persona.fechaNacimiento.Day > DateTime.Today.Day)
            {
                --edad;
            }

            List<Persona> personas = _context.PersonaItems.Where(c => c.documento == persona.documento &&
            c.tipoDocumento == persona.tipoDocumento && c.pais == persona.pais && c.sexo == persona.sexo && 
            c.idPersona != persona.idPersona).ToList();
            if (personas.Count() > 0)
            {
                return "No puede haber personas repetidas.";
            }
            else if (string.IsNullOrWhiteSpace(persona.email) && string.IsNullOrWhiteSpace(persona.telefono))
            {
                return "La persona debe tener, al menos, un dato de contacto.";
            }
            else if (edad < 18)
            {
                return "No pueden crearse personas menores de 18 años.";
            }
            else if (persona.sexo != "F" && persona.sexo != "M")
            {
                return "El sexo debe ser 'M' para masculino y 'F' para femenino.";
            }
            else
            {
                return "Ok";
            }
        }

        //PUT: api/personas/id
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPersonaItem(int id, Persona persona)
        {
            if (id != persona.idPersona)
            {
                return BadRequest();
            }
            string verificacion = Verificacion(persona);
            if (verificacion != "Ok")
            {
                return BadRequest(verificacion);
            }
            _context.Entry(persona).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return NoContent();
        }

        //PATCH: api/personas/id
        [HttpPatch("{id}")]
        public async Task<ActionResult> PatchPersonaItem(int id, [FromBody] JsonPatchDocument<Persona> patchDocument)
        {
            if (patchDocument == null)
            {
                return BadRequest();
            }

            var persona = await _context.PersonaItems.FirstOrDefaultAsync(c => c.idPersona == id);
            if (persona == null)
            {
                return NotFound();
            }

            string verificacion = Verificacion(persona);
            if (verificacion != "Ok")
            {
                return BadRequest(verificacion);
            }

            patchDocument.ApplyTo(persona, ModelState);
            var isValid = TryValidateModel(persona);

            if (!isValid)
            {
                return BadRequest(ModelState);
            }

            await _context.SaveChangesAsync();

            return NoContent();
        }

        //DELETE: api/personas/id
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePersonaItem(int id)
        {
            Persona persona = await _context.PersonaItems.FindAsync(id);

            if (persona == null)
            {
                return NotFound();
            }

            _context.PersonaItems.Remove(persona);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
