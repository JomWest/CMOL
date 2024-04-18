using System;
using System.Collections.Generic;

namespace Dominio;

public partial class Factura
{
    public int IdFactura { get; set; }

    public DateTime? Fecha { get; set; }

    public decimal? Total { get; set; }

    public string? Estado { get; set; }

    public int? IdUsuarios { get; set; }

    public int IdClientes { get; set; }

    public virtual ICollection<DetalleFacura> DetalleFacuras { get; set; } = new List<DetalleFacura>();

    public virtual ICollection<Devolucion> Devolucions { get; set; } = new List<Devolucion>();

    public virtual Cliente IdClientesNavigation { get; set; } = null!;

    public virtual Usuario? IdUsuariosNavigation { get; set; }
}
