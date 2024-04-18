using System;
using System.Collections.Generic;
namespace Dominio
{
    public class FacturaUpdateModel
    {
        public int IdFactura { get; set; }

    public DateTime? Fecha { get; set; }

    public decimal? Total { get; set; }

    public string? Estado { get; set; }

    public int? IdUsuarios { get; set; }

    public int IdClientes { get; set; }
    }
}