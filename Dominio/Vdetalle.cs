using System;
using System.Collections.Generic;

namespace Dominio;

public partial class Vdetalle
{
    public int IdFactura { get; set; }

    public int IdDetalleFactura { get; set; }

    public int? Cantidad { get; set; }

    public int? IdProducto { get; set; }

    public string NombreProducto { get; set; } = null!;

    public int CodigoProd { get; set; }

    public decimal? Subtotal { get; set; }

    public decimal? Total { get; set; }

    public int IdtipoCambio { get; set; }

    public decimal? PrecioCambio { get; set; }
}
