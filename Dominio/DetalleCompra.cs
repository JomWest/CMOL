using System;
using System.Collections.Generic;

namespace Dominio;

public partial class DetalleCompra
{
    public int IdDetalleCompra { get; set; }

    public decimal? PrecioCompra { get; set; }

    public int? CantidadProducto { get; set; }

    public decimal? TotalCompra { get; set; }

    public int? IdCompras { get; set; }

    public int? IdProducto { get; set; }

    public virtual Compra? IdComprasNavigation { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }
}
