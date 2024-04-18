using System;
using System.Collections.Generic;

namespace Dominio;

public partial class Categoria
{
    public int IdCategorias { get; set; }

    public string? Nombre { get; set; }

    public string? Descripción { get; set; }

    public virtual ICollection<Producto> Productos { get; set; } = new List<Producto>();
}
