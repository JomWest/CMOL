using Microsoft.AspNetCore.Mvc;
using Persistencia;
using Dominio;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    [Route("Detallefactura")]
    public class ControladorDetalleFactura : ControllerBase
    {
        private readonly CellMasterDbContext _DbContext;

        public ControladorDetalleFactura(CellMasterDbContext dbContext)
        {
            this._DbContext = dbContext;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var detallesFactura = this._DbContext.DetalleFacuras.ToList();
            return Ok(detallesFactura);
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var detalleFactura = this._DbContext.DetalleFacuras.FirstOrDefault(d => d.IdDetalleFactura == id);
            if (detalleFactura != null)
            {
                return Ok(detalleFactura);
            }
            return NotFound();
        }

        [HttpDelete("Remove/{id}")]
        public IActionResult Remove(int id)
        {
            var detalleFactura = this._DbContext.DetalleFacuras.FirstOrDefault(d => d.IdDetalleFactura == id);
            if (detalleFactura != null)
            {
                this._DbContext.DetalleFacuras.Remove(detalleFactura);
                this._DbContext.SaveChanges();
                return Ok(true);
            }
            return NotFound();
        }
[HttpPost("Create")]     
public IActionResult Create([FromBody] DeatlleFacturaUodateMode deatlleFacturaUodateMode)
{
    if (deatlleFacturaUodateMode == null)
    {
        return BadRequest("Invalid input. Please provide valid data.");
    }

    var detalleFacturas = new DetalleFacura
    {
        Cantidad = deatlleFacturaUodateMode.Cantidad,
        Subtotal = deatlleFacturaUodateMode.Subtotal,
        Total = deatlleFacturaUodateMode.Total,
        IdFactura = deatlleFacturaUodateMode.IdFactura,
        IdProducto = deatlleFacturaUodateMode.IdProducto,
        IdtipoCambio = deatlleFacturaUodateMode.IdtipoCambio,
    };

    this._DbContext.DetalleFacuras.Add(detalleFacturas);
    this._DbContext.SaveChanges();

    return Ok(new { success = true, message = "La Factura se agregó con éxito." });
}


        [HttpPut("Update/{id}")]
        public IActionResult Update(int id, [FromBody] DetalleFacura _detalleFactura)
        {
            var detalleFactura = this._DbContext.DetalleFacuras.FirstOrDefault(d => d.IdDetalleFactura == id);
            if (detalleFactura == null)
            {
                return NotFound("La entidad DetalleFactura no existe y no puede ser actualizada.");
            }
         
            detalleFactura.IdDetalleFactura = _detalleFactura.IdDetalleFactura;
            detalleFactura.Cantidad = _detalleFactura.Cantidad;
            detalleFactura.Subtotal = _detalleFactura.Subtotal;
            detalleFactura.Total = _detalleFactura.Total;
            detalleFactura.IdFactura = _detalleFactura.IdFactura;
            detalleFactura.IdProducto = _detalleFactura.IdProducto;
            detalleFactura.IdtipoCambio = _detalleFactura.IdtipoCambio;

            // Asegúrate de actualizar otros campos según sea necesario

            this._DbContext.SaveChanges();
            return Ok("La entidad DetalleFactura ha sido actualizada con éxito.");
        }
    }
}
