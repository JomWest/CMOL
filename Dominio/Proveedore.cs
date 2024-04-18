using System;
using System.Collections.Generic;

namespace Dominio;

public partial class Proveedore
{
    public int IdProveedores { get; set; }

    public string NombreEmpresa { get; set; } = null!;

    public string Departamento { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();

    public virtual Persona IdProveedoresNavigation { get; set; } = null!;
}
