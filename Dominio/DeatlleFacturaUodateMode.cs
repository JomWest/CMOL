namespace Dominio
{
    public class DeatlleFacturaUodateMode
    {
        public int IdDetalleFactura { get; set; }

    public int? Cantidad { get; set; }

    public decimal? Subtotal { get; set; }

    public decimal? Total { get; set; }

    public int? IdFactura { get; set; }

    public int? IdProducto { get; set; }

    public int? IdtipoCambio { get; set; }
    }
}