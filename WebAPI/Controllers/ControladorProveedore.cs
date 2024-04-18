using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Dominio;
using Persistencia;

namespace WebAPI.Controllers
{
    [Route("Proveedor")]
    public class ProveedorController : ControllerBase
    {
        private readonly CellMasterDbContext _dbContext;

        public ProveedorController(CellMasterDbContext dbcontext)
        {
            this._dbContext = dbcontext;
        }

     [HttpGet("GetAll")]
    public IActionResult GetAll()
    {
        var proveedores = this._dbContext.Proveedores.Include(c => c.IdProveedoresNavigation).ToList();

    var result = proveedores.Select(c => new
    {
        idproveedor = c.IdProveedores,
        nombreEmpresa = c.NombreEmpresa,
        direcion = c.Direccion,
        email = c.Email,
        departamento = c.Departamento,
        IdProveedoresNavigation = new
        {
            idPersona = c.IdProveedoresNavigation.IdPersona,
            cedula = c.IdProveedoresNavigation.Cedula,
            nombre = c.IdProveedoresNavigation.Nombre,
            apellido = c.IdProveedoresNavigation.Apellido,
            telefono = c.IdProveedoresNavigation.Telefono
            // Agrega otras propiedades de "Persona" según sea necesario
        },
    }).ToList();
        return Ok(result);
    }



        [HttpDelete("Delete/{idProveedor}")]
        public IActionResult Delete(int idProveedor)
        {
            var proveedor = this._dbContext.Proveedores.FirstOrDefault(pr => pr.IdProveedores == idProveedor);

            if (proveedor != null)
            {
                this._dbContext.Remove(proveedor);
                this._dbContext.SaveChanges();
                return Ok(true);
            }

            return Ok(false);
        }
[HttpPost("Create")]
public IActionResult Create([FromBody] UpdateModel updateModel)
{
    // Puedes realizar las validaciones necesarias aquí

    var Proveedor = new Proveedore
    {
        NombreEmpresa = updateModel.NombreEmpresa,
        Departamento = updateModel.Departamento,
        Direccion = updateModel.Direccion,
        Email = updateModel.Email,
        
        IdProveedoresNavigation = new Persona
        {
            Cedula = updateModel.Cedula,
            Nombre = updateModel.Nombre,
            Apellido = updateModel.Apellido,
            Telefono = updateModel.Telefono
        }
    };

    this._dbContext.Proveedores.Add(Proveedor);
    this._dbContext.SaveChanges();

return Ok(new { success = true, message = "La entidad Cliente ha sido creada con éxito." });
}



        [HttpPut("Update/{idProveedores}")]
public IActionResult Update(int idproveedores, [FromBody] UpdateModel _proveedores)
{
    var proveedore = this._dbContext.Proveedores
        .Include(c => c.IdProveedoresNavigation)  // Asegúrate de cargar la propiedad de navegación
        .FirstOrDefault(o => o.IdProveedores == idproveedores);

    if (proveedore == null)
    {
        return NotFound("La entidad Usuario no existe y no puede ser actualizada.");
    }

    // Actualiza los campos necesarios
    proveedore.NombreEmpresa = _proveedores.NombreEmpresa;
    proveedore.Departamento = _proveedores.Departamento;
    proveedore.Direccion = _proveedores.Direccion;
    proveedore.Email = _proveedores.Email;

    if (proveedore.IdProveedoresNavigation != null)
    {
        proveedore.IdProveedoresNavigation.Cedula = _proveedores.Cedula;
        proveedore.IdProveedoresNavigation.Nombre = _proveedores.Nombre;
        proveedore.IdProveedoresNavigation.Apellido = _proveedores.Apellido;
        proveedore.IdProveedoresNavigation.Telefono = _proveedores.Telefono;
        
    }

    this._dbContext.SaveChanges();
    return Ok(new { success = true, message = "La entidad Usuario ha sido actualizada con éxito." });
}
    }
}
