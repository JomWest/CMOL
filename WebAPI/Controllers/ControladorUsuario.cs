using Microsoft.AspNetCore.Mvc;
using Persistencia;
using Dominio;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Controllers
{
    [Route("Usuario")]
    public class ControladorUsuario : ControllerBase
    {
        private readonly CellMasterDbContext _DbContext;

        public ControladorUsuario(CellMasterDbContext dbContext)
        {
            this._DbContext = dbContext;
        }

   [HttpGet("GetAll")]
    public IActionResult GetAll()
    {
        var usuarios = this._DbContext.Usuarios.Include(c => c.IdUsuariosNavigation).ToList();

    var result = usuarios.Select(c => new
    {
        idUsuario = c.IdUsuarios,
        email = c.Email,
        cargo = c.Cargo,
        usuarios = c.Usuario1,
        estado = c.Estado,
        contraseña = c.Contraseña,
        idUsuarioNavigation = new
        {
            idPersona = c.IdUsuariosNavigation.IdPersona,
            cedula = c.IdUsuariosNavigation.Cedula,
            nombre = c.IdUsuariosNavigation.Nombre,
            apellido = c.IdUsuariosNavigation.Apellido,
            telefono = c.IdUsuariosNavigation.Telefono
        },
    }).ToList();
        return Ok(result);
    }

        [HttpDelete("Remove/{id}")]
        public IActionResult Remove(int id)
        {
            var usuario = this._DbContext.Usuarios.FirstOrDefault(u => u.IdUsuarios == id);
            if (usuario != null)
            {
                this._DbContext.Usuarios.Remove(usuario);
                this._DbContext.SaveChanges();
                return Ok(true);
            }
            return NotFound();
        }

[HttpPost("Create")]
public IActionResult Create([FromBody] usuarioUpdateModel UsuarioUpdateModel)
{
    // Puedes realizar las validaciones necesarias aquí

    var usuario = new Usuario
    {
        Cargo = UsuarioUpdateModel.Cargo,
        Usuario1 = UsuarioUpdateModel.Usuario1,
        Contraseña = UsuarioUpdateModel.Contraseña,
        Estado = UsuarioUpdateModel.Estado,
        Email = UsuarioUpdateModel.Email,
        IdUsuariosNavigation = new Persona
        {
            Cedula = UsuarioUpdateModel.Cedula,
            Nombre = UsuarioUpdateModel.Nombre,
            Apellido = UsuarioUpdateModel.Apellido,
            Telefono = UsuarioUpdateModel.Telefono

        }
    };

    this._DbContext.Usuarios.Add(usuario);
    this._DbContext.SaveChanges();

return Ok(new { success = true, message = "La entidad Cliente ha sido creada con éxito." });
}

[HttpPut("Update/{idUsuario}")]
public IActionResult Update(int idUsuario, [FromBody] usuarioUpdateModel _usuario)
{
    var usuario = this._DbContext.Usuarios
        .Include(c => c.IdUsuariosNavigation)  // Asegúrate de cargar la propiedad de navegación
        .FirstOrDefault(o => o.IdUsuarios == idUsuario);

    if (usuario == null)
    {
        return NotFound("La entidad Usuario no existe y no puede ser actualizada.");
    }

    // Actualiza los campos necesarios
    usuario.Cargo = _usuario.Cargo;
    usuario.Usuario1 = _usuario.Usuario1;
    usuario.Contraseña = _usuario.Contraseña;
    usuario.Estado = _usuario.Estado;
    usuario.Email = _usuario.Email;

    if (usuario.IdUsuariosNavigation != null)
    {
        usuario.IdUsuariosNavigation.Cedula = _usuario.Cedula;
        usuario.IdUsuariosNavigation.Nombre = _usuario.Nombre;
        usuario.IdUsuariosNavigation.Apellido = _usuario.Apellido;
        usuario.IdUsuariosNavigation.Telefono = _usuario.Telefono;
    }

    this._DbContext.SaveChanges();
    return Ok(new { success = true, message = "La entidad Usuario ha sido actualizada con éxito." });
}
[HttpPost("login")]
public IActionResult Login([FromBody] LoginModel model)
{
    var usuario = _DbContext.Usuarios.FirstOrDefault(u => u.Usuario1 == model.Usuario1);

    if (usuario == null || usuario.Contraseña != model.Contraseña)
    {
        return Unauthorized(new { message = "Nombre de usuario o contraseña incorrectos" });
    }



    return Ok(new { message = "Autenticación exitosa" });
}
    
    [HttpPost("CambiarContraseña")]
public IActionResult CambiarContraseña([FromBody] CambiarContraModal model)
{
    try
    {
        var usuario = _DbContext.Usuarios.FirstOrDefault(u =>
            u.Usuario1 == model.Usuario1 &&
            u.Contraseña == model.Contraseña);

        if (usuario == null)
        {
            return BadRequest("Los datos del usuario no coinciden. Verifique la cédula, nombre de usuario y contraseña actual.");
        }

        usuario.Contraseña = model.NuevaContraseña;
        _DbContext.SaveChanges();

        return Ok("Contraseña cambiada exitosamente.");
    }
    catch (Exception ex)
    {
        return StatusCode(500, $"Error interno del servidor: {ex.Message}");
    }
}
}
}
