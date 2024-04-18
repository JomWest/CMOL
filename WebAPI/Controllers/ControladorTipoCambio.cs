using Microsoft.AspNetCore.Mvc;
using Persistencia;
using Dominio;
using Microsoft.EntityFrameworkCore;
namespace WebApi.Controllers
{
    [Route("TipoCambio")]

    
    public class ControladorTipoCambio : ControllerBase
    {
        private readonly CellMasterDbContext _DbContext;

        public ControladorTipoCambio(CellMasterDbContext dbContext)
        {
            this._DbContext = dbContext;
        }
[HttpGet("ExisteTipoCambioParaFechaActual")]
public IActionResult ExisteTipoCambioParaFechaActual()
{
    try
    {
        DateTime fechaActual = DateTime.Today;
        bool existeTipoCambio = _DbContext.TipoCambios.Any(tc => tc.FechaC.HasValue && tc.FechaC.Value.Date == fechaActual);
        return Ok(new { existeTipoCambio });
    }
    catch (Exception ex)
    {
        // Loguea el error o devuelve un código de estado y mensaje de error específico
        return StatusCode(500, new { error = "Error interno del servidor al comprobar el tipo de cambio." });
    }
}
        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var tiposCambio = this._DbContext.TipoCambios.ToList();
            return Ok(tiposCambio);
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var tipoCambio = this._DbContext.TipoCambios.FirstOrDefault(t => t.IdtipoCambio == id);
            if (tipoCambio != null)
            {
                return Ok(tipoCambio);
            }
            return NotFound();
        }

        [HttpDelete("Remove/{id}")]
        public IActionResult Remove(int id)
        {
            var tipoCambio = this._DbContext.TipoCambios.FirstOrDefault(t => t.IdtipoCambio == id);
            if (tipoCambio != null)
            {
                this._DbContext.TipoCambios.Remove(tipoCambio);
                this._DbContext.SaveChanges();
                return Ok(true);
            }
            return NotFound();
        }

         [HttpPost("Create")]
     public IActionResult Create([FromBody] TipoCambioUpdateModel _cambio)
{

    var cambio = new TipoCambio
    {
      PrecioCambio = _cambio.PrecioCambio,
      FechaC = _cambio.FechaC,
    };

    this._DbContext.TipoCambios.Add(cambio);
    this._DbContext.SaveChanges();

return Ok(new { success = true, message = "El Producto se grego con exito." });
}

        [HttpPut("Update/{id}")]
        public IActionResult Update(int id, [FromBody] TipoCambio _tipoCambio)
        {
            var tipoCambio = this._DbContext.TipoCambios.FirstOrDefault(t => t.IdtipoCambio == id);
            if (tipoCambio == null)
            {
                return NotFound("La entidad TipoCambio no existe y no puede ser actualizada.");
            }
            // Actualiza los campos necesarios
            tipoCambio.IdtipoCambio = _tipoCambio.IdtipoCambio;
            tipoCambio.PrecioCambio = _tipoCambio.PrecioCambio;
            tipoCambio.FechaC = _tipoCambio.FechaC;

            // Asegúrate de actualizar otros campos según sea necesario

            this._DbContext.SaveChanges();
            return Ok("La entidad TipoCambio ha sido actualizada con éxito.");
        }
        [HttpGet("ObtenerTipoCambioHoy")]
public IActionResult ObtenerTipoCambioHoy()
{
    try
    {
        DateTime fechaActual = DateTime.Today;

        var tipoCambioHoy = _DbContext.TipoCambios
            .Where(tc => tc.FechaC.HasValue && tc.FechaC.Value.Date == fechaActual)
            .Select(tc => new { tc.IdtipoCambio, tc.PrecioCambio })
            .FirstOrDefault();

        if (tipoCambioHoy != null)
        {
            return Ok(tipoCambioHoy);
        }
        else
        {
            return NotFound("No se encontró un tipo de cambio para la fecha de hoy.");
        }
    }
    catch (Exception ex)
    {
        // Loguea el error o devuelve un código de estado y mensaje de error específico
        return StatusCode(500, new { error = "Error interno del servidor al obtener el tipo de cambio para la fecha de hoy." });
    }
}
    }
}
