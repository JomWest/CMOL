using System;
using System.Collections.Generic;

namespace Dominio;

public partial class Compra
{
    public int IdCompras { get; set; }

    public DateTime FechaCompra { get; set; }

    public int IdUsuarios { get; set; }

    public int IdProveedores { get; set; }

    public virtual ICollection<DetalleCompra> DetalleCompras { get; set; } = new List<DetalleCompra>();

    public virtual Proveedore IdProveedoresNavigation { get; set; } = null!;

    public virtual Usuario IdUsuariosNavigation { get; set; } = null!;
}
