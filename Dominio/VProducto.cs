using System;
using System.Collections.Generic;

namespace Dominio;

public partial class VProducto
{
    public int Expr1 { get; set; }

    public string NombreProducto { get; set; } = null!;

    public decimal? PrecioVenta { get; set; }

    public int Stock { get; set; }

    public int? StockMinimo { get; set; }

    public int IdCategorias { get; set; }

    public int IdMarca { get; set; }
}
