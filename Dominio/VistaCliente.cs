using System;
using System.Collections.Generic;

namespace Dominio;

public partial class VistaCliente
{
    public int IdPersona { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string Cedula { get; set; } = null!;

    public string? Telefono { get; set; }

    public int IdClientes { get; set; }
    
}
