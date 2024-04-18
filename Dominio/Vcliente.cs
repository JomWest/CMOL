using System;
using System.Collections.Generic;

namespace Dominio;

public partial class Vcliente
{
    public int IdClientes { get; set; }

    public int IdPersona { get; set; }

    public string? Nombre { get; set; }

    public string? Apellido { get; set; }

    public string Cedula { get; set; } = null!;

    public string? Telefono { get; set; }

    public int IdFactura { get; set; }

    public DateTime? Fecha { get; set; }

    public string? Estado { get; set; }

    public int IdDetalleFactura { get; set; }

    public int? Cantidad { get; set; }

    public decimal? Total { get; set; }

    public int? IdProducto { get; set; }

    public int Expr1 { get; set; }

    public string NombreProducto { get; set; } = null!;
}
