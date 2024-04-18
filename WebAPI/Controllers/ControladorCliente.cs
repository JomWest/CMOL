using Microsoft.AspNetCore.Mvc;
using Persistencia;
using Dominio;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Controllers
{ 
    [Route("ClientesController")]
    public class ControladorCliente : ControllerBase
    {
        private readonly CellMasterDbContext _DbContext;

        public ControladorCliente(CellMasterDbContext dbContext)
        {
            this._DbContext = dbContext;
        }

        [HttpGet("GetAll")]
    public IActionResult GetAll()
    {
        var clientes = this._DbContext.Clientes.Include(c => c.IdClientesNavigation).ToList();

    var result = clientes.Select(c => new
    {
    
        idClientesNavigation = new
        {
            idPersona = c.IdClientesNavigation.IdPersona,
            cedula = c.IdClientesNavigation.Cedula,
            nombre = c.IdClientesNavigation.Nombre,
            apellido = c.IdClientesNavigation.Apellido,
            telefono = c.IdClientesNavigation.Telefono
        },
    }).ToList();
        return Ok(result);
    }
        [HttpGet("GetById/{id}")]
        public IActionResult GetById(int id)
        {
            var cliente = this._DbContext.Clientes.FirstOrDefault(c => c.IdClientes == id);
            if (cliente != null)
            {
                return Ok(cliente);
            }
            return NotFound();
        }

        [HttpDelete("Remove/{id}")]
        public IActionResult Remove(int id)
        {
            var cliente = this._DbContext.Clientes.FirstOrDefault(c => c.IdClientes == id);
            if (cliente != null)
            {
                this._DbContext.Clientes.Remove(cliente);
                this._DbContext.SaveChanges();
                return Ok(true);
            }
            return NotFound();
        }

      [HttpPost("Create")]
public IActionResult Create([FromBody] ClientesupdateModal clientesupdateModal)
{
    try
    {

        var nuevoCliente = new Cliente
        {
            IdClientesNavigation = new Persona
            {
                Cedula = clientesupdateModal.Cedula,
                Nombre = clientesupdateModal.Nombre,
                Apellido = clientesupdateModal.Apellido,
                Telefono = clientesupdateModal.Telefono
            }
        };

        this._DbContext.Clientes.Add(nuevoCliente);
        this._DbContext.SaveChanges();

        return Ok(new { success = true, message = "La entidad Cliente ha sido creada con éxito." });
    }
    catch (Exception ex)
    {
        // Manejar cualquier excepción, loguearla o devolver una respuesta de error
        return StatusCode(500, new { success = false, message = $"Error al crear el cliente: {ex.Message}" });
    }
}

        [HttpPut("Update/{idCliente}")]
public IActionResult Update(int idCliente, [FromBody] ClientesupdateModal _cliente)
{
    var cliente = this._DbContext.Clientes
        .Include(c => c.IdClientesNavigation)  // Asegúrate de cargar la propiedad de navegación
        .FirstOrDefault(o => o.IdClientes == idCliente);

    if (cliente == null)
    {
        return NotFound("La entidad Cliente no existe y no puede ser actualizada.");
    }



    if (cliente.IdClientesNavigation != null)
    {
        cliente.IdClientesNavigation.Cedula = _cliente.Cedula;
        cliente.IdClientesNavigation.Nombre = _cliente.Nombre;
        cliente.IdClientesNavigation.Apellido = _cliente.Apellido;
        cliente.IdClientesNavigation.Telefono = _cliente.Telefono;
    }

    this._DbContext.SaveChanges();
    return Ok(new { success = true, message = "La entidad Cliente ha sido actualizada con éxito." });
}
}
}
