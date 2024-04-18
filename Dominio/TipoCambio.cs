using System;
using System.Collections.Generic;

namespace Dominio;

public partial class TipoCambio
{
    public int IdtipoCambio { get; set; }

    public decimal? PrecioCambio { get; set; }

    public DateTime? FechaC { get; set; }

    public virtual ICollection<DetalleFacura> DetalleFacuras { get; set; } = new List<DetalleFacura>();
}
