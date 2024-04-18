using System;
using System.Collections.Generic;

namespace Dominio;

public partial class VFactura
{
    public int Idfactura { get; set; }

    public DateTime? Fecha { get; set; }

    public decimal? TotalFacturas { get; set; }

    public string? EstadoFactura { get; set; }

    public int? IdUsuario { get; set; }

    public int IdClientee { get; set; }

    public int IdPersonaCliente { get; set; }

    public string CedulaCliente { get; set; } = null!;

    public string? NombreCliente { get; set; }

    public string? ApellidoCliente { get; set; }

    public int IdCliente { get; set; }

    public string? NombreTrabajador { get; set; }

    public string? ApellidoTrabajador { get; set; }

    public string CargoUsuario { get; set; } = null!;

    public int IdDetalleFactura { get; set; }

    public int? IdTipoCambio { get; set; }

    public decimal? PrecioCambio { get; set; }

    public int IdClientes { get; set; }

    public int? CantidadProductos { get; set; }

    public decimal? SubTotalFactura { get; set; }

    public decimal? TotalFactura { get; set; }

    public int? NumeroFactura { get; set; }

    public int? IdProducto { get; set; }

    public int IdProductoRef { get; set; }

    public int CodigoProducto { get; set; }

    public string NombreProducto { get; set; } = null!;

    public decimal? PrecioVenta { get; set; }

    public int Stock { get; set; }

    public int IdCambio { get; set; }
}
