namespace RestaurantApp.Models
{
    public class Producto
    {
        public int ProductoID { get; set; }
        public string CodigoProducto { get; set; }
        public string NombreProducto { get; set; }
        public int CategoriaID { get; set; }
        public int UnidadMedidaID { get; set; }
        public decimal PrecioCosto { get; set; }
        public decimal PrecioVenta { get; set; }
        public int MonedaID { get; set; }
        public decimal StockActual { get; set; }
        public decimal StockMinimo { get; set; }
        public bool Activo { get; set; }
        public DateTime FechaCreacion { get; set; }

        // Propiedades de navegación
        public CategoriaProducto Categoria { get; set; }
        public UnidadMedida UnidadMedida { get; set; }
        public Moneda Moneda { get; set; }
    }
}
