using System;
using System.Collections.Generic;

namespace Dominio;

public partial class Vusuario1
{
    public int IdPersona { get; set; }

    public string Cedula { get; set; } = null!;

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? Telefono { get; set; }

    public int IdUsuarios { get; set; }

    public string Cargo { get; set; } = null!;

    public string Usuario { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public string? Email { get; set; }
}
