namespace Dominio
{
    public class usuarioUpdateModel
    {
            public int IdUsuarios { get; set; }
        public string Cedula { get; set; } = null!;

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? Telefono { get; set; }
    

    public string Cargo { get; set; } = null!;

    public string Usuario1 { get; set; } = null!;

    public string Contraseña { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public string? Email { get; set; }

    }
}