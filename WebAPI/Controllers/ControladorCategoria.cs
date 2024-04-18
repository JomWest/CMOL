using Microsoft.AspNetCore.Mvc;
using Persistencia;
using Dominio;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    [Route("CategoriaController")]
    public class ControladorCategoria : ControllerBase
    {
        private readonly CellMasterDbContext _DbContext;

        public ControladorCategoria(CellMasterDbContext dbContext)
        {
            this._DbContext = dbContext;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var categorias = this._DbContext.Categorias.ToList();
            return Ok(categorias);
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var categoria = this._DbContext.Categorias.FirstOrDefault(c => c.IdCategorias == id);
            if (categoria != null)
            {
                return Ok(categoria);
            }
            return NotFound();
        }

        [HttpDelete("Remove/{id}")]
        public IActionResult Remove(int id)
        {
            var categoria = this._DbContext.Categorias.FirstOrDefault(c => c.IdCategorias == id);
            if (categoria != null)
            {
                this._DbContext.Categorias.Remove(categoria);
                this._DbContext.SaveChanges();
                return Ok(true);
            }
            return NotFound();
        }

        [HttpPost("Create")]
     public IActionResult Create([FromBody] UpdatemodelCategoria _categoria)
{

    var categpriaS = new Categoria
    {
      Nombre = _categoria.Nombre,
      Descripción = _categoria.Descripción,
    };

    this._DbContext.Categorias.Add(categpriaS);
    this._DbContext.SaveChanges();

return Ok(new { success = true, message = "El Producto se grego con exito." });
}
  

    [HttpPut("Update/{id}")]
public IActionResult Update(int id, [FromBody] UpdatemodelCategoria _categoria)
{
    var categoria = this._DbContext.Categorias.FirstOrDefault(o => o.IdCategorias == id);
    if (categoria == null)
    {
        return NotFound("La entidad Servicio no existe y no puede ser actualizada.");
    }

   categoria.Nombre = _categoria.Nombre;
   categoria.Descripción = _categoria.Descripción;

    this._DbContext.SaveChanges();
    return Ok(new { success = true, message = "La entidad Servicio ha sido actualizada con éxito." });
    
}
    }
}
