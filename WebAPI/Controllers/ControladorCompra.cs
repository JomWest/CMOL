using Microsoft.AspNetCore.Mvc;
using Persistencia;
using Dominio;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    [Route("ComprasController")]
    public class ControladorCompra : ControllerBase
    {
        private readonly CellMasterDbContext _DbContext;

        public ControladorCompra(CellMasterDbContext dbContext)
        {
            this._DbContext = dbContext;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var compras = this._DbContext.Compras.ToList();
            return Ok(compras);
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var compra = this._DbContext.Compras.FirstOrDefault(c => c.IdCompras == id);
            if (compra != null)
            {
                return Ok(compra);
            }
            return NotFound();
        }

        [HttpDelete("Remove/{id}")]
        public IActionResult Remove(int id)
        {
            var compra = this._DbContext.Compras.FirstOrDefault(c => c.IdCompras == id);
            if (compra != null)
            {
                this._DbContext.Compras.Remove(compra);
                this._DbContext.SaveChanges();
                return Ok(true);
            }
            return NotFound();
        }

     [HttpPost("Create")]     
public IActionResult Create([FromBody] UpdateModelsCompras updateModelsCompras)
{
    if (updateModelsCompras == null)
    {
        return BadRequest("Invalid input. Please provide valid data.");
    }
    var comprass = new Compra
    {
        IdCompras = updateModelsCompras.IdCompras,
        FechaCompra = updateModelsCompras.FechaCompra,
        IdUsuarios = updateModelsCompras.IdUsuarios,
        IdProveedores = updateModelsCompras.IdProveedores,
    };

    this._DbContext.Compras.Add(comprass);
    this._DbContext.SaveChanges();

    return Ok(new { success = true, message = "La Compra se agregó con éxito." });
}

        [HttpPut("Update/{id}")]
        public IActionResult Update(int id, [FromBody] Compra _compra)
        {
            var compra = this._DbContext.Compras.FirstOrDefault(c => c.IdCompras == id);
            if (compra == null)
            {
                return NotFound("La entidad Compra no existe y no puede ser actualizada.");
            }
            compra.IdCompras = _compra.IdCompras;
            compra.FechaCompra = _compra.FechaCompra;
            compra.IdUsuarios = _compra.IdUsuarios;
            compra.IdProveedores = _compra.IdProveedores;


            this._DbContext.SaveChanges();
            return Ok("La entidad Compra ha sido actualizada con éxito.");
     
        }
         [HttpGet("ObtenerUltimoIdCompra")]
   public IActionResult ObtenerUltimoIdCompra()
{
    try
    {
        var ultimoIdCompra = _DbContext.Compras
            .OrderByDescending(c => c.IdCompras)
            .Select(c => c.IdCompras)
            .FirstOrDefault();

        if (ultimoIdCompra == 0)
        {
            return NotFound(new { message = "No se encontró el último ID de compra." });
        }

        return Ok(new { ultimoIdCompra });
    }
    catch (Exception ex)
    {
        return StatusCode(500, new { error = "Error interno del servidor al obtener el último ID de compra." });
    }
}
}

    
}
