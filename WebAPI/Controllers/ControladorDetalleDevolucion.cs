using Microsoft.AspNetCore.Mvc;
using Persistencia;
using Dominio;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("cellmasterapi/[controller]")]
    public class ControladorDetalleDevolucion : ControllerBase
    {
        private readonly CellMasterDbContext _DbContext;

        public ControladorDetalleDevolucion(CellMasterDbContext dbContext)
        {
            this._DbContext = dbContext;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var detallesDevolucion = this._DbContext.DetalleDevolucions.ToList();
            return Ok(detallesDevolucion);
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var detalleDevolucion = this._DbContext.DetalleDevolucions.FirstOrDefault(d => d.Iddetalledevolucion == id);
            if (detalleDevolucion != null)
            {
                return Ok(detalleDevolucion);
            }
            return NotFound();
        }

        [HttpDelete("Remove/{id}")]
        public IActionResult Remove(int id)
        {
            var detalleDevolucion = this._DbContext.DetalleDevolucions.FirstOrDefault(d => d.Iddetalledevolucion == id);
            if (detalleDevolucion != null)
            {
                this._DbContext.DetalleDevolucions.Remove(detalleDevolucion);
                this._DbContext.SaveChanges();
                return Ok(true);
            }
            return NotFound();
        }

        [HttpPost("Create")]
        public IActionResult Create([FromBody] DetalleDevolucion _detalleDevolucion)
        {
            var detalleDevolucion = this._DbContext.DetalleDevolucions.FirstOrDefault(d => d.Iddetalledevolucion == _detalleDevolucion.Iddetalledevolucion);
            if (detalleDevolucion != null)
            {
                return BadRequest("La entidad DetalleDevolucion ya existe. Utiliza la función de actualización en su lugar.");
            }

            this._DbContext.DetalleDevolucions.Add(_detalleDevolucion);
            this._DbContext.SaveChanges();
            return Ok("La entidad DetalleDevolucion ha sido creada con éxito.");
        }

        [HttpPut("Update/{id}")]
        public IActionResult Update(int id, [FromBody] DetalleDevolucion _detalleDevolucion)
        {
            var detalleDevolucion = this._DbContext.DetalleDevolucions.FirstOrDefault(d => d.Iddetalledevolucion == id);
            if (detalleDevolucion == null)
            {
                return NotFound("La entidad DetalleDevolucion no existe y no puede ser actualizada.");
            }
            // Actualiza los campos necesarios
            detalleDevolucion.Iddetalledevolucion = _detalleDevolucion.Iddetalledevolucion;
            detalleDevolucion.Iddevoluciones = _detalleDevolucion.Iddevoluciones;
            detalleDevolucion.IdProducto = _detalleDevolucion.IdProducto;
            detalleDevolucion.Cant = _detalleDevolucion.Cant;
            detalleDevolucion.Precio = _detalleDevolucion.Precio;

            // Asegúrate de actualizar otros campos según sea necesario

            this._DbContext.SaveChanges();
            return Ok("La entidad DetalleDevolucion ha sido actualizada con éxito.");
        }
    }
}
