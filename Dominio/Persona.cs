using System;
using System.Collections.Generic;

namespace Dominio;

public partial class Persona
{
    public int IdPersona { get; set; }

    public string Cedula { get; set; } = null!;

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string? Telefono { get; set; }

    public virtual Cliente? Cliente { get; set; }

    public virtual Proveedore? Proveedore { get; set; }

    public virtual Usuario? Usuario { get; set; }
}
