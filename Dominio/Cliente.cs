using System;
using System.Collections.Generic;

namespace Dominio;

public partial class Cliente
{
    public int IdClientes { get; set; }

    public virtual ICollection<Devolucion> Devolucions { get; set; } = new List<Devolucion>();

    public virtual ICollection<Factura> Facturas { get; set; } = new List<Factura>();

    public virtual Persona IdClientesNavigation { get; set; } = null!;
}
