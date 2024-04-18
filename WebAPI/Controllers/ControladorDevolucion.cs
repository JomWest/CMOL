using Microsoft.AspNetCore.Mvc;
using Persistencia;
using Dominio;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("cellmasterapi/[controller]")]
    public class ControladorDevolucion : ControllerBase
    {
        private readonly CellMasterDbContext _DbContext;

        public ControladorDevolucion(CellMasterDbContext dbContext)
        {
            this._DbContext = dbContext;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var devoluciones = this._DbContext.Devolucions.ToList();
            return Ok(devoluciones);
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var devolucion = this._DbContext.Devolucions.FirstOrDefault(d => d.Iddevoluciones == id);
            if (devolucion != null)
            {
                return Ok(devolucion);
            }
            return NotFound();
        }

        [HttpDelete("Remove/{id}")]
        public IActionResult Remove(int id)
        {
            var devolucion = this._DbContext.Devolucions.FirstOrDefault(d => d.Iddevoluciones == id);
            if (devolucion != null)
            {
                this._DbContext.Devolucions.Remove(devolucion);
                this._DbContext.SaveChanges();
                return Ok(true);
            }
            return NotFound();
        }

        [HttpPost("Create")]
        public IActionResult Create([FromBody] Devolucion _devolucion)
        {
            var devolucion = this._DbContext.Devolucions.FirstOrDefault(d => d.Iddevoluciones == _devolucion.Iddevoluciones);
            if (devolucion != null)
            {
                return BadRequest("La entidad Devolucion ya existe. Utiliza la función de actualización en su lugar.");
            }

            this._DbContext.Devolucions.Add(_devolucion);
            this._DbContext.SaveChanges();
            return Ok("La entidad Devolucion ha sido creada con éxito.");
        }

        [HttpPut("Update/{id}")]
        public IActionResult Update(int id, [FromBody] Devolucion _devolucion)
        {
            var devolucion = this._DbContext.Devolucions.FirstOrDefault(d => d.Iddevoluciones == id);
            if (devolucion == null)
            {
                return NotFound("La entidad Devolucion no existe y no puede ser actualizada.");
            }
            // Actualiza los campos necesarios
            devolucion.Iddevoluciones = _devolucion.Iddevoluciones;
            devolucion.IdFactura = _devolucion.IdFactura;
            devolucion.Fechadevolucion = _devolucion.Fechadevolucion;
            devolucion.Totaldevolucion = _devolucion.Totaldevolucion;
            devolucion.IdUsuarios = _devolucion.IdUsuarios;
            devolucion.IdClientes = _devolucion.IdClientes;
            devolucion.Motivosdevolucion = _devolucion.Motivosdevolucion;
            devolucion.Acciontomadas = _devolucion.Acciontomadas;

            // Asegúrate de actualizar otros campos según sea necesario

            this._DbContext.SaveChanges();
            return Ok("La entidad Devolucion ha sido actualizada con éxito.");
        }
    }
}
