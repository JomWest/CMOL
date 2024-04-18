using Microsoft.AspNetCore.Mvc;
using Persistencia;
using Dominio;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{
    [Route("MarcasController")]
    public class ControladorMarca : ControllerBase
    {
        private readonly CellMasterDbContext _DbContext;

        public ControladorMarca(CellMasterDbContext dbContext)
        {
            this._DbContext = dbContext;
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var marcas = this._DbContext.Marcas.ToList();
            return Ok(marcas);
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var marca = this._DbContext.Marcas.FirstOrDefault(m => m.IdMarca == id);
            if (marca != null)
            {
                return Ok(marca);
            }
            return NotFound();
        }

        [HttpDelete("Remove/{id}")]
        public IActionResult Remove(int id)
        {
            var marca = this._DbContext.Marcas.FirstOrDefault(m => m.IdMarca == id);
            if (marca != null)
            {
                this._DbContext.Marcas.Remove(marca);
                this._DbContext.SaveChanges();
                return Ok(true);
            }
            return NotFound();
        }
    [HttpPost("Create")]
public IActionResult Create([FromBody] Marca _marca)
{

    var marca= this._DbContext.Marcas.FirstOrDefault(o => o.IdMarca == _marca.IdMarca);
    if (marca != null)
    {
        return BadRequest("La entidad Servicio ya existe. Utiliza la función de actualización en su lugar.");
    }
    
    this._DbContext.Marcas.Add(_marca);
    this._DbContext.SaveChanges();
return Ok(new { success = true, message = "La entidad Servicio ha sido creada con éxito." });
}
         [HttpPut("Update/{id}")]
public IActionResult Update(int id, [FromBody] UpdateModelMarca _marca)
{
    var marca = this._DbContext.Marcas.FirstOrDefault(o => o.IdMarca == id);
    if (marca == null)
    {
        return NotFound("La entidad Servicio no existe y no puede ser actualizada.");
    }

   marca.NombreMarca = _marca.NombreMarca;

    this._DbContext.SaveChanges();
    return Ok(new { success = true, message = "La entidad Servicio ha sido actualizada con éxito." });
    
}
    
    }
}
