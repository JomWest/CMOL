using System;
using System.Collections.Generic;

namespace Dominio;

public partial class VistaFactura
{
    public int Idfactura { get; set; }

    public int? IdUsuario { get; set; }

    public int IdClientee { get; set; }

    public string? NombreCliente { get; set; }

    public string CedulaC { get; set; } = null!;

    public string? Chambeador { get; set; }

    public int IdDetalleFactura { get; set; }

    public decimal? PrecioCambio { get; set; }

    public decimal? Precio { get; set; }

    public int? CantidadProductos { get; set; }

    public string NombreProducto { get; set; } = null!;

    public int? IdProducto { get; set; }

    public DateTime? Fecha { get; set; }

    public decimal? TotalFactura { get; set; }
}
