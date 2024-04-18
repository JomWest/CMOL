using Microsoft.AspNetCore.Mvc;
using Persistencia;
using Dominio;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("cellmasterapi/[controller]")]
    public class ControladorPersona : ControllerBase
    {
        private readonly CellMasterDbContext _DbContext;

        public ControladorPersona(CellMasterDbContext dbContext)
        {
            this._DbContext = dbContext;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var personas = this._DbContext.Personas.ToList();
            return Ok(personas);
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var persona = this._DbContext.Personas.FirstOrDefault(p => p.IdPersona == id);
            if (persona != null)
            {
                return Ok(persona);
            }
            return NotFound();
        }

        [HttpDelete("Remove/{id}")]
        public IActionResult Remove(int id)
        {
            var persona = this._DbContext.Personas.FirstOrDefault(p => p.IdPersona == id);
            if (persona != null)
            {
                this._DbContext.Personas.Remove(persona);
                this._DbContext.SaveChanges();
                return Ok(true);
            }
            return NotFound();
        }

        [HttpPost("Create")]
        public IActionResult Create([FromBody] Persona _persona)
        {
            var persona = this._DbContext.Personas.FirstOrDefault(p => p.IdPersona == _persona.IdPersona);
            if (persona != null)
            {
                return BadRequest("La entidad Persona ya existe. Utiliza la función de actualización en su lugar.");
            }

            this._DbContext.Personas.Add(_persona);
            this._DbContext.SaveChanges();
            return Ok("La entidad Persona ha sido creada con éxito.");
        }

        [HttpPut("Update/{id}")]
        public IActionResult Update(int id, [FromBody] Persona _persona)
        {
            var persona = this._DbContext.Personas.FirstOrDefault(p => p.IdPersona == id);
            if (persona == null)
            {
                return NotFound("La entidad Persona no existe y no puede ser actualizada.");
            }
            // Actualiza los campos necesarios
            persona.IdPersona = _persona.IdPersona;
            persona.Cedula = _persona.Cedula;
            persona.Nombre = _persona.Nombre;
            persona.Apellido = _persona.Apellido;
            persona.Telefono = _persona.Telefono;

            // Asegúrate de actualizar otros campos según sea necesario

            this._DbContext.SaveChanges();
            return Ok("La entidad Persona ha sido actualizada con éxito.");
        }
    }
}
