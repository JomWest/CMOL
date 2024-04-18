using System;
using System.Collections.Generic;

namespace Dominio;

public partial class Vusuario
{
    public int IdPersona { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string Cedula { get; set; } = null!;

    public string Cargo { get; set; } = null!;

    public int IdFactura { get; set; }

    public DateTime? Fecha { get; set; }

    public decimal? Total { get; set; }

    public int IdDetalleFactura { get; set; }

    public int? Cantidad { get; set; }

    public decimal? Subtotal { get; set; }

    public decimal? Expr1 { get; set; }

    public int? IdProducto { get; set; }

    public int Expr2 { get; set; }

    public int CodigoProd { get; set; }

    public string NombreProducto { get; set; } = null!;
}
