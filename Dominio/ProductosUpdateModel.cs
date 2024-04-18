namespace Dominio
{
    public class ProductosUpdateModel
    {
        public int IdProducto { get; set; }

    public int CodigoProd { get; set; }

    public string NombreProducto { get; set; } = null!;

    public decimal? PrecioVenta { get; set; }

    public int Stock { get; set; }

    public int? StockMinimo { get; set; }

    public int IdCategorias { get; set; }

    public int IdMarca { get; set; }


    }
}