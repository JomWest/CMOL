using System;
using System.Collections.Generic;

namespace Dominio;

public partial class VClientesN
{
    public int IdClientes { get; set; }

    public int IdPersona { get; set; }

    public string Cedula { get; set; } = null!;

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? Telefono { get; set; }
}
