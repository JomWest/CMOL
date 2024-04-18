using System;
using System.Collections.Generic;

namespace Dominio;

public partial class Usuario
{
    public int IdUsuarios { get; set; }

    public string Cargo { get; set; } = null!;

    public string Usuario1 { get; set; } = null!;

    public string Contraseña { get; set; } = null!;

    public string Estado { get; set; } = null!;

    public string? Email { get; set; }

    public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();

    public virtual ICollection<Devolucion> Devolucions { get; set; } = new List<Devolucion>();

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

    public virtual Persona IdUsuariosNavigation { get; set; } = null!;
}
