using Microsoft.AspNetCore.Mvc;
using Persistencia;
using Dominio;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;

namespace WebAPI.Controllers
{
    [Route("Productos")]
    public class ControladorProducto : ControllerBase
    {
        private readonly CellMasterDbContext _DbContext;

        public ControladorProducto(CellMasterDbContext dbContext)
        {
            this._DbContext = dbContext;
        }

[HttpGet("GetAll")]
public IActionResult GetAll()
{
    var productos = this._DbContext.Productos
        .Include(c => c.IdCategoriasNavigation)  
        .Include(c => c.IdMarcaNavigation)       
        .ToList();

    var result = productos.Select(c => new
    {
        idProducto = c.IdProducto,
        codigoProd = c.CodigoProd,
        nombreProducto = c.NombreProducto,
        precioVenta = c.PrecioVenta,
        stock = c.Stock,
        stockMinimo = c.StockMinimo,
        categoria = new
        {
            idCategorias = c.IdCategoriasNavigation?.IdCategorias,
            nombre = c.IdCategoriasNavigation?.Nombre,
            
        },
        marca = new
        {
            idMarca = c.IdMarcaNavigation?.IdMarca,
            nombreMarca = c.IdMarcaNavigation?.NombreMarca,
          
        }
    }).ToList();

    return Ok(result);
}

        [HttpGet("GetByCode/{code}")]
        public IActionResult GetByCode(int code)
        {
            var producto = this._DbContext.Productos.FirstOrDefault(o => o.IdProducto == code);
            if (producto != null)
            {
                return Ok(producto);
            }
            return NotFound();
        }

 [HttpDelete("Remove/{code}")]
        public IActionResult Remove(int code)
        {
            var producto = this._DbContext.Productos.FirstOrDefault(o => o.IdProducto == code);
            if (producto != null)
            {
                this._DbContext.Productos.Remove(producto);
                this._DbContext.SaveChanges();
                return Ok(true);
            }
            return NotFound();
        }

   [HttpPost("Create")]
     public IActionResult Create([FromBody] ProductosUpdateModel productosUpdateModel)
{

    var productosupdatemodel = new Producto
    {
        CodigoProd = productosUpdateModel.CodigoProd,
        NombreProducto = productosUpdateModel.NombreProducto,
        PrecioVenta = productosUpdateModel.PrecioVenta,
        Stock = productosUpdateModel.Stock,
        StockMinimo = productosUpdateModel.StockMinimo,
        IdCategorias = productosUpdateModel.IdCategorias,
        IdMarca = productosUpdateModel.IdMarca,
    };

    this._DbContext.Productos.Add(productosupdatemodel);
    this._DbContext.SaveChanges();

return Ok(new { success = true, message = "El Producto se grego con exito." });
}

[HttpPut("Update/{id}")]
public IActionResult Update(int id, [FromBody] Producto _producto)
{
    var producto = this._DbContext.Productos.FirstOrDefault(o => o.IdProducto == id);
    if (producto == null)
    {
        return NotFound("La entidad Detalles de solicitud no existe y no puede ser actualizada.");
    }

    // Actualiza los campos necesarios
    producto.CodigoProd = _producto.CodigoProd;
    producto.NombreProducto = _producto.NombreProducto;
    producto.PrecioVenta = _producto.PrecioVenta;
    producto.Stock = _producto.Stock;
    producto.StockMinimo = _producto.StockMinimo;
    producto.IdCategorias = _producto.IdCategorias;
    producto.IdMarca = _producto.IdMarca;

    this._DbContext.SaveChanges();
    return Ok(new { success = true, message = "Los detalles de solicitud han sido actualizados con éxito." });
    

}
}
}

