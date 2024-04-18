using System;
using System.Collections.Generic;

namespace Dominio;

public partial class DetalleDevolucion
{
    public int Iddetalledevolucion { get; set; }

    public int? Iddevoluciones { get; set; }

    public int? IdProducto { get; set; }

    public int? Cant { get; set; }

    public decimal? Precio { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }

    public virtual Devolucion? IddevolucionesNavigation { get; set; }
}
