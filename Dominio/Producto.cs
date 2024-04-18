using System;
using System.Collections.Generic;

namespace Dominio;

public partial class Producto
{
    public int IdProducto { get; set; }

    public int CodigoProd { get; set; }

    public string NombreProducto { get; set; } = null!;

    public decimal? PrecioVenta { get; set; }

    public int Stock { get; set; }

    public int? StockMinimo { get; set; }

    public int IdCategorias { get; set; }

    public int IdMarca { get; set; }

    public virtual ICollection<DetalleCompra> DetalleCompras { get; set; } = new List<DetalleCompra>();

    public virtual ICollection<DetalleDevolucion> DetalleDevolucions { get; set; } = new List<DetalleDevolucion>();

    public virtual ICollection<DetalleFacura> DetalleFacuras { get; set; } = new List<DetalleFacura>();

    public virtual Categoria IdCategoriasNavigation { get; set; } = null!;

    public virtual Marca IdMarcaNavigation { get; set; } = null!;
}
