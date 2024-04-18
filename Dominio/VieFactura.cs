using System;
using System.Collections.Generic;

namespace Dominio;

public partial class VieFactura
{
    public int IdFactura { get; set; }

    public DateTime? Fecha { get; set; }

    public decimal? Total { get; set; }

    public int IdClientes { get; set; }

    public string? Nombre { get; set; }

    public string NombreProducto { get; set; } = null!;

    public decimal? PrecioVenta { get; set; }

    public int? Cantidad { get; set; }

    public string Usuario { get; set; } = null!;
}
