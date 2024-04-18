using System;
using System.Collections.Generic;

namespace Dominio;

public partial class Devolucion
{
    public int Iddevoluciones { get; set; }

    public int? IdFactura { get; set; }

    public DateTime? Fechadevolucion { get; set; }

    public decimal? Totaldevolucion { get; set; }

    public int? IdUsuarios { get; set; }

    public int? IdClientes { get; set; }

    public string? Motivosdevolucion { get; set; }

    public string? Acciontomadas { get; set; }

    public virtual ICollection<DetalleDevolucion> DetalleDevolucions { get; set; } = new List<DetalleDevolucion>();

    public virtual Cliente? IdClientesNavigation { get; set; }

    public virtual Factura? IdFacturaNavigation { get; set; }

    public virtual Usuario? IdUsuariosNavigation { get; set; }
}
