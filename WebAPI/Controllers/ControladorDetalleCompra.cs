using Microsoft.AspNetCore.Mvc;
using Persistencia;
using Dominio;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    [Route("ComprasDetalle")]
    public class ControladorDetalleCompra : ControllerBase
    {
        private readonly CellMasterDbContext _DbContext;

        public ControladorDetalleCompra(CellMasterDbContext dbContext)
        {
            this._DbContext = dbContext;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var detallesCompra = this._DbContext.DetalleCompras.ToList();
            return Ok(detallesCompra);
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var detalleCompra = this._DbContext.DetalleCompras.FirstOrDefault(d => d.IdDetalleCompra == id);
            if (detalleCompra != null)
            {
                return Ok(detalleCompra);
            }
            return NotFound();
        }

        [HttpDelete("Remove/{id}")]
        public IActionResult Remove(int id)
        {
            var detalleCompra = this._DbContext.DetalleCompras.FirstOrDefault(d => d.IdDetalleCompra == id);
            if (detalleCompra != null)
            {
                this._DbContext.DetalleCompras.Remove(detalleCompra);
                this._DbContext.SaveChanges();
                return Ok(true);
            }
            return NotFound();
        }

[HttpPost("Create")]     
public IActionResult Create([FromBody] UpdateMOdelDetalleCompra updateMOdelDetalleCompra)
{
    if (updateMOdelDetalleCompra == null)
    {
        return BadRequest("Invalid input. Please provide valid data.");
    }

    var detalleC = new DetalleCompra
    {
        PrecioCompra = updateMOdelDetalleCompra.PrecioCompra,
        CantidadProducto = updateMOdelDetalleCompra.CantidadProducto,
        TotalCompra = updateMOdelDetalleCompra.TotalCompra,
        IdCompras = updateMOdelDetalleCompra.IdCompras,
        IdProducto = updateMOdelDetalleCompra.IdProducto,
    };

    this._DbContext.DetalleCompras.Add(detalleC);
    this._DbContext.SaveChanges();

    return Ok(new { success = true, message = "La Factura se agregó con éxito." });
}

        [HttpPut("Update/{id}")]
        public IActionResult Update(int id, [FromBody] DetalleCompra _detalleCompra)
        {
            var detalleCompra = this._DbContext.DetalleCompras.FirstOrDefault(d => d.IdDetalleCompra == id);
            if (detalleCompra == null)
            {
                return NotFound("La entidad DetalleCompra no existe y no puede ser actualizada.");
            }

            detalleCompra.IdDetalleCompra = _detalleCompra.IdDetalleCompra;
            detalleCompra.PrecioCompra = _detalleCompra.PrecioCompra;
            detalleCompra.CantidadProducto = _detalleCompra.CantidadProducto;
            detalleCompra.TotalCompra = _detalleCompra.TotalCompra;
            detalleCompra.IdCompras = _detalleCompra.IdCompras;
            detalleCompra.IdProducto = _detalleCompra.IdProducto;


            this._DbContext.SaveChanges();
            return Ok("La entidad DetalleCompra ha sido actualizada con éxito.");
        }
    }
}
