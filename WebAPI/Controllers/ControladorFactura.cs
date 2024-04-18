using Microsoft.AspNetCore.Mvc;
using Persistencia;
using Dominio;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    
    [Route("ControladorFactura")]
    public class ControladorFactura : ControllerBase
    {
        private readonly CellMasterDbContext _DbContext;

        public ControladorFactura(CellMasterDbContext dbContext)
        {
            this._DbContext = dbContext;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var facturas = this._DbContext.Facturas.ToList();
            return Ok(facturas);
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var factura = this._DbContext.Facturas.FirstOrDefault(f => f.IdFactura == id);
            if (factura != null)
            {
                return Ok(factura);
            }
            return NotFound();
        }

        [HttpDelete("Remove/{id}")]
        public IActionResult Remove(int id)
        {
            var factura = this._DbContext.Facturas.FirstOrDefault(f => f.IdFactura == id);
            if (factura != null)
            {
                this._DbContext.Facturas.Remove(factura);
                this._DbContext.SaveChanges();
                return Ok(true);
            }
            return NotFound();
        }

[HttpPost("Create")]     
public IActionResult Create([FromBody] FacturaUpdateModel facturaUpdateModel)
{
    if (facturaUpdateModel == null)
    {
        return BadRequest("Invalid input. Please provide valid data.");
    }
    var factura = new Factura
    {
        Fecha = facturaUpdateModel.Fecha,
        Total = facturaUpdateModel.Total,
        IdUsuarios = facturaUpdateModel.IdUsuarios,
        IdClientes = facturaUpdateModel.IdClientes,
    };

    this._DbContext.Facturas.Add(factura);
    this._DbContext.SaveChanges();

    return Ok(new { success = true, message = "La Factura se agregó con éxito." });
}

        [HttpPut("Update/{id}")]
        public IActionResult Update(int id, [FromBody] Factura _factura)
        {
            var factura = this._DbContext.Facturas.FirstOrDefault(f => f.IdFactura == id);
            if (factura == null)
            {
                return NotFound("La entidad Factura no existe y no puede ser actualizada.");
            }
            // Actualiza los campos necesarios
            factura.IdFactura = _factura.IdFactura;
            factura.Fecha = _factura.Fecha;
            factura.Total = _factura.Total;
            factura.Estado = _factura.Estado;
            factura.IdUsuarios = _factura.IdUsuarios;
            factura.IdClientes = _factura.IdClientes;

            // Asegúrate de actualizar otros campos según sea necesario

            this._DbContext.SaveChanges();
            return Ok("La entidad Factura ha sido actualizada con éxito.");
        }
         [HttpGet("UltimoIdIngresado")]
        public IActionResult ObtenerUltimoIdIngresado()
        {
            try
            {
                var ultimoId = this._DbContext.Facturas
                    .OrderByDescending(f => f.IdFactura)
                    .Select(f => f.IdFactura)
                    .FirstOrDefault();

                return Ok(new { ultimoId });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Error interno del servidor al obtener el último ID de factura ingresado." });
            }
        }
    }
}
