using System;
using System.Collections.Generic;

namespace Dominio;

public partial class DetalleFacura
{
    public int IdDetalleFactura { get; set; }

    public int? Cantidad { get; set; }

    public decimal? Subtotal { get; set; }

    public decimal? Total { get; set; }

    public int? IdFactura { get; set; }

    public int? IdProducto { get; set; }

    public int? IdtipoCambio { get; set; }

    public virtual Factura? IdFacturaNavigation { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }

    public virtual TipoCambio? IdtipoCambioNavigation { get; set; }
}
