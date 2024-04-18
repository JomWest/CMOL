using System;
using System.Collections.Generic;

namespace Dominio;

public partial class VistaInventario
{
    public int IdProducto { get; set; }

    public int IdProveedores { get; set; }

    public string NombreProducto { get; set; } = null!;

    public int Stock { get; set; }

    public decimal? PrecioVenta { get; set; }

    public int IdCategorias { get; set; }

    public int IdMarca { get; set; }

    public string? Nombre { get; set; }

    public string? NombreMarca { get; set; }

    public string NombreEmpresa { get; set; } = null!;
}
