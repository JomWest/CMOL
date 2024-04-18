using System;
using System.Collections.Generic;

namespace Dominio;

public partial class VistaCompra
{
    public int Idcompra { get; set; }

    public DateTime Fecha { get; set; }

    public int IdUsuario { get; set; }

    public string? NombreU { get; set; }

    public string Proveedor { get; set; } = null!;

    public string Producto { get; set; } = null!;

    public int? Cantidad { get; set; }

    public decimal? Precio { get; set; }

    public decimal? Total { get; set; }
}
